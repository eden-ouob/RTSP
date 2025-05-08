using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace RTSPInst
{
    class pushAICAM
    {
        string descfolder = "D:\\ffmpeg\\AICAM";
        ProcessStartInfo startInfo;
        Process process;
        public pushAICAM()
        {
            if (Directory.Exists(descfolder))
            {
                string[] files = Directory.GetFiles(descfolder);
                foreach (string file in files)
                { File.Delete(file); }
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
            startInfo.Arguments = "  -i rtsp://admin:Pass1234@"+
                                  "192.168.1.1:554/stream1 -fflags flush_packets -max_delay 5 -flags -global_header -hls_time 3 -hls_list_size 20 -vcodec copy -y " +
                                  "AICAM.m3u8";

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process = new Process
            { StartInfo = startInfo };
            process.Start();
        }
    }
}
