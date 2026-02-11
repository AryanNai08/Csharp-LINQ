using System.Data.Entity;

public class StudentContext : DbContext
{
    public StudentContext() : base("name=StudentContext")
    {
    }

    public DbSet<Student> Students { get; set; }
}
