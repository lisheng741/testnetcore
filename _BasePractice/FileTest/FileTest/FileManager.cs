using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTest;

internal class FileHelper
{
    public List<string> GetDirectoryFileNames(string path)
    {
        List<string> files = new List<string>();

        var root = new DirectoryInfo(path);
        if (root.Name.StartsWith("."))
        {
            return files;
        }
        foreach(FileInfo fileInfo in root.GetFiles())
        {
            files.Add(fileInfo.FullName);
        }
        foreach(DirectoryInfo directoryInfo in root.GetDirectories())
        {
            files.AddRange(GetDirectoryFileNames(directoryInfo.FullName));
        }

        return files;
    }

    public static void WriteText(string content)
    {
        string fileName = "log.txt";
        FileStream fs;
        if (File.Exists(fileName))
        {
            fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
        }
        else
        {
            fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        }
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(content);
        sw.Flush();
        sw.Close();
    }
}
