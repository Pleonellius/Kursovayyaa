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
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {

        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            //Если авторизации была успешна и в поле класса хранится истина, то делаем движуху:
            if (Auth.auth)
            {
                //Отображаем рабочую форму
                this.Show();
                metroTextBox1.Text = Auth.auth_fio;
                metroTextBox2.Text = Auth.auth_age;
                metroTextBox3.Text = Auth.auth_data;
                metroTextBox4.Text = Auth.auth_telef;
                metroTextBox5.Text = Auth.auth_adres;
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
            string shadowraze = metroTextBox2.Text;
            string ODIN = metroTextBox3.Text;
            //Класс SomeClass объявлен в файле Program.cs, в нём объявлено простое поле. Наша задача, присвоить этому полю значение, 
            //а в другой форме его вытащить.
            SomeClass.variable_class = koko;
            SomeClass.variable_class = shadowraze;
            SomeClass.variable_class = ODIN;
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Form6 frm = new Form6();
            frm.ShowDialog();
        }
    }
}
