using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class SceneAudioData
{
    public string sceneName; // ImageTarget name, "scene01"
    public AudioClip chineseAudio; // Chinese narration
    public AudioClip englishAudio; // English narration
}

public class SceneDetectWithAudio : MonoBehaviour
{
    // Audio data configuration list
    [Header("Audio data configuration list")]
    public List<SceneAudioData> audioDatabase = new List<SceneAudioData>();

    // UI 
    [Header("UI")]
    public TextMeshProUGUI statusDisplayText; // Display playback status (playing narration)
    public TextMeshProUGUI showState; // Display AR recognition status (detected, lost, etc.)
    public Button chinesePlayButton;
    public Button englishPlayButton;

    // Internal state
    private AudioSource audioSource;
    private AudioClip currentChineseClip;
    private AudioClip currentEnglishClip;
    private string lastDetectedScene = "";
    private bool isTracking = false;

    void Start()
    {
        // Initialize audio source
        audioSource = gameObject.AddComponent<AudioSource>();

        // Configure button listener
        if (chinesePlayButton != null)
            chinesePlayButton.onClick.AddListener(PlayChineseAudio);

        if (englishPlayButton != null)
            englishPlayButton.onClick.AddListener(PlayEnglishAudio);

        // Disable buttons when start
        SetButtonsInteractable(false);

        // initial state - show on showState
        UpdateARState("Initial state...");

        // stop playing
        UpdatePlaybackStatus("");
    }

    void Update()
    {
        CheckImageTargets();
    }

    void CheckImageTargets()
    {
        // Find ImageTarget in the scene
        ImageTargetBehaviour[] allTargets = FindObjectsByType<ImageTargetBehaviour>(FindObjectsSortMode.None);
        string currentlyTrackedScene = "";
        bool nowTracking = false;

        foreach (ImageTargetBehaviour target in allTargets)
        {
            var observer = target.GetComponent<ObserverBehaviour>();
            if (observer != null)
            {
                var status = observer.TargetStatus;

                // Determine if the device is being tracked.
                if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
                {
                    currentlyTrackedScene = target.gameObject.name;
                    nowTracking = true;
                    break; // Identify only one target at a time
                }
            }
        }

        // Handling tracking state changes
        if (nowTracking)
        {
            if (!isTracking || currentlyTrackedScene != lastDetectedScene)
            {
                OnSceneDetected(currentlyTrackedScene);
            }
            isTracking = true;
            lastDetectedScene = currentlyTrackedScene;
        }
        else if (isTracking) // From tracking to no tracking
        {
            OnTrackingLost();
            isTracking = false;
            lastDetectedScene = "";
        }
    }

    void OnSceneDetected(string sceneName)
    {
        Debug.Log($"Scene recognition: {sceneName}");

        // show AR state
        UpdateARState($"{sceneName}");

        // stop playing��because of scene change��
        UpdatePlaybackStatus("");

        // Find and set the corresponding audio
        SceneAudioData data = audioDatabase.Find(d => d.sceneName == sceneName);

        if (data != null)
        {
            currentChineseClip = data.chineseAudio;
            currentEnglishClip = data.englishAudio;

            // Enable button
            SetButtonsInteractable(true);

            Debug.Log($"Audio has been switched to {sceneName}");
        }
        else
        {
            Debug.LogWarning($"Audio of {sceneName} not found");
            SetButtonsInteractable(false);
            currentChineseClip = null;
            currentEnglishClip = null;
            UpdateARState($"Audio of {sceneName} not found");
        }
    }

    void OnTrackingLost()
    {
        Debug.Log("Target lost");

        // show AR state
        UpdateARState("Target lost...");

        // stop playing
        UpdatePlaybackStatus("");

        SetButtonsInteractable(false);

        // Stop the currently playing audio
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    void PlayChineseAudio()
    {
        if (currentChineseClip != null && audioSource != null)
        {
            PlayAudio(currentChineseClip, "CN");
        }
    }

    void PlayEnglishAudio()
    {
        if (currentEnglishClip != null && audioSource != null)
        {
            PlayAudio(currentEnglishClip, "EN");
        }
    }

    void PlayAudio(AudioClip clip, string language)
    {
        // Stop the current playback
        if (audioSource.isPlaying)
            audioSource.Stop();

        // Play new audio
        audioSource.clip = clip;
        audioSource.Play();

        Debug.Log($"Playing {language} narration: {clip.name}");

        UpdatePlaybackStatus($"Playing {language} narration...");
    }

    // Update AR state��target loss��- for showState
    void UpdateARState(string message)
    {
        if (showState != null)
        {
            showState.text = message;
        }
    }

    // Update playing state - for statusDisplayText
    void UpdatePlaybackStatus(string message)
    {
        if (statusDisplayText != null)
        {
            statusDisplayText.text = message;
        }
    }

    void SetButtonsInteractable(bool interactable)
    {
        if (chinesePlayButton != null)
            chinesePlayButton.interactable = interactable;

        if (englishPlayButton != null)
            englishPlayButton.interactable = interactable;
    }
}
