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
    public partial class Профиль : MetroFramework.Forms.MetroForm
    {

        public Профиль()
        {
            InitializeComponent();
        }
        //Переменная соединения
        MySqlConnection conn;
        private void Form2_Load(object sender, EventArgs e)
        {
            string connStr = "server=chuc.caseum.ru;port=33333;user=st_2_19_1;database=is_2_19_st1_KURS;password=58458103;";
            // создаём объект для подключения к БД
            conn = new MySqlConnection(connStr);
            //Если авторизации была успешна и в поле класса хранится истина, то делаем движуху:
            if (Auth.auth)
            {
                //Отображаем рабочую форму
                this.Show();
                metroTextBox1.Text = Auth.auth_fio;
                metroTextBox3.Text = Auth.auth_data;
                metroTextBox4.Text = Auth.auth_telef;
                metroTextBox5.Text = Auth.auth_adres;
                metroTextBox6.Text = Auth.auth_id;
                metroTextBox2.Text = Auth.auth_pol;
            }
            //иначе
            else
            {
                //Закрываем форму
                this.Close();
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //Объявляем переменную для передачи значения в другую форму
            string koko = metroTextBox1.Text;
            string ODIN = metroTextBox3.Text;
            //Класс SomeClass объявлен в файле Program.cs, в нём объявлено простое поле. Наша задача, присвоить этому полю значение, 
            //а в другой форме его вытащить.
            SomeClass.variable_class = koko;
            SomeClass.variable_class = ODIN;
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Form6 frm = new Form6();
            frm.ShowDialog();
        }
        //Кнопка изменить 
        private void metroButton6_Click(object sender, EventArgs e)
        {
            metroDateTime1.Visible = true;
            metroTextBox1.Enabled = true;
            metroTextBox3.Visible = false;
            metroTextBox5.Enabled = true;
            metroButton5.Visible = true;
            metroButton7.Visible = true;
            metroButton6.Visible = false;
        }
        //Кнопка отмены изменений
        private void metroButton7_Click(object sender, EventArgs e)
        {
            metroDateTime1.Visible = false;
            metroTextBox1.Enabled = false;
            metroTextBox3.Visible = true;
            metroTextBox5.Enabled = false;
            metroButton5.Visible = false;
            metroButton7.Visible = false;
            metroButton6.Visible = true;
            metroTextBox1.Text = Auth.auth_fio;
            metroTextBox3.Text = Auth.auth_data;
            metroTextBox4.Text = Auth.auth_telef;
            metroTextBox5.Text = Auth.auth_adres;
        }
        //Кнопка сохранения изменений
        private void metroButton5_Click(object sender, EventArgs e)
        {
            
            //Получаем ID изменяемого студента
            string redact_id = metroTextBox6.Text;
            //Получаем значение нового ФИО из TextBox
            string new_fio = metroTextBox1.Text;
            string new_telefon = metroTextBox4.Text;
            string new_adres = metroTextBox5.Text;
            string new_data = metroDateTime1.Value.ToString(string.Format("{0:yyyy-MM-dd}", metroDateTime1.Value));
            // устанавливаем соединение с БД
            conn.Open();
            // запрос обновления данных
            string query2 = $"UPDATE Pacienti SET NamePac = '{new_fio}', GodRoj='{new_data}', Telefon = '{new_telefon}', Adres = '{new_adres}' WHERE id = {redact_id}";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(query2, conn);
            // выполняем запрос
            command.ExecuteNonQuery();
            // закрываем подключение к БД
            conn.Close();
            metroDateTime1.Visible = false;
            metroTextBox1.Enabled = false;
            metroTextBox3.Visible = true;
            metroTextBox5.Enabled = false;
            metroButton5.Visible = false;
            metroButton7.Visible = false;
            metroButton6.Visible = true;
        }
        private void metroButton4_Click(object sender, EventArgs e)
        {
            Контакты form7 = new Контакты();
            form7.ShowDialog();
        }
    }
}
