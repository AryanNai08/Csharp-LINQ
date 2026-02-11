using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LinqQueryandSyntax
{
    internal class BasicQuery
    {
        public static void Run()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            //select and where operators -- method syntax
            var results = employeeList.Select(e => new
            {
                FullName = e.FirstName + " " + e.LastName,
                AnnualSalary = e.AnnualSalary
            }).Where(e => e.AnnualSalary > 50000);

            foreach (var result in results)
            {
                Console.WriteLine($"{result.FullName,-20} : {result.AnnualSalary,10}");
            }


            //select and where operators -- query syntax
            var results1 = from emp in employeeList
                           where emp.AnnualSalary >= 50000
                           select new
                           {
                               FullName = emp.FirstName + " " + emp.LastName,
                               AnnualSalary = emp.AnnualSalary
                           };

            foreach (var result in results1)
            {
                Console.WriteLine($"{result.FullName,-20} : {result.AnnualSalary,10}");
            }


            //Deferred execution of LINQ queries
            var results2 = from emp in employeeList.GetHighSalariedEmplyees()
                           select new
                           {
                               FullName = emp.FirstName + " " + emp.LastName,
                               AnnualSalary = emp.AnnualSalary
                           };

            employeeList.Add(new Employee
            {
                Id = 5,
                FirstName = "Sam",
                LastName = "Davis",
                AnnualSalary = 100000.20m,
                IsManager = true,
                DepartmentId = 2
            });

            foreach (var result in results2)   //above new added employee added in results2 because of deferred execution
            {
                Console.WriteLine($"{result.FullName,-20} : {result.AnnualSalary,10}");
            }


            //Immediate execution of LINQ queries
            var results3 = (from emp in employeeList.GetHighSalariedEmplyees()
                            select new
                            {
                                FullName = emp.FirstName + " " + emp.LastName,
                                AnnualSalary = emp.AnnualSalary
                            }).ToList();

            employeeList.Add(new Employee
            {
                Id = 5,
                FirstName = "Sam",
                LastName = "Davis",
                AnnualSalary = 100000.20m,
                IsManager = true,
                DepartmentId = 2
            });

            foreach (var result in results3) //above new added employee not added in results3 because of immediate execution
            {
                Console.WriteLine($"{result.FullName,-20} : {result.AnnualSalary,10}");
            }

            // join oprerator - method syntax
            var results4 = departmentList.Join(employeeList,
                department => department.Id,
                employee => employee.DepartmentId,
                (department, employee) => new
                {
                    FullName = employee.FirstName + " " + employee.LastName,
                    AnnualSalary = employee.AnnualSalary,
                    DepartmentName = department.LongName
                });

            // join operator - query syntax
            var results5 = from emp in employeeList
                           join dept in departmentList
                           on emp.DepartmentId equals dept.Id
                           select new
                           {
                               FullName = emp.FirstName + " " + emp.LastName,
                               AnnualSalary = emp.AnnualSalary,
                               DepartmentName = dept.LongName
                           };

            foreach (var result in results5)
            {
                Console.WriteLine($"{result.FullName,-20} : {result.AnnualSalary,10} : {result.DepartmentName,-20}");
            }


            //groupjoin operator - method syntax
            var results6 = departmentList.GroupJoin(employeeList,
                dept => dept.Id,
                emp => emp.DepartmentId,
                (dept, employeeGroup) => new
                {
                    Employees = employeeGroup,
                    DepartmentName = dept.LongName
                });

            //groupjoin operator - query syntax
            var results7 = from dept in departmentList
                           join emp in employeeList
                           on dept.Id equals emp.DepartmentId
                           into employeeGroup
                           select new
                           {
                               Employees = employeeGroup,
                               DepartmentName = dept.LongName
                           };

            foreach (var item in results7)
            {
                Console.WriteLine($"Department Name : {item.DepartmentName}");
                foreach (var emp in item.Employees)
                {
                    Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
                }
            }
        }
    }
}
