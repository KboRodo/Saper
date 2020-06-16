using System;
using System.Collections.Generic;
using System.Threading;

namespace Saper
{
    class Program
    {
        static void Main()
        {
            int size, mines,check,option;

            Console.WriteLine("Witaj w grze Saper !");
            
            Console.WriteLine("Wybierz rozmiar planszy, maksymalny rozmiar to 10x10:");
            size = int.Parse(Console.ReadLine());
            while (size > 10)
            {
                Console.WriteLine("Maksymalny rozmiar planszy to 10x10");
                size = int.Parse(Console.ReadLine());
            }
            Console.Clear();

            check = (size*size) - 1;
            Console.WriteLine($"Podaj ilość min, maksymalna ilość min to: {check}:");
            mines = int.Parse(Console.ReadLine());
            while (mines > check)
            {
                Console.WriteLine($"Liczba min nie może być większa niż {check}");
                mines=int.Parse(Console.ReadLine());
            }

            Minefield gameField = new Minefield(size, mines);
        
            while (true)//wybieranie opcji gry
            {
                gameField.printMinefield();
                Console.WriteLine("Możliwe opcje:\n1-wejdź na pole 2-dodaj flagę, 3-usuń flagę, 4-koniec gry");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1://wejdz na pole
                        break;
                    case 2://dodaj flage
                        Console.WriteLine("WYBRANO OPCJE DODAJ FLAGE");
                        flagSetup(gameField,1);
                        break;
                    case 3://usub flage
                        Console.WriteLine("WYBRANO OPCJE USUŃ FLAGE");
                        flagSetup(gameField, 0);
                        break;

                    case 4://wyjscie z gry
                        Console.WriteLine("Wychodzenie z gry...");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Nie ma takiej opcji, wybierz inną");
                        break;
                }

            }
            
        }
        static void flagSetup(Minefield myMinefield, int flagState)//zmiana statusu flagi
        {
            int x, y;
            string fieldAddress;
            Console.WriteLine("PODAJ ADRES POLA: ");
            while (true)
            {
                fieldAddress = Console.ReadLine();
                x = char.ToUpper(fieldAddress[0]) - 65;
                y = int.Parse(fieldAddress[1].ToString());
                if (x < myMinefield.getSize() && y < myMinefield.getSize())//sprawdzanie czy podane pole istnieje na planszy
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Takie pole nie istnieje, wybierz inne");
                }
            }
            while (fieldAddress.Length > 2)
            {
                Console.WriteLine("Podaj adres komórki nie używając spacji np A4");
                fieldAddress = Console.ReadLine();
            }
            
            if(flagState==1)//flagstate = 1 - postawienie flagi
            {
                myMinefield.setFlag(x, y);
            }
            else //flagstate = 0 - usuniecie flagi;
            {
                myMinefield.removeFlag(x, y);
            }
        }
        
    }
}
