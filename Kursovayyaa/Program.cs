using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovayyaa
{
    static class SomeClass
    {
        //Статичное поле, которое хранит значение для передачи его между формами
        public static string variable_class;
        //Статичное поле, которое хранит значения ID добавленного клиента на Form15-addClient
        public static string new_inserted_id;
        //Статичное поле, которое хранит значение ID добаленного заказа
        public static string new_inserted_mainOrder_id;
        public static string aeee;
        public static string shadowraze;
    }
    //Класс необходимый для хранения состояния авторизации во время работы программы
    static class Auth
    {
        //Статичное поле, которое хранит значение статуса авторизации
        public static bool auth = false;
        //Статичное поле, которое хранит значения ФИО пользователя
        public static string auth_fio = null;
        //Статичное поле, которое хранит значения Возраста пользователя
        public static string auth_age = null;
        //Статичное поле, которое хранит значения Даты рождения пользователя
        public static string auth_data = null;
        //Статичное поле, которое хранит значения Телефона пользователя
        public static string auth_telef = null;
        //Статичное поле, которое хранит значения Адреса пользователя
        public static string auth_adres = null;
        //Статичное поле, которое хранит значения id пользователя
        public static string auth_id = null;
        //Статичное поле, которое хранит значения Пола пользователя
        public static string auth_pol = null;
    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Авторизация());
        }
    }
}
