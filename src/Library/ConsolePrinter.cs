namespace Library
{
    public class ConsolePrinter : IPrinter
    {
        void PrintTable(Table table)
        {
            Console.WriteLine(table);
        }
    }
}