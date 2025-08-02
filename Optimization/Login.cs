using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;

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

            // Загружаем файл с GitHub при инициализации формы
            string content = DownloadFileFromGitHub(githubUrl);
            if (content != null)
            {
                usedKeysInMemory = new List<string>(content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            }
            else
            {
                MessageBox.Show("Не удалось загрузить список ключей.", "Ошибка");
            }

            this.Login_Button.Click += Login_Button_Click;
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            string enteredKey = Login_textBox.Text.Trim();

            // Проверка привязки в реестре
            string registryPath = @"Software\Optimization";
            string registryValueName = "LicenseKey";

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                string storedKey = key?.GetValue(registryValueName) as string;

                if (string.IsNullOrEmpty(storedKey))
                {
                    // Не привязан — привязать
                    var result = MessageBox.Show("Ключ не привязан. Хотите привязать ключ к ПК?", "Привязать ключ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        using (var writableKey = Registry.CurrentUser.CreateSubKey(registryPath))
                        {
                            writableKey.SetValue(registryValueName, enteredKey);
                        }
                        MessageBox.Show("Ключ успешно привязан к ПК.", "Успех");
                        // Добавляем ключ в список использованных
                        usedKeysInMemory.Add(enteredKey);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Для входа необходимо привязать ключ.", "Ошибка");
                        return;
                    }
                }
                else
                {
                    if (string.Equals(storedKey, enteredKey, StringComparison.OrdinalIgnoreCase))
                    {
                        // Проверка, что ключ не использовался
                        if (usedKeysInMemory.Exists(k => string.Equals(k, enteredKey, StringComparison.OrdinalIgnoreCase)))
                        {
                            MessageBox.Show("Этот ключ уже был использован.", "Ошибка");
                            return;
                        }

                        // Добавляем в список использованных
                        usedKeysInMemory.Add(enteredKey);

                        MessageBox.Show("Успешный вход!", "Успех");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Неверный ключ.", "Ошибка");
                        return;
                    }
                }
            }

            // После проверки в реестре, если ключ не найден или не совпадает, проверяем список из GitHub
            if (!usedKeysInMemory.Any(k => string.Equals(k, enteredKey, StringComparison.OrdinalIgnoreCase)))
            {
                // Можно здесь реализовать дополнительную проверку, например, валидность ключа
                // В текущем случае считаем, что любой незанятый ключ из списка допустим
                // Или, если нужно, можно показать сообщение или выполнить другую логику
            }
        }

        private string DownloadFileFromGitHub(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при скачивании файла: {ex.Message}");
                return null;
            }
        }
    }
}