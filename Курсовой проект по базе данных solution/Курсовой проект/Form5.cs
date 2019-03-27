using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using xNet;

namespace Курсовой_проект
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
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
                    NewReserveAdmin(textBox1.Text);
                    MessageBox.Show("Резервный администратор успешно сменен!Изменения будут приняты при перезапуске программы!");
                    Close();
                }
            }
            catch 
            {
                MessageBox.Show("Ошибка авторизации(отсутсвует интернет соединение или неправильно введены данные или отсутстует подключение к базе данных)","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            void NewReserveAdmin(string nameAdmin)
            {
                string strconnect = "server=localhost;user=root;database=libraryconsole;password=odmen4204;";
                MySqlConnection objconnect = new MySqlConnection(strconnect);
                objconnect.Open();
                string strcommand = string.Format("UPDATE administrators SET Login = " + '"' + nameAdmin + '"' + " WHERE idAdministrators = 3");
                MySqlCommand command = new MySqlCommand(strcommand, objconnect);
                int col = command.ExecuteNonQuery();
                objconnect.Close();
            }
        }
    }
}
