using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTSPInst
{
    class pushSURCAM
    {
        string descfolder = "D:\\ffmpeg\\SURCAM";
        ProcessStartInfo startInfo;
        Process process;
        public pushSURCAM()
        {
            if (Directory.Exists(descfolder))
            {
                string[] files = Directory.GetFiles(descfolder);
                foreach (string file in files)
                {File.Delete(file);}
            }
        }
        public void stardownload()
        {
            if (!Directory.Exists(descfolder)) Directory.CreateDirectory(descfolder);
            downloadvideo();
        }
        public void stopdownload()
        { process.Kill(); }
        void downloadvideo()
        {
            startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = descfolder;
            startInfo.FileName = Application.StartupPath + @"\ffmpeg.exe";
            startInfo.Arguments = "  -i rtsp://admin:Pass1234@" +
                                  "192.168.1.1:554/stream1 -fflags flush_packets -max_delay 5 -flags -global_header -hls_time 3 -hls_list_size 0 -vcodec copy -y " +
                                  "SURCAM.m3u8";

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process = new Process
            { StartInfo = startInfo };
            process.Start();
        }
    }
}
