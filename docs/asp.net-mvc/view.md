# View

\<style> 區域為影片區塊布局

```css
<style>
    /* 使用 flexbox 來顯示兩個影片區塊並排 */
    .video-container {
        display: flex;
        justify-content: space-between; /* 讓兩個區塊間距保持均等 */
        gap: 10px; /* 兩個影片區塊之間的間距 */
        height: 400px; /* 固定的高度 */
    }

    /* 獨立的影片區塊 */
    .video-block {
        display: flex;
        flex-direction: column;
    }

    /* 左邊影片佔據 70% 寬度，右邊影片佔據 30% 寬度 */
    .video-block.left {
        flex: 5.71; /* 70% 的寬度 */
    }

    .video-block.right {
        flex: 4.29; /* 30% 的寬度 */
    }

    /* 確保影片自適應寬度並保持比例 */
    .video-block video {
        width: 100%;
        height: 100%; /* 讓影片高度自適應 */
        object-fit: contain; /* 保持影片原始比例 */
    }
</style>
```



\<video> 區域為 video-js 影片撥放器設定

```css
<video id="my_video_1" class="video-js vjs-fluid vjs-default-skin" controls preload="none" data-setup='{"autoplay": false}'>
    <source src="http://192.168.1.1/Cam/AICAM" type="application/x-mpegURL">
</video>
```



\<script> 區域為 videojs 自動撥放設定

```css
<script>
    // 初始化兩個視頻播放器
    var player1 = videojs('my_video_1');
    var player2 = videojs('my_video_2');

    // 由用戶交互後開始播放
    player1.ready(function() {
        console.log('player1 播放器已經準備好');
        player1.muted(true);  // 使視頻靜音
        player1.play();
    });

    player2.ready(function() {
        console.log('player2 播放器已經準備好');
        player2.muted(true);  // 使視頻靜音
        player2.play();
    });
</script>
```

***

程式主體：

```css
@{
    Layout = null;  // 這樣就不使用任何 Layout，這會移除 header 和 footer 部分
}

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>HLS Video Player</title>

    <link href="/js/video-js.css" rel="stylesheet">
    <script src="/js/video.js"></script>
    <script src="/js/signalr.min.js"></script>
    <style>
        /* 使用 flexbox 來顯示兩個影片區塊並排 */
        .video-container {
            display: flex;
            justify-content: space-between; /* 讓兩個區塊間距保持均等 */
            gap: 10px; /* 兩個影片區塊之間的間距 */
            height: 400px; /* 固定的高度 */
        }

        /* 獨立的影片區塊 */
        .video-block {
            display: flex;
            flex-direction: column;
        }

        /* 左邊影片佔據 70% 寬度，右邊影片佔據 30% 寬度 */
        .video-block.left {
            flex: 5.71; /* 70% 的寬度 */
        }

        .video-block.right {
            flex: 4.29; /* 30% 的寬度 */
        }

        /* 確保影片自適應寬度並保持比例 */
        .video-block video {
            width: 100%;
            height: 100%; /* 讓影片高度自適應 */
            object-fit: contain; /* 保持影片原始比例 */
        }
    </style>
</head>
<body>
    <div class="video-container">
        <!-- 左邊影片區塊 (佔 70% 寬度) -->
        <div class="video-block left">
            <!-- AICAM 播放器 -->
            <video id="my_video_1" class="video-js vjs-fluid vjs-default-skin" controls preload="none" data-setup='{"autoplay": false}'>
                <source src="http://192.168.1.1:8089/Cam/AICAM" type="application/x-mpegURL">
            </video>
        </div>

        <!-- 右邊影片區塊 (佔 30% 寬度) -->
        <div class="video-block right">
            <!-- SURCAM 播放器 -->
            <video id="my_video_2" class="video-js vjs-fluid vjs-default-skin" controls preload="none" data-setup='{"autoplay": false}'>
                <source src="http://192.168.1.1:8089/Cam/SURCAM" type="application/x-mpegURL">
            </video>
        </div>
    </div>

    <script>
        // 初始化兩個視頻播放器
        var player1 = videojs('my_video_1');
        var player2 = videojs('my_video_2');

        // 由用戶交互後開始播放
        player1.ready(function() {
            console.log('player1 播放器已經準備好');
            player1.muted(true);  // 使視頻靜音
            player1.play();
        });

        player2.ready(function() {
            console.log('player2 播放器已經準備好');
            player2.muted(true);  // 使視頻靜音
            player2.play();
        });
    </script>
</body>
</html>

```
