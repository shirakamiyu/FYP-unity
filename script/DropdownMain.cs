using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownMain : MonoBehaviour
{
    public TextMeshProUGUI ShowPlayText;
    public TMP_Dropdown dropdown; // 引用 Dropdown 組件
    public AudioSource audioSource; // 引用 AudioSource 組件
    public AudioClip[] audioClips; // 存儲對應選項的音頻片段
    // [SerializeField]private TMP_Text ShowText;

    void Start()
    {
        // 監聽 Dropdown 的值變化事件
        dropdown.onValueChanged.AddListener(DropdownSoundText);
    }

    public void DropdownSoundText(int index)
    {
        // stop if there is any audio playing now
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // play new audio
        audioSource.clip = audioClips[index];
        audioSource.Play();

        switch (index)
        {
            case 0:
                ShowPlayText.text = "";
                break;
            case 1:
            case 2:
            case 3:
                ShowPlayText.text = "正在播放： " + dropdown.options[index].text;
                break;
            case 4:
                ShowPlayText.text = "";
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                ShowPlayText.text = "Now Playing: " + dropdown.options[index].text;
                break;
        }
    }

}