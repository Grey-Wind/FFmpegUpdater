using System.Diagnostics;
using System.IO.Compression;

Console.WriteLine("开始下载，请耐心等待"); // 输出文本并换行
Download("https://hub.ggo.icu/BtbN/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl-shared.zip", "./", "ffmpeg.zip");
Console.WriteLine("下载完成"); // 输出文本并换行

// 等待0.5秒
Thread.Sleep(500);

//删除旧版本
Delete("ffmpeg-master-latest-win64-gpl-shared");

//等待0.25秒缓冲，防止产生bug
System.Threading.Thread.Sleep(250);

string zipFilePath = "ffmpeg.zip";  // 要解压缩的 ZIP 文件路径

// 获取当前目录
string currentDirectory = Directory.GetCurrentDirectory();

// 使用 ZipFile 类的 ExtractToDirectory 方法解压缩文件到当前目录
ZipFile.ExtractToDirectory(zipFilePath, currentDirectory);

// 下面开始添加Path
string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
string ffmpegBinPath = Path.Combine(appDirectory, "ffmpeg", "bin");

Console.WriteLine("请选择要添加路径的类型：");
Console.WriteLine("1. 用户路径");
Console.WriteLine("2. 系统路径");
Console.Write("请输入选项（1 或 2）：");

string option = Console.ReadLine();
if (option == "1")
{
    if (!IsDirectoryInPath(ffmpegBinPath, Environment.GetEnvironmentVariable("PATH")))
    {
        AddDirectoryToPath(ffmpegBinPath, EnvironmentVariableTarget.User);
        Console.WriteLine("目录已成功添加到用户路径环境变量中。");
    }
    else
    {
    Console.WriteLine("目录已存在于用户路径环境变量中。");
    }
}
else if (option == "2")
{
    if (!IsDirectoryInPath(ffmpegBinPath, Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine)))
    {
        AddDirectoryToPath(ffmpegBinPath, EnvironmentVariableTarget.Machine);
        Console.WriteLine("目录已成功添加到系统路径环境变量中。");
    }
    else
    {
        Console.WriteLine("目录已存在于系统路径环境变量中。");
    }
}
else
{
    Console.WriteLine("无效的选项！");
}

    Console.ReadLine();

static bool IsDirectoryInPath(string directory, string pathVariable)
{
    string[] paths = pathVariable.Split(';');

    foreach (string path in paths)
    {
        if (string.Equals(path.Trim(), directory, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
    }

    return false;
}

static void AddDirectoryToPath(string directory, EnvironmentVariableTarget target)
{
    string pathVariable = Environment.GetEnvironmentVariable("PATH", target);
    pathVariable = $"{directory};{pathVariable}";

    // 启动一个新进程来设置环境变量
    using (Process process = new Process())
    {
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = $"/C setx PATH \"{pathVariable}\" /{GetTargetString(target)}";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();
        process.WaitForExit();
    }
}

static string GetTargetString(EnvironmentVariableTarget target)
{
    return target == EnvironmentVariableTarget.Machine ? "M" : "U";
}














void Download(string url, string folderPath, string fileName)
{
    string filePath = Path.Combine(folderPath, fileName);

    using (var client = new HttpClient())
    {
        using (var response = client.GetAsync(url).Result)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                var stream = response.Content.ReadAsStreamAsync().Result;
                stream.CopyTo(fileStream);
            }
        }
    }
}

void Delete(string name)
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
