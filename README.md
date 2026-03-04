施工中。。。。   
cs script解釋

## 關於c#   
- using xxxx
  - python中import xxxx是同樣的意思，也就是引用libraries
- public xxxx
  - 變量，這個不用解釋了
  - GameObject（對象），Button（按鈕），TextMeshProUGUI（文字），TMP_InputField（文字輸入框）等都是unity中的部件
- Header（xxxx）
  - 沒有特別意義，增加可讀性來分類變量
- void start()
  - 啓動場景自動

------

### 📄<i> [ARRatingSystem.cs](./Assets/Model/script/ARRatingSystem.cs) </i>

- void start()
  - 啓動場景將運行裏面的子程序
- IEnumerator AutoShowRatingPanel()
  - 經過特定時間后，顯示評分系統
  - 當評分系統顯示過一次便不會再顯示
- void InitializeUI()
  - 選擇評分，根據分數修改對應數量星星圖標顔色變化
  - 為兩個按鈕添加監聽器：按下將提交表格 / 按下將關閉表格
  - 顯示提交及關閉評分界面
- void UpdateServerURL()
  - 更新數據庫伺服器URL
  - 顯示正在使用的端口
- IEnumerator TestConnection()
  - 更新數據庫伺服器URL
  - 測試與數據庫的鏈接（timeout = 15）
  - 鏈接成功與不成功都會顯示
- void SetRating(int rating)
  - 根據評分修改對應數量星星圖標顔色變化
  - 顯示評分“ x / 5 ”
- void ResetRatingUI()
  - 初始化評分界面
  - 默認評分“ 0 / 5 ”，無意見，用戶名
- SubmitRating()
  - 0分不會被提交
- IEnumerator SendRatingToServer()
  - 提交時顯示文字“提交中”
  - 檢查意見框是否空白、用戶名是否空白
  - 如果提交成功顯示“已提交”，失敗則顯示“提交失敗：（error）”
- void ShowMessage(string message, Color color)
  - 顯示文字+特定顔色
- void ShowMessage(string message)
  - 調用ShowMessage(string message, Color color)顯示文字，默認白色
- IEnumerator HideMessageAfterDelay()
  - 一定延遲后隱藏顯示的訊息
- public void ShowRatingPanel()
  - 顯示評分界面，同時初始化UI + 測試鏈接
- public void HideRatingPanel()
  - 關閉評分界面
- private class BypassCertificateHandler : CertificateHandler
  - 自定義的證書處理器，用於跳過 SSL/TLS 證書驗證
 
------

### 📄<i> [ChangeLanguage.cs](./Assets/Model/script/ChangeLanguage.cs) </i>   <sub><---嚴重屎山代碼根據地</sub>

- void start()
  - 啓動場景時不顯示幫助界面
  - 默認場景顯示語言為英文
- public void ToggleLanguage()
  - 已綁定到按鈕作爲監聽器
  - 檢查如果顯示中文，便取消顯示中文文本，改爲顯示英文，相反同理
- public void Show()   <sub><---已報廢</sub>
  - 根據當前文本語言顯示對應的幫助界面
- public void Hide()   <sub><---已報廢</sub>
  - 如上，只是改爲關閉
- public void HelpPlannel_show()
  - 根據第幾頁，顯示對應的圖片和切換按鈕
  - 變量CountPages用於計算頁數，一開始為0
- public void HelpPlannel_NextPage()
  - 下一頁，CountPages+
- public void HelpPlannel_ReturnPage()
  - 上一頁，CountPages-
- public void HelpPlannel_close()
  - 關閉幫助界面
  - 重置CountPages = 0
- public void HelpPlannel_open()
  - 開啓幫助界面
- public void HelpPlannel_open()
  - 已綁定到按鈕作爲監聽器
  - 按下切換語言按鈕便會替換文字框的内容（也就是中文與英文）

------

### 📄<i> [MainUI.cs](./Assets/Model/script/MainUI.cs) </i>

- private void Awake()
  - 不重要，防止出bug
- private void Start()
  - 默認關閉界面不顯示
- public void ShowPanel() 
  - 顯示關閉界面
- public void HidePanel()
  - 關閉關閉界面
- public void Exit()
  - 切換到主界面（scene 1）
  - 取消顯示關閉界面和清空文字内容
 
------

### 📄<i> [OpenManeger.cs](./Assets/Model/script/OpenManeger.cs) </i>

- public static int SceneOpen = 0;
  - 全局變量
  - 用於主界面顯示版本訊息用
- private void Awake()
  - 不重要，防止出bug
- private void Start()
  - 圖片的透明度改爲0
- private IEnumerator Load()
  - 輪流在一秒内將圖片的透明度由0增加到1（漸入），在減少到0（漸出）
  - 切換到主界面（scene 1）

------

### 📄<i> [SceneDetect.cs](./Assets/Model/script/SceneDetect.cs) </i>

- void Update()
  - 每一幀都偵測
  - 如果在場景中有追蹤中的模型，currentTargetName = 該模型的名稱
  - 文本顯示追蹤中的模型名稱

------

### 📄<i> [SceneDetectWithAudio.cs](./Assets/Model/script/SceneDetectWithAudio.cs) </i>

- xxxx
  - xxx

 
------

### 📄<i> [UIController.cs](./Assets/Model/script/UIController.cs) </i>

- private void Awake()
  - 不重要，防止出bug
- private void Start()
  - 圖片的透明度改爲0
  - 調用LoadTitle()
- private IEnumerator LoadTitle()
  - 輪流在一秒内將圖片的透明度由0增加到1（漸入），在減少到0（漸出）
  - 檢查全局變量（SceneOpen）是否為0，是便會顯示版本咨詢（初次打開APP），將全局變量+1
  - 為按鈕增加監聽器
- private void Hide()
  - 關閉界面，通用于兩個界面
- private void OnStartButtonClick()
  - 按下調用Load()前往相機界面（scene 2）
- private void OnExitButtonClick()
  - 按下關閉APP
- private IEnumerator Load()
  - 輪流在一秒内將圖片的透明度由0增加到1（漸入），在減少到0（漸出）
  - 切換到相機界面（scene 2）
