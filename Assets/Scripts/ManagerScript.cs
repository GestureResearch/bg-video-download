using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public VideoPlayer player;

    public RawImage rawImage;
    public AudioSource audioSource;

    DownloadOperation dload;
    string URL = "https://gestureresearch.github.io/hero-test/";

    private bool flag;

    void Start()
    {
        Application.runInBackground = true;
    }
    
    void Update()
    {
        if (flag)
        {
            StartCoroutine(StartCheck());
        }
    }

    public void DownloadVideo()
    {
        flag = true;
    }

    public void PlayVideoButton()
    {
        player.url = "file://" + Application.persistentDataPath + "/1.mp4";
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        player.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!player.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = player.texture;
        player.Play();
        audioSource.Play();
    }

    IEnumerator StartCheck()
    {
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Application.persistentDataPath.ToString()+ "/" + i.ToString() + ".mp4";
            if (!System.IO.File.Exists(filePath))
            {
                dload = BackgroundDownloads.StartOrContinueDownload(@URL+i.ToString()+".mp4");
            }
        }
        yield return new WaitForSeconds(30);
    }
}
