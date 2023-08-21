using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloLambda
{
    public class Book {
        public string name;
        public double price;
        public List<string> categories = new List<string>();
        public Book(string name, double price)
        {
            this.name = name;
            this.price = price;
        }
        public string UName()
        {
            return this.name.ToUpper();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var book1 = new Book("java tutorial", 100);
            book1.categories = new List<string> { "drama", "romance" };
            var book2 = new Book("haskell", 200);
            book2.categories = new List<string> { "history", "drama" };

            List<Book> books = new List<Book>();
            books.Add(book1);
            books.Add(book2);

            List<string> upperNames = books.Select(b =>
             new
             {
                 name = b.name,
                 price = b.price,
                 shortName = b.name.Substring(0, 3)
             }).Select(a=>a.shortName + "-" + a.name).ToList();


            List<string> categories = books.SelectMany(x => x.categories.Select(a => a)).ToList();

            //drame {java, c}
            //romance {java}

            //history {}

        
            //Console.WriteLine(total);
            Console.Read();

        }
        static string myUpper(string input) {
            return input.ToUpper();
        }
    }
}
