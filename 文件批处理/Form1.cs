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
using Code;

namespace 文件批处理
{
    public partial class Form1 : Form
    {
        //string foldPath;
        string[] foldPaths;
        string[] filesPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            List<string> dirs = new List<string>();
            List<string> files = new List<string>();
            String[] fileNames = (String[])e.Data.GetData(DataFormats.FileDrop);
            for (int i = 0; i < fileNames.Length; i++)
            {
                if (Directory.Exists(fileNames[i]))
                {
                    textBox1.Text += fileNames[i] + "; ";
                    dirs.Add(fileNames[i]);
                }
                else if (File.Exists(fileNames[i]))
                {
                    textBox2.Text += fileNames[i] + "; ";
                    files.Add(fileNames[i]);
                }
            }
            foldPaths = dirs.ToArray();
            filesPath = files.ToArray();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "请选择文件路径";
            foldPaths = new string[1];
            if (folder.ShowDialog() == DialogResult.OK)
            {
                foldPaths[0] = folder.SelectedPath;
                //MessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = foldPaths[0];
                textBox2.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog files = new OpenFileDialog();
            files.Multiselect = true;
            if (files.ShowDialog() == DialogResult.OK)
            {
                filesPath = files.FileNames;
                foreach (string str in filesPath)
                {
                    textBox2.Text += (str+";");
                }
                textBox1.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*            byte data;
                        if (textBox3.Text == "")
                            data = 0;
                        else
                        {
                            data = (byte)Convert.ToInt32(textBox3.Text, 16);
                        }*/
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                int a = 1;
                foreach (string foldPath in foldPaths)
                    a = dircode.dirCl(foldPath);
                int length = filesPath.Length;
                FileInfo[] filesP = new FileInfo[length];
                for (int i = 0; i < length; i++)
                {
                    filesP[i] = new FileInfo(filesPath[i]);
                }
                dircode.FilePcl(filesP);
                textBox1.Text = textBox2.Text = "";
                MessageBox.Show("完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (textBox1.Text != "")
            {
                int a = 1;
                foreach (string foldPath in foldPaths)
                    a = dircode.dirCl(foldPath);
                if (a == 0) 
                { 
                    MessageBox.Show("完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
                textBox1.Text = textBox2.Text = "";
            }
            else if (textBox2.Text != "") 
            {
                int length = filesPath.Length;
                FileInfo[] filesP = new FileInfo[length];
                for (int i = 0; i < length; i++)
                {
                    filesP[i] = new FileInfo(filesPath[i]);
                }
                dircode.FilePcl(filesP);
                textBox1.Text = textBox2.Text = "";
                MessageBox.Show("完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else
            {
                MessageBox.Show("情选择文件夹或文件", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox1.Text = textBox2.Text = "";
        }
    }
}
