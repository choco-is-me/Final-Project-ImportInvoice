using System;
using System.Windows.Forms;

namespace ImportInvoice
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void createNewImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New main = new New();
            main.MdiParent = this;
            main.Show();
        }

        private void receiptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Receipt receipt = new Receipt();
            receipt.MdiParent = this;
            receipt.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
