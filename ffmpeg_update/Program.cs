using System.Diagnostics;

namespace ffmpeg_update
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process.Start(new ProcessStartInfo("cmd", "/c start ffmpeg_downloader.exe") { CreateNoWindow = true });
        }
    }
}
