using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqQueryandSyntax
{
    public class AggregateDemo
    {
        public static void Run()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments(employeeList);

            // =========================================================
            // 1. SEQUENCEEQUAL (Compare two lists)
            // METHOD SYNTAX
            // =========================================================
            var list2 = Data.GetEmployees();

            bool same = employeeList.SequenceEqual(list2, new EmployeeComparer());
            Console.WriteLine($"Both lists same? {same}");



            // =========================================================
            // 2. CONCAT (Merge two lists)
            // METHOD SYNTAX
            // =========================================================
            List<Employee> newEmployees = new List<Employee>
            {
                new Employee{Id=5, FirstName="Tony", LastName="Stark"},
                new Employee{Id=6, FirstName="Steve", LastName="Rogers"}
            };

            var concatResult = employeeList.Concat(newEmployees);

            Console.WriteLine("\nConcat Result:");
            foreach (var e in concatResult)
                Console.WriteLine($"{e.Id} {e.FirstName}");



            // =========================================================
            // 3. COUNT, SUM, AVERAGE, MAX
            // METHOD SYNTAX
            // =========================================================
            int count = employeeList.Count();
            decimal totalSalary = employeeList.Sum(e => e.AnnualSalary);
            decimal avgSalary = employeeList.Average(e => e.AnnualSalary);
            decimal maxSalary = employeeList.Max(e => e.AnnualSalary);

            Console.WriteLine($"\nTotal Employees: {count}");
            Console.WriteLine($"Total Salary: {totalSalary}");
            Console.WriteLine($"Average Salary: {avgSalary}");
            Console.WriteLine($"Max Salary: {maxSalary}");



            // =========================================================
            // 4. DISTINCT (Remove duplicates)
            // METHOD SYNTAX
            // =========================================================
            List<int> numbers = new List<int> { 1, 2, 2, 3, 4, 4, 5 };

            var distinct = numbers.Distinct();
            Console.WriteLine("\nDistinct numbers:");
            foreach (var n in distinct)
                Console.WriteLine(n);



            // =========================================================
            // 5. TAKE & SKIP (Paging concept)
            // METHOD SYNTAX
            // =========================================================
            var firstTwo = employeeList.Take(2);
            Console.WriteLine("\nTake first 2:");
            foreach (var e in firstTwo)
                Console.WriteLine(e.FirstName);

            var skipTwo = employeeList.Skip(2);
            Console.WriteLine("\nSkip first 2:");
            foreach (var e in skipTwo)
                Console.WriteLine(e.FirstName);



            // =========================================================
            // 6. CONVERSION OPERATOR (ToList)
            // QUERY SYNTAX
            // =========================================================
            var highSalary = (from e in employeeList
                              where e.AnnualSalary > 50000
                              select e).ToList();

            Console.WriteLine("\nHigh salary employees:");
            foreach (var e in highSalary)
                Console.WriteLine(e.FirstName);



            // =========================================================
            // 7. SELECT vs SELECTMANY
            // METHOD SYNTAX
            // =========================================================

            // Select → returns collection inside collection
            var selectDept = departmentList.Select(d => d.Employees);
            Console.WriteLine("\nSelect result:");
            foreach (var dept in selectDept)
                if (dept != null)
                    foreach (var emp in dept)
                        Console.WriteLine(emp.FirstName);

            // SelectMany → flatten result
            var selectManyDept = departmentList.SelectMany(d => d.Employees ?? Enumerable.Empty<Employee>());
            Console.WriteLine("\nSelectMany result:");
            foreach (var emp in selectManyDept)
                Console.WriteLine(emp.FirstName);



            // =========================================================
            // 8. LET KEYWORD
            // QUERY SYNTAX
            // =========================================================
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
        }
    }
}
