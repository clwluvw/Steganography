using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public enum State
        {
            Hiding,
            Filling_Width_Zeros
        };

        public static string extractText(Bitmap bmp)
        {
            int colorUnitIndex = 0;
            int charValue = 0;
            string extractedText = String.Empty;
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    for (int n = 0; n < 3; n++)
                    {
                        switch (colorUnitIndex % 3)
                        {
                            case 0:
                                charValue = charValue * 2 + pixel.R % 2;
                                break;
                            case 1:
                                charValue = charValue * 2 + pixel.G % 2;
                                break;
                            case 2:
                                charValue = charValue * 2 + pixel.B % 2;
                                break;
                        }
                        colorUnitIndex++;
                        if (colorUnitIndex % 8 == 0)
                        {
                            charValue = reverseBits(charValue);
                            if (charValue == 0)
                            {
                                return extractedText;
                            }
                            char c = (char)charValue;
                            extractedText += c.ToString();
                        }
                    }
                }
            }
            return extractedText;
        }

        public static int reverseBits(int n)
        {
            int result = 0;
            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;
                n /= 2;
            }
            return result;
        }

        string chosen_file = "", chosen_file2 = "", chosen_file3 = "", chosen_file4 = "", count = "";
        int iSuccess = 1, iSuccess2 = 1, iSuccess3 = 1, iSuccess4 = 1;

        public void decoding()
        {
            SoundPlayer decoding = new SoundPlayer("decoding.wav");
            Process myprocess = new Process();
            ProcessStartInfo startinfo = new ProcessStartInfo();
            startinfo.FileName = "coding.exe";
            myprocess.StartInfo = startinfo;
            myprocess.Start();
            decoding.Play();
            myprocess.WaitForExit();
            decoding.Stop();
            MessageBox.Show("Decoding finished.","message",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            MessageBox.Show("Please insert the first image.","message",MessageBoxButtons.OK,MessageBoxIcon.Information);
            openFileDialog1.Title = "Insert an Image";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "BMP Images|*.BMP";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                chosen_file = openFileDialog1.FileName;
                Bitmap countimg = new Bitmap(chosen_file);
                count = extractText(countimg);
                if (count.Contains("1pifco"))
                {
                    chosen_file = openFileDialog1.FileName;
                    iSuccess = 0;
                    pictureBox1.Visible = true;
                    pictureBox1.Image = Image.FromFile(chosen_file);
                }
                else
                {
                    if (count.Contains("2pifco"))
                    {
                        iSuccess2 = 0;
                        MessageBox.Show("Please insert the second image.", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        openFileDialog1.Title = "Insert an Image";
                        openFileDialog1.FileName = "";
                        openFileDialog1.Filter = "BMP Images|*.BMP";
                        if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                        {
                            MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            chosen_file2 = openFileDialog1.FileName;
                            Bitmap newimg21 = new Bitmap(chosen_file);
                            Bitmap newimg22=new Bitmap(chosen_file2);
                            pictureBox2.Size = new System.Drawing.Size(newimg21.Width, newimg21.Height);
                            pictureBox3.Size = new System.Drawing.Size(newimg22.Width, newimg22.Height);
                            pictureBox3.Location = new Point(newimg21.Width + 11, 11);
                            pictureBox2.Visible = true;
                            pictureBox3.Visible = true;
                            pictureBox2.Image = Image.FromFile(chosen_file);
                            pictureBox3.Image = Image.FromFile(chosen_file2);
                        }
                    }
                    else
                    {
                        if (count.Contains("3pifco"))
                        {
                            iSuccess3 = 0;
                            MessageBox.Show("Please insert the second image.", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            openFileDialog1.Title = "Insert an Image";
                            openFileDialog1.FileName = "";
                            openFileDialog1.Filter = "BMP Images|*.BMP";
                            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                            {
                                MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                chosen_file2 = openFileDialog1.FileName;
                                Bitmap newimg31 = new Bitmap(chosen_file);
                                Bitmap newimg32 = new Bitmap(chosen_file2);
                                pictureBox4.Size = new System.Drawing.Size(newimg31.Width, newimg31.Height);
                                pictureBox5.Size = new System.Drawing.Size(newimg32.Width, newimg32.Height);
                                pictureBox5.Location = new Point(newimg31.Width + 12, 12);
                                MessageBox.Show("Please insert the third image.", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                openFileDialog1.Title = "Insert an Image";
                                openFileDialog1.FileName = "";
                                openFileDialog1.Filter = "BMP Images|*.BMP";
                                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                                {
                                    MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                                else
                                {
                                    chosen_file3 = openFileDialog1.FileName;
                                    Bitmap newimg33 = new Bitmap(chosen_file3);
                                    pictureBox6.Size = new System.Drawing.Size(newimg33.Width, newimg33.Height);
                                    pictureBox6.Location = new Point(2 * newimg32.Width + 12, 12);
                                    pictureBox4.Visible = true;
                                    pictureBox5.Visible = true;
                                    pictureBox6.Visible = true;
                                    pictureBox4.Image = Image.FromFile(chosen_file);
                                    pictureBox5.Image = Image.FromFile(chosen_file2);
                                    pictureBox6.Image = Image.FromFile(chosen_file3);
                                }
                            }
                        }
                        else
                        {
                            if (count.Contains("4pifco"))
                            {
                                iSuccess4 = 0;
                                MessageBox.Show("Please insert the second image.", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                openFileDialog1.Title = "Insert an Image";
                                openFileDialog1.FileName = "";
                                openFileDialog1.Filter = "BMP Images|*.BMP";
                                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                                {
                                    MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                                else
                                {
                                    chosen_file2 = openFileDialog1.FileName;
                                    Bitmap newimg41 = new Bitmap(chosen_file);
                                    Bitmap newimg42 = new Bitmap(chosen_file2);
                                    pictureBox7.Size = new System.Drawing.Size(newimg41.Width, newimg41.Height);
                                    pictureBox8.Size = new System.Drawing.Size(newimg42.Width, newimg42.Height);
                                    pictureBox8.Location = new Point(newimg41.Width + 12, 12);
                                    MessageBox.Show("Please insert the third image.", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    openFileDialog1.Title = "Insert an Image";
                                    openFileDialog1.FileName = "";
                                    openFileDialog1.Filter = "BMP Images|*.BMP";
                                    if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                                    {
                                        MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    }
                                    else
                                    {
                                        chosen_file3 = openFileDialog1.FileName;
                                        Bitmap newimg43=new Bitmap(chosen_file3);
                                        pictureBox9.Size = new System.Drawing.Size(newimg43.Width, newimg43.Height);
                                        pictureBox9.Location = new Point(12, newimg43.Height + 12);
                                        MessageBox.Show("Please insert the fourth image.", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        openFileDialog1.Title = "Insert an Image";
                                        openFileDialog1.FileName = "";
                                        openFileDialog1.Filter = "BMP Images|*.BMP";
                                        if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                                        {
                                            MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                        }
                                        else
                                        {
                                            chosen_file4 = openFileDialog1.FileName;
                                            Bitmap newimg44 = new Bitmap(chosen_file4);
                                            pictureBox10.Size = new System.Drawing.Size(newimg44.Width, newimg44.Height);
                                            pictureBox10.Location = new Point(newimg44.Width + 12, newimg44.Height + 12);
                                            pictureBox7.Visible = true;
                                            pictureBox8.Visible = true;
                                            pictureBox9.Visible = true;
                                            pictureBox10.Visible = true;
                                            pictureBox7.Image = newimg41;
                                            pictureBox8.Image = newimg42;
                                            pictureBox9.Image = newimg43;
                                            pictureBox10.Image = newimg44;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please insert a valid Image.","message",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (iSuccess==0)
            {
                string text;
                if (chosen_file == "" || chosen_file == " ")
                {
                    MessageBox.Show("You should Insert the second picture.", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Bitmap myimg = new Bitmap(chosen_file);
                    text = extractText(myimg);
                    text = text.Remove(text.Length - 6);
                    decoding();
                    textBox1.Text = text;
                }
            }
            else if (iSuccess2==0)
            {
                string text1;
                Bitmap myimg21 = new Bitmap(chosen_file);
                text1 = extractText(myimg21);
                text1 = text1.Remove(text1.Length - 6);
                if (chosen_file2==""||chosen_file2==" ")
                {
                    MessageBox.Show("You should Insert the second picture.","message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    Bitmap myimg22 = new Bitmap(chosen_file2);
                    decoding();
                    textBox1.Text = text1 + extractText(myimg22);
                }
            }
            else if (iSuccess3==0)
            {
                string text2;
                Bitmap myimg31 = new Bitmap(chosen_file);
                text2 = extractText(myimg31);
                text2 = text2.Remove(text2.Length - 6);
                if (chosen_file2 == "" || chosen_file2 == " ")
                {
                    MessageBox.Show("You should Insert the second picture.", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Bitmap myimg32 = new Bitmap(chosen_file2);
                    if (chosen_file3 == "" || chosen_file3 == " ")
                    {
                        MessageBox.Show("You should Insert the third picture.", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Bitmap myimg33 = new Bitmap(chosen_file3);
                        decoding();
                        textBox1.Text = text2 + extractText(myimg32) + extractText(myimg33);
                    }
                }
            }
            else if (iSuccess4==0)
            {
                string text3;
                Bitmap myimg41 = new Bitmap(chosen_file);
                text3 = extractText(myimg41);
                text3 = text3.Remove(text3.Length - 6);
                if (chosen_file2 == "" || chosen_file2 == " ")
                {
                    MessageBox.Show("You should Insert the second picture.", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Bitmap myimg42 = new Bitmap(chosen_file2);
                    if (chosen_file3 == "" || chosen_file3 == " ")
                    {
                        MessageBox.Show("You should Insert the third picture.", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Bitmap myimg43 = new Bitmap(chosen_file3);
                        if (chosen_file4 == "" || chosen_file4 == " ")
                        {
                            MessageBox.Show("You should Insert the fourth picture.", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            Bitmap myimg44 = new Bitmap(chosen_file4);
                            decoding();
                            textBox1.Text = text3 + extractText(myimg42) + extractText(myimg43) + extractText(myimg44);
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to exit?", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
