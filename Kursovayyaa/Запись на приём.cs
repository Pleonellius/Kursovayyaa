using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Kursovayyaa
{
    public partial class Form4 : MetroFramework.Forms.MetroForm
    {
        // строка подключения к БД
        string connStr = "server=10.90.12.110;port=33333;user=st_2_19_1;database=is_2_19_st1_KURS;password=58458103;";
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            metroDateTime1.MinDate = DateTime.Now;
            metroDateTime1.MaxDate = metroDateTime1.MinDate.AddDays(14);
            metroDateTime1.Value.ToString(string.Format("{0:yyyy-MM-dd}", metroDateTime1.Value));
            metroTextBox1.Text = SomeClass.variable_class;
            metroTextBox2.Text = SomeClass.new_inserted_id;
            metroTextBox3.Text = SomeClass.new_inserted_mainOrder_id;
            metroTextBox4.Text = SomeClass.aeee;
            metroTextBox5.Text = Auth.auth_fio;
            metroTextBox6.Text = Auth.auth_adres;
            metroTextBox7.Text = Auth.auth_data;
            metroTextBox8.Text = SomeClass.shadowraze;
            metroTextBox9.Text = Auth.auth_pol;
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Кнопка записи
        private void metroButton2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                //Открываем соединение
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand($"INSERT INTO Taloni (NamePaci, idVraha, Time, Data, Kab, Spec, telf, vrahtelf)" +
                   "VALUES (@name, @vrah, @time, @date, @kab, @speci, @telfi, @vt)", conn))
                {
                    //Использование параметров в запросах. Это повышает безопасность работы программы
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = metroTextBox5.Text;
                    cmd.Parameters.Add("@vrah", MySqlDbType.VarChar).Value = metroTextBox1.Text;
                    cmd.Parameters.Add("@time", MySqlDbType.VarChar).Value = metroTextBox8.Text;
                    cmd.Parameters.Add("@kab", MySqlDbType.VarChar).Value = metroTextBox4.Text;
                    cmd.Parameters.Add("@speci", MySqlDbType.VarChar).Value = metroTextBox2.Text;
                    cmd.Parameters.Add("@telfi", MySqlDbType.VarChar).Value = Auth.auth_telef;
                    cmd.Parameters.Add("@vt", MySqlDbType.VarChar).Value = metroTextBox3.Text;
                    cmd.Parameters.Add("@date", MySqlDbType.Timestamp).Value = string.Format("{0:yyyy-MM-dd}", metroDateTime1.Value);
                    int insertedRows = cmd.ExecuteNonQuery();
                    // закрываем подключение  БД
                    conn.Close();
                    MessageBox.Show("Запись прошла успешно !");
                    this.Close();
                }
            }
        }
        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
