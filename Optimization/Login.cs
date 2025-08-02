using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Threading;

namespace Optimization
{
    public partial class Login : Form
    {
        private readonly string[] validKeys;
        private readonly string githubUrl = "https://raw.githubusercontent.com/creater22/Programma/master/Optimization/Used.txt";

        private List<string> usedKeysInMemory = new List<string>(); // Хранение содержимого файла в памяти

        public Login(string[] keys)
        {
            InitializeComponent();
            validKeys = keys;
            // Загружаем файл с GitHub асинхронно
            LoadUsedKeysFromGitHubAsync();

            this.Login_Button.Click += Login_Button_Click;
        }

        private async void LoadUsedKeysFromGitHubAsync()
        {
            string content = await DownloadFileFromGitHubAsync(githubUrl);
            if (content != null)
            {
                usedKeysInMemory = new List<string>(content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            }
            else
            {
                MessageBox.Show("Не удалось загрузить список ключей.", "Ошибка");
            }
        }

        private async Task<string> DownloadFileFromGitHubAsync(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return await client.DownloadStringTaskAsync(url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при скачивании файла: {ex.Message}");
                return null;
            }
        }

        private async void Login_Button_Click(object sender, EventArgs e)
        {
            string enteredKey = Login_textBox.Text.Trim();
            if (string.IsNullOrEmpty(enteredKey))
            {
                MessageBox.Show("Пожалуйста, введите ключ.", "Внимание");
                return;
            }

            await CheckAndProcessKeyAsync(enteredKey);
        }

        private async Task CheckAndProcessKeyAsync(string enteredKey)
        {
            string registryPath = @"Software\Optimization";
            string registryValueName = "LicenseKey";

            // Локальная проверка: если ключ уже использован
            if (usedKeysInMemory.Any(k => string.Equals(k, enteredKey, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Этот ключ уже был использован.", "Ошибка");
                return;
            }

            using (var key = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                string storedKey = key?.GetValue(registryValueName) as string;

                if (string.IsNullOrEmpty(storedKey))
                {
                    // Не привязан — привязать
                    AskAndBindKey(enteredKey, registryPath, registryValueName);
                    return;
                }

                // Проверка совпадения ключа с привязанным
                if (string.Equals(storedKey, enteredKey, StringComparison.OrdinalIgnoreCase))
                {
                    // Имитация долгой проверки (например, чтобы усложнить перебор)
                    await SimulateLongCheck();

                    // После задержки считаем, что проверка прошла
                    // Можно добавить дополнительные условия, если нужно

                    // Проверка, что ключ не использовался ранее
                    if (usedKeysInMemory.Any(k => string.Equals(k, enteredKey, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("Этот ключ уже был использован.", "Ошибка");
                        return;
                    }

                    // Успешный вход
                    usedKeysInMemory.Add(enteredKey);
                    MessageBox.Show("Успешный вход!", "Успех");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный ключ.", "Ошибка");
                }
            }
        }

        private async Task SimulateLongCheck()
        {
            // Имитируем задержку, чтобы усложнить перебор
            await Task.Delay(2000); // задержка 5 секунд
        }

        private void AskAndBindKey(string enteredKey, string registryPath, string registryValueName)
        {
            var result = MessageBox.Show("Ключ не привязан. Хотите привязать ключ к ПК?", "Привязать ключ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (var writableKey = Registry.CurrentUser.CreateSubKey(registryPath))
                {
                    writableKey.SetValue(registryValueName, enteredKey);
                }
                MessageBox.Show("Ключ успешно привязан к ПК.", "Успех");
                usedKeysInMemory.Add(enteredKey);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Для входа необходимо привязать ключ.", "Ошибка");
            }
        }
    }
}