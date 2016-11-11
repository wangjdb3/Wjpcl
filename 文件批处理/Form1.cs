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
        string foldPath;
        string[] filesPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "请选择文件路径";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                foldPath = folder.SelectedPath;
                //MessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = foldPath;
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
            if (textBox1.Text != "")
            {
                int a = dircode.dirCl(foldPath);
                if (a == 0) 
                { 
                    MessageBox.Show("完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
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
