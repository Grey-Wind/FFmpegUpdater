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
