using Npgsql;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogovaTask1Popov
{
    public partial class LoginForm : Form
    {
        private PostgresConnector bd = new PostgresConnector();
        private int loginAttempts = 0;
        private bool isLocked = false;

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void buttonEnter_Click(object sender, EventArgs e)
        {
            if (isLocked)
            {
                MessageBox.Show("Вход заблокирован. Пожалуйста, подождите.");
                return;
            }

            string login = textBoxLog.Text;
            string password = textBoxPass.Text;

            NpgsqlCommand cmd_ex = new NpgsqlCommand("SELECT COUNT(*) FROM users " +
                "WHERE login = @login and password = @password", bd.getConnection());

            cmd_ex.Parameters.AddWithValue("@login", login);
            cmd_ex.Parameters.AddWithValue("@password", password);

            bd.openConnection();

            int user = Convert.ToInt32(cmd_ex.ExecuteScalar());

            bd.closeConnection();

            if (user > 0)
            {
                this.Hide();
                new MenuForm(login).ShowDialog();
            }
            else
            {
                loginAttempts++;

                if (loginAttempts >= 3)
                {
                    isLocked = true;
                    loginAttempts = 0;
                    await Task.Delay(20000);
                    isLocked = false;
                }

                MessageBox.Show("Неправильно введён логин или пароль.");
            }
        }
    }
}