using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;  

namespace Optimization
{
    public partial class Login : Form
    {
        private readonly string[] validKeys;

        public Login(string[] keys)
        {
            InitializeComponent();
            validKeys = keys;

            // Назначение обработчика события
            this.Login_Button.Click += Login_Button_Click;
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            string enteredKey = Login_textBox.Text.Trim();

            // Проверка наличия ключа в реестре
            string registryPath = @"Software\MyApp";
            string registryValueName = "LicenseKey";

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                string storedKey = key?.GetValue(registryValueName) as string;

                if (string.IsNullOrEmpty(storedKey))
                {
                    // Ключ не привязан - предложить привязать
                    var result = MessageBox.Show("Ключ не привязан. Хотите привязать ключ к ПК?", "Привязать ключ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Можно сохранить ключ в реестр
                        using (var writableKey = Registry.CurrentUser.CreateSubKey(registryPath))
                        {
                            writableKey.SetValue(registryValueName, enteredKey);
                        }
                        MessageBox.Show("Ключ успешно привязан к ПК.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Можно разрешить вход
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        // Пользователь отказался
                        MessageBox.Show("Для входа необходимо привязать ключ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Проверяем, совпадает ли введенный ключ с привязанным
                    if (string.Equals(storedKey, enteredKey, StringComparison.OrdinalIgnoreCase))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный ключ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool IsKeyValid(string key)
        {
            if (validKeys == null || validKeys.Length == 0)
            {
                return !string.IsNullOrEmpty(key);
            }
            return validKeys.Any(k => string.Equals(k?.Trim(), key, StringComparison.OrdinalIgnoreCase));
        }
    }
}