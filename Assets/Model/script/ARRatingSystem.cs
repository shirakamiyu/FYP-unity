using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class ARRatingSystem : MonoBehaviour
{
    [Header("Server setting")]
    public string hostName = "shirakamiyu.asuscomm.com";
    public int httpsPort = 8444;
    public bool useHTTPS = true;

    [Header("UI - TextMeshPro")]
    public GameObject ratingPanel;
    public Button[] starButtons;
    public TextMeshProUGUI ratingText;
    public TMP_InputField commentInput;
    public TMP_InputField UserIDInput;
    public Button submitButton;
    public Button hideButton;
    public TextMeshProUGUI connectionStatus;

    [Header("Show message")]
    public TextMeshProUGUI messageText;
    public float messageDisplayTime = 3f;

    [Header("Auto Show Settings")]
    public float showAfterSeconds = 60f;  // 1 min
    public bool autoShowEnabled = true;

    private int selectedRating = 0;
    private Color defaultColor = Color.white;
    public Color selectedColor = Color.yellow;
    private string apiURL;
    private static bool hasShown = false;

    void Start()
    {
        InitializeUI();
        UpdateServerURL();
        StartCoroutine(TestConnection());

        if (autoShowEnabled)
        {
            StartCoroutine(AutoShowRatingPanel());
        }
    }

    IEnumerator AutoShowRatingPanel()
    {
        Debug.Log($"Rating panel will show after {showAfterSeconds} seconds");

        // waitting for time
        yield return new WaitForSeconds(showAfterSeconds);

        // checking shown or not
        if (!hasShown && ratingPanel != null && !ratingPanel.activeInHierarchy)
        {
            ShowRatingPanel();
            hasShown = true;
        }
    }

    void InitializeUI()
    {
        for (int i = 0; i < starButtons.Length; i++)
        {
            int starValue = i + 1;
            starButtons[i].onClick.AddListener(() => SetRating(starValue));
        }

        submitButton.onClick.AddListener(SubmitRating);
        hideButton.onClick.AddListener(HideRatingPanel);
        ResetRatingUI();

        if (messageText != null)
        {
            messageText.text = "Submit";
        }

        if (ratingPanel != null)
        {
            ratingPanel.SetActive(false);
        }
    }

    void UpdateServerURL()
    {
        apiURL = $"https://{hostName}:{httpsPort}/submit_rating.php";
        Debug.Log($"API URL: {apiURL}");

        if (connectionStatus != null)
        {
            connectionStatus.text = "using HTTPS 8444 port";
            connectionStatus.color = Color.blue;
        }
    }

    IEnumerator TestConnection()
    {
        if (connectionStatus == null) yield break;

        connectionStatus.color = Color.yellow;
        connectionStatus.text = "testing HTTPS connect...";

        string testURL = $"https://{hostName}:{httpsPort}/applications.html";

        using (UnityWebRequest www = UnityWebRequest.Get(testURL))
        {
            www.timeout = 15;
            www.certificateHandler = new BypassCertificateHandler();

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                connectionStatus.text = "HTTPS connected";
                connectionStatus.color = Color.green;
            }
            else
            {
                connectionStatus.text = $"HTTPS disconnect:\n {www.error}";
                connectionStatus.color = Color.red;
            }
        }
    }

    void SetRating(int rating)
    {
        selectedRating = rating;

        if (ratingText != null)
        {
            ratingText.text = $"{rating} / 5";
        }

        for (int i = 0; i < starButtons.Length; i++)
        {
            starButtons[i].image.color = i < rating ? selectedColor : defaultColor;
        }

        submitButton.interactable = true;
    }

    void ResetRatingUI()
    {
        selectedRating = 0;

        if (ratingText != null)
        {
            ratingText.text = "0 / 5";
            ratingText.color = Color.gray;
        }

        if (commentInput != null)
        {
            commentInput.text = "";
        }

        if (UserIDInput != null)
        {
            UserIDInput.text = "user";
        }

        submitButton.interactable = false;

        foreach (Button star in starButtons)
        {
            star.image.color = defaultColor;
        }
    }

    public void SubmitRating()
    {
        if (selectedRating == 0)
        {
            ShowMessage("0 / 5");
            return;
        }

        StartCoroutine(SendRatingToServer());
    }

    IEnumerator SendRatingToServer()
    {
        submitButton.interactable = false;

        if (connectionStatus != null)
        {
            connectionStatus.text = "submiting...";
            connectionStatus.color = Color.yellow;
        }

        string comment = commentInput != null ? commentInput.text : "";
        string userID = UserIDInput != null && !string.IsNullOrWhiteSpace(UserIDInput.text)
        ? UserIDInput.text.Trim()
        : "user";

        WWWForm form = new WWWForm();
        form.AddField("rating", selectedRating);
        form.AddField("comment", comment);
        form.AddField("user_id", userID);

        using (UnityWebRequest www = UnityWebRequest.Post(apiURL, form))
        {
            www.timeout = 15;
            www.certificateHandler = new BypassCertificateHandler();

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // 解析響應以獲得更多信息
                string responseText = www.downloadHandler.text;
                Debug.Log("Server response: " + responseText);

                if (connectionStatus != null)
                {
                    connectionStatus.text = "submited!";
                    connectionStatus.color = Color.green;
                }

                ShowMessage("thank you!", Color.green);
                yield return new WaitForSeconds(1.5f);
                ratingPanel.SetActive(false);
                ResetRatingUI();
            }
            else
            {
                if (connectionStatus != null)
                {
                    connectionStatus.text = "not submited";
                    connectionStatus.color = Color.red;
                }

                Debug.LogError("Submit failed: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler?.text);

                ShowMessage("submit failed: " + www.error, Color.red);
                submitButton.interactable = true;
            }
        }
    }

    void ShowMessage(string message, Color color)
    {
        Debug.Log(message);

        if (messageText != null)
        {
            messageText.text = message;
            messageText.color = color;
            messageText.gameObject.SetActive(true);
            StartCoroutine(HideMessageAfterDelay());
        }
    }

    void ShowMessage(string message)
    {
        ShowMessage(message, Color.white);
    }

    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDisplayTime);
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    public void ShowRatingPanel()
    {
        if (ratingPanel != null)
        {
            ratingPanel.SetActive(true);
            hideButton.gameObject.SetActive(true);
            ResetRatingUI();
            StartCoroutine(TestConnection());
            hasShown = true; // 標記為已顯示
        }
    }

    public void HideRatingPanel()
    {
        if (ratingPanel != null)
        {
            ratingPanel.SetActive(false);
            hideButton.gameObject.SetActive(false);
        }
    }

    private class BypassCertificateHandler : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}