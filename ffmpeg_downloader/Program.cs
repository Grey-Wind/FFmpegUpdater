using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Diagnostics;

namespace ffmpeg_downloader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始下载，请耐心等待"); // 输出文本并换行
            Download("https://hub.ggo.icu/BtbN/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl-shared.zip", "./", "ffmpeg.zip");
            Console.WriteLine("下载完成"); // 输出文本并换行
            // 创建 SpeechSynthesizer 对象
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                // 选择音频输出设备（可选）
                // synthesizer.SetOutputToDefaultAudioDevice();

                // 设置语速和音量（可选）
                // synthesizer.Rate = -2; // 语速范围：-10 到 10
                // synthesizer.Volume = 80; // 音量范围：0 到 100

                // 合成并播放文本
                SpeakText("下载完成", synthesizer);
            }
            // 等待用户按下任意键
            Console.WriteLine("按任意键继续安装...");
            Console.ReadKey();
            Process.Start(new ProcessStartInfo("cmd", "/c start ffmpeg_install.exe") { CreateNoWindow = true });
        }

        // 使用static修饰符，使方法成为静态方法
        public static void Download(string url, string folderPath, string fileName)
        {
            // 拼接文件的完整路径
            string filePath = Path.Combine(folderPath, fileName);

            // 创建 WebClient 对象
            using (var client = new WebClient())
            {
                // 创建文件流
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // 设置缓冲区大小
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    // 发送 HTTP 请求并获取响应流
                    using (var responseStream = client.OpenRead(url))
                    {
                        // 循环读取响应流的数据
                        while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            // 写入文件
                            fileStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
        }


        static void DownloadChunk(string url, FileStream fileStream, byte[] buffer, long startRange, long endRange)
        {
            // 创建 HTTP 请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // 设置下载范围
            request.AddRange(startRange, endRange);

            // 获取响应
            using (WebResponse response = request.GetResponse())
            {
                // 打开响应流
                using (Stream responseStream = response.GetResponseStream())
                {
                    int bytesRead;
                    while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        // 写入文件
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        // 使用static修饰符，使方法成为静态方法
        public static void Delete(string name)
        {
            if (Directory.Exists(name))
            {
                // 如果是文件夹，删除文件夹及其内容
                Directory.Delete(name, true);
            }
            else if (File.Exists(name))
            {
                // 如果是文件，删除文件
                File.Delete(name);
            }
        }

        static void SpeakText(string text, SpeechSynthesizer synthesizer)
        {
            try
            {
                synthesizer.Speak(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法朗读文本：" + ex.Message);
            }
        }
    }
}
