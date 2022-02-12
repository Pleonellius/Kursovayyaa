using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovayyaa
{
    public partial class Form4 : MetroFramework.Forms.MetroForm
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            metroTextBox1.Text = SomeClass.variable_class;
            metroTextBox2.Text = SomeClass.new_inserted_id;
            metroTextBox3.Text = SomeClass.new_inserted_mainOrder_id;
            metroTextBox4.Text = SomeClass.aeee;
            metroTextBox5.Text = Auth.auth_fio;
            metroTextBox6.Text = Auth.auth_age;
            metroTextBox7.Text = Auth.auth_data;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
