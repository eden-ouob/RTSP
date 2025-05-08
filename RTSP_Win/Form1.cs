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
    public partial class Form1 : Form
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;
        pushAICAM pushAICAM = new pushAICAM();
        pushSURCAM pushSURCAM = new pushSURCAM();
        clearhis clearhis = new clearhis();
        public Form1()
        {
            InitializeComponent(); InitializeNotifyIcon();
            // 啟動時隱藏視窗
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false; // 隱藏工作列圖示
            this.Hide();
            pushAICAM.stardownload();
            pushSURCAM.stardownload();
            clearhis.startclearfields();
        }
        private void InitializeNotifyIcon()
        {
            // 建立 NotifyIcon
            notifyIcon = new NotifyIcon();
            notifyIcon.Text = "RTSP";
            notifyIcon.Visible = true;
            // 建立 ContextMenuStrip 作為右鍵選單
            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit", null, ExitApplication);
            contextMenu.Items.Add(exitItem);
            // 設定 NotifyIcon 的 ContextMenuStrip
            notifyIcon.ContextMenuStrip = contextMenu;
        }
        private void ExitApplication(object sender, EventArgs e)
        {
            pushAICAM.stopdownload();
            pushSURCAM.stopdownload();
            clearhis.stopclearfields();
            notifyIcon.Dispose();
            Application.Exit();
        }
    }
}
