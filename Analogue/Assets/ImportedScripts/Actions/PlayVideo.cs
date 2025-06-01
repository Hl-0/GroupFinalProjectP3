using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// Play a single video or play from a list of videos 
/// </summary>
[RequireComponent(typeof(VideoPlayer))]
public class PlayVideo : MonoBehaviour
{
    [Tooltip("Whether video should play on load")]
    public bool playAtStart = false;

    [Tooltip("Material used for playing the video (Uses URP/Unlit by default)")]
    public RenderTexture videoMaterial = null;
    public RenderTexture initialTexture = null;

    [Tooltip("List of video clips to pull from")]
    public List<VideoClip> videoClips = new List<VideoClip>();

    private VideoPlayer videoPlayer = null;
    private RawImage texture = null;

    private readonly string shaderUsed = "Universal Render Pipeline/Unlit";

    private Material offMaterial = null;
    private int index = 0;

    private void Awake()
    {
        texture = GetComponent<RawImage>();
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoClips.Count > 0)
            videoPlayer.clip = videoClips[0];

        texture.texture = initialTexture;
    }


    private void Start()
    {
        if (playAtStart)
        {
            Play();
        }
        else
        {
            Stop();
        }
    }

    public void NextClip()
    {
        index = ++index % videoClips.Count;
        Play();
    }

    public void PreviousClip()
    {
        index = --index % videoClips.Count;
        Play();
    }

    public void RandomClip()
    {
        if (videoClips.Count > 0)
        {
            index = Random.Range(0, videoClips.Count);
            Play();
        }
    }

    public void PlayAtIndex(int value)
    {
        if (videoClips.Count > 0)
        {
            index = Mathf.Clamp(value, 0, videoClips.Count);
            Play();
        }
    }

    public void Play()
    {
        ApplyVideoMaterial();
        videoPlayer.Play();
    }

    public void Stop()
    {
        texture.texture = initialTexture;
        videoPlayer.Stop();
    }

    public void TogglePlayStop()
    {
        bool isPlaying = !videoPlayer.isPlaying;
        SetPlay(isPlaying);
    }

    public void TogglePlayPause()
    {
        texture.texture = videoMaterial;

        if (videoPlayer.isPlaying)
            videoPlayer.Pause();
        else
            videoPlayer.Play();
    }

    public void SetPlay(bool value)
    {
        if (value)
        {
            Play();
        }
        else
        {
            Stop();
        }
    }

    private void ApplyVideoMaterial()
    {
        texture.texture = videoMaterial;
    }

    private void OnValidate()
    {
            
        if (TryGetComponent(out VideoPlayer videoPlayer))
            videoPlayer.targetMaterialProperty = "_BaseMap";
    }
}