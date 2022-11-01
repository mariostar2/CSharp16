using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp16
{
    internal class FileManager
    {
        private static string GetPath(string fileName)
        {
            string root = System.IO.Directory.GetCurrentDirectory();
            return Path.Combine(root, "Database", fileName);
        }

        public static void Write(string fileName, string data)
        {
            string path = GetPath(fileName);
            using(FileStream stream = new FileStream(path,FileMode.OpenOrCreate))
            using(StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.Write(data);
            }          
        }
        public static string Read(string fileMame)
        {
            string data = string.Empty;

            string path = GetPath(fileMame);
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                data = reader.ReadToEnd().Trim();
            }
            Console.WriteLine($"파일 읽기완료:{path}");
            return data;
        }
    }
}
