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

       
        public static DataTable GetDashboardMetrics(DateTime selectedDate)
        {
            DataTable dt = new DataTable();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                string pattern1 = selectedDate.ToString("yyyy-MM") + "%";
                string pattern2 = "%" + selectedDate.ToString("MM/yyyy") + "%";

                string query = @"SELECT 
                            (SELECT COUNT(*) FROM Employees) as TotalEmployees, 
                            COALESCE(SUM(BasicSalary + Allowances), 0) as TotalGross, 
                            COALESCE(SUM(Deductions), 0) as TotalDeductions, 
                            COALESCE(SUM(NetSalary), 0) as TotalNet 
                         FROM SalaryRecords
                         WHERE PaymentDate LIKE @P1 OR PaymentDate LIKE @P2";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@P1", pattern1);
                    cmd.Parameters.AddWithValue("@P2", pattern2);

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

        public static bool UpdateEmployee(string employeeId, string fullName, string phone, string email, string position, string gender)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                string query = @"UPDATE Employees 
                        SET FullName = @FullName, 
                            PhoneNumber = @PhoneNumber, 
                            Email = @Email, 
                            Position = @Position, 
                            Gender = @Gender
                        WHERE EmployeeID = @EmployeeID";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Position", position);
                    cmd.Parameters.AddWithValue("@Gender", gender);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool UpdateEmployeeSalaryDetails(string employeeId, double basicSalary, double allowances, double deductions)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // 1. Update baseline in Employees table
                string empQuery = @"UPDATE Employees 
                            SET BasicSalary = @BasicSalary, 
                                Allowances = @Allowances, 
                                Deductions = @Deductions
                            WHERE EmployeeID = @EmployeeID";

                int rowsAffected = 0;

                using (var cmd = new SQLiteCommand(empQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@BasicSalary", basicSalary);
                    cmd.Parameters.AddWithValue("@Allowances", allowances);
                    cmd.Parameters.AddWithValue("@Deductions", deductions);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                // 2. Update history in SalaryRecords table (reusing the same 'conn')
                double netSalary = basicSalary + allowances - deductions;
                string recordQuery = @"UPDATE SalaryRecords 
                               SET BasicSalary = @BasicSalary, 
                                   Allowances = @Allowances, 
                                   Deductions = @Deductions, 
                                   NetSalary = @NetSalary 
                               WHERE EmployeeID = @EmployeeID";

                using (var cmdRecord = new SQLiteCommand(recordQuery, conn))
                {
                    cmdRecord.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmdRecord.Parameters.AddWithValue("@BasicSalary", basicSalary);
                    cmdRecord.Parameters.AddWithValue("@Allowances", allowances);
                    cmdRecord.Parameters.AddWithValue("@Deductions", deductions);
                    cmdRecord.Parameters.AddWithValue("@NetSalary", netSalary);
                    cmdRecord.ExecuteNonQuery();
                }

                return rowsAffected > 0;
            }
        }


        public static DataTable GetSalaryRecords(DateTime selectedDate)
        {
            DataTable dt = new DataTable();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Prepare patterns for both "2026-07%" and "%07/2026%"
                string pattern1 = selectedDate.ToString("yyyy-MM") + "%";
                string pattern2 = "%" + selectedDate.ToString("MM/yyyy") + "%";

                string query = @"SELECT 
                            s.EmployeeID, 
                            COALESCE(e.FullName, '') AS FullName, 
                            COALESCE(e.Position, '') AS Position, 
                            s.BasicSalary, 
                            s.Allowances, 
                            s.Deductions, 
                            s.NetSalary 
                         FROM SalaryRecords s
                         LEFT JOIN Employees e ON s.EmployeeID = e.EmployeeID
                         WHERE s.PaymentDate LIKE @P1 OR s.PaymentDate LIKE @P2
                         ORDER BY s.RecordID DESC";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@P1", pattern1);
                    cmd.Parameters.AddWithValue("@P2", pattern2);

                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }


        public static bool DoesSalaryRecordExist(string employeeId, DateTime paymentDate)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Format to "YYYY-MM%" (e.g., "2026-07%")
                string monthYearPrefix = paymentDate.ToString("yyyy-MM") + "%";

                string query = @"SELECT COUNT(1) FROM SalaryRecords 
                        WHERE EmployeeID = @EmployeeID 
                        AND PaymentDate LIKE @MonthYearPrefix";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@MonthYearPrefix", monthYearPrefix);

                    long count = Convert.ToInt64(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public static bool ClearAllSalaryRecords()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                string deleteSalaryRecordsQuery = "DELETE FROM SalaryRecords;";
                using (var cmd = new SQLiteCommand(deleteSalaryRecordsQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

        }

        public static bool SaveSalaryRecord(string employeeId, double basicSalary, double allowances, double deductions, DateTime paymentDate)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                double netSalary = basicSalary + allowances - deductions;

                string query = @"INSERT INTO SalaryRecords (EmployeeID, BasicSalary, Allowances, Deductions, NetSalary, PaymentDate)
                         VALUES (@EmployeeID, @BasicSalary, @Allowances, @Deductions, @NetSalary, @PaymentDate)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@BasicSalary", basicSalary);
                    cmd.Parameters.AddWithValue("@Allowances", allowances);
                    cmd.Parameters.AddWithValue("@Deductions", deductions);
                    cmd.Parameters.AddWithValue("@NetSalary", netSalary);
                    cmd.Parameters.AddWithValue("@PaymentDate", paymentDate.ToString("yyyy-MM-dd"));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static void FixMissingPaymentDates()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Updates any blank/NULL dates to 'YYYY-MM-DD' format
                string query = @"UPDATE SalaryRecords 
                         SET PaymentDate = strftime('%Y-%m-%d', 'now') 
                         WHERE PaymentDate IS NULL OR PaymentDate = ''";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}