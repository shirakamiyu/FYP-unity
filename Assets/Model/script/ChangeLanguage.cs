using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    public TextMeshProUGUI StartText_Eng;
    public TextMeshProUGUI StartText_Chin;
    public TextMeshProUGUI EndText_Eng;
    public TextMeshProUGUI EndText_Chin;

    public GameObject helpPanel_Eng;
    public GameObject helpPanel_Chin;
    public GameObject versionPanel;
    //public Button CloseVersion;

    public Button helpButton;
    public Button closeButton;

    public TextMeshProUGUI Helptext01;
    public TextMeshProUGUI Helptext02;
    public TextMeshProUGUI Helptext03;

    public GameObject HelpPlannel;
    public Button CloseImage;
    public Button Nextbutton;
    public Button ForwardButton;

    public static int CountPages = 0;

    void Start()
    {
        // 初始状态下隐藏弹窗
        helpPanel_Eng.SetActive(false);
        helpPanel_Chin.SetActive(false);

        StartText_Chin.gameObject.SetActive(false);
        EndText_Chin.gameObject.SetActive(false);

        /* helpButton.onClick.AddListener(Show);
        closeButton.onClick.AddListener(Hide); */
    }

    public void ToggleLanguage()
    {
        if (StartText_Chin.gameObject.activeSelf)
        {
            // return english title
            StartText_Chin.gameObject.SetActive(false);
            EndText_Chin.gameObject.SetActive(false);
            StartText_Eng.gameObject.SetActive(true);
            EndText_Eng.gameObject.SetActive(true);
        }
        else
        {
            // reture chinese title
            StartText_Chin.gameObject.SetActive(true);
            EndText_Chin.gameObject.SetActive(true);
            StartText_Eng.gameObject.SetActive(false);
            EndText_Eng.gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        // helpPanel.SetActive(true);
        helpButton.gameObject.SetActive(false);
        if (StartText_Chin.gameObject.activeSelf)
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
        // CloseVersion.gameObject.SetActive(false);
    }

    public void HelpPlannel_show()
    {
        switch(CountPages)
        {
            case 0:
                Helptext01.gameObject.SetActive(true);
                Helptext02.gameObject.SetActive(false);
                Helptext03.gameObject.SetActive(false);
                ForwardButton.gameObject.SetActive(false);
                Nextbutton.gameObject.SetActive(true);
                CloseImage.gameObject.SetActive(false);
                break;
            case 1:
                Helptext01.gameObject.SetActive(false);
                Helptext02.gameObject.SetActive(true);
                Helptext03.gameObject.SetActive(false);
                ForwardButton.gameObject.SetActive(true);
                Nextbutton.gameObject.SetActive(true);
                CloseImage.gameObject.SetActive(false);
                break;
            case 2:
                Helptext01.gameObject.SetActive(false);
                Helptext02.gameObject.SetActive(false);
                Helptext03.gameObject.SetActive(true);
                Nextbutton.gameObject.SetActive(false);
                ForwardButton.gameObject.SetActive(false);
                CloseImage.gameObject.SetActive(true);
                break;
        }
    }

    public void HelpPlannel_NextPage()
    {
        // counting the number of pages shown
        CountPages++;
        HelpPlannel_show();
    }

    public void HelpPlannel_ReturnPage()
    {
        // counting the number of pages shown
        CountPages--;
        HelpPlannel_show();
    }

    public void HelpPlannel_close()
    {
        // close help plannel
        HelpPlannel.SetActive(false);
        Helptext01.gameObject.SetActive(false);
        Helptext02.gameObject.SetActive(false);
        Helptext03.gameObject.SetActive(false);
        CloseImage.gameObject.SetActive(false);
        Nextbutton.gameObject.SetActive(false);
        ForwardButton.gameObject.SetActive(false);
        CountPages = 0;
    }

    public void HelpPlannel_open()
    {
        // open plannel and show text for page 1
        HelpPlannel.SetActive(true);
        Helptext01.gameObject.SetActive(true);
        Helptext02.gameObject.SetActive(false);
        Helptext03.gameObject.SetActive(false);
        Nextbutton.gameObject.SetActive(true);
        ForwardButton.gameObject.SetActive(false);
        CloseImage.gameObject.SetActive(false);
    }

    public void HelpPlannel_changeLanguage()
    {
        // check page 1 is eng or chin, then change it into another language
        if (Helptext01.text == "Users can scan the pages of storybook using app.")
        {
            Helptext01.text = "用戶在閱讀故事書時,可以利用APP掃描書頁圖片";
        }
        else
        {
            Helptext01.text = "Users can scan the pages of storybook using app.";
        }

        // check page 2 is eng or chin, then change it into another language
        if (Helptext02.text == "3D models will be displayed and show the storyline.")
        {
            Helptext02.text = "APP的鏡頭中會出現3D模型演出故事內容";
        }
        else
        {
            Helptext02.text = "3D models will be displayed and show the storyline.";
        }

        // check page 3 is eng or chin, then change it into another language
        if (Helptext03.text == "In the top right corner, users can select the language for the narration.")
        {
            Helptext03.text = "畫面右上角的清單可以選擇語言播放旁白";
        }
        else
        {
            Helptext03.text = "In the top right corner, users can select the language for the narration.";
        }
    }

}