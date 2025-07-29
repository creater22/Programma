using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Optimization
{
    static class Program
    {
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Загружаем ключи с GitHub
            string[] keys = await LoadKeysFromGitHubAsync();

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
            }
        }

        static async Task<string[]> LoadKeysFromGitHubAsync()
        {
            string url = "https://raw.githubusercontent.com/creater22/Programma/master/Optimization/Keys.txt"; // исправленный URL
            using (var client = new System.Net.Http.HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(url);
                    return response.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки ключей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new string[0];
                }
            }
        }
    }
}