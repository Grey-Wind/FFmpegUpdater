using System.Diagnostics;
using System;
using System.IO;
using System.IO.Compression;

namespace ffmpeg_install
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //删除旧版本
            Delete("ffmpeg-master-latest-win64-gpl-shared");

            //等待0.5秒缓冲，防止产生bug
            System.Threading.Thread.Sleep(500);

            string zipFilePath = "ffmpeg.zip";  // 要解压缩的 ZIP 文件路径

            // 获取当前目录
            string currentDirectory = Directory.GetCurrentDirectory();

            // 使用 ZipFile 类的 ExtractToDirectory 方法解压缩文件到当前目录
            ZipFile.ExtractToDirectory(zipFilePath, currentDirectory);

            
            //下面用来启动path添加

            string relativeExecutablePath = @".\ffmpeg_path.exe";

            // 获取相对目录的绝对路径
            string absoluteDirectoryPath = Path.GetFullPath(".");
            string absoluteExecutablePath = Path.Combine(absoluteDirectoryPath, relativeExecutablePath);

            ProcessStartInfo startInfo = new ProcessStartInfo(absoluteExecutablePath);
            startInfo.Verb = "runas"; // 以管理员权限运行

            try
            {
                Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine("无法以管理员权限启动程序： " + ex.Message);
                Console.WriteLine("请联系软件制作者");
                Console.WriteLine("按任意键退出...");
                Console.ReadKey();
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
    }
}
