using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniTask
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void labelUsername_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(usernameTextBox.Text == "" || passwordTextBox.Text == "" )
            {
                MessageBox.Show("Моля въведете потребителско име и парола!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var connection = new Connection()._Connection;
            connection.Open();
            string query = "SELECT ID,USERNAME,PASSWORD FROM USERS WHERE USERNAME=@user AND PASSWORD=@pass";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@user", usernameTextBox.Text);
            command.Parameters.AddWithValue("@pass", passwordTextBox.Text);

            if ((object)command.ExecuteScalar() != null)
            {
                this.Hide();
                MainForm mF = new MainForm();
                mF.Show();
                
            }
            else
            {
                MessageBox.Show("Грешни потребителски детайли!", "Неуспешен вход!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            connection.Close();
        }
    }
}
