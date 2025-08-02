using System;
using System.Drawing;
using System.Windows.Forms;

namespace Optimization
{
    public partial class MainForm : Form
    {
        private Panel menuPanel;
        private Panel contentPanel;
        private FlowLayoutPanel layout;

        // Объявляем кнопки
        private Button btnMain;
        private Button btnGPU;
        private Button btnCPU;
        private Button btnRAM;
        private Button btnLatency;
        private Button btnAutorans;
        private Button btnService;
        private Button btnTelemetry;
        private Button btnEnergy;
        private Button btnExtreme;
        private Button btnClean;

        public MainForm(Point startPosition)
        {
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

            // Создаем кнопки с новыми названиями
            btnMain = CreateButton("Главная");
            btnGPU = CreateButton("GPU");
            btnCPU = CreateButton("CPU");
            btnRAM = CreateButton("RAM");
            btnLatency = CreateButton("Latency");
            btnAutorans = CreateButton("Autorans");
            btnService = CreateButton("Service");
            btnTelemetry = CreateButton("Telemetry");
            btnEnergy = CreateButton("Energy");
            btnExtreme = CreateButton("Extreme");
            btnClean = CreateButton("Clean");

            // Назначаем обработчики
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

            // Создаем FlowLayoutPanel для вертикального списка с прокруткой
            layout = new FlowLayoutPanel();
            layout.FlowDirection = FlowDirection.TopDown;
            layout.Dock = DockStyle.Fill;
            layout.Padding = new Padding(0);
            layout.AutoSize = false;
            layout.AutoScroll = true; // включена вертикальная прокрутка
            layout.WrapContents = false; // отключить перенос элементов по строкам

            // Добавляем кнопки
            layout.Controls.AddRange(new Control[]
            {
                btnMain, btnGPU, btnCPU, btnRAM, btnLatency, btnAutorans, btnService, btnTelemetry, btnEnergy, btnExtreme, btnClean
            });

            // Добавляем панели на форму
            this.Controls.Add(contentPanel);
            this.Controls.Add(menuPanel);
            menuPanel.Controls.Add(layout);

            // Обновляем ширину кнопок при изменении размера формы
            this.SizeChanged += (s, e) =>
            {
                foreach (Control ctrl in layout.Controls)
                {
                    ctrl.Width = layout.ClientSize.Width;
                }
            };

            // Активируем первую кнопку
            ActivateButton(btnMain);
        }

        private Button CreateButton(string text)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Height = 50;
            btn.Width = 200; // изначально, будет скорректировано при resize
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            btn.FlatStyle = FlatStyle.Flat;
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(24, 30, 54);
            // Уменьшено расстояние между вкладками
            btn.Margin = new Padding(0, 2, 0, 2); // уменьшено межкнопочное пространство
            btn.Padding = new Padding(10, 0, 0, 0);
            btn.FlatAppearance.BorderSize = 0;
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
            // Обновляем содержимое
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