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
    public partial class Авторизация : MetroFramework.Forms.MetroForm
    {

        // строка подключения к БД
        string connStr = "server=10.90.12.110;port=33333;user=st_2_19_1;database=is_2_19_st1_KURS;password=58458103;";
        //Переменная соединения
        MySqlConnection conn;
        //Логин и пароль к данной форме Вы сможете посмотреть в БД db_test таблице t_user

        //Вычисление хэша строки и возрат его из метода
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
        public void GetUserInfo(string Telefon_)
        {
            //Объявлем переменную для запроса в БД
            string selected_id_stud = metroTextBox1.Text;
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string sql = $"SELECT * FROM Pacienti WHERE Telefon='{Telefon_}'";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                Auth.auth_fio = reader[1].ToString();
                Auth.auth_age = reader[4].ToString();
                Auth.auth_data = reader[2].ToString();
                Auth.auth_telef = reader[3].ToString();
                Auth.auth_adres = reader[4].ToString();
                Auth.auth_pol = reader[5].ToString();
                Auth.auth_id = reader[0].ToString();
            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
        }
        public Авторизация()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Инициализируем соединение с подходящей строкой
            conn = new MySqlConnection(connStr);
        }
        private void metroButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Кнопка авторизации
        private void metroButton1_Click(object sender, EventArgs e)
        {
            //Проверка на пустые поля и сама авторизация
                if (metroTextBox1.Text == "" || metroTextBox2.Text == "")
                {
                    MessageBox.Show("Заполните все поля");
                }
                else
                {
                    String login = metroTextBox1.Text;
                    String pass = metroTextBox2.Text;
                    //Запрос в БД на предмет того, если ли строка с подходящим логином и паролем
                    string sql = "SELECT * FROM Pacienti WHERE Telefon = @un and Password = @up";
                    //Открытие соединения
                    conn.Open();
                    //Объявляем таблицу
                    DataTable table = new DataTable();
                    //Объявляем адаптер
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    //Объявляем команду
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    //Определяем параметры
                    command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
                    command.Parameters.Add("@up", MySqlDbType.VarChar, 25);
                    //Присваиваем параметрам значение
                    command.Parameters["@un"].Value = login;
                    command.Parameters["@up"].Value = sha256(pass);
                    //Заносим команду в адаптер
                    adapter.SelectCommand = command;
                    //Заполняем таблицу
                    adapter.Fill(table);
                    //Закрываем соединение
                    conn.Close();
                    //Если вернулась больше 0 строк, значит такой пользователь существует
                    if (table.Rows.Count > 0)
                    {
                        //Присваеваем глобальный признак авторизации
                        Auth.auth = true;
                        //Достаем данные пользователя в случае успеха
                        GetUserInfo(metroTextBox1.Text);
                        this.Hide();
                        Профиль form2 = new Профиль();
                        form2.ShowDialog();
                    }
                    else
                    {
                        //Отобразить сообщение о том, что авторизаия неуспешна
                        MessageBox.Show("Логин или пароль не правильны");
                    }
                } 
        }
        //Нельзя писать буквы
        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            Регистрацияя form5 = new Регистрацияя();
            form5.ShowDialog();
        }
        //Визуальные фишки для textbox'ов
        private void metroTextBox1_Enter(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == "Введите номер телефона")
            {
                metroTextBox1.Text = "";
                metroTextBox1.ForeColor = Color.Gray;
            }
        }

        private void metroTextBox1_Leave(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == "")
            {
                metroTextBox1.Text = "Введите номер телефона";
                metroTextBox1.ForeColor = Color.Gray;
            }
        }
        private void metroTextBox2_Enter(object sender, EventArgs e)
        {
            if (metroTextBox2.Text == "Введите пароль")
            {
                metroTextBox2.PasswordChar = '\0'; 
                metroTextBox2.Text = "";
                metroTextBox2.ForeColor = Color.Gray;
            }
        }

        private void metroTextBox2_Leave(object sender, EventArgs e)
        {
            if (metroTextBox2.Text == "")
            {
                metroTextBox2.PasswordChar = '\0';
                metroTextBox2.Text = "Введите пароль";
                metroTextBox2.ForeColor = Color.Gray;
            }
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox1.Checked==true)
            {
                metroTextBox2.PasswordChar = '\0';
            }
            else
            {
                metroTextBox2.PasswordChar = '*';
            }
        }
    }
}
