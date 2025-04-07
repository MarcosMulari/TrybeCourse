using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using System.IO;
using Car;
using Tests;

namespace ParkingSystem
{
    class Program
    {
        private static List<Car.Car> parkedCars = new List<Car.Car>();
        private const decimal HourlyRate = 5.00m;
        private const string ExcelFilePath = "ParkingRecords.xlsx";

        static void Main(string[] args)
        {
            LoadDataFromExcel();
    
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE ESTACIONAMENTO ===");
                Console.WriteLine("1. Registrar entrada de veículo");
                Console.WriteLine("2. Registrar saída de veículo");
                Console.WriteLine("3. Visualizar veículos estacionados");
                Console.WriteLine("4. Gerar dados de teste");
                Console.WriteLine("0. Sair do sistema"); 
                Console.Write("Escolha uma opção: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterEntry();
                        break;
                    case "2":
                        RegisterExit();
                        break;
                    case "3":
                        ShowParkedCars();
                        break;
                    case "4":
                        ParkingTests.GenerateTestData(parkedCars);
                        SaveDataToExcel();
                        Console.WriteLine("Dados de teste gerados e salvos com sucesso!");
                        Console.ReadKey();
                        break;
                    case "0":
                        SaveDataToExcel();
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void RegisterEntry()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR ENTRADA ===");
            
            Console.Write("Digite a placa do veículo: ");
            var plate = Console.ReadLine()?.ToUpper();

            if (string.IsNullOrWhiteSpace(plate))
            {
                Console.WriteLine("Placa inválida!");
                Console.ReadKey();
                return;
            }

            if (parkedCars.Any(c => c.Plate == plate && c.IsParked))
            {
                Console.WriteLine($"Veículo com placa {plate} já está estacionado!");
                Console.ReadKey();
                return;
            }

            var newCar = new Car.Car
            {
                Id = Guid.NewGuid(),
                Plate = plate,
                IsParked = true,
                Entry = DateTime.Now,
                Exit = DateTime.MinValue
            };

            parkedCars.Add(newCar);
            
            Console.WriteLine($"\nEntrada registrada para {plate} às {newCar.Entry:HH:mm}");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void RegisterExit()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR SAÍDA ===");
            ShowParkedCars(true);
            Console.Write("Digite a placa do veículo: ");
            var plate = Console.ReadLine()?.ToUpper();

            var car = parkedCars.FirstOrDefault(c => c.Plate == plate && c.IsParked);
            
            if (car == null)
            {
                Console.WriteLine($"Veículo com placa {plate} não encontrado ou já saiu!");
                Console.ReadKey();
                return;
            }

            car.Exit = DateTime.Now;
            car.IsParked = false;
            
            var parkingTime = car.Exit - car.Entry;
            var totalHours = Math.Round(parkingTime.TotalHours);
    
            if (totalHours < 1)
            {
                totalHours = 1;
            }
            var totalToPay = (decimal)totalHours * HourlyRate;

            Console.WriteLine($"\nPlaca: {car.Plate}");
            Console.WriteLine($"Entrada: {car.Entry:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Saída: {car.Exit:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Tempo estacionado: {parkingTime:hh\\:mm\\:ss}");
            Console.WriteLine($"Total a pagar: R$ {totalToPay:F2}");
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void ShowParkedCars(bool FromExit = false)
        {
            Console.Clear();
            Console.WriteLine("=== VEÍCULOS ESTACIONADOS ===");
            
            var currentlyParked = parkedCars.Where(c => c.IsParked).ToList();
            
            if (!currentlyParked.Any())
            {
                Console.WriteLine("Nenhum veículo estacionado no momento.");
            }
            else
            {
                foreach (var car in currentlyParked)
                {
                    Console.WriteLine($"Placa: {car.Plate} - Entrada: {car.Entry:dd/MM/yyyy HH:mm}");
                }
            }
            if (!FromExit)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void LoadDataFromExcel()
        {
            if (!File.Exists(ExcelFilePath))
            {
                return;
            }

            try
            {
                using (var workbook = new XLWorkbook(ExcelFilePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1);

                    foreach (var row in rows)
                    {
                        parkedCars.Add(new Car.Car
                        {
                            Id = Guid.Parse(row.Cell(1).GetString()),
                            Plate = row.Cell(2).GetString(),
                            IsParked = row.Cell(3).GetBoolean(),
                            Entry = row.Cell(4).GetDateTime(),
                            Exit = row.Cell(5).GetDateTime()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados do Excel: {ex.Message}");
            }
        }

        static void SaveDataToExcel()
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Estacionamento");
                    
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Placa";
                    worksheet.Cell(1, 3).Value = "Estacionado";
                    worksheet.Cell(1, 4).Value = "Entrada";
                    worksheet.Cell(1, 5).Value = "Saída";

                    int row = 2;
                    foreach (var car in parkedCars)
                    {
                        worksheet.Cell(row, 1).Value = car.Id.ToString();
                        worksheet.Cell(row, 2).Value = car.Plate;
                        worksheet.Cell(row, 3).Value = car.IsParked;
                        worksheet.Cell(row, 4).Value = car.Entry;
                        worksheet.Cell(row, 5).Value = car.Exit;
                        row++;
                    }

                    workbook.SaveAs(ExcelFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar dados no Excel: {ex.Message}");
            }
        }
    }
}