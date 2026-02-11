using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQExample_Simple
{
    public class GroupbyAndOrderby
    {
        public static void Run()
        {
            List<Employee> employeeList = Data.GetEmployees();

            // =========================================================
            // 1. WHERE + ORDERBY (METHOD SYNTAX)
            // =========================================================
            // Method-based LINQ
            // Filters employees salary > 40000 and sorts by salary
            var result1 = employeeList
                          .Where(e => e.AnnualSalary > 40000)
                          .OrderBy(e => e.AnnualSalary);

            Console.WriteLine("Method Syntax - Salary > 40000");
            foreach (var e in result1)
                Console.WriteLine($"{e.FirstName} {e.AnnualSalary}");



            // =========================================================
            // 2. WHERE + ORDERBY (QUERY SYNTAX)
            // =========================================================
            // Query-based LINQ
            var result2 = from emp in employeeList
                          where emp.AnnualSalary > 40000
                          orderby emp.AnnualSalary
                          select emp;

            Console.WriteLine("\nQuery Syntax - Salary > 40000");
            foreach (var e in result2)
                Console.WriteLine($"{e.FirstName} {e.AnnualSalary}");



            // =========================================================
            // 3. GROUP BY (METHOD SYNTAX)
            // =========================================================
            // Group employees by DepartmentId
            var groupMethod = employeeList.GroupBy(e => e.DepartmentId);

            Console.WriteLine("\nGroup By Department (Method)");
            foreach (var group in groupMethod)
            {
                Console.WriteLine($"Department {group.Key}");
                foreach (var emp in group)
                    Console.WriteLine($"   {emp.FirstName}");
            }



            // =========================================================
            // 4. GROUP BY (QUERY SYNTAX)
            // =========================================================
            var groupQuery = from emp in employeeList
                             group emp by emp.DepartmentId;

            Console.WriteLine("\nGroup By Department (Query)");
            foreach (var group in groupQuery)
            {
                Console.WriteLine($"Department {group.Key}");
                foreach (var emp in group)
                    Console.WriteLine($"   {emp.FirstName}");
            }



            // =========================================================
            // 5. ANY operator (METHOD SYNTAX)
            // =========================================================
            bool anyHighSalary = employeeList.Any(e => e.AnnualSalary > 70000);
            Console.WriteLine($"\nAny salary > 70000: {anyHighSalary}");



            // =========================================================
            // 6. ALL operator (METHOD SYNTAX)
            // =========================================================
            bool allAbove20k = employeeList.All(e => e.AnnualSalary > 20000);
            Console.WriteLine($"All salary > 20000: {allAbove20k}");



            // =========================================================
            // 7. FIRST & FIRSTORDEFAULT
            // =========================================================
            var firstEmp = employeeList.FirstOrDefault(e => e.DepartmentId == 3);

            Console.WriteLine("\nFirst employee in Dept 3:");
            if (firstEmp != null)
                Console.WriteLine(firstEmp.FirstName);
            else
                Console.WriteLine("Not found");



            // =========================================================
            // 8. SINGLE & SINGLEORDEFAULT
            // =========================================================
            var singleEmp = employeeList.SingleOrDefault(e => e.Id == 2);

            Console.WriteLine("\nSingle employee with Id=2:");
            if (singleEmp != null)
                Console.WriteLine(singleEmp.FirstName);
            else
                Console.WriteLine("Not found");



            // =========================================================
            // 9. ELEMENTAT & ELEMENTATORDEFAULT
            // =========================================================
            var empIndex = employeeList.ElementAtOrDefault(3);

            Console.WriteLine("\nElement at index 3:");
            if (empIndex != null)
                Console.WriteLine(empIndex.FirstName);
            else
                Console.WriteLine("No element");



            // =========================================================
            // 10. OFTYPE operator
            // =========================================================
            ArrayList mixed = Data.GetHeterogeneousDataCollection();

            // Method syntax
            var onlyInts = mixed.OfType<int>();

            Console.WriteLine("\nOnly integers from mixed collection:");
            foreach (var i in onlyInts)
                Console.WriteLine(i);
        }
    }



    // =========================================================
    // DATA SOURCE CLASS
    // =========================================================
    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee{ Id=1, FirstName="Bob", LastName="Jones", AnnualSalary=60000, IsManager=true, DepartmentId=1},
                new Employee{ Id=2, FirstName="Sarah", LastName="Jameson", AnnualSalary=80000, IsManager=true, DepartmentId=3},
                new Employee{ Id=3, FirstName="Douglas", LastName="Roberts", AnnualSalary=40000, IsManager=false, DepartmentId=2},
                new Employee{ Id=4, FirstName="Jane", LastName="Stevens", AnnualSalary=30000, IsManager=false, DepartmentId=3}
            };
        }

        public static ArrayList GetHeterogeneousDataCollection()
        {
            ArrayList arr = new ArrayList();
            arr.Add(100);
            arr.Add("Hello");
            arr.Add(200);
            arr.Add(new Employee { Id = 10, FirstName = "Test", LastName = "User", AnnualSalary = 50000, DepartmentId = 1 });
            return arr;
        }
    }



    // =========================================================
    // EMPLOYEE CLASS (MODEL)
    // =========================================================
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public bool IsManager { get; set; }
        public int DepartmentId { get; set; }
    }
}
