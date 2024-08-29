

using ORM2.Data;
using ORM2.Models;



var db = new Database("Data Source = DESKTOP-VKCKL67\\MSSQLSERVER02; Initial Catalog = Library; Integrated Security = true");
Console.WriteLine("Telebe ID-nizi daxil edin:");
int StudentId = int.Parse(Console.ReadLine());
    


    while (true)
    {
        Console.WriteLine("Menyu:");
        Console.WriteLine("1. Butun kitablari gor");
        Console.WriteLine("2. Goturduyunuz kitablari gor");
        Console.WriteLine("3. Kitab gotur:");
        Console.WriteLine("4. Kitab qaytar");
        Console.WriteLine("5. Cixis");
        Console.Write("Secim edin: ");
        string choice = Console.ReadLine();
        
        switch (choice)
        {
            case "1":
            List<Book> Books= db.getAllBooks();
            foreach (var item in Books)
            {
                Console.WriteLine(item);
            }
            db.getAllBooks();
                break;
            case "2":
            List<Book> takenBooks=db.takenBooks(StudentId);
            foreach (var item in takenBooks)
            {
                Console.WriteLine(item);
            }
            break;
            case "3":
                Console.WriteLine("Kitab ID-i daxil edin:");
                int TakingBookId = int.Parse(Console.ReadLine());
                db.TakeBook(StudentId,TakingBookId);
                break;
            case "4":
                Console.WriteLine("Kitab ID-i daxil edin:");
                int ReturningBookId = int.Parse(Console.ReadLine());
                db.returnBook(StudentId, ReturningBookId);
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Yanlıs secim.");
                break;
        }
    }



//Console.WriteLine();

