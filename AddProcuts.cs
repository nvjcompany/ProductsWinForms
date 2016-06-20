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
using System.Text.RegularExpressions;
namespace UniTask
{
    public partial class AddProcuts : Form
    {
        public AddProcuts()
        {
            InitializeComponent();
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {

            if (textBoxProductName.Text == "" || textBoxProductPrice.Text == "" || textBoxProductQuentity.Text == "")
            {
                MessageBox.Show("Моля попълнете всички полета!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Regex expression = new Regex("[^a-zA-Z0-9_ ]+");
            string name = textBoxProductName.Text;
            if (expression.IsMatch(name))
            {
                MessageBox.Show("Моля използвайте само цифри и букви в името на продукта!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal price;
            if (!decimal.TryParse(textBoxProductPrice.Text, out price) || Convert.ToDecimal(textBoxProductPrice.Text) <= 0)
            {
                MessageBox.Show("Невалидна или празна стойност на цената! Моля не използвайте стойности по-малки или равни на 0!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quentity;

            if (!int.TryParse(textBoxProductQuentity.Text, out quentity) || Convert.ToInt32(textBoxProductQuentity.Text) < 0)
            {
                MessageBox.Show("Невалидна или празна стойност на Количеството! Моля не използвайте стойности по-малки от 0!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var connection = new Connection()._Connection)
            {
                connection.Open();
                string query = "INSERT INTO PRODUCTS (NAME,PRICE,QUENTITY) VALUES (@name, @price, @quentity)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@quentity", quentity);

                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Успешно добавено!");
                }
                else
                {
                    MessageBox.Show("Възникна грешка моля опитайте отново или се свържете с администратор!", "Грешка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mF = new MainForm();
            mF.Show();
            mF.buttonLoadProducts_Click(sender, e);
        }
    }
}
