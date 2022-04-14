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
    public partial class Регистрацияя : MetroFramework.Forms.MetroForm
    {
        // строка подключения к БД
        string connStr = "server=chuc.caseum.ru;port=33333;user=st_2_19_1;database=is_2_19_st1_KURS;password=58458103;";
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
            command.Parameters["@un"].Value = textBox1.Text;
            //Заносим команду в адаптер
            adapter.SelectCommand = command;
            //Заполняем таблицу
            adapter.Fill(table);
            conn.Close();
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой номер телефона уже есть, введите другой номер телефона или авторизируйтесь !");
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

        public Регистрацияя()
        {
            InitializeComponent();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            // строка подключения к БД
            string connStr = "server=chuc.caseum.ru;port=33333;user=st_2_19_1;database=is_2_19_st1_KURS;password=58458103;";
            // создаём объект для подключения к БД
            conn = new MySqlConnection(connStr);
            //Вызов метода обновления списка преподавателей с передачей в качестве параметра ListBox
        }
        //Кнопка регистрации
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
                using (MySqlCommand cmd = new MySqlCommand($"INSERT INTO Pacienti (NamePac, GodRoj, Telefon, Adres, pol, Password, nomerps, seriaps)" +
                   "VALUES (@name, @godr, @telef, @adres, @po, @passw, @nps, @sps)", conn))
                {
                    if (textBox3.TextLength < 6)
                    {
                        MessageBox.Show("Минимум 6 цифры в номере паспорта");
                    }
                    else
                    {
                        if (textBox2.TextLength < 4)
                        {
                            MessageBox.Show("Минимум 4 цифры в серии паспорта");
                        }
                        else
                        {
                            if (metroComboBox1.SelectedIndex == -1)
                            {
                                MessageBox.Show("Выберите пол");
                            }
                            else
                            {
                                if (textBox1.TextLength < 11)
                                {
                                    MessageBox.Show("Минимум 11 цифр в номере телефона");
                                }
                                else
                                {
                                    //Условная конструкция
                                    if (metroTextBox1.Text == "" || metroTextBox3.Text == "" || textBox1.Text == "" || metroTextBox5.Text == "" || textBox3.Text == "" || textBox2.Text == "")
                                    {
                                        MessageBox.Show("Заполните все поля !");
                                    }
                                    else
                                    {
                                        //Использование параметров в запросах. Это повышает безопасность работы программы
                                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = metroTextBox1.Text;
                                        cmd.Parameters.Add("@passw", MySqlDbType.VarChar).Value = sha256(metroTextBox3.Text);
                                        cmd.Parameters.Add("@telef", MySqlDbType.VarChar).Value = textBox1.Text;
                                        cmd.Parameters.Add("@adres", MySqlDbType.VarChar).Value = metroTextBox5.Text;
                                        cmd.Parameters.Add("@po", MySqlDbType.VarChar).Value = metroComboBox1.SelectedItem.ToString();
                                        cmd.Parameters.Add("@godr", MySqlDbType.Timestamp).Value = string.Format("{0:yyyy-MM-dd}", metroDateTime1.Value);
                                        cmd.Parameters.Add("@nps", MySqlDbType.VarChar).Value = textBox3.Text;
                                        cmd.Parameters.Add("@sps", MySqlDbType.VarChar).Value = textBox2.Text;
                                        int insertedRows = cmd.ExecuteNonQuery();
                                        // закрываем подключение  БД
                                        conn.Close();
                                        MessageBox.Show("Регистрация прошла успешно !");
                                        this.Close();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Показать пароль
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
        private void metroTextBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //Нельзя вводить буквы, рабочий бакепасе
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(",")) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void metroTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Нельзя вводить буквы, рабочий бакепасе
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(",")) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }

        private void metroTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Нельзя вводить буквы, рабочий бакепасе
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(",")) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }
    }
}
