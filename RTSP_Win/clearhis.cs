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
        public void startclearfields()
        {t1 = new Thread(clearfields);t1.Start();}
        public void stopclearfields()
        { if (t1.IsAlive) t1.Abort(); }

        void clearfields()
        {
            while(true)
            {
                if (lasttime == "")
                { clrfields(); lasttime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); }
                else
                {
                    if (DateTime.Now.AddSeconds(-60)>=DateTime.Parse(lasttime))
                    { clrfields(); lasttime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); }
                }
                Thread.Sleep(1000);
            }
        }
        void clrfields()
        {
            foreach (var folder in descfolder)
            {
                // 取得所有 .ts 檔案並排序
                var files = Directory.GetFiles(folder, "*.ts")
                    .OrderBy(f => System.Text.RegularExpressions.Regex.Replace(Path.GetFileName(f), @"\d+", m => m.Value.PadLeft(10, '0')))
                    .ToArray();

                // 如果 .ts 檔案超過 500，刪除最舊的200個檔案
                if (files.Length > 500)
                {
                    for (int i = 0; i < files.Length - 200; i++)
                    {
                        File.Delete(files[i]);
                    }
                }                
            }
        }  
    }
}
