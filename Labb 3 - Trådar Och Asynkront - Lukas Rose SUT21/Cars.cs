using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3___Trådar_Och_Asynkront___Lukas_Rose_SUT21
{
    public static class Cars
    {
        public static List<Car> cars;
        public static int finishedCount { get; set; } = 0;
        static Decimal TotalDistance { get; set; } = 0.00m;
        public static List<string> Log { get; set; } = new List<string>();

        static Cars()
        {
            cars = new List<Car>();
        }

        public static void AddRange(List<Car> c)
        {
            cars.AddRange(c);
        }

        public static void GetScore()
        {
            while (finishedCount < cars.Count)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter)
                {
                    TotalDistance = 0;
                    Console.SetCursorPosition(0, 3);
                    for (int i = 0; i < 3; i++) { Console.WriteLine(new String(' ', Console.WindowWidth)); }
                    Console.SetCursorPosition(0, 3);
                    foreach (Car car in cars) { PrintDistance(car); }
                }
                else if(key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            if (finishedCount == cars.Count) 
            {
                LogAddPrint("Alla bilar har gått i mål och racet är över!\nTryck Enter för att avsluta.\n");
                Console.ReadLine();
            }          
        }

        public static void PrintDistance(Car car)
        {
            if (car.Distance < 3)
            {
                if (car.NoErrors)
                {
                    TotalDistance += car.Distance;
                    Console.WriteLine($"{car.Name} {new string(' ', 15 - car.Name.Length)} Hastighet: {car.Speed}{new string(' ', 3 - car.Speed.ToString().Length)} Km/h     Sträcka: {car.Distance.ToString("0.##")} Km");
                }
                else
                {
                    Console.WriteLine($"{car.Name} {new string(' ', 15 - car.Name.Length)} Hastighet: 0   Km/h     Sträcka: {car.Distance.ToString("0.##")} Km     {car.ErrorMessage}");
                }
            }
            else
            {
                Console.WriteLine($"{car.Name} har gått i mål!");
            }
        }

        public static void raceInit()
        {
            Console.SetWindowSize(120, 30);
            Console.SetCursorPosition(Console.WindowWidth/2-4, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("BILRACE!");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\nTryck Enter för att uppdatera ställningarna: \n\n\n\n\nHändelselogg:");            
            Console.ResetColor();
            Console.SetCursorPosition(0, 3);
            foreach (Car car in cars) { PrintDistance(car); }

            LogAddPrint("Racet har börjat! Försten till 3 Km vinner!\n");
        }

        public static void Disaster()
        {
            Random randy = new();
            while (finishedCount < cars.Count)
            {
                Thread.Sleep(30000);
                foreach (Car car in cars)
                {
                    if (car.NoErrors)
                    {
                        int chance = randy.Next(1, 51);
                        if (chance == 1)
                        {
                            Task refill = new Task(car.Refill);                         
                            refill.Start();
                            LogAddPrint($"{car.Name} har slut på bensin!\n");
                        }
                        else if (chance > 45 && chance < 48)
                        {
                            Task changeTire = new Task(car.ChangeTire);
                            changeTire.Start();
                            LogAddPrint($"{car.Name} har fått punktering!\n");
                        }
                        else if (chance > 3 && chance < 9)
                        {
                            Task birdOnWindow = new Task(car.BirdOnWindow);
                            birdOnWindow.Start();
                            LogAddPrint($"{car.Name} har fått en fågel på vindrutan!\n");
                        }
                        else if (chance > 20 && chance < 31)
                        {
                            car.EngineFault();
                            LogAddPrint($"Motorfel! {car.Name} saktar ned med 1 Km/h!\n");
                        }
                    }                  
                }
            }
        }
        public static void LogAddPrint(string input)
        {
            Log.Add(input);
            PrintLog();
        }
        public static void PrintLog()
        {
            Console.SetCursorPosition(0, 8);
            int rows = Console.WindowHeight - Console.CursorTop - 2;
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine(new String(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 8);
            Log.Reverse();
            foreach (var item in Log)
            {
                Console.WriteLine(item);
                if (Console.CursorTop == Console.WindowHeight-2)
                {
                    break;
                }
            }
            Log.Reverse();
        }
    }
}
