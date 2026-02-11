using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Student")]
public class Student
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public int Standard { get; set; }
}
