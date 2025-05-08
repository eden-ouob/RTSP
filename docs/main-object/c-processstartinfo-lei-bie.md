# C# ProcessStartInfo 類別

`ProcessStartInfo` 是 C# 中用來設定並啟動外部程式（如 `ffmpeg.exe`）的 **配置物件**。

屬於 `.NET` 的 `System.Diagnostics` 命名空間。

藉由`ProcessStartInfo` 類別能夠細部控制如何啟動一個新的程式。

以 `ProcessStartInfo` 類別建立 startInfo 範例：

```csharp
ProcessStartInfo startInfo;
```

常用屬性：

<table><thead><tr><th width="219.333251953125">屬性</th><th>說明</th></tr></thead><tbody><tr><td><code>FileName</code></td><td>要執行的程式檔案，例如 <code>ffmpeg.exe</code> 或 <code>cmd.exe</code>。</td></tr><tr><td><code>Arguments</code></td><td>執行這個程式時要傳入的參數，例如 <code>-i input.mp4 -c:v copy output.ts</code>。</td></tr><tr><td><code>WorkingDirectory</code></td><td>執行時的工作目錄，會影響程式找相對路徑的方式。</td></tr><tr><td><code>UseShellExecute</code></td><td>是否透過 Shell（命令列）來啟動，預設是 <code>true</code>，但若你要取得輸出、錯誤訊息，要設為 <code>false</code>。</td></tr><tr><td><code>RedirectStandardOutput</code></td><td>是否要重導程式的標準輸出（用於擷取程式執行時輸出的文字）。</td></tr><tr><td><code>RedirectStandardError</code></td><td>是否要擷取錯誤輸出。</td></tr><tr><td><code>CreateNoWindow</code></td><td>設為 <code>true</code> 表示不顯示黑色 Console 視窗，常用於背景處理。</td></tr><tr><td><code>WindowStyle</code></td><td>設定視窗樣式，如 <code>Hidden</code>、<code>Normal</code> 等。</td></tr></tbody></table>

實作範例：

{% code lineNumbers="true" %}
```csharp
ProcessStartInfo startInfo;
 
startInfo = new ProcessStartInfo();
startInfo.WorkingDirectory = descfolder;
startInfo.FileName = Application.StartupPath + @"\ffmpeg.exe";
startInfo.Arguments = "-i rtsp://admin:Pass1234@"+
                      "192.168.20.11:554/stream1 -fflags flush_packets -max_delay 5 -flags -global_header -hls_time 3 -hls_list_size 20 -vcodec copy -y " +
                      "AICAM.m3u8";

startInfo.WindowStyle = ProcessWindowStyle.Hidden;
process = new Process { StartInfo = startInfo };
process.Start();
```
{% endcode %}
