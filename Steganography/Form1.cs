using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 coding = new Form2();
            coding.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 decoding = new Form3();
            decoding.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to exit?", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        
    }
}
