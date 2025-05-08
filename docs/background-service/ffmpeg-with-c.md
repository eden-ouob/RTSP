# FFMPEG with C\#

**命名空間 `RTSPInst`**：代表整個程式的命名空間。

```csharp
namespace RTSPInst
```

**自定義類別 `pushAICAM`**：封裝了一個下載直播串流的功能。

```csharp
class pushAICAM
```

**C#類別**`ProcessStartInfo`與`Process`：建立 startInfo 與 process

```csharp
ProcessStartInfo startInfo;
```

物件`pushAICAM` 建構子：public pushAICAM(){}

內容是**在這個物件被建立的同時，自動清空指定資料夾內的所有檔案**。

```csharp
public pushAICAM(){
    // 檢查資料夾是否存在
    if (Directory.Exists(descfolder)){ 
        // 取得所有檔案路徑
        string[] files = Directory.GetFiles(descfolder); 
        // 逐一處理每個檔案
        foreach (string file in fAiles){ 
            // 刪除檔案
            File.Delete(file); 
        }
    }
}
```

使用 ffmpeg 建立 private function `downloadvideo()`，程式參數由物件`startInfo`設計，程式管控由物件 `process` 管理：

```csharp
        void downloadvideo()
        {
            startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = descfolder;
            startInfo.FileName = Application.StartupPath + @"\ffmpeg.exe";
            startInfo.Arguments = "-i rtsp://admin:Pass1234@"+
                                  "192.168.20.11:554/stream1 -fflags flush_packets -max_delay 5 -flags -global_header -hls_time 3 -hls_list_size 20 -vcodec copy -y " +
                                  "AICAM.m3u8";

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process = new Process　{ StartInfo = startInfo };
            process.Start();
        }
```

整體程式實作：

{% code lineNumbers="true" %}
```csharp
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace RTSPInst {
    class pushAICAM {
        string descfolder = "D:\\ffmpeg\\AICAM";
        ProcessStartInfo startInfo;
        Process process;
        
        public pushAICAM() {
            if (Directory.Exists(descfolder)) {
                string[] files = Directory.GetFiles(descfolder);
                foreach (string file in files)
                { File.Delete(file); }
            }
        }
        
        public void stardownload() {
            if (!Directory.Exists(descfolder)) Directory.CreateDirectory(descfolder);
            downloadvideo(); 
        }
        
        public void stopdownload() { process.Kill(); }
        
        void downloadvideo() {
            startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = descfolder;
            startInfo.FileName = Application.StartupPath + @"\ffmpeg.exe";
            startInfo.Arguments = "-i rtsp://admin:Pass1234@"+
                                  "192.168.20.11:554/stream1 -fflags flush_packets -max_delay 5 -flags -global_header -hls_time 3 -hls_list_size 20 -vcodec copy -y " +
                                  "AICAM.m3u8";

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process = new Process　{ StartInfo = startInfo };
            process.Start();
        }
    }
}
```
{% endcode %}
