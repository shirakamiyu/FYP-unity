using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    private Image _image;
    public Image _CNtitle; // chinese title
    public Image _ENtitle; // english title

    public Button startButton; // start button
    public Button exitButton;  // exit button

    public GameObject bgImage; // background image
    public GameObject versionPanel;

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
        _image = GetComponentInChildren<Image>();

        _CNtitle.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0); // 初始化为透明
        _ENtitle.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0); // 初始化为透明

        StartCoroutine(LoadTitle());
    }

    private IEnumerator LoadTitle()
    {
        // 
        yield return _CNtitle.DOFade(1, 1).WaitForCompletion(); // 渐入效果

        // 
        yield return _ENtitle.DOFade(1, 1).WaitForCompletion(); // 渐入效果

        versionPanel.SetActive(true);

        startButton.onClick.AddListener(OnStartButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnStartButtonClick()
    {
        StartCoroutine(Load());
    }

    private void OnExitButtonClick()
    {
        // exit the app
        Application.Quit();
    }

    private IEnumerator Load()
    {
        _image.raycastTarget = true;
        // yield return _image.DOFade(1, 1).WaitForCompletion();

        // 禁用背景图像和按钮
        bgImage.SetActive(false);
        startButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        yield return SceneManager.LoadSceneAsync(2); // loading scene 2

        yield return _image.DOFade(0, 2).WaitForCompletion();
        _image.raycastTarget = false;
    }
}