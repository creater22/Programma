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
            string enteredKey = Login_textBox.Text.Trim();

            if (IsKeyValid(enteredKey))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный ключ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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