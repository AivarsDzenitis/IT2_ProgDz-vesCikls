using UnityEngine;
using UnityEngine.Video;

public class VideoTabManager : MonoBehaviour
{
    public VideoPlayer player;
    public VideoClip[] videos;

    private int currentVideo = 0;

    public void OpenVideo(int index)
    {
        // 1. Guard against unassigned components or empty arrays
        if (player == null)
        {
            Debug.LogError("VideoPlayer reference is missing!", this);
            return;
        }

        if (videos == null || videos.Length == 0)
        {
            Debug.LogWarning("No video clips assigned to the videos array!", this);
            return;
        }

        // 2. Validate index range
        if (index < 0 || index >= videos.Length)
            return;

        currentVideo = index;

        // 3. Switch clip and play safely
        player.Stop();
        player.clip = videos[index];
        player.Play();
    }

    public void NextVideo()
    {
        if (videos == null || videos.Length == 0) return;

        currentVideo++;
        if (currentVideo >= videos.Length)
            currentVideo = 0;

        OpenVideo(currentVideo);
    }

    public void PreviousVideo()
    {
        if (videos == null || videos.Length == 0) return;

        currentVideo--;
        if (currentVideo < 0)
            currentVideo = videos.Length - 1;

        OpenVideo(currentVideo);
    }
}