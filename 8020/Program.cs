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

            Manager m1 = new Manager("Gil", new DateTime(2018, 6, 6), 20000, "HW manager");
            Manager m2 = new Manager("Yossi");

            Console.WriteLine(m1);
            Console.WriteLine(m2);

            Manager m3 = new Manager(30000);
            Manager m4 = new Manager();

            Guard g1 = new Guard("John", new DateTime(2020, 8, 1), 3000, "Night");
            Console.WriteLine(g1);
            Guard g2 = new Guard("Tim", "Day");
            Console.WriteLine(g2);

            CEO c1 = new CEO("Alice", new DateTime(2010, 1, 1), 100000, "Chief Executive Officer", 5000);
            Console.WriteLine(c1); // Outputs: Alice earns 100000 Shekels, started on 01/01/2010. Bonus = 50000, Title = Chief Executive Officer, Vacation = 30, Stock Options = 5000

            // Default CEO
            CEO c2 = new CEO();
            Console.WriteLine(c2); // Outputs: Unknown CEO earns 50000 Shekels, started on (current date). Bonus = (depends on current year) + Stock Options = 10000
        }
    }

    //encapsuation - כימוס
    //abstraction
    //inheritance 
    public class Employee
    {
        protected string name;
        protected int salary;
        protected DateTime startDate;
        protected const int defaultSalary = 10000;

        protected static int numEmployees = 0;



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

        //all employees get vacation based on # of years worked, multiplied by mult (depends on type of employee)
        //default is 1
        public int Vacation(int mult = 1)
        {
            return (DateTime.Now.Year - this.startDate.Year) * mult;
        }

        public override string ToString()
        {
            //return base.ToString();
            return $"{this.name} earns {this.salary} Shekels, started on {this.startDate}. Bonus = {this.Bonus()}, vacation = {this.Vacation()}";
        }

    }


    public class Manager : Employee
    {
        private string title;

        //this is added only at the end of the question.
        private Employee[] managed; 
        private const int maxManaged = 10;

        public Manager() { }

        public Manager(string name, DateTime startDate, int salary, string title) : base(name, startDate, salary) //explicitly
        {
            this.title = title;
            managed = new Employee[maxManaged]; //this is added only at the end of the question.

        }

        public Manager(string name) : base(name)
        {
            this.title = "Big Boss";
        }

        public Manager(int salary) //the parameterless constructor is called implicitly
        //this is equivilant to public Manager(int salary) : base()
        {
            this.title = "Boss";
            this.salary = salary;
        }

        public override string ToString()
        {
            //return base.ToString();
            return base.ToString() + $", Title = {this.title}, Vacation = {Vacation(2)}";
        }


        public override int Bonus()
        {
            return (base.Bonus() * 3);
        }
    }



    public class Guard : Employee
    {
        private string shift; // e.g., "Morning", "Evening", "Night"

        // Constructor
        public Guard(string name, DateTime startDate, int salary, string shift) : base(name, startDate, salary)
        {
            this.shift = shift;
        }

        // Overloaded constructor with default shift
        public Guard(string name, string shift) : base(name)
        {
            this.shift = shift;
        }

        // Override Bonus method
        public override int Bonus()
        {
            return 500; // Fixed bonus amount for guards
        }

        public override string ToString()
        {
            return base.ToString() + $", Shift = {this.shift}";
        }
    }


    public class CEO : Manager
    {
        private int stockOptions; // New field specific to CEO

        // Constructor with full initialization
        public CEO(string name, DateTime startDate, int salary, string title, int stockOptions)
            : base(name, startDate, salary, title)
        {
            this.stockOptions = stockOptions;
        }

        // Constructor with default values
        public CEO() : base("Unknown CEO", DateTime.Now, 50000, "CEO")
        {
            this.stockOptions = 10000; // Default stock options
        }

        // Override the Bonus method
        public override int Bonus()
        {
            // Base Bonus (from Manager) + stock options * 10
            return base.Bonus() + (this.stockOptions * 10);
        }

        public override string ToString()
        {
            // Add stock options to the ToString method
            return base.ToString() + $", Stock Options = {this.stockOptions}";
        }
    }

    class Company
    {
        string name; //company name
        CEO c;
        Employee[] employees;
        Manager[] managers;
        Guard[] guards;

        public Company(string name, CEO c, int maxEmployees)
        {
            this.name = name;
            this.c = c;
            Employee[] employees = new Employee[maxEmployees];
            Manager[] managers = new Manager[maxEmployees];
            Guard[] guards = new Guard[maxEmployees];
        }

    }


}
