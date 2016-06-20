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
using UniTask;

namespace UniTask
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сигурни ли сте че искате да излезете от системата?", "Изход!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            Application.Exit();
        }

        public void buttonLoadProducts_Click(object sender, EventArgs e)
        {
            using (var connection = new Connection()._Connection)
            {
                connection.Open();
                string query = "SELECT ID, NAME, PRICE, QUENTITY  FROM PRODUCTS";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();
                dataGridViewMainForm.Rows.Clear();
                while (reader.Read())
                {
                    int id = (int)reader["ID"];
                    string name = (string)reader["NAME"];
                    decimal price = (decimal)reader["PRICE"];
                    int quentity = (!reader.IsDBNull(reader.GetOrdinal("QUENTITY")) ? (int)reader["QUENTITY"] : 0);
                    dataGridViewMainForm.Rows.Add(id, name, price, quentity);
                }
            }
        }

        private void buttonDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dataGridViewMainForm.Rows.Count == 0)
            {
                MessageBox.Show("Не сте заредили продуктите или не съществуват такива!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Сигурни ли сте че искате да изтриете този продукт? Действието ви ще е необратимо!", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            using (var connection = new Connection()._Connection)
            {
                connection.Open();
                string query = "DELETE FROM PRODUCTS WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", dataGridViewMainForm.CurrentRow.Cells[0].Value.ToString());
                command.ExecuteNonQuery();
                this.buttonLoadProducts_Click(sender, e);
            }

        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddProcuts add = new AddProcuts();
            add.Show();

        }

        private void dataGridViewMainForm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            if (dataGridViewMainForm.Rows.Count == 0)
            {
                MessageBox.Show("Не сте заредили продуктите или не съществуват такива!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var connection = new Connection()._Connection)
            {
                int ID = 0;
                string NAME = "";
                int QUENTITY = 0;
                decimal PRICE = 0;
                connection.Open();

                string query = "SELECT ID,NAME,PRICE,QUENTITY FROM PRODUCTS WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ID", dataGridViewMainForm.CurrentRow.Cells[0].Value.ToString());

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ID = (int)reader["ID"];
                    NAME = (string)reader["NAME"];
                    PRICE = (decimal)reader["PRICE"];
                    QUENTITY = (!reader.IsDBNull(reader.GetOrdinal("QUENTITY")) ? (int)reader["QUENTITY"] : 0);
                }
                this.Hide();
                EditProduct editProduct = new EditProduct(ID.ToString(), NAME, PRICE.ToString(), QUENTITY.ToString());
                editProduct.Show();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
