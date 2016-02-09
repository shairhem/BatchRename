using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BatchRename
{
    public partial class startForm : Form
    {
        public startForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text) && !String.IsNullOrWhiteSpace(textBox2.Text))
                rename();
            else
                MessageBox.Show("Must have folder path and filename");
        }
        
        private void rename()
        {
            string filename = textBox2.Text;
            string folder = textBox1.Text;
            List<string> infofile = new List<string>();
            DirectoryInfo d = new DirectoryInfo(@folder);
            FileInfo[] infos = d.GetFiles();
            int fileCount = infos.Count();
            progressBar1.Maximum = fileCount;
            string ext = "";
            string temp;
            int x = 0;
            foreach (FileInfo f in infos)
            {
            //foreach start
                temp = "";
                label3.Text = "renaming " + f.Name;
                label3.Update();
                if(f.Name != "infofile.txt")
                {
                //if start
                    try
                    {
                        ext = f.Name.Substring(f.Name.LastIndexOf('.'));
                        temp = folder + "\\" + filename + x + ext;
                        File.Move(f.FullName, temp);                       
                    }
                    catch (Exception err)
                    {
                        ext = ".jpg";
                        temp = folder + "\\" + filename + x + ext;
                        File.Move(f.FullName, temp);
                    }
                    infofile.Add(temp);
                    x++;

                }
                //end of if
                progressBar1.Increment(1);
            }
            //end of foreach
            writeToInfofile(filename,folder,infofile);
            MessageBox.Show("done");
        }

        private void writeToInfofile(string filename, string folder, List<string> infofile)
        {
            folder += "\\infofile.txt";
            StreamWriter writer = new StreamWriter(@folder);
            foreach (string i in infofile)
            {
                writer.WriteLine(i);
            }
            writer.Close();
        }
    }
}

