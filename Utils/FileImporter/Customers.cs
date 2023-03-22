using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fileImporter.Data;
using fileImporter.Interfaces;
using fileImporter.Models;
using fileImporter.Repositories;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace fileImporter.Utils.FileImporter
{
    public class Customers
    {
        public string _directory { get; set; }

        public Customers(string directory)
        {
            this._directory = directory;
        }

        public async void ImportCustomers()

        {
            // Abre a planilha criada
            var arquivoExcel = new ExcelPackage(new FileInfo(_directory));

            // Localiza e planilha a ser acessada
            ExcelWorksheet customersFile = arquivoExcel.Workbook.Worksheets.FirstOrDefault();

            // Obtem o numero de linhas e colunas
            int rows = customersFile.Dimension.Rows;
            int cols = customersFile.Dimension.Columns;

            // Lista de clientes
            List<Customer> customers = new List<Customer>();

            // Contagem de registros
            int contagemRegistros = 0;

            using (var db = new FileImporterContext())
            {
                db.ChangeTracker.AutoDetectChangesEnabled = false;

                var customerRepository = new CustomerRepository(db);

                // percorre as linhas e colunas da planilha
                for (int i = 2; i <= rows; i++)
                {
                    Customer customer = new Customer();

                    for (int j = 1; j <= cols; j++)
                    {
                        switch (j)
                        {
                            case 2:
                                customer.Name = customersFile.Cells[i, j].Text.ToString();
                                break;
                            case 3:
                                customer.Address = customersFile.Cells[i, j].Text.ToString();
                                break;
                            case 4:
                                customer.Email = customersFile.Cells[i, j].Text.ToString();
                                break;
                            case 5:
                                customer.Phone = Converters.ToInt32(customersFile.Cells[i, j].Text);
                                break;
                            case 6:
                                customer.City = customersFile.Cells[i, j].Text.ToString();
                                break;
                            default:
                                break;
                        }
                    }

                    customers.Add(customer);
                    contagemRegistros++;

                    if (contagemRegistros % 4000 == 0)
                    {
                        contagemRegistros = 0;

                        await customerRepository.SaveRangeAsync(customers);
                        customers.Clear();
                    }
                }

                if (customers.Any()) 
                    await customerRepository.SaveRangeAsync(customers);
            }
        }


        public void ExportCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer() { ID = 1, Name = "teste", Address = "teste", City = "teste", Email = "teste", Phone=123  }
            };

            // Define a licença
            // Cria instância do ExcelPackage 
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();

            // Nome da planilha
            var workSheet = excel.Workbook.Worksheets.Add("PlanilhaClientes");

            // Define propriedades da planilha
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            // Define propriedades da primeira linha
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            // Define o cabeçalho da planilha(base 1)
            workSheet.Cells[1, 1].Value = "Cod.";
            workSheet.Cells[1, 2].Value = "Name";
            workSheet.Cells[1, 3].Value = "Address";
            workSheet.Cells[1, 4].Value = "Email";
            workSheet.Cells[1, 5].Value = "Phone";
            workSheet.Cells[1, 6].Value = "City";

            int index = 2;
            foreach (var customer in customers)
            {
                workSheet.Cells[index, 1].Value = customer.ID;
                workSheet.Cells[index, 2].Value = customer.Name;
                workSheet.Cells[index, 3].Value = customer.Address;
                workSheet.Cells[index, 4].Value = customer.Email;
                workSheet.Cells[index, 5].Value = customer.Phone;
                workSheet.Cells[index, 6].Value = customer.City;
                index++;
            }

            // Ajusta o tamanho da coluna
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();

            // Se o arquivo existir exclui
            if (File.Exists(_directory))
                File.Delete(_directory);

            // Cria o arquivo excel no disco fisico
            FileStream objFileStrm = File.Create(_directory);
            objFileStrm.Close();

            // Escreve o conteudo para o arquivo excel
            File.WriteAllBytes(_directory, excel.GetAsByteArray());

            //Fecha o arquivo excel
            excel.Dispose();
            Console.WriteLine($"Planilha criada com sucesso em : {_directory}\n");
        }
    }
}