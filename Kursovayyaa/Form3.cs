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
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {
        public void reload_list()
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            GetListUsers();
        }
        string id_selected_ima;
        string id_selected_special;
        string id_selected_staj;
        string id_selected_kabinet;
        string id_selected_vrema;
        public void GetSelectedFIOString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_ima;
            string index_selected_special;
            string index_selected_staj;
            string index_selected_kabinet;
            string index_selected_vrema;
            //Индекс выбранной строки
            index_selected_ima = dataGridView1.SelectedCells[0].RowIndex.ToString();
            index_selected_special = dataGridView1.SelectedCells[1].RowIndex.ToString();
            index_selected_staj = dataGridView1.SelectedCells[2].RowIndex.ToString();
            index_selected_kabinet = dataGridView1.SelectedCells[4].RowIndex.ToString();
            index_selected_vrema = dataGridView1.SelectedCells[5].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_ima = dataGridView1.Rows[Convert.ToInt32(index_selected_ima)].Cells[0].Value.ToString();
            id_selected_special = dataGridView1.Rows[Convert.ToInt32(index_selected_special)].Cells[1].Value.ToString();
            id_selected_staj = dataGridView1.Rows[Convert.ToInt32(index_selected_staj)].Cells[2].Value.ToString();
            id_selected_kabinet = dataGridView1.Rows[Convert.ToInt32(index_selected_kabinet)].Cells[4].Value.ToString();
            id_selected_vrema = dataGridView1.Rows[Convert.ToInt32(index_selected_kabinet)].Cells[5].Value.ToString();
            string variable = id_selected_ima;
            string ae = id_selected_special;
            string ue = id_selected_staj;
            string vae = id_selected_kabinet;
            string astralStep = id_selected_vrema;
            //Класс SomeClass объявлен в файле Program.cs, в нём объявлено простое поле. Наша задача, присвоить этому полю значение, 
            //а в другой форме его вытащить.
            SomeClass.variable_class = variable;
            SomeClass.new_inserted_id = ae;
            SomeClass.new_inserted_mainOrder_id = ue;
            SomeClass.aeee = vae;
            SomeClass.shadowraze = astralStep;
        }
        public Form3()
        {
            InitializeComponent();
        }
        //Переменная соединения
        MySqlConnection conn;
        //DataAdapter представляет собой объект Command , получающий данные из источника данных.
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //и ограничивающие данные, а также связи между таблицами.
        private DataSet ds = new DataSet();
        //Представляет одну таблицу данных в памяти.
        private DataTable table = new DataTable();
        public void GetListUsers()
        {
            //Запрос для вывода строк в БД
            string commandStr = "SELECT IMAVraha AS 'Ф.И.О Врача', Special AS 'Специальность', Staj AS 'Стаж', obrazovanie AS 'Образование', kabinet AS 'Кабинет',vrema AS 'Время' FROM Vrahi";
            //Открываем соединение
            conn.Open();
            //Объявляем команду, которая выполнить запрос в соединении conn
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            //Заполняем таблицу записями из БД
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            //Закрываем соединение
            conn.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            label1.Text = Auth.auth_fio;
            // строка подключения к БД
            string connStr = "server=caseum.ru;port=33333;user=st_2_1_19;database=st_2_1_19;password=68201560;";
            // создаём объект для подключения к БД
            conn = new MySqlConnection(connStr);
            //Вызываем метод для заполнение дата Грида
            GetListUsers();
            //Видимость полей в гриде
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 40;
            dataGridView1.Columns[1].FillWeight = 20;
            dataGridView1.Columns[2].FillWeight = 20;
            dataGridView1.Columns[3].FillWeight = 20;
            //Режим для полей "Только для чтения"
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Left))
            {
                //Отвечает за то куда нажали
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];

                dataGridView1.CurrentRow.Selected = true;
                GetSelectedFIOString();
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox1.SelectedIndex)
            {

                case 0:
                    bSource.Filter = "";
                    break;
                case 1:
                    bSource.Filter = $"[Специальность] LIKE'" + "Хирург" + "%'";
                    break;
                case 2:
                    bSource.Filter = $"[Специальность] LIKE'" + "Дерматолог" + "%'";
                    break;
                case 3:
                    bSource.Filter = $"[Специальность] LIKE'" + "Окулист" + "%'";
                    break;
                case 4:
                    bSource.Filter = $"[Специальность] LIKE'" + "Терапевт" + "%'";
                    break;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Selected == true)
            {
                GetSelectedFIOString();
                Form4 frm = new Form4();
                frm.ShowDialog();

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            reload_list();
        }
    }
}
