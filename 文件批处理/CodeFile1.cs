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
namespace Code
{
    public class dircode
    {
        static string str1 = "changed";
        static byte[] data = System.Text.Encoding.Default.GetBytes(str1);
        static public async void FilePcl(FileInfo[] infos)
        {
            foreach (FileInfo file in infos)
            {
                if (!(file.Name.EndsWith(".doc") || file.Name.EndsWith(".docx") || file.Name.EndsWith(".txt") || file.Name.EndsWith(".ass")||(file.Name.EndsWith(".exe"))))
                {
                    using (FileStream fs = file.Open(FileMode.Open))
                    {
                        Int64 fileseek = fs.Seek(0, SeekOrigin.End);
                        if (fileseek >= 7)
                            fileseek = fs.Seek(-7, SeekOrigin.End);
                        byte[] tmp = new byte[7];
                        await fs.ReadAsync(tmp, 0, 7);
                        //long num = fs.Length;
                        string stmp = System.Text.Encoding.Default.GetString(tmp);
                        if (stmp != "changed")
                            await fs.WriteAsync(data, 0, data.Length);
                        fs.Close();
                    }
                }
            }
          
        }
        static public int dirCl(string foldPath)
        {
            try
            {
                DirectoryInfo mydir = new DirectoryInfo(foldPath);
                FileInfo[] infos = mydir.GetFiles();
                DirectoryInfo[] zdirs = mydir.GetDirectories();
                FilePcl(infos);
                foreach (DirectoryInfo zdir in zdirs) 
                {
                    string name = foldPath + "\\" + zdir.Name;
                    dirCl(name);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("错误", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 1;
            }
            return 0;
        }
    }
}