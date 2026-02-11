using System.Data.Entity;

public class RecordContext : DbContext
{
    public RecordContext() : base("name=RecordContext")
    {
    }

    public DbSet<Record> Records { get; set; }
}
