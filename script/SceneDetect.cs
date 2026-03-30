using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using TMPro;

public class SceneDetect : MonoBehaviour
{
    public TextMeshProUGUI modelNameText;

    void Update()
    {
        // 使用新的 FindObjectsByType
        ImageTargetBehaviour[] allTargets = FindObjectsByType<ImageTargetBehaviour>(FindObjectsSortMode.None);
        string currentTargetName = "";

        // 檢查哪個目標被追蹤
        foreach (ImageTargetBehaviour target in allTargets)
        {
            var observer = target.GetComponent<ObserverBehaviour>();
            if (observer != null)
            {
                var status = observer.TargetStatus;
                if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
                {
                    currentTargetName = target.gameObject.name;
                    break;
                }
            }
        }

        // 更新顯示
        if (modelNameText != null)
        {
            if (!string.IsNullOrEmpty(currentTargetName))
            {
                modelNameText.text = "辨識到: " + currentTargetName;
            }
            else
            {
                modelNameText.text = "";
            }
        }
    }
}