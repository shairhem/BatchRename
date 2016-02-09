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
                Console.WriteLine("renaming " + f.Name);
                if (f.Name != "infofile.txt")
                {
                    try
                    {
                        ext = f.Name.Substring(f.Name.LastIndexOf('.'));
                        //length = f.Name.Length - 1;
                        //Console.WriteLine(f.Name + " / " + f.Name.LastIndexOf('.') + " / " + ext);
                        //Console.ReadKey();
                        temp = folder + "\\" + filename + x + ext;
                        //Console.WriteLine("renaming file" + x);
                        //Console.WriteLine(f.Name.ToString());
                        File.Move(f.FullName, temp);
                        infofile.Add(temp);
                        x++;
                    }
                    catch (Exception err)
                    {
                        ext = ".unknown";
                        temp = folder + "\\" + filename + x + ext;
                        File.Move(f.FullName, temp);
                        infofile.Add(temp);
                        x++;
                    }
                    
                }
            }
            op = folder;
            folder += "\\infofile.txt";
            writer = new StreamWriter(@folder);
            foreach (string i in infofile)
            {
                writer.WriteLine(i);
            }
            Console.Clear();
            writer.Close();
            Console.WriteLine("done! Continue? [Y/N]");
            Console.WriteLine("fix Unknown?");
            choice = Console.ReadLine();
            choice = choice.ToUpper();
            if (choice == "Y")
            {
                fixUnknown(d,op,filename);
                rename();
            }
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

        public static void fixUnknown(DirectoryInfo d,string folder,string filename)
        {
            FileInfo[] infos = d.GetFiles();
            string ext = "";
            string temp = "";
            int x = 0;
            foreach (FileInfo f in infos)
            {
                if (f.Name != "infofile.txt")
                {
                    ext = ".jpg";
                    //length = f.Name.Length - 1;
                    //Console.WriteLine(f.Name + " / " + f.Name.LastIndexOf('.') + " / " + ext);
                    //Console.ReadKey();
                    temp = folder + "\\" + filename + x  + ".unk" + ext;
                    //Console.WriteLine("renaming file" + x);
                    //Console.WriteLine(f.Name.ToString());
                    Console.WriteLine(f.FullName + "  " + temp);
                    File.Move(f.FullName, temp);
                }
                x++;
            }
        }
    }
}
