using System.ComponentModel.Design;

namespace LinqExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            int[] age = { 12, 30, 32, 34, 53, 11, 2, 3, 4, 66 };

            //Select * from age where age > 18 in sql

            var a=from i in age where i > 18 select i;


            foreach(var i in a)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("****************");
            Console.WriteLine("ascending order");

            //var b=from i in age where i>20 orderby i descending select i;  //desc order
            var b = from i in age where i > 20 orderby i  select i;

            foreach (var i in b)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("desc order");

            var c=from i in age where i>20 orderby i descending select i;  //desc order
           

            foreach (var i in c)
            {
                Console.WriteLine(i);
            }
        }
    }
}
