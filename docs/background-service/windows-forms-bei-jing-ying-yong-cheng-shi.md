# Windows Forms 背景應用程式

程式架構：

**命名空間：** `RTSPInst`\
**主類別：** `Form1`（繼承自 `Form`）\
**主要目的：**&#x5728;背景執行三個服務：

* `pushAICAM.stardownload()`：開始 AI 攝影機影像推播。
* `pushSURCAM.stardownload()`：開始監控攝影機影像推播。
* `clearhis.startclearfields()`：啟動清除歷史資料任務。

***

主要功能：

1. 表單初始化（`Form1()` 建構子）

```csharp
this.WindowState = FormWindowState.Minimized;
this.ShowInTaskbar = false;
this.Hide();
```

* 啟動程式時隱藏視窗，並不顯示在工作列。
* 表示這個程式不提供 UI 操作，是個背景服務。



2. **NotifyIcon 與 ContextMenu（`InitializeNotifyIcon()`）**

```csharp
notifyIcon = new NotifyIcon();
notifyIcon.Icon = Properties.Resources.ae61t_loyb9_001;
notifyIcon.Text = "聲音照相立即影像推播服務";
notifyIcon.Visible = true;
```

* 在系統托盤顯示一個小圖示。
* 顯示提示文字為：聲音照相立即影像推播服務。



3. **結束應用程式（`ExitApplication()`）**

```csharp
pushAICAM.stopdownload();
pushSURCAM.stopdownload();
clearhis.stopclearfields();
notifyIcon.Dispose();
Application.Exit();
```

當使用者從右鍵選單點選 **結束服務** 時，程式會：

* 停止三個背景任務。
* 移除系統托盤圖示。
* 結束整個應用程式。

***

程式主體：

{% code lineNumbers="true" %}
```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTSPInst
{
    public partial class Form1 : Form {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;
        pushAICAM pushAICAM = new pushAICAM();
        pushSURCAM pushSURCAM = new pushSURCAM();
        clearhis clearhis = new clearhis();

        public Form1() {
            InitializeComponent(); InitializeNotifyIcon();
            // 啟動時隱藏視窗
            this.WindowState = FormWindowState.Minimized;
            // 隱藏工作列圖示
            this.ShowInTaskbar = false; 
            this.Hide();
            pushAICAM.stardownload();
            pushSURCAM.stardownload();
            clearhis.startclearfields();
        }
        private void InitializeNotifyIcon() {
            // 建立 NotifyIcon
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.ae61t_loyb9_001;
            notifyIcon.Text = "聲音照相立即影像推播服務";
            notifyIcon.Visible = true;
            // 建立 ContextMenuStrip 作為右鍵選單
            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem exitItem = new ToolStripMenuItem("結束服務", null, ExitApplication);
            contextMenu.Items.Add(exitItem);
            // 設定 NotifyIcon 的 ContextMenuStrip
            notifyIcon.ContextMenuStrip = contextMenu;
        }
        private void ExitApplication(object sender, EventArgs e) {
            pushAICAM.stopdownload();
            pushSURCAM.stopdownload();
            clearhis.stopclearfields();
            notifyIcon.Dispose();
            Application.Exit();
        }
    }
}
```
{% endcode %}
