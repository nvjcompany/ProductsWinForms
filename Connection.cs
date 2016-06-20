using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTask
{
    public class Connection
    {
        private string connectionString = @"Server=DIDU-PC\SQLEXPRESS;Database=UniTaskProducts;Trusted_Connection=True;";
        private SqlConnection connection = null;
        public SqlConnection _Connection { get { return this.connection; } }

        public Connection()
        {
                this.connection = new SqlConnection(this.connectionString);
        }
        

    }
}
