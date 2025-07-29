using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Optimization
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Попытка загрузить ключи из файла
            string[] keys = LoadKeysFromXml();

            // Создаём и показываем форму логина
            var loginForm = new Login(keys);
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Получение позиции окна логина
                var position = loginForm.Location;

                // Запуск основной формы
                var mainForm = new MainForm(position);
                Application.Run(mainForm);
            }
            else
            {
                // Пользователь вышел или закрыл окно
                // Можно оставить сообщение или ничего не делать
            }
        }

        static string[] LoadKeysFromXml()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keys.xml");
            if (!File.Exists(filePath))
            {
                // Не показываем сообщение
                return new string[0];
            }

            try
            {
                var doc = XDocument.Load(filePath);
                var keys = doc.Descendants("Key")
                              .Select(k => (string)k.Attribute("value"))
                              .Where(k => !string.IsNullOrEmpty(k))
                              .ToArray();

                return keys;
            }
            catch
            {
                // Ошибки при чтении файла — возвращаем пустой массив
                return new string[0];
            }
        }
    }
}