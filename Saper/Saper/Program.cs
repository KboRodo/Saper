using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MinefieldLibrary;

namespace Saper
{
    class Program
    {
        static void Main()
        {
            int size, mines, check, option;
            Stopwatch stoper = new Stopwatch();
            stoper.Start();

            Console.WriteLine("Witaj w grze Saper !");

            Console.WriteLine("Wybierz rozmiar planszy, maksymalny rozmiar to 10x10:");
            while (int.TryParse(Console.ReadLine(), out size) == false)//sprawdzenie czy dane wprowadzone są w poprawnym formacie
            {
                Console.WriteLine("Porszę podać prawidłowy rozmiar od 2-10");
            }
            while (size > 10 && size > 1)//sprawdzenie czy liczba miesci sie w określonym zakresie
            {
                Console.WriteLine("Podaj prawidłowy rozmiar od 2-10");
                size = int.Parse(Console.ReadLine());
            }
            Console.Clear();

            check = (size * size) - 1;
            Console.WriteLine($"Podaj ilość min, maksymalna ilość min to: {check}:");
            while (int.TryParse(Console.ReadLine(), out mines) == false)//sprawdzenie czy dane wprowadzone są w poprawnym formacie
            {
                Console.WriteLine($"Podaj prawidłową ilość min, maksymalna ilość min to: {check} ");
            }
            while (mines > check)//sprawdzenie czy liczba miesci sie w określonym zakresie
            {
                Console.WriteLine($"Podaj prawidłową ilość min, maksymalna ilość min to: {check} ");
                mines = int.Parse(Console.ReadLine());
            }

            Minefield gameField = new Minefield(size, mines);
            gameField.populateMinefield();
            gameField.printMinefield();

            while (true)//wybieranie opcji gry
            {
                Console.WriteLine("Możliwe opcje:\n1-wejdź na pole 2-dodaj flagę, 3-usuń flagę, 4-koniec gry\n5-pokaż miny (opcja demonstracyjna), 6-ukryj miny (opcja demonstracyjna)");

                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1://wejdz na pole
                        enterNewSpace(gameField);
                        break;
                    case 2://dodaj flage
                        flagSetup(gameField,1);
                        gameField.printMinefield();
                        break;
                    case 3://usub flage
                        flagSetup(gameField,0);
                        gameField.printMinefield();
                        break;

                    case 4://wyjscie z gry
                        Console.WriteLine("Wychodzenie z gry...");
                        Console.Clear();
                        Environment.Exit(0);
                        break;

                    case 5://wyswietl miny
                        gameField.printMines();
                        break;

                    case 6://ukryj miny
                        gameField.printMinefield();
                        break;

                    default:
                        gameField.printMinefield();
                        Console.WriteLine("Nie ma takiej opcji, wybierz inną");
                        break;
                }
                if (gameField.isGameFinished() == true)//sprawdzenie czy gra się zakończyła-wszystkie miny zostały rozbrojone
                {
                    stoper.Stop();
                    Console.WriteLine("\nGratulacje, udało ci się znaleźć wszyskie miny!!!");
                    Console.WriteLine($"Twój czas to: {stoper.Elapsed.Minutes} minut i {stoper.Elapsed.Seconds} sekund");
                    break;
                }
            }

        }
        static void flagSetup(Minefield myMinefield, int flagstate)
        {
            int x, y;
            string fieldAddress;
            Console.WriteLine("PODAJ ADRES POLA: ");
            while (true)
            {
                fieldAddress = Console.ReadLine();
                while (fieldAddress.Length != 2)
                {
                    Console.WriteLine("Podaj adres komórki nie używając spacji np A4");
                    fieldAddress = Console.ReadLine();
                }
                x = char.ToUpper(fieldAddress[0]) - 65;
                if (int.TryParse(fieldAddress[1].ToString(), out y))//sprawdzenie
                {
                    if (x < myMinefield.getSize() && y < myMinefield.getSize())//sprawdzanie czy podane pole istnieje na planszy
                    {
                        if(flagstate == 1)//stawianie flagi
                        {
                            myMinefield.setFlag(x, y);
                        }
                        else//usuwanie flagi
                        {
                            myMinefield.removeFlag(x, y);
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Takie pole nie istnieje, wybierz inne");
                    }
                }
                else
                {
                    Console.WriteLine("Podaj poprawny adres komórki np A4");
                }
            }
        }
        static void enterNewSpace(Minefield myMinefield)//wejscie na nowe pole
        {
            int x, y;
            string fieldAddress;
            Console.WriteLine("PODAJ ADRES POLA: ");
            while (true)
            {
                fieldAddress = Console.ReadLine();
                while (fieldAddress.Length != 2)
                {
                    Console.WriteLine("Podaj adres komórki nie używając spacji np A4");
                    fieldAddress = Console.ReadLine();
                }
                x = char.ToUpper(fieldAddress[0]) - 65;
                if(int.TryParse(fieldAddress[1].ToString(), out y))//sprawdzenie
                {
                    if (x < myMinefield.getSize() && y < myMinefield.getSize())//sprawdzanie czy podane pole istnieje na planszy
                    {
                        if (myMinefield.getFlag(x, y) == false)//sprawdzenie czy na polu znajduje sie flaga
                        {
                            if (myMinefield.amIDead(x, y)==true)
                            {
                                Debug.WriteLine("Dead");
                                Console.WriteLine("Wszedłeś na minę, nie żyjesz");
                                Environment.Exit(0);
                                break;  
                            }
                            else
                            {
                                myMinefield.uncoverEmptySpaces(x, y);
                                myMinefield.printMinefield();
                                break;
                            }
                           
                        }
                        else
                        {
                            Console.WriteLine("Nie mozesz wejsc na pole oznaczone flagą, wybierz inne pole");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Takie pole nie istnieje, wybierz inne");
                    }
                }
                else
                {
                    Console.WriteLine("Podaj poprawny adres komórki np A4");
                }
            }
        }
    }
}
