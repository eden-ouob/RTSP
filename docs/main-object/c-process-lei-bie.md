# C# Process 類別

`Process` 代表**一個正在執行的應用程式實體，**&#x5C6C;於 `.NET` 的 `System.Diagnostics` 命名空間中。

你可以用它來：

* 啟動一個程式
* 監控它的執行狀態（是否結束）
* 擷取輸出或錯誤訊息
* 強制關閉程式
* 讀取執行檔路徑、PID（進程 ID）等資訊

常用屬性：

<table><thead><tr><th width="282.4444580078125">成員</th><th>說明</th></tr></thead><tbody><tr><td><code>Start()</code></td><td>啟動程式（根據給定的 <code>ProcessStartInfo</code>）</td></tr><tr><td><code>Kill()</code></td><td>強制關閉程式</td></tr><tr><td><code>HasExited</code></td><td>判斷程式是否已經結束</td></tr><tr><td><code>WaitForExit()</code></td><td>等待程式執行完成</td></tr><tr><td><code>Id</code></td><td>取得這個 Process 的進程 ID</td></tr><tr><td><code>StandardOutput</code> / <code>StandardError</code></td><td>擷取程式的輸出與錯誤資料流（需要搭配 <code>Redirect...</code>）</td></tr><tr><td><code>Exited</code> 事件</td><td>當程式結束時觸發，可用來處理收尾工作</td></tr></tbody></table>

範例：

{% code lineNumbers="true" %}
```csharp
using System.Diagnostics;

ProcessStartInfo startInfo = new ProcessStartInfo();
startInfo.FileName = "notepad.exe";  // 開啟記事本
startInfo.UseShellExecute = true;

Process process = new Process();
process.StartInfo = startInfo;
process.Start();

// 等待使用者關閉記事本
process.WaitForExit();

Console.WriteLine("記事本已關閉");
```
{% endcode %}
