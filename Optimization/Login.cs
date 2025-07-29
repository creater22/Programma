using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
            string enteredPassword = Login_textBox.Text.Trim();

            if (IsPasswordCorrect(enteredPassword))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                var errorForm = new ErrorDialog("Неверный ключ", Color.Red);
                errorForm.ShowDialog(this);
            }
        }

        private bool IsPasswordCorrect(string password)
        {
            if (validKeys == null || validKeys.Length == 0)
            {
                // Если ключи не загружены, допускаем любой ненулевой ввод
                return !string.IsNullOrEmpty(password);
            }
            return validKeys.Any(k => (k?.Trim().Equals(password, StringComparison.OrdinalIgnoreCase)) ?? false);
        }
    }
}