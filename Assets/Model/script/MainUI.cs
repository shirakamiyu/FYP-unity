using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI ShowPlayText; // text 
    public Button exitButton; // exit button

    private void Awake()
    {
        // check if EventSystem assigned or not
        if (Object.FindFirstObjectByType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Exit()
    {
        SceneManager.LoadSceneAsync(1);
        exitButton.gameObject.SetActive(false);
        ShowPlayText.text = "";
    }
}
