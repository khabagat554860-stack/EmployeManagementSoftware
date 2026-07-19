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
        // Centralized database filename used across all application forms
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

            // 1. Create Users Table (Fixed: contains the Password column for Sign Up)
            const string createUsersQuery = @"CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT,
                Email TEXT,
                Username TEXT UNIQUE,
                Password TEXT,
                ContactNumber TEXT)";

            using var cmdUsers = new SQLiteCommand(createUsersQuery, conn);
            cmdUsers.ExecuteNonQuery();

            // 2. Create Unified Employees Table (Uses FullName to match the search/save fields)
            const string createEmployeesQuery = @"CREATE TABLE IF NOT EXISTS Employees (
                EmployeeID TEXT PRIMARY KEY,
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

            // 3. Create SalaryRecords Table (Fixed: contains the Month & Year fields)
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
        /// Calculates active dashboard metrics to feed live updates to your cards.
        /// </summary>
        public static DataTable GetDashboardMetrics()
        {
            DataTable dt = new DataTable();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Aggregates total employee counts and calculates historical transaction costs
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
    }
}