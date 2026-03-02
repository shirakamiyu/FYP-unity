<div align="center">

  # AR-Assisted Learning System
  
  以3D模型的方式呈現故事中的情節, 帶來**視覺與聽覺**的融合, 有助吸引孩子的興趣  
  與背景場景結合的音效, 讓孩子可以身臨其境了解所在地  
  以卡通風格設計的動物模型, 使孩子可以認識動物的特點  
<hr>

> | Unity editor version: 6000.0.51 |   
> How to download the unity file of this AR project   
> Please download all files as .zip file  
> Remember that you also need to download the files in tag which is same as the title of branch that you downloaded  
> Please put files of tag in correct file path that shown in tag

> | Unity引擎版本: 6000.0.51 |  
> 下載前請留意下載的版本（1.0.xx）  
> 請根據對應分支名稱, 下載相應tag中的文件  
> 將文件移動到正確的路徑避免項目出現錯誤

<hr>

</div>

APK file download: https://drive.google.com/drive/folders/12gVKVVvhTwfpDIvIvk0nN38UfOLZo1QM?usp=sharing   
APP安裝包下載地址: https://drive.google.com/drive/folders/12gVKVVvhTwfpDIvIvk0nN38UfOLZo1QM?usp=sharing   

<hr>

## 1.0.13
重新設計應用UI, 包括
- 按鈕
- 美術元素
- 開屏介紹
- 切換效果
  
## 1.0.14
- 音效試運行   
- 為沙灘模型添加背景音樂

## 1.0.15
- 界面上半部分重做  
- 增設下拉式選項播放旁白，支援中英播放   
- 相機界面上半部分會顯示當前播放的旁白

## 1.0.16
- 修復中文文本無法顯示問題   
- 修復每次切換主畫面重複顯示版本資訊問題   
- 説明界面重做    
- 增加新場景（森林、小溪）  
- 第一幕（沙灘場景）模型添加動畫
- 優化UI

## 1.0.17 
- 優化界面切換邏輯
- 增加新場景（貓貓屋子）    

## 1.0.18
- 引入評分系統，連接數據庫
- 增加新場景（非洲草原）    

## 1.0.19 
- UI優化3.0
- 優化旁白播放邏輯，轉用中英文按鈕，根據已偵測場景切換播放源
- 補全所有場景的中英文旁白
- 逐步完整所有場景模型動畫
- 增加新場景（草原、高山）    
   
   
   [![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/shirakamiyu/FYP-unity)   
<hr>

## 未來開發動向--年底前完成

### 應用開發
- [x] 調整模型權重   
- [x] 添加場景音效   
- [x] 增加更多場景：草原、高山   
- [x] 整合多個mixamo動作為一   
      >>> 透過Unity Controller可以整合多個動作，包括兩個動作之間銜接的長度 >>> 最後一個動作會不斷loop   
      >>>>>>> P.S 需要在帶有動作且已轉換人型骨骼的fbx文件中找到xixamo.com（多爲以上名稱）的部分按下ctrl+D取得單獨的animation文件   
- [x] 模型動作需分開，不能共享   
      >>> 原帶有骨架的fbx文件apply unity的人型骨骼后添加animation clip會出現上下移動而不是實際動作   
      >>> 骨架文件導入mixamo然後下載帶有動作的fbx文件（duck_waving.fbx）可以使animation順利播放 【問題解決】   

### 實體開發   
- [x] 完善故事線  
- [x] 繪製故事插圖   
- [x] 利用AI: NanoBanana生成統一風格的插圖  

<hr>

<sub>評分系統，連接DB >>> ITP4903，ITP4922 >>> 透過家中電腦作爲數據庫服務器接收評分及意見，以端口轉發使外部網絡能夠訪問（數據庫以XAMPP的MyPHPadmin運行）    
限制域名訪問SDK（Vuforia，ARCore） >>> </sub>
