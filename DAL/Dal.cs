using BusinessObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Dal
    {
        static SqlConnection connection;
        static SqlCommand command;

        private static string GetConnectionString()
        {
            return @"data source=DESKTOP-N979CRP;initial catalog=EmpDb;integrated security=true";
        }
        private static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
        public void AddEmployee(Employee employee)
        {
            try
            {
                connection = GetConnection();
                command = new SqlCommand();
                if (employee is OnContract)
                {
                    command.CommandText = "Insert into Employee(name, emp_type,Reporting_Manager," +
                    "Contract_Basis,Chargers_PerDay, Duration_In_Days," +
                    "OnContract_FinalSalary)" +
                    " values (@name, 1, @Reporting_Manager, @Contract_Basis, " +
                    "@Chargers_PerDay,@Duration, @OnContract_FinalSalary)";

                    command.Connection = connection;
                    command.Parameters.AddWithValue("@name", employee.name);
                    command.Parameters.AddWithValue("@Reporting_Manager", employee.reportingManager);
                    command.Parameters.AddWithValue("@Contract_Basis", ((OnContract)employee).contractDate);
                    command.Parameters.AddWithValue("@Chargers_PerDay", ((OnContract)employee).charges);
                    command.Parameters.AddWithValue("@Duration", ((OnContract)employee).duration);
                    command.Parameters.AddWithValue("@OnContract_FinalSalary", ((OnContract)employee).paymentAmt);

                }
                else if (employee is OnPayroll)
                {
                    command.CommandText = "Insert into Employee(name, emp_type," +
                        "Reporting_Manager, Joining_Date,Experience,Basic_salary," +
                        "NetSalary,DA,HRA,PF) values " +
                        "(@name,2, @Reporting_Manager,@Joining_Date,@Experience,@Basic_salary,@NetSalary,@DA,@HRA,@PF)";

                    command.Connection = connection;
                    command.Parameters.AddWithValue("@name", employee.name);
                    command.Parameters.AddWithValue("@Reporting_Manager", employee.reportingManager);
                    command.Parameters.AddWithValue("@Joining_Date", ((OnPayroll)employee).joiningDate);
                    command.Parameters.AddWithValue("@Experience", ((OnPayroll)employee).exp);
                    command.Parameters.AddWithValue("@Basic_salary", ((OnPayroll)employee).basicSalary);
                    command.Parameters.AddWithValue("@DA", ((OnPayroll)employee).da);
                    command.Parameters.AddWithValue("@HRA", ((OnPayroll)employee).hra);
                    command.Parameters.AddWithValue("@PF", ((OnPayroll)employee).pf);
                    command.Parameters.AddWithValue("@NetSalary", ((OnPayroll)employee).netSalary);

                }
                connection.Open();


                int count = command.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("Record has been added");
                }
                else
                    Console.WriteLine("Some Error came");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
                command.Dispose();
                connection.Dispose();
            }
        }

        public void EditEmployee(int id, Employee employee)
        {
            try
            {
                connection = GetConnection();
                command = new SqlCommand();
                if (employee is OnContract)
                {
                    command.CommandText = "Update Employee Set" +
                        "name = @name,emp_type = 1,Reporting_Manager=@Reporting_Manager," +
                        "Contract_Basis = @Contract_Basis,Charges_PerDay = @Charges_PerDay,Duration_In_Days = @Duration," +
                        "OnContract_FinalSalary = @OnContract_FinalSalary" +
                        "where id = @id";

                    command.Connection = connection;
                    command.Parameters.AddWithValue("@name", employee.name);
                    command.Parameters.AddWithValue("@Reporting_Manager", employee.reportingManager);
                    command.Parameters.AddWithValue("@Contract_Basis", ((OnContract)employee).contractDate);
                    command.Parameters.AddWithValue("@Chargers_PerDay", ((OnContract)employee).charges);
                    command.Parameters.AddWithValue("@Duration", ((OnContract)employee).duration);
                    command.Parameters.AddWithValue("@OnContract_FinalSalary", ((OnContract)employee).paymentAmt);

                }
                else if (employee is OnPayroll)
                {


                    command.CommandText = "Update Employee set" +
                        "name=@name,emp_type=2,Reporting_Manager = @Reporting_Manager" +
                        "Joining_Date = @Joining_Date,Experience = @Experience," +
                        "Basic_salary = @Basic_salary,NetSalary = @NetSalary," +
                        "DA = @DA,HRA=@HRA,PF=@PF" +
                        "where id = @id";

                    command.Connection = connection;
                    command.Parameters.AddWithValue("@name", employee.name);
                    command.Parameters.AddWithValue("@Reporting_Manager", employee.reportingManager);
                    command.Parameters.AddWithValue("@Joining_Date", ((OnPayroll)employee).joiningDate);
                    command.Parameters.AddWithValue("@Experience", ((OnPayroll)employee).exp);
                    command.Parameters.AddWithValue("@Basic_salary", ((OnPayroll)employee).basicSalary);
                    command.Parameters.AddWithValue("@DA", ((OnPayroll)employee).da);
                    command.Parameters.AddWithValue("@HRA", ((OnPayroll)employee).hra);
                    command.Parameters.AddWithValue("@PF", ((OnPayroll)employee).pf);
                    command.Parameters.AddWithValue("@NetSalary", ((OnPayroll)employee).netSalary);

                }
                connection.Open();


                int count = command.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("Record has been updated");
                }
                else
                    Console.WriteLine("Some Error came");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
                command.Dispose();
                connection.Dispose();
            }
        }
        public void DeleteEmployee(int id)
        {
            try
            {
                using (connection = GetConnection())
                {
                    using (command = new SqlCommand())
                    {
                        command.CommandText = "Delete from Employee where id=@id";
                        command.Parameters.AddWithValue("@id", id);
                        command.Connection = connection;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            using (connection = GetConnection())
            {
                using (command = new SqlCommand())
                {
                    command.CommandText = "Select * from employee";
                    command.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            employees.Append(new Employee((int)reader["id"], reader["name"].ToString(), reader[2].ToString()));

                        }
                    }

                }
            }
            return employees;
        }
        public Employee GetEmployee(int id)
        {
            Employee emp = null;
            using (connection = GetConnection())
            {
                using (command = new SqlCommand())
                {
                    command.CommandText = "Select * from employee where id=@id";
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {



                            emp = (Employee)reader["id"];

                        }
                    }


                }
            }

            return emp;
        }
    }
}
