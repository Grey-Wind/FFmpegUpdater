using System;

class Program
{
    static void Main(string[] args)
    {
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

                // 进行修改，这里将文本中的 "Hello" 替换为 "Hi"
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
                Console.WriteLine("按任意键结束...");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("path文件不存在，请联系软件制作者");
            Console.WriteLine("你6，你乱删文件，存在肯定是有意义的");
            Console.WriteLine("按任意键结束...");
            Console.ReadKey();
        }
    }
}
