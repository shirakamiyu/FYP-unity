using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    public GameObject StartText_Eng;
    public GameObject StartText_Chin;
    public GameObject EndText_Eng;
    public GameObject EndText_Chin;

    public GameObject helpPanel_Eng;
    public GameObject helpPanel_Chin;
    public GameObject versionPanel;

    public Button helpButton;
    public Button closeButton;

    void Start()
    {
        // ��ʼ״̬�����ص���
        helpPanel_Eng.SetActive(false);
        helpPanel_Chin.SetActive(false);

        StartText_Chin.SetActive(false);
        EndText_Chin.SetActive(false);

        helpButton.onClick.AddListener(Show);
        closeButton.onClick.AddListener(Hide);
    }

    public void ToggleLanguage()
    {
        if (StartText_Chin.activeSelf)
        {
            // ��� StartText_Chin �Ǽ���״̬�������ز���ʾӢ���ı�
            StartText_Chin.SetActive(false);
            EndText_Chin.SetActive(false);
            StartText_Eng.SetActive(true);
            EndText_Eng.SetActive(true);
        }
        else
        {
            // ������ʾ�����ı�������Ӣ���ı�
            StartText_Chin.SetActive(true);
            EndText_Chin.SetActive(true);
            StartText_Eng.SetActive(false);
            EndText_Eng.SetActive(false);
        }
    }

    public void Show()
    {
        // helpPanel.SetActive(true);
        helpButton.gameObject.SetActive(false);
        if (StartText_Chin.activeSelf)
        {
            helpPanel_Eng.SetActive(false);
            helpPanel_Chin.SetActive(true);
        }
        else
        {
            helpPanel_Chin.SetActive(false);
            helpPanel_Eng.SetActive(true);
        }
    }

    public void Hide()
    {
        if (helpPanel_Chin.activeSelf)
        {
            helpPanel_Chin.SetActive(false);
        }
        else if (helpPanel_Eng.activeSelf)
        {
            helpPanel_Eng.SetActive(false);
        }
        else
        {
            versionPanel.SetActive(false);
        }
            helpButton.gameObject.SetActive(true);
    }
}