using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Курсовой_проект
{
    public partial class Form2 : Form
    {
        string idCell;
        Bitmap picture;
        byte[] binaryImage;
        int mod;
        public Form2(string s, int index, string id, int modAccess)
        {
            try
            {
                mod = modAccess;
                InitializeComponent();
                Text = s;
                Dataproc obj1 = new Dataproc();
                obj1.get_spec(id);
                obj1.readimages(id);
                textBox1.Text = obj1.spec;
                if (obj1.images[0].Data != null)
                {
                    byte[] bytearray = obj1.images[0].Data;
                    ImageConverter converter = new ImageConverter();
                    Image img = (Image)converter.ConvertFrom(bytearray);
                    Bitmap bmp = new Bitmap(img);
                    pictureBox1.Image = bmp;
                    picture = bmp;
                    idCell = id;
                }
                idCell = id;
                SetAcess();
            }
            catch
            {
                MessageBox.Show("При инициализации окна произошла ошибка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files(*.JPG;*.PNG)|*.JPG;*.PNG";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                    picture = (Bitmap)pictureBox1.Image;
                    Image img = pictureBox1.Image;
                    Dataproc obj1 = new Dataproc();
                    MemoryStream memoryStream = new MemoryStream();
                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    binaryImage = memoryStream.ToArray();
                    obj1.load_file_BD("Pictures", binaryImage, idCell);
                    memoryStream.Dispose();
                }
                catch
                {
                    MessageBox.Show("ошибка открытия файла", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

            private void textBox2_TextChanged(object sender, EventArgs e)
            {

            }

            private void pictureBox1_Click(object sender, EventArgs e)
            {
                Form3 openform3 = new Form3(picture);
                openform3.Show();
            }

            private void textBox1_TextChanged(object sender, EventArgs e)
            {

            }

            private void load_text_from_textbox1(object sender, EventArgs e)
            {
             
            }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string cellValue = textBox1.Text.ToString();
                string strcommand;
                string strconnect = "server=localhost;user=root;database=libraryconsole;password=odmen4204;";
                MySqlConnection objconnect = new MySqlConnection(strconnect);
                objconnect.Open();
                strcommand = string.Format("UPDATE consoles SET Spec = " + '"' + cellValue + '"' + " WHERE id = {1}", cellValue, idCell);
                MySqlCommand command = new MySqlCommand(strcommand, objconnect);
                int col = command.ExecuteNonQuery();
                objconnect.Close();
            }
            catch
            {
                MessageBox.Show("При сохранении текста в базу данных произошла ошибка!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image !=null)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "Сохранить изображение как...";
                save.OverwritePrompt = true;
                save.CheckPathExists = true;
                save.Filter = "Image files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(save.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка!не удалось сохранить изображения");
                    }
                }
            }
            
        }

        private void SetAcess()
        {
            if (mod == 0)
            {
                textBox1.ReadOnly = true;
                button1.Hide();
                button2.Hide();
            }
        }
    }  
 }
