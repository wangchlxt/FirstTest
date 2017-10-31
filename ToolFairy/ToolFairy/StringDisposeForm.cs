using CSharpUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ToolFairy
{
    public partial class StringDisposeForm : Form
    {
        public StringDisposeForm()
        {
            InitializeComponent();
        }

        private void buttonMD5_Click(object sender, EventArgs e)
        {
            richTextBoxOut.Text = StringCoding.EncodeMD5(richTextBoxIn.Text);
        }

        private void buttonUrlEncoding_Click(object sender, EventArgs e)
        {
            string str = HttpUtility.UrlEncode(richTextBoxIn.Text);
            StringBuilder sb = new StringBuilder();

            string[] arr = str.Split('%');
            int i = 0;
            foreach (string s in arr)
            {
                if (i > 0)
                {
                    string s1 = s.Substring(0, 2);
                    string s2 = s.Substring(2);

                    sb.Append("%");
                    sb.Append(s1.ToUpper());
                    sb.Append(s2);
                }
                else
                {
                    sb.Append(s);
                }

                i++;
            }

            richTextBoxOut.Text = sb.ToString();
        }

        private void buttonUrlDecoding_Click(object sender, EventArgs e)
        {
            richTextBoxIn.Text = HttpUtility.UrlDecode(richTextBoxOut.Text);
        }

        private void buttonUpper_Click(object sender, EventArgs e)
        {
            richTextBoxOut.Text = richTextBoxIn.Text.ToUpper();
        }

        private void buttonLower_Click(object sender, EventArgs e)
        {
            richTextBoxOut.Text = richTextBoxIn.Text.ToLower();
        }

        private void richTextBoxIn_TextChanged(object sender, EventArgs e)
        {
            labelInTip.Text = richTextBoxIn.Text.Length.ToString();
        }

        private void richTextBoxOut_TextChanged(object sender, EventArgs e)
        {
            labelOutTip.Text = richTextBoxOut.Text.Length.ToString();
        }

        private void buttonDeleteSpecialCharacter_Click(object sender, EventArgs e)
        {
            richTextBoxOut.Text = Regex.Replace(richTextBoxIn.Text, @"\W", "_");
        }

        private void buttonHmacsha1_Click(object sender, EventArgs e)
        {
            string key = textBoxHmacsha1Key.Text;
            if(string.IsNullOrEmpty(key))
            {
                key = DateTime.Now.ToString();
            }

            richTextBoxOut.Text = StringCoding.EncodeHmacsha1(key, richTextBoxIn.Text);
        }
        
        private void buttonBase64Encode_Click(object sender, EventArgs e)
        {
            richTextBoxOut.Text = StringCoding.EncodeBase64(ConvertUtil.ConvertEncodingString(comboBoxBase64.Text), richTextBoxIn.Text);
        }

        private void buttonBase64Decode_Click(object sender, EventArgs e)
        {
            richTextBoxIn.Text = StringCoding.DecodeBase64(ConvertUtil.ConvertEncodingString(comboBoxBase64.Text), richTextBoxOut.Text);
        }

        private void buttonSHA1_Click(object sender, EventArgs e)
        {
            richTextBoxOut.Text = StringCoding.EncodeSHA1(richTextBoxIn.Text, ConvertUtil.ConvertEncodingString(comboBoxSHA1.Text));
        }
    }
}
