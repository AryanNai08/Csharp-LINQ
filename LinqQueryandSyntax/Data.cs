using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqQueryandSyntax
{
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
        public IEnumerable<Employee>? Employees { get; set; }
    }

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

        public static List<Department> GetDepartments()
        {
            return new List<Department>
            {
                new Department { Id = 1, ShortName = "HR", LongName = "Human Resources" },
                new Department { Id = 2, ShortName = "FN", LongName = "Finance" },
                new Department { Id = 3, ShortName = "TE", LongName = "Technology" }
            };
        }

        public static List<Department> GetDepartments(IEnumerable<Employee> employees)
        {
            return new List<Department>
            {
                new Department
                {
                    Id=1, ShortName="HR", LongName="Human Resources",
                    Employees = employees.Where(e=>e.DepartmentId==1)
                },
                new Department
                {
                    Id=2, ShortName="FN", LongName="Finance",
                    Employees = employees.Where(e=>e.DepartmentId==2)
                },
                new Department
                {
                    Id=3, ShortName="TE", LongName="Technology",
                    Employees = employees.Where(e=>e.DepartmentId==3)
                }
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

    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee? x, Employee? y)
        {
            if (x == null || y == null) return false;

            return x.Id == y.Id &&
                   x.FirstName.ToLower() == y.FirstName.ToLower() &&
                   x.LastName.ToLower() == y.LastName.ToLower();
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public static class EnumerableCollectionExtentionMethods
    {
        public static IEnumerable<Employee> GetHighSalariedEmplyees(this IEnumerable<Employee> employees)
        {
            foreach (Employee emp in employees)
            {
                Console.WriteLine($"Accessing employee: {emp.FirstName} {emp.LastName}");
                if (emp.AnnualSalary >= 50000)
                    yield return emp;
            }
        }
    }
}
