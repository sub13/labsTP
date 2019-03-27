using System;
using System.Drawing;
using System.Windows.Forms;

namespace Курсовой_проект
{
    public partial class Form3 : Form
    {
        public Form3(Bitmap picture)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            pictureBox1.Image = picture;

        }
        private void events()
        {
            KeyDown += new KeyEventHandler(esc);
        }

        private void esc(object sender,KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            events();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
