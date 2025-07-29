using System;
using System.Drawing;
using System.Windows.Forms;

namespace Optimization
{
    public partial class MainForm : Form
    {
        // Объявляем панели
        private Panel menuPanel;
        private Panel contentPanel;

        // Объявляем кнопки меню
        private Button btnMain;
        private Button btnGPU;
        private Button btnCPU;
        private Button btnRAM;
        private Button btnService;
        private Button btnTelemetry;
        private Button btnEnergy;
        private Button btnExtremal;

        public MainForm(Point startPosition)
        {
            // Инициализация формы
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = startPosition;

            this.Size = new Size(900, 600);

            // Создаем панели
            menuPanel = new Panel();
            menuPanel.BackColor = Color.FromArgb(24, 30, 54);
            menuPanel.Dock = DockStyle.Left;
            menuPanel.Width = 200;

            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;

            // Создаем кнопку
            Func<string, Button> createButton = (string text) =>
            {
                Button btn = new Button();
                btn.Text = text;
                btn.Width = 200;
                btn.Height = 50;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.BackColor = Color.FromArgb(24, 30, 54);
                btn.Margin = new Padding(0, 10, 0, 10); // Отступ сверху и снизу
                btn.Padding = new Padding(10, 0, 0, 0);
                btn.FlatAppearance.BorderSize = 0; // Убираем границу
                return btn;
            };

            // Создаем кнопки
            btnMain = createButton("Главная");
            btnGPU = createButton("GPU");
            btnCPU = createButton("CPU");
            btnRAM = createButton("RAM");
            btnService = createButton("SERVICE");
            btnTelemetry = createButton("TELEMETRY");
            btnEnergy = createButton("ENERGY");
            btnExtremal = createButton("EXTREMAL");

            // Назначаем обработчики
            btnMain.Click += (s, e) => ActivateButton(btnMain);
            btnGPU.Click += (s, e) => ActivateButton(btnGPU);
            btnCPU.Click += (s, e) => ActivateButton(btnCPU);
            btnRAM.Click += (s, e) => ActivateButton(btnRAM);
            btnService.Click += (s, e) => ActivateButton(btnService);
            btnTelemetry.Click += (s, e) => ActivateButton(btnTelemetry);
            btnEnergy.Click += (s, e) => ActivateButton(btnEnergy);
            btnExtremal.Click += (s, e) => ActivateButton(btnExtremal);

            // Используем FlowLayoutPanel для вертикального расположения с расстоянием
            FlowLayoutPanel layout = new FlowLayoutPanel();
            layout.FlowDirection = FlowDirection.TopDown;
            layout.Dock = DockStyle.Fill;
            layout.Padding = new Padding(0);
            layout.AutoSize = false;

            layout.Controls.AddRange(new Control[] {
                btnMain, btnGPU, btnCPU, btnRAM, btnService, btnTelemetry, btnEnergy, btnExtremal
            });

            // Добавляем панели на форму
            this.Controls.Add(contentPanel);
            this.Controls.Add(menuPanel);
            menuPanel.Controls.Add(layout);

            // Активируем первую кнопку
            ActivateButton(btnMain);
        }

        private void ActivateButton(Button btn)
        {
            // Снимаем выделение со всех
            foreach (Control c in ((FlowLayoutPanel)btn.Parent).Controls)
            {
                if (c is Button button)
                {
                    button.BackColor = Color.FromArgb(24, 30, 54);
                }
            }

            // Выделяем активную
            btn.BackColor = Color.FromArgb(37, 46, 94);

            // Меняем содержимое
            ShowContent(btn.Text);
        }

        private void ShowContent(string title)
        {
            contentPanel.Controls.Clear();
            Label lbl = new Label()
            {
                Text = $"Это содержимое для: {title}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14),
            };
            contentPanel.Controls.Add(lbl);
        }
    }
}
