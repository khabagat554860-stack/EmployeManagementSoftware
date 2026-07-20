using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeManagementSoftware
{
    public static class DatabaseHelper
    {
        // Unified database filename used across all forms
        private const string DB_NAME = "employees.db";

        public static string ConnectionString { get; } = $"Data Source={DB_NAME};Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(DB_NAME))
            {
                SQLiteConnection.CreateFile(DB_NAME);
            }

            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();

            // 1. Create Users Table (For login credentials)
            const string createUsersQuery = @"CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT,
                Email TEXT,
                Username TEXT UNIQUE,
                ContactNumber TEXT)";

            using var cmdUsers = new SQLiteCommand(createUsersQuery, conn);
            cmdUsers.ExecuteNonQuery();

            // 2. Create Unified Employees Table (Matches AddEmployee form structural fields)
            const string createEmployeesQuery = @"CREATE TABLE IF NOT EXISTS Employees (
                EmployeeID INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT NOT NULL,
                PhoneNumber TEXT,
                Email TEXT,
                Position TEXT,
                Gender TEXT,
                Photo BLOB,
                BasicSalary REAL DEFAULT 0,
                Allowances REAL DEFAULT 0,
                Deductions REAL DEFAULT 0)";

            using var cmdEmployees = new SQLiteCommand(createEmployeesQuery, conn);
            cmdEmployees.ExecuteNonQuery();

            // 3. Create SalaryRecords Table (Fixed: contains the missing Month & Year fields)
            const string createSalaryRecordsQuery = @"CREATE TABLE IF NOT EXISTS SalaryRecords (
                RecordID INTEGER PRIMARY KEY AUTOINCREMENT,
                EmployeeID TEXT,
                BasicSalary REAL,
                Allowances REAL,
                Deductions REAL,
                NetSalary REAL,
                PaymentDate TEXT,
                PayPeriodMonth TEXT,
                PayPeriodYear TEXT)";

            using var cmdSalary = new SQLiteCommand(createSalaryRecordsQuery, conn);
            cmdSalary.ExecuteNonQuery();
        }

        /// <summary>
        /// Calculates active totals to feed the dashboard metric cards at the top of the form.
        /// </summary>
        public static DataTable GetDashboardMetrics()
        {
            DataTable dt = new DataTable();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Counts total workers from the registered list, 
                // and aggregates real financial entries out of saved pay slips history.
                string query = @"SELECT 
                                    (SELECT COUNT(*) FROM Employees) as TotalEmployees, 
                                    COALESCE(SUM(BasicSalary + Allowances), 0) as TotalGross, 
                                    COALESCE(SUM(Deductions), 0) as TotalDeductions, 
                                    COALESCE(SUM(NetSalary), 0) as TotalNet 
                                 FROM SalaryRecords";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }


            return dt;
        }
        public static DataTable GetEmployees()
        {
            DataTable dt = new DataTable();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                string query = @"SELECT
                            EmployeeID,
                            FullName,
                            PhoneNumber,
                            Email,
                            Position,
                            Gender
                         FROM Employees
                         ORDER BY EmployeeID DESC";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }
        public static DataTable SearchEmployees(string keyword)
        {
            DataTable dt = new DataTable();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                string query = @"SELECT EmployeeID,
                                FullName,
                                PhoneNumber,
                                Email,
                                Position,
                                Gender
                         FROM Employees
                         WHERE FullName LIKE @keyword
                            OR Position LIKE @keyword
                            OR EmployeeID LIKE @keyword";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }
    }
}