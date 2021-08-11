using System;
namespace ProjectRenamer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var starter = new Starter();
            starter.Run();
        }
    }
}
