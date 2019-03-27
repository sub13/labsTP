using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Курсовой_проект
{
    public partial class Form1 : Form
    {
        int l;
        int[] index = new int[2];
        int mod = 2;
        Dictionary<int, string> pic_id = new Dictionary<int, string>();
        int buf;
        public delegate void form_delegate(object sender, EventArgs args);

        public Form1(int modAccess)
        {
            try
            {
                mod = modAccess;
                InitializeComponent();
                Dataproc obj1 = new Dataproc();
                obj1.loadbase();
                obj1.max_allowed_packet();
                foreach (string[] s in obj1.adata)
                {
                    dataGridView1.Rows.Add(s);
                }
                int i = 0;
                while (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    pic_id.Add(i, dataGridView1.Rows[i].Cells[0].Value.ToString());
                    i++;
                }
                i = 0;
                while (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    dataGridView1.Rows[i].Cells[0].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value); //конвертируем значения некоторых столбцов для корректной сортировки
                    i++;
                }
                i = 0;
                while (dataGridView1.Rows[i].Cells[3].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() != "")
                        dataGridView1.Rows[i].Cells[3].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value); //конвертируем значения некоторых столбцов для корректной сортировки
                    else
                        dataGridView1.Rows[i].Cells[3].Value = null;
                    i++;
                }
                i = 0;
                while (dataGridView1.Rows[i].Cells[4].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells[4].Value.ToString() != "")
                        dataGridView1.Rows[i].Cells[4].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value); //конвертируем значения некоторых столбцов для корректной сортировки
                    else
                        dataGridView1.Rows[i].Cells[4].Value = null;
                    i++;
                }
                i = 0;
                while (dataGridView1.Rows[i].Cells[6].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells[6].Value.ToString() != "")
                        dataGridView1.Rows[i].Cells[6].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value); //конвертируем значения некоторых столбцов для корректной сортировки
                    else
                        dataGridView1.Rows[i].Cells[6].Value = null;
                    i++;
                }
                SetAcess();
                events();
            }
            catch
            {
                MessageBox.Show("При инициализации окна произошла ошибка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void events()
        {
            dataGridView1.CellParsing += new DataGridViewCellParsingEventHandler(load_data_fromDataGridViev);
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(EndEdit);
            dataGridView1.UserDeletingRow += new DataGridViewRowCancelEventHandler(DELETE);
            dataGridView1.CellBeginEdit += new DataGridViewCellCancelEventHandler(BeginEditCell);
        }


        private void BeginEditCell(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
                buf = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        }

        private void DELETE(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1)
                {
                    e.Cancel = true;
                    return;
                }
                string strconnect = "server=localhost;user=root;database=libraryconsole;password=odmen4204;";
                MySqlConnection objconnect = new MySqlConnection(strconnect);
                objconnect.Open();
                string strcommand = string.Format("DELETE FROM `libraryconsole`.`consoles` WHERE(`id` = '{0}');", dataGridView1.Rows[e.Row.Index].Cells[0].Value);
                MySqlCommand command = new MySqlCommand(strcommand, objconnect);
                int col = command.ExecuteNonQuery();
                objconnect.Close();
            }
            catch
            {
                MessageBox.Show("При удалении ячейки произошла ошибка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null)
                    return;
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == "")
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    return;
                }
                if (e.ColumnIndex == 0)
                {
                    int i = 0;
                    //dataGridView1.CancelEdit();
                    while (dataGridView1.Rows[i].Cells[0].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() && i != e.RowIndex)
                        {
                            BeginInvoke(new MethodInvoker(() =>
                            {
                                MessageBox.Show("Такой id уже есть!Введите пожалуйста уникальное значение");
                                if (buf != 0)
                                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = buf;
                                else
                                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                            }));
                            return;
                        }
                        i++;
                    }
                }
                if ((e.ColumnIndex == 0 || e.ColumnIndex == 3) && dataGridView1.CurrentCell.Value != null && dataGridView1.CurrentCell.Value !="")
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if ((e.ColumnIndex == 4) && dataGridView1.CurrentCell.Value != null && dataGridView1.CurrentCell.Value != "")
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
            catch
            {
                MessageBox.Show("При редактировании ячейки что-то пошло не так!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress += new KeyPressEventHandler(cell_only_integer);
            tb.KeyPress += new KeyPressEventHandler(cell_only_float);
            tb.KeyPress += new KeyPressEventHandler(cell_only_symbol);
            tb.KeyPress += new KeyPressEventHandler(check_id);
        }


        void check_id(object sender, KeyPressEventArgs e)
        {
            
            if (dataGridView1.CurrentCell.ColumnIndex != 0 && dataGridView1.CurrentRow.Cells[0].Value == null)
            {   
                MessageBox.Show("Пожалуйста!сначала заполните ячейку id(ключ) целочисленным значением");
                e.Handled = true;
            }
            //if(e.KeyChar == (char)Keys.Return)
            //{
            //    int i = 0;
            //    while (true)
            //    {
            //        if (dataGridView1.Rows[i].Cells[0].Value == null)
            //            break;
            //        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == dataGridView1.CurrentCell.EditedFormattedValue.ToString())
            //        {
            //            MessageBox.Show("Такой id уже есть!Введите пожалуйста уникальное значение");
            //            e.Handled = true;
            //            break;
            //        }
            //        i++;
            //    }
            //}
        }
        
        void cell_only_float(object sender, KeyPressEventArgs e)
        {
            int key = 0;
            if (dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 6)
            {
                int pointPos = dataGridView1.CurrentCell.EditedFormattedValue.ToString().IndexOf(',');
                for (int j = 0; j < dataGridView1.CurrentCell.EditedFormattedValue.ToString().Length; j++)
                {
                    if (dataGridView1.CurrentCell.EditedFormattedValue.ToString()[j] == ',')
                    {
                        key = 1;
                        break;
                    }
                }

                if (!(Char.IsDigit(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != ','))
                {
                    e.Handled = true;
                }
                else
                {
                    if ((e.KeyChar == ',') && key == 1)
                        e.Handled = true;
                }
            }

        }

        void cell_only_integer(object sender, KeyPressEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                if (!(Char.IsDigit(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
        }

        void cell_only_symbol(object sender, KeyPressEventArgs e)
        {
             if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                if (!(Char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                if (e.RowIndex != -1 && dataGridView1.Rows[e.RowIndex].Cells[0].Value !=null)
                {
                    Form2 openform2 = new Form2(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[5].Value), e.RowIndex, Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value), mod);
                    openform2.Show();
                }
            } 
        }



        private void load_data_fromDataGridViev(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                int z = 0;
                string cellValue;
                if (e.ColumnIndex == 0 && e.Value != "")
                {
                    int idNew = Convert.ToInt32(e.Value);
                    while (dataGridView1.Rows[z].Cells[0].Value != null)
                    {
                        int idOld = Convert.ToInt32(dataGridView1.Rows[z].Cells[0].Value);
                        if (idOld == idNew)
                        {
                            return;
                        }
                        z++;
                    }
                }
                cellValue = e.Value.ToString();
                int l = cellValue.Length;
                int x, y;
                string[] element = { "id", "Developer", "Country", "Generation", "memory", "Model", "Price" };
                x = e.RowIndex;
                y = e.ColumnIndex;
                string strcommand;
                int col; //i = 0; int key = 0;
                string strconnect = "server=localhost;user=root;database=libraryconsole;password=odmen4204;";
                MySqlConnection objconnect = new MySqlConnection(strconnect);
                objconnect.Open();
                if (cellValue == "" && e.ColumnIndex == 0)
                {
                    strcommand = string.Format("DELETE FROM `libraryconsole`.`consoles` WHERE(`id` = '{0}');", dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    MySqlCommand command = new MySqlCommand(strcommand, objconnect);
                    col = command.ExecuteNonQuery();
                    return;
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    strcommand = string.Format("INSERT INTO consoles (`id`) VALUES('{0}'); ", e.Value);
                    MySqlCommand command = new MySqlCommand(strcommand, objconnect);
                    col = command.ExecuteNonQuery();
                }
                else
                {
                    string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    for (int j = 0; j < l; j++)
                    {
                        if (cellValue[j] == ',')
                        {
                            char[] chars = cellValue.ToCharArray();
                            chars[j] = '.';
                            cellValue = new String(chars);
                            if (j == 0)
                                cellValue = "0" + cellValue;
                            break;
                        }
                    }
                    if(cellValue =="")
                        strcommand = string.Format("UPDATE `libraryconsole`.`consoles` SET `{0}` =NULL WHERE(`id` = '{1}');", element[y], id);
                    else
                    strcommand = string.Format("UPDATE consoles SET {0} = " + '"' + cellValue + '"' + " WHERE id = {2}", element[y], cellValue, id);
                    MySqlCommand command = new MySqlCommand(strcommand, objconnect);
                    col = command.ExecuteNonQuery();
                    objconnect.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("При загрузки данных в базу данных произошла ошибка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
      
            if (comboBox1.Text == "Имп. в Excel")
            {
                SaveToFile obj = new SaveToFile();
                obj.saveExcel(dataGridView1);
            }
            if (comboBox1.Text == "Новый Админ")
            {
                if (mod == 0)
                {
                    MessageBox.Show("Сменить резервного администратора может только сам администратор");
                    return;
                }
                Form5 obj = new Form5();
                obj.Show();
            }
            if (comboBox1.Text == "Выйти")
            {
                Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
            dataGridView1.Rows[i].Selected = false;
            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        if (textBox1.Text == "")
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                            {
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                                dataGridView1.Rows[i].Selected = true;
                                dataGridView1.FirstDisplayedScrollingRowIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void SetAcess()
        {
            if (mod == 1)
            {
                MessageBox.Show("Вы вошли с правами администратора!");
                dataGridView1.ReadOnly = false;
            }
            else
            {
                MessageBox.Show("Вы вошли с правами клиента");
                dataGridView1.ReadOnly = true;
            }
        }
    }
}
