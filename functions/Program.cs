using System;
using functions.Data.Structure.Rectangle;
using functions.Data.Structure.Triangle;


class Program
{
    private static Rectangle? recentRectangle;
    private static Triangle? recentTriangle;

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            DisplayLastShapes();
            
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Calcular área do Retângulo");
            Console.WriteLine("2. Calcular área do Triângulo");
            Console.WriteLine("3. Sair");
            Console.Write("Opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CalculateAndUpdateRectangle(); 
                    break;
                case "2":
                    CalculateAndUpdateTriangle();
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void DisplayLastShapes()
{
    Console.WriteLine("\n----------------------------------------\n");
    Console.WriteLine("Últimos cálculos:");
    
    if (recentRectangle != null)
    {
        Console.WriteLine($"Retângulo: Altura = {recentRectangle.GetHeight()}, " +
                        $"Largura = {recentRectangle.GetWidth()}, " +
                        $"Área = {recentRectangle.CalculateArea()}");
    }
    else
    {
        Console.WriteLine("Retângulo: Nenhum cálculo realizado");
    }

    if (recentTriangle != null) 
    {
        Console.WriteLine($"Altura = {recentTriangle.GetHeight()}, " +
                        $"Triângulo: Base = {recentTriangle.GetBaseLength()}, " +
                        $"Área = {recentTriangle.CalculateArea():F2}");
    }
    else
    {
        Console.WriteLine("Triângulo: Nenhum cálculo realizado");
    }
    
    Console.WriteLine("\n----------------------------------------\n");
}

    static void CalculateAndUpdateRectangle()
    {
        Console.WriteLine("\nCálculo do Retângulo");
        var (height, width) = ReadRectangleDimensions();
        
        if (recentRectangle == null)
        {
            recentRectangle = new Rectangle(height, width);
        }
        else
        {
            recentRectangle.UpdateDimensions(height, width);
        }
        
        Console.WriteLine($"Área atualizada: {recentRectangle.CalculateArea()}");
        Console.ReadKey();
    }


    static void CalculateAndUpdateTriangle()
{
    Console.WriteLine("\nCálculo do Triângulo");
    var (baseLength, height) = ReadTriangleDimensions();
    
    if (recentTriangle == null)
    {
        recentTriangle = new Triangle(baseLength, height);
    }
    else
    {
        recentTriangle.UpdateDimensions(baseLength, height);
    }
    
    Console.WriteLine($"Área atualizada: {recentTriangle.CalculateArea():F2}");
    Console.ReadKey();
}

    static (int height, int width) ReadRectangleDimensions()
    {
        int height = ReadInteger("Digite a altura do retângulo: ");
        int width = ReadInteger("Digite a largura do retângulo: ");
        return (height, width);
    }

    static (int baseLength, int height) ReadTriangleDimensions()
    {
        int baseLength = ReadInteger("Digite a base do triângulo: ");
        int height = ReadInteger("Digite a altura do triângulo: ");
        return (baseLength, height);
    }

    static int ReadInteger(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value) && value > 0)
            {
                return value;
            }
            Console.WriteLine("Valor inválido. Digite um número inteiro positivo.");
        }
    }
}