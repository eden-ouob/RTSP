# 清除歷史紀錄

這個類別名叫 `clearhis`，目的是定期清除位於特定資料夾中的 `.ts` 影片檔案，避免硬碟空間被過多的錄影檔佔滿。

***

主要功能：啟動與停止清理作業

* `startclearfields()`：啟動背景執行緒 `t1`，並執行 `clearfields` 方法。

```csharp
public void startclearfields() {
    t1 = new Thread(clearfields);
    t1.Start();
}
```

* `stopclearfields()`：如果 `t1` 還活著，就強制中止這個執行緒。

```csharp
public void stopclearfields() { 
    if (t1.IsAlive) t1.Abort(); 
}
```

* `clearfields()`：無限迴圈，每秒檢查一次。

如果 lasttime 是空的，表示第一次啟動，立刻清理。否則每 60秒 清理一次。

每次清理後更新 lasttime 為現在時間。

```csharp
while(true) {
    if (lasttime == "")　{
        clrfields(); 
        lasttime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); 
    }
    else {
        if (DateTime.Now.AddSeconds(-60)>=DateTime.Parse(lasttime))　{
             clrfields(); 
             lasttime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); 
         }
    }
    Thread.Sleep(1000);
}
```

* `clrfields()`：
  * **取得 `.ts` 檔案清單並排序**：使用正則表達式來補 0，確保數字比較準確（例如 "2.ts" 排在 "10.ts" 前）。
  * **控制檔案數量**：如果 `.ts` 檔案數量超過 500，會刪除最舊的檔案，只保留最新的 200 個。

```csharp
void clrfields() {
    foreach (var folder in descfolder) {
        // 取得所有 .ts 檔案並排序
        var files = Directory.GetFiles(folder, "*.ts")
            .OrderBy(f => System.Text.RegularExpressions.Regex.Replace(Path.GetFileName(f), @"\d+", m => m.Value.PadLeft(10, '0')))
            .ToArray();

        // 如果 .ts 檔案超過 500，刪除最舊的200個檔案
        if (files.Length > 500) {
            for (int i = 0; i < files.Length - 200; i++) {
                File.Delete(files[i]);
            }
        }                
    }
} 
```

***

程式主體：

{% code lineNumbers="true" %}
```csharp
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace RTSPInst
{
    class clearhis
    {
        string[] descfolder = new string[] {"D:\\ffmpeg\\AICAM", "D:\\ffmpeg\\SURCAM"};
        string lasttime = "";
        Thread t1;
        
        public void startclearfields() {
            t1 = new Thread(clearfields);
            t1.Start();
        }
        public void stopclearfields() {
            if (t1.IsAlive) t1.Abort(); 
        }

        void clearfields() {
            while(true) {
                if (lasttime == "") {
                    clrfields(); lasttime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else {
                    if (DateTime.Now.AddSeconds(-60)>=DateTime.Parse(lasttime))
                        clrfields(); 
                        lasttime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    }
                }
                Thread.Sleep(1000);
            }
        }
        void clrfields() {
            foreach (var folder in descfolder) {
                // 取得所有 .ts 檔案並排序
                var files = Directory.GetFiles(folder, "*.ts")
                    .OrderBy(f => System.Text.RegularExpressions.Regex.Replace(Path.GetFileName(f), @"\d+", m => m.Value.PadLeft(10, '0')))
                    .ToArray();

                // 如果 .ts 檔案超過 500，刪除最舊的200個檔案
                if (files.Length > 500) {
                    for (int i = 0; i < files.Length - 200; i++) {
                        File.Delete(files[i]);
                    }
                }                
            }
        }  
    }
}
```
{% endcode %}
