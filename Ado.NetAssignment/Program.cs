using BusinessObject;

namespace Ado.NetAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BAL.Bal bal = new BAL.Bal();

            byte ch;
            char choice = 'y';
            while (choice == 'y')
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine("1.Add Record");
                Console.WriteLine("2.Delete Record ");
                Console.WriteLine("3.Edit Record");
                Console.WriteLine("4.List of Employee");
                Console.WriteLine("5.Search Employee By ID");
                Console.WriteLine("Enter Your Choice");
                ch = byte.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        {
                            Console.WriteLine("Press\n1.Oncontract basis\n2.on payroll");
                            int x = Convert.ToInt32(Console.ReadLine());

                            if (x == 1)
                            {
                                Employee onContract = new OnContract();

                                onContract.GetDetails();
                                if (onContract is OnContract)
                                {
                                    CalculateOnContract(((OnContract)onContract).duration, ((OnContract)onContract).charges);
                                }

                                Console.WriteLine(bal.AddEmployee(onContract));





                            }
                            else if (x == 2)
                            {


                                Employee onPayroll = new OnPayroll();
                                onPayroll.GetDetails();
                                if (onPayroll is OnPayroll)
                                    CalculateOnPayroll(((OnPayroll)onPayroll).basicSalary, ((OnPayroll)onPayroll).exp);

                                Console.WriteLine(bal.AddEmployee(onPayroll));



                            }

                            else
                            {
                                Console.WriteLine("Enter a valid Option");
                            }
                            break;
                        }
                    case 2:
                        {

                            //List<Employee> list = dal.GetEmpoloyees();
                            //Console.WriteLine(list.Count);
                            //foreach (Employee employee in list)
                            //{
                            //    Console.WriteLine(employee.name);
                            //}
                            Console.WriteLine("Enter the employee id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            bal.DeleteEmployee(id);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Press\n1.Oncontract basis\n2.on payroll");
                            int x = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter employee id:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            if (x == 1)
                            {
                                Employee onContract = new OnContract();

                                onContract.GetDetails();
                                if (onContract is OnContract)
                                {
                                    CalculateOnContract(((OnContract)onContract).duration, ((OnContract)onContract).charges);
                                }

                                Console.WriteLine(bal.EditEmployee(id, onContract));





                            }
                            else if (x == 2)
                            {


                                Employee onPayroll = new OnPayroll();
                                onPayroll.GetDetails();
                                if (onPayroll is OnPayroll)
                                    CalculateOnPayroll(((OnPayroll)onPayroll).basicSalary, ((OnPayroll)onPayroll).exp);

                                Console.WriteLine(bal.EditEmployee(id, onPayroll));



                            }

                            else
                            {
                                Console.WriteLine("Enter a valid Option");
                            }

                            break;
                        }
                    case 4:
                        {
                            List<Employee> list = bal.GetEmployees();
                            foreach (Employee emp in list)
                            {
                                Console.WriteLine(emp);
                            }
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Enter employee id:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Employee emp = bal.GetEmployee(id);
                            Console.WriteLine(emp);
                            break;
                        }
                }
            }
        }
        public static double CalculateOnContract(int days, double chargePerDay)
        {
            return days * chargePerDay;
        }
        public static double CalculateOnPayroll(double basicSalary, byte exp)
        {
            double netSalary;
            if (exp > 10)
            {
                netSalary = (((10 / 100) * basicSalary) + ((8.5 / 100) * basicSalary) - 6500);
            }
            else if (exp > 7 && exp < 10)
            {
                netSalary = (((7 / 100) * basicSalary) + ((6.5 / 100) * basicSalary) - 4100);
            }
            else if (exp > 5 && exp < 7)
            {
                netSalary = (((4.1 / 100) * basicSalary) + ((3.8 / 100) * basicSalary) - 1800);
            }
            else
            {
                netSalary = (((1.9 / 100) * basicSalary) + ((2.0 / 100) * basicSalary) - 1200);
            }
            return netSalary;
        }
    }
}