using System;
using System.Linq;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== STUDENT RECORD CRUD =====");
            Console.WriteLine("1. Insert");
            Console.WriteLine("2. View All");
            Console.WriteLine("3. Update");
            Console.WriteLine("4. Delete");
            Console.WriteLine("5. Search");
            Console.WriteLine("0. Exit");
            Console.Write("Enter choice: ");

            int ch = Convert.ToInt32(Console.ReadLine());

            switch (ch)
            {
                case 1: Insert(); break;
                case 2: ViewAll(); break;
                case 3: Update(); break;
                case 4: Delete(); break;
                case 5: Search(); break;
                case 0: return;
                default: Console.WriteLine("Invalid"); break;
            }
        }
    }

    // INSERT
    static void Insert()
    {
        using (RecordContext db = new RecordContext())
        {
            Record r = new Record();

            Console.Write("Enter Name: ");
            r.Name = Console.ReadLine();

            Console.Write("Enter Age: ");
            r.Age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Gender: ");
            r.Gender = Console.ReadLine();

            Console.Write("Enter Standard: ");
            r.Standard = Convert.ToInt32(Console.ReadLine());

            db.Records.Add(r);
            db.SaveChanges();

            Console.WriteLine("Inserted Successfully");
        }
    }

    // VIEW
    static void ViewAll()
    {
        using (RecordContext db = new RecordContext())
        {
            var list = db.Records.ToList();

            if (list.Count > 0)
            {
                foreach (var s in list)
                    Console.WriteLine($"{s.Id} {s.Name} {s.Age} {s.Gender} Std:{s.Standard}");
            }
            else
            {
                Console.WriteLine("Table is empty");
            }

        }
    }

    // UPDATE
    static void Update()
    {
        using (RecordContext db = new RecordContext())
        {
            Console.Write("Enter Id to update: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var rec = db.Records.FirstOrDefault(x => x.Id == id);

            if (rec != null)
            {
                Console.Write("New Name: ");
                rec.Name = Console.ReadLine();

                Console.Write("New Age: ");
                rec.Age = Convert.ToInt32(Console.ReadLine());

                Console.Write("New Gender: ");
                rec.Gender = Console.ReadLine();

                Console.Write("New Standard: ");
                rec.Standard = Convert.ToInt32(Console.ReadLine());

                db.SaveChanges();
                Console.WriteLine("Updated Successfully");
            }
            else
            {
                Console.WriteLine("Record not found");
            }
        }
    }

    // DELETE
    static void Delete()
    {
        using (RecordContext db = new RecordContext())
        {
            Console.Write("Enter Id to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var rec = db.Records.FirstOrDefault(x => x.Id == id);

            if (rec != null)
            {
                db.Records.Remove(rec);
                db.SaveChanges();
                Console.WriteLine("Deleted Successfully");
            }
            else
            {
                Console.WriteLine("Record not found");
            }
        }
    }

    // SEARCH
    static void Search()
    {
        using (RecordContext db = new RecordContext())
        {
            Console.Write("Enter name to search: ");
            string name = Console.ReadLine();

            var result = db.Records
                           .Where(x => x.Name.Contains(name))
                           .ToList();

            if (result.Count>0)
            {
                foreach (var r in result)
                    Console.WriteLine($"{r.Id} {r.Name} {r.Age}");
            }
            else
            {
                Console.WriteLine("No record found");
            }
            
        }
    }
}
