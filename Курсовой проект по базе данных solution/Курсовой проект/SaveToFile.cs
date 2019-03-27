using System;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Курсовой_проект
{
    class SaveToFile
    {
        public void saveExcel(DataGridView dataGridView1)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Сохранить изображение как...";
            save.OverwritePrompt = true;
            save.CheckPathExists = true;
            save.Filter = "Excel Files|.xlsx"; 
            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = excelApp.Workbooks.Add();
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    for (int j = 1; j < dataGridView1.ColumnCount; j++)
                    {
                        if(j + 1 == dataGridView1.ColumnCount)
                            worksheet.Rows[1].Columns[j] = "Изображение";
                        else
                            worksheet.Rows[1].Columns[j] = dataGridView1.Columns[j - 1].HeaderText; // заполненине заголовков.
                    }
                    for (int i = 2; i < dataGridView1.RowCount + 1; i++)
                    {
                        for (int j = 1; j < dataGridView1.ColumnCount - 1; j++)
                        {
                            worksheet.Rows[i].Columns[j] = dataGridView1.Rows[i - 2].Cells[j - 1].Value; // заполнение остальных ячеек
                            if (j == 1 || j == 4)
                                worksheet.Cells[i, j].NumberFormat = "0";
                            if (j == 5)
                                worksheet.Cells[i, j].NumberFormat = "0,000";
                            if (j == 7)
                                worksheet.Cells[i, j].NumberFormat = "0,00";
                    }    
                    }
                    worksheet.Columns.AutoFit();
                    for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        Excel.Range rg = worksheet.get_Range("A" + 1, "A" + 1);
                        Dataproc obj1 = new Dataproc();
                        obj1.readimages(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        if (obj1.images[0].Data != null)
                        {
                            object missing = System.Reflection.Missing.Value;
                            byte[] bytearray = obj1.images[0].Data;
                            ImageConverter converter = new ImageConverter();
                            Image img = (Image)converter.ConvertFrom(bytearray);
                            Bitmap objBitmap = new Bitmap(img, new Size(400,400));
                            Clipboard.SetImage(objBitmap);
                            Excel.Range oRange = (Excel.Range)worksheet.Cells[i+2,8];
                            rg.Rows[i+2].RowHeight = 300;
                            rg.Columns[8].ColumnWidth = 56.43F;
                            worksheet.Paste(oRange,missing);
                    }
                    rg.Columns[8].ColumnWidth = 56.43F;
                    }
                    excelApp.AlertBeforeOverwriting = false;
                    workbook.SaveAs(save.FileName);
                    excelApp.Quit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка не удалось экспортировать БД в файл Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
