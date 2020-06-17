using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Saper
{
    class Program
    {
        static void Main()
        {
            int size, mines, check, option;

            Console.WriteLine("Witaj w grze Saper !");

            Console.WriteLine("Wybierz rozmiar planszy, maksymalny rozmiar to 10x10:");
            size = int.Parse(Console.ReadLine());
            while (size > 10)
            {
                Console.WriteLine("Maksymalny rozmiar planszy to 10x10");
                size = int.Parse(Console.ReadLine());
            }
            Console.Clear();

            check = (size * size) - 1;
            Console.WriteLine($"Podaj ilość min, maksymalna ilość min to: {check}:");
            mines = int.Parse(Console.ReadLine());
            while (mines > check)
            {
                Console.WriteLine($"Liczba min nie może być większa niż {check}");
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
                        gameField.printMinefield();
                        break;
                    case 2://dodaj flage
                        flagSetup(gameField, 1);
                        gameField.printMinefield();
                        break;
                    case 3://usub flage
                        flagSetup(gameField, 0);
                        gameField.printMinefield();
                        break;

                    case 4://wyjscie z gry
                        Console.WriteLine("Wychodzenie z gry...");
                        Thread.Sleep(1000);
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
                while (fieldAddress.Length > 2)
                {
                    Console.WriteLine("Podaj adres komórki nie używając spacji np A4");
                    fieldAddress = Console.ReadLine();
                }
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

            if (flagState == 1)//flagstate = 1 - postawienie flagi
            {
                myMinefield.setFlag(x, y);
            }
            else //flagstate = 0 - usuniecie flagi;
            {
                myMinefield.removeFlag(x, y);
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
                while (fieldAddress.Length > 2)
                {
                    Console.WriteLine("Podaj adres komórki nie używając spacji np A4");
                    fieldAddress = Console.ReadLine();
                }
                x = char.ToUpper(fieldAddress[0]) - 65;
                y = int.Parse(fieldAddress[1].ToString());
                if (x < myMinefield.getSize() && y < myMinefield.getSize())//sprawdzanie czy podane pole istnieje na planszy
                {

                }
                else
                {
                    Console.WriteLine("Takie pole nie istnieje, wybierz inne");
                }
                if (myMinefield.getFlag(x, y) == false)//sprawdzenie czy na polu znajduje sie flaga
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nie mozesz wejsc na pole oznaczone flagą, wybierz inne pole");
                }
            }
            if (amIDead(myMinefield, x, y) == true)//sprawdzenie czy na polu znajduje sie mina
            {
                Console.WriteLine("Nie zyjesz");
            }
            else//nie ma miny wyswietlamy wartości 
            {
                uncoverEmptySpaces(myMinefield, x, y);
            }
        }
        static bool amIDead(Minefield myMinefield, int x, int y)//sprawdza czy na polu jest mina
        {
            if (myMinefield.getMine(x, y) == true && myMinefield.getFlag(x, y) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void uncoverEmptySpaces(Minefield myMinefield, int x, int y)//odkrywa pionowy pas planszy przecinający punkt aż do znalezienia pola sąsiadującego z miną
        {
            for (int diffT = y; diffT >= 0; diffT--)//w góre od (x,y)
            {
                if (myMinefield.isDiscovered(x, diffT) == false)//metoda odkrywa pola które sie są bombami lub poprzednio odkrytymi polami graniczącymi z miną
                {
                   
                    if (myMinefield.getMine(x, diffT)==false)
                    {
                        if (diffT < myMinefield.getSize() - 1 && myMinefield.checkBorder(x, diffT + 1) > 0)
                        {
                            break;
                        }
                        else
                        {
                            myMinefield.uncoverSpace(x, diffT);
                            uncoverRow(myMinefield, x, diffT);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int diffB = y; diffB < myMinefield.getSize(); diffB++)//w dół od (x,y)
            {
                if (myMinefield.isDiscovered(x, diffB) == false)//metoda odkrywa pola które sie są bombami lub poprzednio odkrytymi polami graniczącymi z miną
                {
                   
                    if (myMinefield.getMine(x, diffB)==false)
                    {
                        if (diffB > 0 && myMinefield.checkBorder(x, diffB - 1) > 0)
                        {
                            break;
                        }
                        else
                        {
                            myMinefield.uncoverSpace(x, diffB);
                            uncoverRow(myMinefield, x, diffB);
                        }
                        
                    }
                    else
                    { 
                        break;
                    }
                }
            }
        }
        static void uncoverRow(Minefield myMinefield, int x, int y)//odkrywa poziomy pas planszy planszy przecinający punkt aż do znalezienia pola sąsiadującego z miną
        {
            {
                for (int diffL = x; diffL >= 0; diffL--)//na lewo od (x,y)
                {
                    if (myMinefield.isDiscovered(diffL, y) == false)//metoda odkrywa pola które sie są bombami lub poprzednio odkrytymi polami graniczącymi z miną
                    {
                        if (myMinefield.getMine(diffL,y)==false)
                        {
                            if (diffL < myMinefield.getSize() - 1 && myMinefield.checkBorder(diffL + 1, y) > 0)
                            {
                                break;
                            }
                            else
                            {
                                myMinefield.uncoverSpace(diffL, y);
                                uncoverEmptySpaces(myMinefield, diffL, y);
                            }
                        }                      
                        else
                        {
                            break;
                        }
                    }
                   
                }
                for (int diffR = x; diffR < myMinefield.getSize(); diffR++)//na prawo od (x,y)
                {
                    if (myMinefield.isDiscovered(diffR, y) == false)//metoda odkrywa pola które sie są bombami lub poprzednio odkrytymi polami graniczącymi z miną
                    {
                        if (myMinefield.getMine(diffR, y) ==false)
                        {
                            if (diffR > 0 && myMinefield.checkBorder(diffR - 1, y) > 0)
                            {
                                break;
                            }
                            else
                            {
                                myMinefield.uncoverSpace(diffR, y);
                                uncoverEmptySpaces(myMinefield, diffR, y);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
