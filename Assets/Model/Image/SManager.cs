using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SManeger : MonoBehaviour
{
    public Image _image;
    public Image _hkiit;
    public Image _fyp;
    public GameObject _bgImage;

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
        //_image = GetComponentInChildren<Image>();
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0); // 初始化为透明
        _hkiit.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0); // 初始化为透明
        _fyp.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0); // 初始化为透明
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        // 
        yield return _hkiit.DOFade(1, 2).WaitForCompletion(); // 渐入效果
        yield return _hkiit.DOFade(0, 1).WaitForCompletion(); // 渐出效果

        //
        yield return _fyp.DOFade(1, 2).WaitForCompletion(); // 渐入效果
        yield return _fyp.DOFade(0, 1).WaitForCompletion(); // 渐出效果

        //
        yield return _image.DOFade(1, 3).WaitForCompletion(); // 渐入效果
        yield return _image.DOFade(0, 2).WaitForCompletion(); // 渐出效果

        yield return SceneManager.LoadSceneAsync(1); // loading scene 1

        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
        _bgImage.SetActive(false);
    }
}