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
    public partial class Form5 : MetroFramework.Forms.MetroForm
    {
        // строка подключения к БД
        string connStr = "server=caseum.ru;port=33333;user=st_2_1_19;database=st_2_1_19;password=68201560;";
        public Boolean CheckUser()
        {
            //Запрос в БД на предмет того, если ли строка с подходящим логином и паролем
            string sql = "SELECT * FROM Pacienti WHERE Telefon = @un";
            //Открытие соединения
            conn.Open();
            //Объявляем таблицу
            DataTable table = new DataTable();
            //Объявляем адаптер
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            //Объявляем команду
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
            command.Parameters["@un"].Value = metroTextBox4.Text;
            //Заносим команду в адаптер
            adapter.SelectCommand = command;
            //Заполняем таблицу
            adapter.Fill(table);
            conn.Close();
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой номер телефона уже есть, введите другой номер телефона или авторизайтесь !");
                return true;
            }
            else
            {
                return false;
            }
            
        }
        static string sha256(string randomString)
        {
            //Тут происходит криптографическая магия. Смысл данного метода заключается в том, что строка залетает в метод
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        //Объявляем соединения с БД
        MySqlConnection conn;

        public Form5()
        {
            InitializeComponent();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            // строка подключения к БД
            string connStr = "server=caseum.ru;port=33333;user=st_2_1_19;database=st_2_1_19;password=68201560;";
            // создаём объект для подключения к БД
            conn = new MySqlConnection(connStr);
            //Вызов метода обновления списка преподавателей с передачей в качестве параметра ListBox
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                if (CheckUser())
                {
                    return;
                }
                //Открываем соединение
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand($"INSERT INTO Pacienti (NamePac, Age, GodRoj, Telefon, Adres, Password)" +
                   "VALUES (@name, @age, @godr, @telef, @adres, @passw)", conn))
                {
                    //Условная конструкция
                    if (metroTextBox1.Text == "" || metroTextBox2.Text== "" || metroTextBox3.Text == "" || metroTextBox4.Text== "" || metroTextBox5.Text== "")
                    {
                        MessageBox.Show("Заполните все поля !");
                    }
                    else
                    {
                        //Использование параметров в запросах. Это повышает безопасность работы программы
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = metroTextBox1.Text;
                        cmd.Parameters.Add("@age", MySqlDbType.VarChar).Value = metroTextBox2.Text;
                        cmd.Parameters.Add("@passw", MySqlDbType.VarChar).Value = sha256(metroTextBox3.Text);
                        cmd.Parameters.Add("@telef", MySqlDbType.VarChar).Value = metroTextBox4.Text;
                        cmd.Parameters.Add("@adres", MySqlDbType.VarChar).Value = metroTextBox5.Text;
                        cmd.Parameters.Add("@godr", MySqlDbType.Timestamp).Value = metroDateTime1.Value;
                        int insertedRows = cmd.ExecuteNonQuery();
                        // закрываем подключение  БД
                        conn.Close();
                        MessageBox.Show("Регистрация прошла успешно !");
                        this.Close();
                    }
                }
            }

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox1.Checked == true)
            {
                metroTextBox3.PasswordChar = '\0';
            }
            else
            {
                metroTextBox3.PasswordChar = '*';
            }
        }
    }
}
