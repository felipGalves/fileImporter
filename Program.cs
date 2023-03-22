using System;
using fileImporter.Data;
using fileImporter.Repositories;
using fileImporter.Utils;
using fileImporter.Utils.ConsoleConversation;
using Spectre.Console;

namespace fileImporter
{
    public static class Program
    {
        public async static Task Main()
        {
            using (var db = new FileImporterContext())
            {
                var consoleConversation = new ConsoleConversation(
                    new Repositories.CustomerRepository(db)
                );

                AnsiConsole.Clear();

                do {
                    consoleConversation.ShowMenuOptions();
                    await consoleConversation.ExecuteChoice();
                } while (true);
            }
        }
    }
}