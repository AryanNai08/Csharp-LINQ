using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LinqQueryandSyntax
{
    class Aggregate
    {
       public static void Run()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments(employeeList);

            // =====================================================
            // 1. SEQUENCEEQUAL (Compare 2 lists)
            // =====================================================
            // METHOD BASED
            var empList2 = Data.GetEmployees();
            bool same = employeeList.SequenceEqual(empList2, new EmployeeComparer());
            Console.WriteLine($"Both employee lists same? {same}");



            // =====================================================
            // 2. CONCAT (Merge two lists)
            // =====================================================
            // METHOD BASED
            List<Employee> newEmployees = new List<Employee>
            {
                new Employee{Id=5, FirstName="Tony", LastName="Stark"},
                new Employee{Id=6, FirstName="Steve", LastName="Rogers"}
            };

            var concatResult = employeeList.Concat(newEmployees);
            Console.WriteLine("\nConcat Result:");
            foreach (var e in concatResult)
                Console.WriteLine($"{e.Id} {e.FirstName}");



            // =====================================================
            // 3. COUNT, SUM, AVERAGE, MAX (Aggregate operators)
            // =====================================================
            // METHOD BASED
            int count = employeeList.Count();
            decimal totalSalary = employeeList.Sum(e => e.AnnualSalary);
            decimal avgSalary = employeeList.Average(e => e.AnnualSalary);
            decimal maxSalary = employeeList.Max(e => e.AnnualSalary);

            Console.WriteLine($"\nTotal Employees: {count}");
            Console.WriteLine($"Total Salary: {totalSalary}");
            Console.WriteLine($"Average Salary: {avgSalary}");
            Console.WriteLine($"Max Salary: {maxSalary}");



            // =====================================================
            // 4. DISTINCT (remove duplicates)
            // =====================================================
            List<int> numbers = new List<int> { 1, 2, 2, 3, 4, 4, 5 };

            // METHOD BASED
            var distinct = numbers.Distinct();
            Console.WriteLine("\nDistinct numbers:");
            foreach (var n in distinct)
                Console.WriteLine(n);



            // =====================================================
            // 5. TAKE & SKIP (Paging concept)
            // =====================================================
            // METHOD BASED
            var firstTwo = employeeList.Take(2);
            Console.WriteLine("\nTake first 2:");
            foreach (var e in firstTwo)
                Console.WriteLine(e.FirstName);

            var skipTwo = employeeList.Skip(2);
            Console.WriteLine("\nSkip first 2:");
            foreach (var e in skipTwo)
                Console.WriteLine(e.FirstName);



            // =====================================================
            // 6. CONVERSION OPERATORS
            // =====================================================
            // QUERY BASED -> ToList()
            var highSalary = (from e in employeeList
                              where e.AnnualSalary > 50000
                              select e).ToList();

            Console.WriteLine("\nHigh salary employees:");
            foreach (var e in highSalary)
                Console.WriteLine(e.FirstName);



            // =====================================================
            // 7. SELECT vs SELECTMANY
            // =====================================================

            // METHOD: Select (returns collection inside collection)
            var selectDept = departmentList.Select(d => d.Employees);
            Console.WriteLine("\nSelect result:");
            foreach (var dept in selectDept)
                foreach (var emp in dept)
                    Console.WriteLine(emp.FirstName);

            // METHOD: SelectMany (flat result)
            var selectManyDept = departmentList.SelectMany(d => d.Employees);
            Console.WriteLine("\nSelectMany result:");
            foreach (var emp in selectManyDept)
                Console.WriteLine(emp.FirstName);



            // =====================================================
            // 8. LET KEYWORD (Query syntax)
            // =====================================================
            var letQuery = from emp in employeeList
                           let bonus = emp.IsManager ? emp.AnnualSalary * 0.04m : emp.AnnualSalary * 0.02m
                           let total = emp.AnnualSalary + bonus
                           where total > 50000
                           select new
                           {
                               emp.FirstName,
                               TotalSalary = total
                           };

            Console.WriteLine("\nLET keyword result:");
            foreach (var item in letQuery)
                Console.WriteLine($"{item.FirstName} {item.TotalSalary}");

            Console.ReadKey();
        }
    }

    // =====================================================
    // EMPLOYEE COMPARER (used in SequenceEqual, Union etc)
    // =====================================================
    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.Id == y.Id &&
                   x.FirstName.ToLower() == y.FirstName.ToLower() &&
                   x.LastName.ToLower() == y.LastName.ToLower();
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    // =====================================================
    // MODELS
    // =====================================================
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public bool IsManager { get; set; }
        public int DepartmentId { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }

    // =====================================================
    // DATA SOURCE
    // =====================================================
    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee{Id=1,FirstName="Bob",LastName="Jones",AnnualSalary=60000,IsManager=true,DepartmentId=2},
                new Employee{Id=2,FirstName="Sarah",LastName="Jameson",AnnualSalary=80000,IsManager=true,DepartmentId=3},
                new Employee{Id=3,FirstName="Douglas",LastName="Roberts",AnnualSalary=40000,IsManager=false,DepartmentId=1},
                new Employee{Id=4,FirstName="Jane",LastName="Stevens",AnnualSalary=30000,IsManager=false,DepartmentId=3}
            };
        }

        public static List<Department> GetDepartments(IEnumerable<Employee> employees)
        {
            return new List<Department>
            {
                new Department
                {
                    Id=1,ShortName="HR",LongName="Human Resources",
                    Employees = employees.Where(e=>e.DepartmentId==1)
                },
                new Department
                {
                    Id=2,ShortName="FN",LongName="Finance",
                    Employees = employees.Where(e=>e.DepartmentId==2)
                },
                new Department
                {
                    Id=3,ShortName="TE",LongName="Technology",
                    Employees = employees.Where(e=>e.DepartmentId==3)
                }
            };
        }
    }
}
