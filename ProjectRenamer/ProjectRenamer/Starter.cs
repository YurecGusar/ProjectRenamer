using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRenamer
{
    public class Starter
    {
        private Creator _creator;
        private ConfigService _config;

        public Starter()
        {
            _creator = new Creator();
            _config = new ConfigService();
        }

        public void Run()
        {
            Console.WriteLine("Name of New Project With StyleCop:");
            var name = Console.ReadLine();
            Console.WriteLine("Path for New Project(not necessary): ");
            var path = Console.ReadLine();
            Console.WriteLine("Add base folders?:");
            foreach (var item in _config.FolderNames)
            {
                Console.WriteLine($"\t{item}");
            }
            Console.Write("(Y/N) :");
            var addFolders = Console.ReadLine();
            _creator.MakeProject(name, addFolders.ToUpper(), path);
        }
    }
}
