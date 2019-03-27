using System;
using System.Windows.Forms;
using xNet;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace Курсовой_проект
{
    public partial class Form4 : Form
    {
        private int modAccess = 1;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var request = new HttpRequest();
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Пожалуйста заполните логин и пароль!", "Поля не заполнены", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string z = "https://oauth.vk.com/token?grant_type=password&client_id=6780620&client_secret=ZdiObbxapID0N7vSenUJ&username=" + textBox1.Text + "&password=" + textBox2.Text;
                string response = request.Get("https://oauth.vk.com/token?grant_type=password&client_id=2274003&client_secret=hHbZxrka2uZ6jB1inYsH&username=" + textBox1.Text + "&password=" + textBox2.Text).ToString();
                dynamic json = JObject.Parse(response);
                if (response.Contains("user_id"))
                {
                    string token = json.access_token;
                    modAccess = checkAccess(textBox1.Text);
                    Hide();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    Form1 obj = new Form1(modAccess);
                    obj.FormClosed += new FormClosedEventHandler(form1closed);
                    obj.Show();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка авторизации(отсутсвует интернет соединение или неправильно введены данные)","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("http://vk.com");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private int checkAccess(string nameAdmin)
        {
            string strconnect = "server=localhost;user=root;database=libraryconsole;password=odmen4204;";
            MySqlConnection objconnect = new MySqlConnection(strconnect);
            objconnect.Open();
            string[] str = new string[100000];
            string reqdata = "SELECT * FROM administrators ORDER by idAdministrators";
            MySqlCommand command = new MySqlCommand(reqdata, objconnect);
            MySqlDataReader reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                str[i] = reader[1].ToString();
                if(str[i] == nameAdmin)
                {
                    reader.Close();
                    objconnect.Close();
                    return 1;
                }
                i++;
            }
            reader.Close();
            int col = command.ExecuteNonQuery();
            objconnect.Close();
            return 0;
        }

        private void form1closed(object sender, FormClosedEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            Show();
        }

    }
}
