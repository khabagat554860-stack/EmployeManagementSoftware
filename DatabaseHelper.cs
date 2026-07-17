using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace EmployeManagementSoftware
{
    public class DatabaseHelper
    {
        private const string DB_NAME = "SalaryDB.sqlite";
        public static string ConnectionString => $"Data Source={DB_NAME};Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(DB_NAME))
            {
                SQLiteConnection.CreateFile(DB_NAME);
                using (var conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = @"
                    CREATE TABLE Employees (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        EmployeeId TEXT UNIQUE,
                        Name TEXT,
                        Position TEXT,
                        BasicSalary REAL,
                        Allowances REAL,
                        Deductions REAL,
                        NetSalary REAL,
                        PaymentDate TEXT
                    )";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
