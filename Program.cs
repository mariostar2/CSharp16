using System;
using System.IO;                //input, output.
using System.Text;

namespace CSharp16
{
    class UserData
    {
        public string name;
        public int level;
        public int gold;

        public string GetSaveData()
        {
            /*
            [0]Name,테스터B
            [1]level,63
            [2]gold,354000           
            */
            string data = string.Empty;
            data += $"{name}\n";
            data += $"{level}\n";
            data += $"{gold}\n";
            return data;
        }

        public void SetSavaData(string data)
        {
            string[] datas = data.Split('\n');
            name = datas[0].Split(',')[1];
            level = int.Parse(datas[1]);
            gold = int.Parse(datas[2]);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            if (false)
            {

                string str = "ABC,DEF,GHI";
                string[] splits = str.Split(',');            //string.Split(char): 문자를 기준으로 자르자
                foreach (string split in splits)
                    Console.WriteLine(split);


                //파일 읽기 , 쓰기
                //txt파일
                // = 기본 텍스트 파일이다. 어떠한 형식이 없다
                string projectpath = Directory.GetCurrentDirectory();
                string rootPath = $"{projectpath}/Database";
                //Directory.CreateDirectory($"{projectpath}|ItemDB");

                //해당 경로에 디렉토리가 존재하는가>
                if (!Directory.Exists(rootPath))
                    Directory.CreateDirectory(rootPath);

                //ItemDB.txt가 있는가?
                string itemDBPath = $"{rootPath}/ItemDB.txt";
                if (!File.Exists(itemDBPath))
                {
                    Console.WriteLine("새로운 파일 생성!!");
                    File.Create(itemDBPath);
                }

                WriteLog("유저가 로그인을 했습니다");

                using (FileStream stream = new FileStream(itemDBPath, FileMode.OpenOrCreate))
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    while (reader.EndOfStream)
                        Console.WriteLine(reader.ReadLine());
                }

                //Console.WriteLine("파일 쓰기 완료");

                //File.Create($"{rootPath}/ItemDB.TXT");

            }

            UserData user = new UserData();
            user.SetSavaData(FileManager.Read("ItemDB.txt"));

            Console.WriteLine($"이름:{user.name}");
            Console.WriteLine($"레벨:{user.level}");
            Console.WriteLine($"골드:{user.gold}");
            user.level += 1;
            FileManager.Write("ItemDB.txt", user.GetSaveData());

            //데이터 포멧 형식
            //XML  : HTML의 tag형식을 닮은 데이터 포맷.
            //CSV  :Comma separated value
            //JSON : 

            ItemDB itemDB = new ItemDB();
        }
        static void WriteLog(string log)
        {

            string path = $"{Directory.GetCurrentDirectory()}/Database/Log:{DateTime.Now.ToShortDateString()}.txt";
            //using문 내에서 사용하면 코드블록이 끝나고 자동으로 연결 스트링이 해제된다.
            //해당 경로에 파일을 스트림하고 Writer를 이용해 쓰기모드로 진행한다.
            using (FileStream stream = new FileStream(path, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.GetEncoding("utf-8")))
            {
                writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:user가 로그아웃을 하였습니다.");

            }

            Console.WriteLine("파일 쓰기 완료");
        }
    }
}
