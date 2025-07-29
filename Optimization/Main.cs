using System.Drawing;
using System.Windows.Forms;


namespace Optimization
{
    public partial class MainForm : Form
    {
        public MainForm(Point startPosition)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = startPosition;
        }
    }
}

