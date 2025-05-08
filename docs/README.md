# FFMPEG

透過 ffmpeg 轉換 RTSP （Real Time Streaming Protocol）串流成 HLS（HTTP Live Streaming）格式的 AICAM.m3u8

指令執行 ffmpeg：

```bash
ffmpeg.exe 
-i rtsp://admin:Pass1234@192.168.20.11:554/stream1 
-fflags flush_packets 
-max_delay 5 
-flags 
-global_header 
-hls_time 3 
-hls_list_size 20 
-vcodec copy 
-y AICAM.m3u8
```

依序解釋各個參數：

```bash
-i rtsp://admin:Pass1234@192.168.20.11:554/stream1
```

指定輸入來源：一個 RTSP 串流位址，其中包含使用者名稱（admin）和密碼（Pass1234），來源位於本地網路的 IP `192.168.20.11`，使用 port `554`，串流路徑是 `/stream1`。

```bash
-fflags flush_packets
```

告訴 FFmpeg 在串流時立即送出封包，減少延遲。

```bash
-max_delay 5
```

設定最大解碼延遲為 5 微秒

```bash
-flags -global_header
```

啟用 `global_header`，這是為了支援一些封裝格式（像 HLS）所需的設定，把解碼器的全域資訊放到每個分段中。

```bash
-hls_time 3
```

設定 HLS 的每個 `.ts` 檔案片段長度為 3 秒。

```bash
-hls_list_size 20
```

M3U8 播放清單最多保留 20 個片段；達到這個數量後會移除最舊的，這使得播放清單可以「滑動」更新。

```bash
-vcodec copy
```

不轉換影片編碼格式，直接複製原始串流的編碼，這樣可以減少 CPU 使用率和轉換延遲。

```bash
-y AICAM.m3u8
```

輸出為 `AICAM.m3u8`，`-y` 表示如果檔案已存在，強制覆蓋。
