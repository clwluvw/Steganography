using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public enum State
        {
            Hiding,
            Filling_Width_Zeros
        };

        public static Bitmap embedText(string text, Bitmap bmp)
        {
            State state = State.Hiding;
            int charIndex = 0;
            int charValue = 0;
            long pixelElementIndex = 0;
            int zeros = 0;
            int R = 0, G = 0, B = 0;
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    R = pixel.R - pixel.R % 2;
                    G = pixel.G - pixel.G % 2;
                    B = pixel.B - pixel.B % 2;
                    for (int n = 0; n < 3; n++)
                    {
                        if (pixelElementIndex % 8 == 0)
                        {
                            if (state == State.Filling_Width_Zeros && zeros == 8)
                            {
                                if ((pixelElementIndex - 1) % 3 < 2)
                                {
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }
                                return bmp;
                            }
                            if (charIndex >= text.Length)
                            {
                                state = State.Filling_Width_Zeros;
                            }
                            else
                            {
                                charValue = text[charIndex++];
                            }
                        }
                        switch (pixelElementIndex % 3)
                        {
                            case 0:
                                if (state == State.Hiding)
                                {
                                    R += charValue % 2;
                                    charValue /= 2;
                                }
                                break;
                            case 1:
                                if (state == State.Hiding)
                                {
                                    G += charValue % 2;
                                    charValue /= 2;
                                }
                                break;
                            case 2:
                                if (state == State.Hiding)
                                {
                                    B += charValue % 2;
                                    charValue /= 2;
                                }
                                bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                break;
                        }
                        pixelElementIndex++;
                        if (state == State.Filling_Width_Zeros)
                        {
                            zeros++;
                        }
                    }
                }
            }
            return bmp;
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

        public void coding()
        {
            SoundPlayer coding = new SoundPlayer("coding.wav");
            Process myprocess = new Process();
            ProcessStartInfo startinfo = new ProcessStartInfo();
            startinfo.FileName = "coding.exe";
            myprocess.StartInfo = startinfo;
            myprocess.Start();
            coding.Play();
            myprocess.WaitForExit();
            coding.Stop();
        }

        string chosen_file = " ";
        string mytext = " ";
        Bitmap img = new Bitmap(550, 294);

        public string[] mysplit(int a,int b,int j,string[] newstr,string[] split)
        {
            for (int i = 0; i < 4; i++)
            {
                while (j < b)
                {
                    newstr[i] += split[j] + " ";
                    j++;
                }
                b += a / 4;
                if (b > a)
                {
                    break;
                }
            }
            return newstr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                Bitmap myimg = new Bitmap(chosen_file);
                using (Graphics g=Graphics.FromImage(img))
                {
                    g.DrawImage(myimg, 0, 0, 550, 294);
                }
                pictureBox1.Image = img;
                int maxtext;
                maxtext = (img.Width * img.Height) / 8;
                textBox1.MaxLength = maxtext;
                label2.Text = maxtext.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mytext = textBox1.Text;
            if(chosen_file==" "||chosen_file=="")
            {
                MessageBox.Show("You should insert an image first","message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else if (mytext==" "||mytext=="")
            {
                MessageBox.Show("You should type a text first","message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                int a, j = 0, b, c;
                mytext = textBox1.Text;
                string[] split = mytext.Split(new char[] { ' ', ',', '.', ':', '\t' });
                a = split.GetLength(0);
                if (a==1)
                {
                    textBox1.Text = textBox1.Text;
                    mytext = textBox1.Text + "1pifco";
                    Bitmap image1 = new Bitmap(chosen_file);
                    image1 = embedText(mytext, img);
                    coding();
                    var result = MessageBox.Show("Coding finished. Do you want to Save you coding image?", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "BMP Images (*.bmp)|*.bmp";
                        ImageFormat format = ImageFormat.Bmp;
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            image1.Save(sfd.FileName, format);
                            MessageBox.Show("Your coding image saved successfully.", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else if (a==2)
                {
                    b = a / 2;
                    mytext = textBox1.Text;
                    string[] split1 = mytext.Split(new char[] { ' ', ',', '.', ':', '\t' });
                    string[] newstr1 = new string[2];
                    string t21,t22;
                    for (int i = 0; i < 2; i++)
                    {
                        while (j < b)
                        {
                            newstr1[i] += split1[j] + " ";
                            j++;
                        }
                        b += a / 2;
                        if (b > a)
                        {
                            break;
                        }
                    }
                    t21=newstr1[0]+"2pifco";
                    t22=newstr1[1];
                    Rectangle rect = new Rectangle(0, 0, img.Width / 2, img.Height);
                    Bitmap img2_1 = img.Clone(rect, img.PixelFormat);
                    Bitmap img21 = new Bitmap(chosen_file);
                    img21 = embedText(t21, img2_1);
                    rect = new Rectangle(img.Width / 2, 0, img.Width / 2, img.Height);
                    Bitmap img2_2 = img.Clone(rect, img.PixelFormat);
                    Bitmap img22 = new Bitmap(chosen_file);
                    img22 = embedText(t22, img2_2);
                    coding();
                    var result = MessageBox.Show("Coding finished. Do you want to Save you coding image?", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            string folderName = "Coding Pictures";
                            string path = System.IO.Path.Combine(fbd.SelectedPath, folderName);
                            System.IO.Directory.CreateDirectory(path);
                            fbd.RootFolder = Environment.SpecialFolder.Desktop;
                            img21.Save(System.IO.Path.Combine(path, 1 + ".bmp"));
                            img22.Save(System.IO.Path.Combine(path, 2 + ".bmp"));
                            MessageBox.Show("Your coding image saved successfully.", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else if (a==3)
                {
                    b = a / 3;
                    mytext = textBox1.Text;
                    string[] split2 = mytext.Split(new char[] { ' ', ',', '.', ':', '\t' });
                    string[] newstr2 = new string[3];
                    string t31, t32, t33;
                    for (int i = 0; i < 3; i++)
                    {
                        while (j < b)
                        {
                            newstr2[i] += split2[j] + " ";
                            j++;
                        }
                        b += a / 3;
                        if (b > a)
                        {
                            break;
                        }
                    }
                    t31 = newstr2[0] + "3pifco";
                    t32 = newstr2[1];
                    t33 = newstr2[2];
                    Rectangle rect = new Rectangle(0, 0, img.Width / 3, img.Height);
                    Bitmap img3_1 = img.Clone(rect, img.PixelFormat);
                    Bitmap img31 = new Bitmap(chosen_file);
                    img31 = embedText(t31, img3_1);
                    rect = new Rectangle(img.Width / 3, 0, img.Width / 3, img.Height);
                    Bitmap img3_2 = img.Clone(rect, img.PixelFormat);
                    Bitmap img32 = new Bitmap(chosen_file);
                    img32 = embedText(t32, img3_2);
                    rect = new Rectangle(2 * img.Width / 3, 0, img.Width / 3, img.Height);
                    Bitmap img3_3 = img.Clone(rect, img.PixelFormat);
                    Bitmap img33 = new Bitmap(chosen_file);
                    img33 = embedText(t33, img3_3);
                    coding();
                    var result = MessageBox.Show("Coding finished. Do you want to Save you coding image?", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            string folderName = "Coding Pictures";
                            string path = System.IO.Path.Combine(fbd.SelectedPath, folderName);
                            System.IO.Directory.CreateDirectory(path);
                            fbd.RootFolder = Environment.SpecialFolder.Desktop;
                            img31.Save(System.IO.Path.Combine(path, 1 + ".bmp"));
                            img32.Save(System.IO.Path.Combine(path, 2 + ".bmp"));
                            img33.Save(System.IO.Path.Combine(path, 3 + ".bmp"));
                            MessageBox.Show("Your coding image saved successfully.", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else if (a>=4)
                {
                    string[] newstr = new string[4];
                    b = a / 4;
                    c = a % 4;
                    switch (c)
                    {
                        case 0:
                            mysplit(a, b, j, newstr, split);
                            break;
                        case 1:
                            mysplit(a, b, j, newstr, split);
                            newstr[3] += split[a - 1];
                            break;
                        case 2:
                            mysplit(a, b, j, newstr, split);
                            newstr[2] += split[a - 2];
                            newstr[3] += split[a - 1];
                            break;
                        case 3:
                            mysplit(a, b, j, newstr, split);
                            newstr[1] += split[a - 3];
                            newstr[2] += split[a - 2];
                            newstr[3] += split[a - 1];
                            break;
                    }
                    string t1, t2, t3, t4;
                    t1 = newstr[0] + "4pifco";
                    t2 = newstr[1];
                    t3 = newstr[2];
                    t4 = newstr[3];
                    Rectangle rect = new Rectangle(0, 0, img.Width / 2, img.Height / 2);
                    Bitmap img4_1 = img.Clone(rect, img.PixelFormat);
                    Bitmap img41 = new Bitmap(chosen_file);
                    img41 = embedText(t1, img4_1);
                    rect = new Rectangle(img.Width / 2, 0, img.Width / 2, img.Height / 2);
                    Bitmap img4_2 = img.Clone(rect, img.PixelFormat);
                    Bitmap img42 = new Bitmap(chosen_file);
                    img42 = embedText(t2, img4_2);
                    rect = new Rectangle(0, img.Height / 2, img.Width / 2, img.Height / 2);
                    Bitmap img4_3 = img.Clone(rect, img.PixelFormat);
                    Bitmap img43 = new Bitmap(chosen_file);
                    img43 = embedText(t3, img4_3);
                    rect = new Rectangle(img.Width / 2, img.Height / 2, img.Width / 2, img.Height / 2);
                    Bitmap img4_4 = img.Clone(rect, img.PixelFormat);
                    Bitmap img44 = new Bitmap(chosen_file);
                    img44 = embedText(t4, img4_4);
                    coding();
                    var result = MessageBox.Show("Coding finished. Do you want to Save you coding image?", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            string folderName = "Coding Pictures";
                            string path = System.IO.Path.Combine(fbd.SelectedPath, folderName);
                            System.IO.Directory.CreateDirectory(path);
                            fbd.RootFolder = Environment.SpecialFolder.Desktop;
                            img41.Save(System.IO.Path.Combine(path, 1 + ".bmp"));
                            img42.Save(System.IO.Path.Combine(path, 2 + ".bmp"));
                            img43.Save(System.IO.Path.Combine(path, 3 + ".bmp"));
                            img44.Save(System.IO.Path.Combine(path, 4 + ".bmp"));
                            MessageBox.Show("Your coding image saved successfully.", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation Cancelled", "message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int words;
            words = textBox1.TextLength;
            label2.Text = words.ToString();
        }
    }
}
