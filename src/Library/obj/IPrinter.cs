using System;

namespace Library
{
    public interface IPrinter
    {
        void PrintTable(Table table);
        {
            Console.WriteLine(table);
        }
    }
}