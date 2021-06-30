using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;

namespace SimpleNotepad
{
    public partial class Form1 : Form
    {
        private string file_text; // содержимое документа
        private string file_name; // наименование документа
        
        public Form1()
        {   
            InitializeComponent();
            file_name = "Новый документ";
            this.Text = "Текстовый редактор Simple Notepad";
            file_text = "";
            Name_File.Text = file_name;
            Date.Text = Convert.ToString(DateTime.Today.ToLongDateString());
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_text = richTextBox1.Text;
            
            string stroka = file_text;
            stroka = stroka.Replace(" ", string.Empty);    
            stroka = stroka.Replace("\n", string.Empty);

            if (stroka.Length == 0)
            {
                this.Close();
            }
            else
            {
                string answer = Save_this_text(file_name), save;
                if (answer == "Yes")
                {
                    save = Save_file(file_text);
                    if (save == "Ok")
                    {
                        MessageBox.Show(" Файл успешно сохранён !!! ");
                        this.Close();
                    }
                }
                else
                {
                    if (answer == "No")
                    {
                        this.Close();
                    }
                }
            }
        }

        private void открытьСуществующийДокументtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( openFileDialog1.ShowDialog() == DialogResult.OK &&
                openFileDialog1.FileName.Length > 0 )
            {   
                file_name = openFileDialog1.FileName;
                this.Text = "Текстовый редактор Simple Notepad"; 
                Name_File.Text = file_name;
                file_text = System.IO.File.ReadAllText(file_name);
                richTextBox1.Text = file_text;
                MessageBox.Show("   Файл " + file_name + " успешно открыт ! ");
            }
        }

        private void создатьНовыйДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_text = richTextBox1.Text;
            string stroka = file_text;
            stroka = stroka.Replace(" ", string.Empty);
            stroka = stroka.Replace("\n", string.Empty);

            if (stroka.Length == 0) richTextBox1.Clear();
            else
            {
                string answer = Save_this_text(file_name);
                if (answer == "Yes")
                { 
                    if (Save_file(file_text) == "Ok") 
                    {
                        Main_info_of_NewDocument(); 
                    } 
                }
                else
                {
                    if (answer == "No") 
                    { 
                        Main_info_of_NewDocument(); 
                    }
                }
            }  
        }

        private void сохранитьТекущийДокументToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            string save = Save_file(richTextBox1.Text); 
        }

        public void Main_info_of_NewDocument() 
        {
            richTextBox1.Clear();
            file_name = "Новый документ";
            this.Text = "Текстовый редактор Simple Notepad"; Name_File.Text = file_name;
            file_text = "";
            Name_File.Text = file_name;
        }

        public string Save_this_text (string file_name) 
        {
            DialogResult result = MessageBox.Show
                (" Сохранить изменения в " + file_name + " ??? ", 
                " SimpleNotepad ", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes) 
                return "Yes";
            else
            {
                if (result == DialogResult.No) 
                    return "No";
                else 
                { 
                    if (result == DialogResult.Cancel) 
                        return "Cancel"; 
                    else 
                        return ""; }
            }
        }
        public string Save_file(string file_text) 
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.Cancel) 
                return "Cancel";
            else
            {
                if (result == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
                {
                    string file_name = saveFileDialog1.FileName;
                    System.IO.File.WriteAllText(file_name, file_text);
                    this.Text = "Текстовый редактор Simple Notepad"; 
                    Name_File.Text = file_name;
                    MessageBox.Show(" Файл " + file_name + "успешно сохранён !!! ");
                }
                return "Ok";
            }
        }

    }
}
