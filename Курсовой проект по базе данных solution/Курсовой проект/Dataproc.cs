using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Курсовой_проект
{
    public class Img
    {
        public Img( byte[] data)
        {
            Data = data;
         
        }
        public byte[] Data { get; private set; }  
    }

    class Dataproc
    {
        public List<Img> images = new List<Img>();
        public List<string[]> adata = new List<string[]>();
        public string spec;
        public void loadbase()
        {
            string strconnect = "server=localhost;user=root;database=libraryconsole;password=odmen4204;";
            MySqlConnection objconnect = new MySqlConnection(strconnect);
            objconnect.Open();
            string reqdata = "SELECT * FROM consoles ORDER by id";
            MySqlCommand command = new MySqlCommand(reqdata, objconnect);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                adata.Add(new string[100000]);
                for (int i = 0; i < 7; i++)
                {
                    adata[adata.Count - 1][i] = reader[i].ToString();
                }
            }
            reader.Close();
            objconnect.Close();
        }

        public void readimages(string id)
        {
            string strconnect = "server=localhost;user=root;database=libraryconsole;password=odmen4204;";
            MySqlConnection objconnect = new MySqlConnection(strconnect);
            objconnect.Open();
            string reqdata = string.Format("SELECT Pictures FROM consoles WHERE id={0}", id);
            MySqlCommand command = new MySqlCommand(reqdata, objconnect);
            var data = command.ExecuteScalar();
            var datArr = data as byte[];
            Img image = new Img(datArr);
            images.Add(image);
            objconnect.Close();
        }
        public void get_spec(string id)
        {
            string strconnect = "server=localhost;user=root;database=libraryconsole;password=odmen4204;";
            MySqlConnection objconnect = new MySqlConnection(strconnect);
            objconnect.Open();
            string reqdata = string.Format("SELECT Spec FROM consoles WHERE id={0}",id);
            MySqlCommand command = new MySqlCommand(reqdata, objconnect);
            spec = command.ExecuteScalar().ToString();    
            int col = command.ExecuteNonQuery();
            objconnect.Close();
        }

        public void load_file_BD(string nameCell,byte[] binaryImage,string id)
        {
            string strconnect = "server=localhost;user=root;database=libraryconsole;password=odmen4204;";
            MySqlConnection objconnect = new MySqlConnection(strconnect);
            string strcommand = string.Format("UPDATE consoles SET {0} = (?) WHERE id = {1}", nameCell, id);
            MySqlParameter param = new MySqlParameter("image",MySqlDbType.VarBinary);
            MySqlCommand command = new MySqlCommand(strcommand, objconnect);
            param.Value = binaryImage;
            command.Parameters.Add(param);
            objconnect.Open();
            int col = command.ExecuteNonQuery();
            objconnect.Close();
        }
        
        public void max_allowed_packet()
        {
            string strconnect = "server=localhost;user=root;database=libraryconsole;password=odmen4204;";
            MySqlConnection objconnect = new MySqlConnection(strconnect);
            objconnect.Open();
            string commandreq = string.Format("SET GLOBAL max_allowed_packet=1073741824");
            MySqlCommand command = new MySqlCommand(commandreq, objconnect);
            int col = command.ExecuteNonQuery();
            objconnect.Close();
        }
    }   
    
}

