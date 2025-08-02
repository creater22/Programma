using System;
using System.Drawing;
using System.Windows.Forms;

namespace Optimization
{
    public partial class MainForm : Form
    {
        private Panel contentPanel;
        private Panel menuPanel;
        private FlowLayoutPanel layout;
        private Button btnMouseFix; // Для кнопки внутри вкладки "Latency"

        public MainForm(Point startPosition)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = startPosition;
            this.Size = new Size(900, 600);

            // Создаем панели
            menuPanel = new Panel
            {
                BackColor = Color.FromArgb(24, 30, 54),
                Dock = DockStyle.Left,
                Width = 200
            };

            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            // Создаем основную панель с кнопками
            layout = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill,
                Padding = new Padding(0),
                AutoSize = false,
                AutoScroll = true,
                WrapContents = false
            };

            // Создаем кнопки
            var btnMain = CreateButton("Главная");
            var btnGPU = CreateButton("GPU");
            var btnCPU = CreateButton("CPU");
            var btnRAM = CreateButton("RAM");
            var btnLatency = CreateButton("Latency");
            var btnAutorans = CreateButton("Autorans");
            var btnService = CreateButton("Service");
            var btnTelemetry = CreateButton("Telemetry");
            var btnEnergy = CreateButton("Energy");
            var btnExtreme = CreateButton("Extreme");
            var btnClean = CreateButton("Clean");

            // Создаем кнопку для MouseFix, которая внутри вкладки "Latency"
            btnMouseFix = CreateButton("MouseFix");
            btnMouseFix.Click += (s, e) => ShowMouseFixControl();

            // Назначаем обработчики для других кнопок
            btnMain.Click += (s, e) => ActivateButton(btnMain);
            btnGPU.Click += (s, e) => ActivateButton(btnGPU);
            btnCPU.Click += (s, e) => ActivateButton(btnCPU);
            btnRAM.Click += (s, e) => ActivateButton(btnRAM);
            btnLatency.Click += (s, e) => ActivateButton(btnLatency);
            btnAutorans.Click += (s, e) => ActivateButton(btnAutorans);
            btnService.Click += (s, e) => ActivateButton(btnService);
            btnTelemetry.Click += (s, e) => ActivateButton(btnTelemetry);
            btnEnergy.Click += (s, e) => ActivateButton(btnEnergy);
            btnExtreme.Click += (s, e) => ActivateButton(btnExtreme);
            btnClean.Click += (s, e) => ActivateButton(btnClean);

            // Создаем вкладку "Latency"
            var tabControl = new TabControl
            {
                Dock = DockStyle.Left,
                Width = 200
            };

            var tabLatency = new TabPage("Latency");
            var latencyPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight
            };
            latencyPanel.Controls.Add(btnMouseFix); // добавляем кнопку внутрь вкладки

            tabLatency.Controls.Add(latencyPanel);
            tabControl.TabPages.Add(tabLatency);

            // Создаем другие вкладки, например "Main"
            var tabMain = new TabPage("Main");
            var lblMain = new Label
            {
                Text = "Основное содержимое",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            tabMain.Controls.Add(lblMain);
            tabControl.TabPages.Add(tabMain);

            // Добавляем всё на форму
            this.Controls.Add(contentPanel);
            this.Controls.Add(menuPanel);
            this.Controls.Add(tabControl);
            menuPanel.Controls.Add(layout);

            // Активируем первую кнопку
            ActivateButton(btnMain);
        }

        private Button CreateButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Height = 50,
                Width = 200,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(24, 30, 54),
                Margin = new Padding(0, 2, 0, 2),
                Padding = new Padding(10, 0, 0, 0),
                FlatAppearance = { BorderSize = 0 }
            };
            return btn;
        }

        private void ActivateButton(Button btn)
        {
            // Снимаем выделение со всех
            foreach (Control c in layout.Controls)
            {
                if (c is Button button)
                {
                    button.BackColor = Color.FromArgb(24, 30, 54);
                }
            }
            // Выделяем активную
            btn.BackColor = Color.FromArgb(37, 46, 94);
            // Показываем содержимое (по названию)
            ShowContent(btn.Text);
        }

        private void ShowContent(string title)
        {
            contentPanel.Controls.Clear();
            Label lbl = new Label
            {
                Text = $"Это содержимое для: {title}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14),
            };
            contentPanel.Controls.Add(lbl);
        }

        private void ShowMouseFixControl()
        {
            contentPanel.Controls.Clear();
            var mouseFixControl = new MouseFixControl();
            mouseFixControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(mouseFixControl);
        }
    }
}