using System;
using System.Linq;

class Program
{
    static void Main()
    {
        StudentContext db = new StudentContext();

        Console.WriteLine("All Students:\n");

        var all = db.Students.ToList();
        foreach (var s in all)
            Console.WriteLine($"{s.Name} {s.Age} {s.Gender}");

        Console.WriteLine("\nAge > 20:\n");

        var result = db.Students
                       .Where(s => s.Age > 20)
                       .OrderBy(s => s.Name);

        foreach (var s in result)
            Console.WriteLine($"{s.Name} {s.Age}");

        Console.WriteLine("\nGroup By Standard:\n");

        var group = db.Students.GroupBy(s => s.Standard);

        foreach (var g in group)
        {
            Console.WriteLine($"Standard {g.Key}");
            foreach (var s in g)
                Console.WriteLine($"   {s.Name}");
        }
    }
}
