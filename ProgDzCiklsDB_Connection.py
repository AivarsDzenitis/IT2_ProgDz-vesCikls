import tkinter as tk
from tkinter import ttk, messagebox
import mysql.connector


class DatabaseApp:

    def __init__(self, root):
        self.root = root
        self.root.title("Python Database Manager")
        self.root.geometry("900x600")

        self.connection = None

        connection_frame = ttk.LabelFrame(
            root, text="Database Connection", padding=10
        )
        connection_frame.pack(fill="x", padx=10, pady=10)

        self.host = self.create_field(connection_frame, "Host:", 0, "localhost")
        self.user = self.create_field(connection_frame, "Username:", 1, "root")
        self.password = self.create_field(
            connection_frame, "Password:", 2, ""
        )
        self.database = self.create_field(
            connection_frame, "Database:", 3, ""
        )

        ttk.Button(
            connection_frame,
            text="Connect",
            command=self.connect_database
        ).grid(row=4, column=0, columnspan=2, pady=10)

        self.status = ttk.Label(
            connection_frame,
            text="Status: Not connected",
            foreground="red"
        )
        self.status.grid(row=4, column=2, columnspan=2)

        table_frame = ttk.LabelFrame(
            root, text="Database Tables", padding=10
        )
        table_frame.pack(fill="x", padx=10, pady=5)

        self.table_select = ttk.Combobox(
            table_frame, state="readonly", width=40
        )
        self.table_select.pack(side="left", padx=5)

        ttk.Button(
            table_frame,
            text="Load Table",
            command=self.load_table
        ).pack(side="left", padx=5)

        # Data display
        data_frame = ttk.LabelFrame(
            root, text="Table Data", padding=10
        )
        data_frame.pack(fill="both", expand=True, padx=10, pady=10)

        self.tree = ttk.Treeview(data_frame)
        self.tree.pack(side="left", fill="both", expand=True)

        scrollbar_y = ttk.Scrollbar(
            data_frame, orient="vertical",
            command=self.tree.yview
        )
        scrollbar_y.pack(side="right", fill="y")

        scrollbar_x = ttk.Scrollbar(
            root, orient="horizontal",
            command=self.tree.xview
        )
        scrollbar_x.pack(fill="x", padx=10)

        self.tree.configure(
            yscrollcommand=scrollbar_y.set,
            xscrollcommand=scrollbar_x.set
        )

    def create_field(self, parent, label, row, default):
        ttk.Label(parent, text=label).grid(
            row=row, column=0, sticky="w", pady=3
        )

        entry = ttk.Entry(parent, width=30)
        entry.grid(row=row, column=1, padx=5, pady=3)
        entry.insert(0, default)

        if label == "Password:":
            entry.config(show="*")

        return entry

    def connect_database(self):
        try:
            self.connection = mysql.connector.connect(
                host=self.host.get(),
                user=self.user.get(),
                password=self.password.get(),
                database=self.database.get()
            )

            self.status.config(
                text="Status: Connected!",
                foreground="green"
            )

            self.load_tables()

        except mysql.connector.Error as error:
            self.connection = None

            self.status.config(
                text="Status: Connection failed",
                foreground="red"
            )

            messagebox.showerror(
                "Database Error", str(error)
            )

    def load_tables(self):
        if not self.connection or not self.connection.is_connected():
            return

        cursor = self.connection.cursor()

        cursor.execute("SHOW TABLES")

        tables = [row[0] for row in cursor.fetchall()]

        self.table_select["values"] = tables

        if tables:
            self.table_select.current(0)
        else:
            messagebox.showinfo(
                "Database",
                "No tables found in this database."
            )

        cursor.close()

    def load_table(self):
        table = self.table_select.get()

        if not table:
            messagebox.showwarning(
                "Warning", "Select a table first!"
            )
            return

        try:
            cursor = self.connection.cursor()

            # Safely quote the table name
            escaped_table = table.replace("`", "``")

            cursor.execute(
                f"SELECT * FROM `{escaped_table}` LIMIT 500"
            )

            rows = cursor.fetchall()
            columns = [column[0] for column in cursor.description]

            # Clear previous data
            self.tree.delete(*self.tree.get_children())

            self.tree["columns"] = columns
            self.tree["show"] = "headings"

            # Create column headings
            for column in columns:
                self.tree.heading(column, text=column)
                self.tree.column(
                    column, width=130, minwidth=80
                )

            # Display records
            for row in rows:
                self.tree.insert("", "end", values=row)

            cursor.close()

        except mysql.connector.Error as error:
            messagebox.showerror(
                "Error", str(error)
            )

if __name__ == "__main__":
    root = tk.Tk()
    app = DatabaseApp(root)
    root.mainloop()
