using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Optimization
{
    public partial class ErrorDialog : Form
    {
        private Label messageLabel;

        public ErrorDialog(string message, Color textColor)
        {
            // В центре от начального окна
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(350, 150);
            this.BackColor = Color.LightGray;

            // Создание ярлыка и настройка
            messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.ForeColor = textColor;
            messageLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            messageLabel.Dock = DockStyle.Fill;
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Добавление ярлыка
            this.Controls.Add(messageLabel);

            // Границы 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;

            // Кнопка закрытия на enter
            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Anchor = AnchorStyles.Bottom;
            okButton.Size = new Size(75, 30);
            okButton.Location = new Point(this.ClientSize.Width - okButton.Width - 10, this.ClientSize.Height - okButton.Height - 10);
            this.Controls.Add(okButton);

            // Нажанитие enter
            this.AcceptButton = okButton;
        }
    }
}
