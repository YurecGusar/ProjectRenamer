using System;
namespace ProjectRenamer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Name of New Project With StyleCop:");
            var name = Console.ReadLine();
            Console.WriteLine("Path for New Project(not necessary): ");
            var path = Console.ReadLine();
            Console.WriteLine("Add base folders? (Y/N)");
            var addFolders = Console.ReadLine();
            new Creator().MakeProject(name, addFolders.ToUpper(), path);
        }
    }
}
