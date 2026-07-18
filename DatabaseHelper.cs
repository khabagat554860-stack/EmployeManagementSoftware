using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace EmployeManagementSoftware
{
    public static class DatabaseHelper
    {
        private const string DB_NAME = "SalaryDB.sqlite";
        public static string ConnectionString => $"Data Source={DB_NAME};Version=3;";

        private static string dbFileName = "StaffroomDB.db";

        
        public static string ConnectionString => $"Data Source={dbFileName};Version=3;";

        public static void InitializeDatabase()
        {
            // Creates the database file if it doesn't exist yet
            if (!File.Exists(DB_NAME))
            {
                SQLiteConnection.CreateFile(DB_NAME);
            }

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // 1. Create Users Table (From Incoming: develop)
                string createUsersQuery = @"CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT,
                Email TEXT,
                Username TEXT UNIQUE,
                ContactNumber TEXT,
                Password TEXT
            );";

                using (var cmd = new SQLiteCommand(createUsersQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // 2. Create Employees Salary Table (From Current: Salaryfinal)
                string createEmployeesQuery = @"CREATE TABLE IF NOT EXISTS Employees (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                EmployeeId TEXT UNIQUE,
                Name TEXT,
                Position TEXT,
                BasicSalary REAL,
                Allowances REAL,
                Deductions REAL,
                NetSalary REAL,
                PaymentDate TEXT
            );";

                using (var cmd = new SQLiteCommand(createEmployeesQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
