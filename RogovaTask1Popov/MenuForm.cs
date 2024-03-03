using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogovaTask1Popov
{
    public partial class MenuForm : Form
    {

        private string loggedInUser;
        private PostgresConnector postgresConnector;

        public MenuForm(string loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            labelLoggedInUser.Text = $"Logged in as: {loggedInUser}";
            postgresConnector = new PostgresConnector();
        }

        private void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
