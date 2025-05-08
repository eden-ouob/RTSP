# Controller

RTSP 路由：回傳由 [Background service](../background-service/windows-forms-bei-jing-ying-yong-cheng-shi.md) 儲存的 .m3u8 文件路徑至前端網頁

```csharp
// 當前視圖顯示的 HLS 流
[HttpGet("RTSP")]
public IActionResult RTSP() {
    // 假設這是你存放 .m3u8 文件的位置
    string AICAM_m3u8FileUrl = "/ffmpeg/AICAM";  // 通過 HTTP 路徑訪問
    string SURCAM_m3u8FileUrl = "/ffmpeg/SURCAM";  // 通過 HTTP 路徑訪問

    // 將兩個 .m3u8 文件的 URL 傳遞到視圖
    var model = new { AICAM = AICAM_m3u8FileUrl, SURCAM = SURCAM_m3u8FileUrl };
    return View(model);
}
```



AICAM 路由：回傳 .m3u8 文件中所記錄的 RTSP 影像路徑

```csharp
// 返回 AICAM 的 .m3u8 文件
[HttpGet("AICAM")]
public async Task<IActionResult> Example_AICAM() {
    Response.Headers.Add("Access-Control-Allow-Origin", "*");
    string filePath = "/ffmpeg/AICAM/AICAM.m3u8";
    return File(System.IO.File.OpenRead(filePath), "application/octet-stream", enableRangeProcessing: true);
}
```



tsFileName 路由：回傳 RTSP 影像路徑 (.ts)

```
string filePath = "/ffmpeg/AICAM/" + tsFileName;
                return File(System.IO.File.OpenRead(filePath), "application/octet-stream", enableRangeProcessing: true);
```

***

程式主體：

```csharp
using Microsoft.AspNetCore.Mvc;

namespace RTSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CamController : Controller
    {
        // 當前視圖顯示的 HLS 流
        [HttpGet("RTSP")]
        public IActionResult RTSP()
        {
            // 假設這是你存放 .m3u8 文件的位置
            string AICAM_m3u8FileUrl = "/ffmpeg/AICAM";  // 通過 HTTP 路徑訪問
            string SURCAM_m3u8FileUrl = "/ffmpeg/SURCAM";  // 通過 HTTP 路徑訪問

            // 將兩個 .m3u8 文件的 URL 傳遞到視圖
            var model = new { AICAM = AICAM_m3u8FileUrl, SURCAM = SURCAM_m3u8FileUrl };
            return View(model);
        }

        // 返回 AICAM 的 .m3u8 文件
        [HttpGet("AICAM")]
        public async Task<IActionResult> Example_AICAM()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            string filePath = "/ffmpeg/AICAM/AICAM.m3u8";
            return File(System.IO.File.OpenRead(filePath), "application/octet-stream", enableRangeProcessing: true);
        }

        // 返回 SURCAM 的 .m3u8 文件
        [HttpGet("SURCAM")]
        public async Task<IActionResult> Example_SURCAM()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            string filePath = "/ffmpeg/SURCAM/SURCAM.m3u8";
            return File(System.IO.File.OpenRead(filePath), "application/octet-stream", enableRangeProcessing: true);
        }

        // 返回 .ts 文件
        [HttpGet("{tsFileName}")]
        public async Task<IActionResult> Example_GetTS(string tsFileName)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            // 正確的檔案路徑處理
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "AICAM", tsFileName);
            if (tsFileName.Substring(0, 5) == "AICAM")
            {
                string filePath = "/ffmpeg/AICAM/" + tsFileName;
                return File(System.IO.File.OpenRead(filePath), "application/octet-stream", enableRangeProcessing: true);
            }
            else if (tsFileName.Substring(0, 6) == "SURCAM")
            {
                string filePath = "/ffmpeg/SURCAM/" + tsFileName;
                return File(System.IO.File.OpenRead(filePath), "application/octet-stream", enableRangeProcessing: true);
            }
            else
            {
                Console.WriteLine("Error: tsFileName is not AICAM or SURCAM");
                return null;
            }
        }
    }
}

```
