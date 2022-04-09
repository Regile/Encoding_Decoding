using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encoder_Decoder
{
    public partial class Form1 : Form
    {
        static string sKey;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sKey = textBox1.Text;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;

                saveFileDialog1.Filter = "des filter |*.des";

                if(saveFileDialog1.ShowDialog()== DialogResult.OK)
                {
                    string destination = saveFileDialog1.FileName;
                    Encrypt(source, destination, sKey);
                }
            }
        }

        private void  Encrypt(string source, string destination, string sKey) 
        {
            FileStream fsInput = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(destination, FileMode.Create, FileAccess.Write);

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

                ICryptoTransform desencrypt = DES.CreateEncryptor();

                CryptoStream cryptoStream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

                byte[] byteArrayInput = new byte[fsInput.Length - 0];

                fsInput.Read(byteArrayInput, 0, byteArrayInput.Length);
                cryptoStream.Write(byteArrayInput, 0, byteArrayInput.Length);
                cryptoStream.Close();
            }

            catch
            {
                MessageBox.Show("Ошибка");
                return;
            }

            fsInput.Close();
            fsEncrypted.Close(); 
        }

        private void Decrypt(string source, string destination, string sKey)
        {
            FileStream fsInput = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(destination, FileMode.Create, FileAccess.Write);

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

                ICryptoTransform desencrypt = DES.CreateDecryptor();

                CryptoStream cryptoStream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

                byte[] byteArrayInput = new byte[fsInput.Length - 0];

                fsInput.Read(byteArrayInput, 0, byteArrayInput.Length);
                cryptoStream.Write(byteArrayInput, 0, byteArrayInput.Length);
                cryptoStream.Close();
            }

            catch
            {
                MessageBox.Show("Ошибка");
                return;
            }

            fsInput.Close();
            fsEncrypted.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            sKey = textBox1.Text;
            openFileDialog1.Filter = "des files |*.des";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;
                saveFileDialog1.Filter = "txt files |*.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string destination = saveFileDialog1.FileName;
                    Decrypt(source, destination, sKey);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
