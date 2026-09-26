using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FontUsageChecker : EditorWindow
{
    [MenuItem("Tools/Check Unused Fonts")]
    public static void ShowWindow()
    {
        GetWindow<FontUsageChecker>("Font Usage Checker");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("掃描專案中未使用的字體", GUILayout.Height(40)))
        {
            CheckUnusedFonts();
        }
    }

    private static void CheckUnusedFonts()
    {
        // 1. 搜尋 Assets/Model/fonts/ 資料夾下的所有字體檔案
        string fontFolderPath = "Assets/Model/fonts";
        if (!Directory.Exists(fontFolderPath))
        {
            Debug.LogError($"找不到資料夾: {fontFolderPath}");
            return;
        }

        string[] fontGuids = AssetDatabase.FindAssets("t:Font t:TMP_FontAsset", new[] { fontFolderPath });
        HashSet<string> allFontPaths = new HashSet<string>();
        foreach (var guid in fontGuids)
        {
            allFontPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
        }

        // 2. 搜尋專案內所有的 Scene 同 Prefab
        string[] allAssetGuids = AssetDatabase.FindAssets("t:Scene t:Prefab");
        HashSet<string> usedFontPaths = new HashSet<string>();

        foreach (var guid in allAssetGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string[] dependencies = AssetDatabase.GetDependencies(assetPath, true);

            foreach (var dep in dependencies)
            {
                if (allFontPaths.Contains(dep))
                {
                    usedFontPaths.Add(dep);
                }
            }
        }

        // 3. 印出結果到 Console
        Debug.Log($"=== 掃描完成！總共找到 {allFontPaths.Count} 個字體資源 ===");

        int unusedCount = 0;
        foreach (var fontPath in allFontPaths)
        {
            if (!usedFontPaths.Contains(fontPath))
            {
                Debug.LogWarning($"[未使用/可刪除] {fontPath}", AssetDatabase.LoadAssetAtPath<Object>(fontPath));
                unusedCount++;
            }
            else
            {
                Debug.Log($"[使用中] {fontPath}");
            }
        }

        Debug.Log($"=== 掃描結束：有 {unusedCount} 個字體沒有被任何 Scene 或 Prefab 使用 ===");
    }
}