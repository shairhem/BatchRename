using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BatchRename
{
    class Program
    {
        static void Main(string[] args)
        {
            rename();
            return;
        }

        static void rename()
        {
            string choice = "";
            string folder = "";
            int x = 1;
            string temp = "";
            string ext = "";
            int length = 0;
            string op = "";
            string filename = "";
            char[] end = { '"' };
            List<string> infofile = new List<string>();
            StreamWriter writer;

            Console.WriteLine("input directory: ");
            folder = Console.ReadLine();
            folder = folder.Trim(end);
            Console.WriteLine("input filename: ");
            filename = Console.ReadLine();
            DirectoryInfo d = new DirectoryInfo(@folder);
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                if (f.Name != "infofile.txt")
                {
                    ext = f.Name.Substring(f.Name.LastIndexOf('.'));
                    //length = f.Name.Length - 1;
                    //Console.WriteLine(f.Name + " / " + f.Name.LastIndexOf('.') + " / " + ext);
                    //Console.ReadKey();
                    temp = folder + "\\" + filename + x + ext;
                    Console.WriteLine("renaming file" + x);
                    //Console.WriteLine(f.Name.ToString());
                    File.Move(f.FullName, temp);
                    infofile.Add(temp);
                    x++;
                }
            }
            folder += "\\infofile.txt";
            writer = new StreamWriter(@folder);
            foreach (string i in infofile)
            {
                writer.WriteLine(i);
            }
            Console.Clear();
            Console.WriteLine("done! Continue? [Y/N]");
            choice = Console.ReadLine();
            choice = choice.ToUpper();
            if (choice == "Y")
            {
                rename();
            }
            else return;
            Console.ReadKey();
        }
    }
}
