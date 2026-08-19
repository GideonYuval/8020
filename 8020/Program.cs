using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8020
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            DateTime d1 = new DateTime(2019, 6, 1);
            DateTime d2 = new DateTime(2022, 8, 1);
            Console.WriteLine(d2.Year-d1.Year);
            Console.WriteLine(DateTime.Now);
            */


            Employee e1 = new Employee("Dan", new DateTime(2015, 1, 1), 5000);
            Console.WriteLine(e1);
            Employee e2 = new Employee("Amit");
            Console.WriteLine(e2);
            Console.WriteLine(Employee.GetNumEmployees());

            Manager m1 = new Manager("Gil", new DateTime(2018, 6, 6), 20000, "HW manager");
            Console.WriteLine(m1);

            Manager m2 = new Manager("Yossi", "SW manager");
            Console.WriteLine(m2);

            Manager m3 = new Manager("Big Boss");
            Console.WriteLine(m3);
            m1.Raise(2000, "Good work");

            e1.Work();
            //e1.Work("Secret"); //Employee doesn't have this
            m1.Work();
            m1.Work("Secret");
            m1.Quiz();

            Company c1 = new Company("Intel", 100);
            Company c2 = new Company("Microsoft", 50);

            c1.AddEmployee(e1);
            c2.AddEmployee(e2);
        }
    }

    //encapsuation - כימוס
    //abstraction
    //inheritance 
    public class Employee
    {
        //the following attributes start off as private. We change them to protected only when we add the Manager class
        protected string name;
        protected int salary;
        private DateTime startDate;
        private const int defaultSalary = 10000;
        private static int numEmployees = 0;



        public Employee(string name, DateTime startDate, int salary)
        {
            this.name = name;
            this.startDate = startDate;
            this.salary = salary;
            numEmployees++;
        }

        public Employee() : this("EinShem", DateTime.Now, defaultSalary)
        {

        }

        public Employee(string name) : this(name, DateTime.Now, defaultSalary) { }



        public virtual int Bonus()
        {
            return (DateTime.Now.Year - this.startDate.Year) * 1000;
        }


        public override string ToString()
        {
            //return base.ToString();
            return $"{this.name} earns {this.salary} Shekels, started on {this.startDate}. Bonus = {this.Bonus()}";
        }

        public static int GetNumEmployees() { return numEmployees; }

        public void Work()
        {
            Console.WriteLine($"{name} is working on general tasks.");
        }

    }


    public class Manager : Employee
    {
        private string title;

        //this is added only at the end of the question.
        private Employee[] managed; 
        private const int maxManaged = 10;

        public Manager(string name, DateTime startDate, int salary, string title) : base(name, startDate, salary) //explicitly
        {
            this.title = title;
            managed = new Employee[maxManaged]; //this is added only at the end of the question.

        }

        public Manager(string name, string title) : base(name)
        {
            this.title = title;
        }

        public Manager(string title)
        //the parameterless Base (Employee) constructor is called implicitly (in our case, the parameterless ctor chain another ctor)
        //this is equivilant to public Manager(string name) : base()
        {
            this.title = title;
        }

        
        public void Raise(int raise)
        {
            salary += raise; //requires salary to be protected, not private
        }

        public override int Bonus()
        {
            //base.Bonus() calls the Employee Bonus
            //without "base", we'd just call Manager.Bonus() recursively
            return (base.Bonus() * 3);
        }

        public override string ToString()
        {
            //return base.ToString();
            return base.ToString() + $", Title = {this.title}";
        }

        public void Raise(int raise, string reason) //overload Raise()
        {
            this.salary += raise;
            Console.WriteLine($"Granted {name} {raise} NIS raise for: {reason}");
        }

        public void Work(string project)
        {
            Console.WriteLine($"{name} is managing the {project} project.");
        }

        public void Quiz()
        {
            this.Work();
            this.Work("Alpha");
        }

    }





    class Company
    {
        private string name; //company name
        private Employee[] employees;
        private int employeeCount;


        public Company(string name, int maxEmployees)
        {
            this.name = name;
            this.employees = new Employee[maxEmployees];
            employeeCount = 0;
        }


        public bool AddEmployee(Employee emp)
        {
            if (employeeCount < employees.Length && emp != null)
            {
                employees[employeeCount] = emp;
                employeeCount++;
                return true;
            }
            return false;
        }

    }


}
