using System.IO.Compression;

Console.WriteLine("开始下载，请耐心等待"); // 输出文本并换行
Download("https://hub.ggo.icu/BtbN/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl-shared.zip", "./", "ffmpeg.zip");
Console.WriteLine("下载完成"); // 输出文本并换行

// 等待0.5秒
Thread.Sleep(500);

Console.WriteLine("按任意键继续安装...");
Console.ReadKey();

//删除旧版本
Delete("ffmpeg-master-latest-win64-gpl-shared");

//等待0.5秒缓冲，防止产生bug
System.Threading.Thread.Sleep(500);

string zipFilePath = "ffmpeg.zip";  // 要解压缩的 ZIP 文件路径

// 获取当前目录
string currentDirectory = Directory.GetCurrentDirectory();

// 使用 ZipFile 类的 ExtractToDirectory 方法解压缩文件到当前目录
ZipFile.ExtractToDirectory(zipFilePath, currentDirectory);

string filePath = "./path";

if (System.IO.File.Exists(filePath))
{
    string fileContent = System.IO.File.ReadAllText(filePath);

    string path = Environment.GetEnvironmentVariable("PATH");
    if (fileContent.Contains("true"))
    {
        Console.WriteLine("你已经添加过了");
        Console.WriteLine("按任意键结束...");
        Console.ReadKey();
    }
    else if (fileContent.Contains("false"))
    {
        string directory = @".\ffmpeg-master-latest-win64-gpl-shared\bin";
        string fullPath = System.IO.Path.GetFullPath(directory);

        if (!path.Contains(fullPath))
        {
            path += ";" + fullPath;
            Environment.SetEnvironmentVariable("PATH", path);
        }

        Console.WriteLine("已将目录添加到 PATH 环境变量。");

        // 读取文件内容
        string fileText = System.IO.File.ReadAllText(filePath);

        // 进行修改，这里将文本中的 "false" 替换为 "true"
        fileText = fileText.Replace("false", "true");

        // 将修改后的内容写回文件
        System.IO.File.WriteAllText(filePath, fileText);

        Console.WriteLine("按任意键结束...");
        Console.ReadKey();
    }
    else
    {
        // 文件内容不符合预期
        Console.WriteLine("文件内容不是true或false");
        Console.WriteLine("你在乱改什么玩意？");
        Console.WriteLine("不会改的东西以后不要动");
        Console.WriteLine("乖乖找开发者去吧");
        Console.WriteLine("再见，按任意键关了...");
        Console.ReadKey();
    }
}
else
{
    Console.WriteLine("path文件不存在，请联系软件制作者");
    Console.WriteLine("你6，你乱删文件，存在肯定是有意义的");
    Console.WriteLine("再见，自己找回文件去吧，按任意键关了去回收站...");
    Console.ReadKey();
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
