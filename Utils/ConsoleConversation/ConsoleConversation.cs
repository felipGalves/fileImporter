using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fileImporter.DTO;
using fileImporter.Interfaces;
using fileImporter.Models;
using fileImporter.Repositories;
using fileImporter.Utils.ConsoleConversation.Enums;
using Spectre.Console;

namespace fileImporter.Utils.ConsoleConversation
{
    public class ConsoleConversation
    {
        private UserChoices _userChoice { get; set; }

        private CustomerRepository _customer;

        public ConsoleConversation(
            CustomerRepository customerRepository
        )
        {
            this._customer = customerRepository;
        }

        public void ShowMenuOptions()
        {

            var rule = new Rule("[red]MENU[/]");
            rule.LeftJustified();
            AnsiConsole.Write(rule);

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .AddChoices(Converters.GetDescriptionsFromEnum<UserChoices>())
            );

            this._userChoice = Converters.GetValueFromDescription<UserChoices>(choice);
        }

        public async Task ExecuteChoice()
        {
            switch (this._userChoice)
            {
                case UserChoices.CustomerListing:
                    CustomerListing();
                    break;
                case UserChoices.RegisterNewCustomer:
                    await RegisterNewCustomer();
                    break;
                case UserChoices.DeleteAllCustomers:
                    await DeleteAllCustomers();
                    break;
                case UserChoices.ClearHistory:
                    ClearHistory();
                    break;
            }
        }

        private void ClearHistory()
        {
            AnsiConsole.Clear();
        }

        private async Task DeleteAllCustomers()
        {
            if (!AnsiConsole.Confirm("Are you sure you want to delete all customers?").ToString().ToUpper().Equals("S"))
            {
                await AnsiConsole.Progress()
                    .StartAsync(async ctx => 
                    {
                        var task1 = ctx.AddTask("[red]deleting all customers...[/]");
                        var task2 = _customer.RemoveAllAsync();

                        while(!ctx.IsFinished) 
                        {
                            await task2;
                            await Task.Delay(6);

                            // Increment
                            task1.Increment(1.5);
                        }
                });
            }
        }

        private void CustomerListing(int? customerID = null)
        {
            var customers = this._customer
                .GetAllAsync()
                .GetAwaiter()
                .GetResult();

            this.BuildTable(customers, customerID);
        }

        private void BuildTable(List<Customer> customers, int? customerID = null)
        {
            var table = new Table();

            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Address");
            table.AddColumn("Email");
            table.AddColumn("Phone");
            table.AddColumn("City");

            string cellColor = string.Empty;

            foreach (var customer in customers)
            {
                if (customerID != null && customer.ID == customerID)
                    cellColor = "green";

                table.AddRow(
                    BuildTableCell(customer.ID.ToString(), cellColor),
                    BuildTableCell(customer.Name, cellColor),
                    BuildTableCell(customer.Address, cellColor),
                    BuildTableCell(customer.Email, cellColor),
                    BuildTableCell(customer.Phone.ToString(), cellColor),
                    BuildTableCell(customer.City, cellColor)
                );
            };

            AnsiConsole.Write(table);
        }

        private async Task RegisterNewCustomer()
        {
            var rule = new Rule("[red]Register a New Customer[/]");
            rule.LeftJustified();
            AnsiConsole.Write(rule);

            CustomerDTO customerDTO = new CustomerDTO()
            {
                Name = AnsiConsole.Ask<string>("[green]NAME[/]:"),
                Email = AnsiConsole.Ask<string>("[green]EMAIL[/]:"),
                Address = AnsiConsole.Ask<string>("[green]ADDRESS[/]:"),
                Phone = Converters.ToInt32(AnsiConsole.Ask<string>("[green]PHONE[/]:")),
                City = AnsiConsole.Ask<string>("[green]CITY[/]:")
            };

            Customer? customer = null;

            await AnsiConsole.Progress()
                .StartAsync(async ctx => 
                {
                    var task1 = ctx.AddTask("[yellow]Registering New Customer...[/]");

                    var task2 = _customer
                            .SaveAsync(customerDTO);

                    while(!ctx.IsFinished) 
                    {
                        await Task.Delay(5);
                        await task2;

                         // Increment
                        task1.Increment(1.5);
                    }

                    customer = task2.Result;
            });
            

            var panel = new Panel("[green]Customer Created Successfully[/]");
            panel.Border = BoxBorder.Rounded;

            AnsiConsole.Write(panel);

            CustomerListing(customer?.ID);
        }

        private string BuildTableCell(string valor, string colorStr = null) 
            =>  string.IsNullOrWhiteSpace(colorStr) ? valor : $"[{colorStr}]{valor}[/]";
    }

}