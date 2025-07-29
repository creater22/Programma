using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace Optimization
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            // Получение ключей
            string url = "https://raw.githubusercontent.com/creater22/Programma/master/Optimization/Keys.txt";
            string fileContent = await GetContentFromGitHubAsync(url);
            string[] keys = Array.Empty<string>();
            if (fileContent != null)
            {
                keys = fileContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine($"Найдено ключей: {keys.Length}");
            }
            else
            {
                MessageBox.Show("Не удалось загрузить ключи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Запуск Windows Forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var loginForm = new Login(keys))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Здесь можно открыть основную форму после успешной авторизации
                    var mainForm = new MainForm(new Point(100, 100));
                    Application.Run(mainForm);
                }
                else
                {
                    // Пользователь отменил вход
                }
            }
        }

        static async Task<string> GetContentFromGitHubAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    return await client.GetStringAsync(url);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении файла: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
