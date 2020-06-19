using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MinefieldLibrary
{
    public class Minefield
    {
        private int Mines { get; set; }
        private int Flags { get; set; }

        private int DefusedMines { get; set; }
        private int Size { get; set; }//roziar wybranej planszy
        private Element[,] myMinefield = new Element[10, 10];

        private string[] Alphabet = { " ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };


        public Minefield(int size, int mines)//konstruktor pola
        {
            Mines = mines;
            Flags = mines;
            Size = size;
            DefusedMines = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    myMinefield[i, j] = new Element();
                }
            }
            Debug.WriteLine($"utworzono pole o wymiarach {Size} i {Mines} minach");
        }
        public int getSize()
        {
            return Size;
        }
        public void printMinefield()//wyswietlanie obrazu planszy z zaznaczonymi flagami
        {
            Console.Clear();//czyszczenie ekranu
            Console.WriteLine($"Pozostałe flagi: {Flags} \n");

            Console.Write("|");//wyswietlanie liter na gorze planszy
            for (int k = 0; k < Size + 1; k++)
            {
                Console.Write($" {Alphabet[k]} |");
            }
            Console.Write("\n");
            Console.Write("|");


            for (int l = 0; l < Size + 1; l++)//pierwszy separator poziomy
            {
                Console.Write("---|");
            }
            Console.Write("\n");

            for (int i = 0; i < Size; i++)
            {

                Console.Write($"| {i}");//wyswietlanie numeru wiersza
                for (int j = 0; j < Size; j++)
                {
                    Console.Write(" | ");
                    Console.Write(myMinefield[j, i]);//tu ma sie wyswietlac pole                                        
                }
                Console.Write(" |");
                Console.Write("\n");
                Console.Write("|");
                for (int l = 0; l < Size + 1; l++)
                {
                    Console.Write("---|");
                }
                Console.Write("\n");
            }
        }

        public void printMines()//wyswietlanie obrazu planszy z zaznaczonymi minami
        {
            Console.Clear();//czyszczenie ekranu
            Console.WriteLine($"Pozostałe flagi: {Flags} \n");
            Console.Write("|");//wyswietlanie liter na gorze planszy
            for (int k = 0; k < Size + 1; k++)
            {
                Console.Write($" {Alphabet[k]} |");
            }
            Console.Write("\n");
            Console.Write("|");


            for (int l = 0; l < Size + 1; l++)//pierwszy separator poziomy
            {
                Console.Write("---|");
            }
            Console.Write("\n");

            for (int i = 0; i < Size; i++)
            {

                Console.Write($"| {i}");//wyswietlanie numeru wiersza
                for (int j = 0; j < Size; j++)
                {
                    Console.Write(" | ");
                    myMinefield[j, i].printMine(); //wyswietlanie min na planszy

                }
                Console.Write(" |");
                Console.Write("\n");
                Console.Write("|");
                for (int l = 0; l < Size + 1; l++)
                {
                    Console.Write("---|");
                }
                Console.Write("\n");
            }
        }

        public void setFlag(int x, int y)//ustawianie flagi na polu
        {
            if (Flags > 0)
            {
                myMinefield[x, y].SetFlag();

                if (myMinefield[x, y].isMine() == true)
                {
                    DefusedMines++;
                }
            }
            Flags--;
        }

        public bool getFlag(int x, int y)//sprawdza czy na polu jest flaga
        {
            return myMinefield[x, y].getFlag();
        }


        public bool isDiscovered(int x, int y)
        {
            return myMinefield[x, y].isDiscovered();
        }

        public void removeFlag(int x, int y)//usuwanie flagi z pola
        {
            if (myMinefield[x, y].isMine() == true)
            {
                DefusedMines--;
            }
            if (myMinefield[x, y].getFlag() == true)
            {
                Flags++;
                myMinefield[x, y].RemoveFlag();
            }
        }

        public int checkBorder(int x, int y)
        {
            return myMinefield[x, y].checkBorder();
        }

        public bool isGameFinished()//sprawdza czy gra sie zakończyła-czy wszystkie miny zostały oznaczone
        {
            if (Mines == DefusedMines)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void populateMinefield()//funkcja spawnujące miny w losowych polach planszy
        {
            Debug.WriteLine($"Rozkładanie {Mines} min na polu");
            Random randomNumber = new Random();
            int x, y;
            for (int i = 0; i < Mines; i++)//Dla kazdej miny wybierane są losowe koordynaty pola mieszczącego się w zakresie planszy
            {
                x = randomNumber.Next(0, Size);
                y = randomNumber.Next(0, Size);
                while (myMinefield[x, y].isMine() ==true)//losowe pole wybierane jest do momentu aż zostanie znalezione pole, na którym nie ma miny
                {
                    x = randomNumber.Next(0, Size);
                    y = randomNumber.Next(0, Size);
                }
                myMinefield[x, y].plantMine();//na polu zostanie zespawnowana mina
                for (int m = -1; m < 2; m++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if ((x + m) < Size && (x + m) >= 0 && (y + j) < Size && (y + j) >= 0)//pola otaczające minę zostaną o tym "powiadomione"
                        {
                            if (myMinefield[x + m, y + j].isMine() == false)
                            {
                                myMinefield[x + m, y + j].addMineBorder();
                            }
                        }
                    }
                }
            }
            Debug.WriteLine("Rozłożono miny");
        }
        public bool amIDead(int x, int y)//sprawdza czy na polu jest mina
            {
            if (myMinefield[x,y].isMine() == true && myMinefield[x,y].getFlag() == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void uncoverEmptySpaces(int x, int y)//odkrywa pionowy pas planszy przecinający punkt aż do znalezienia pola sąsiadującego z miną
        {
            for (int diffT = y; diffT >= 0; diffT--)//w góre od (x,y)
            {
                if (myMinefield[x,diffT].isDiscovered() == false)//funkcja odkrywa pola które sie są bombami lub poprzednio odkrytymi polami graniczącymi z miną
                {
                   
                    if (myMinefield[x,diffT].isMine()==false)
                    {
                        if (diffT <Size - 1 && myMinefield[x,diffT+1].checkBorder() > 0)
                        {
                            break;
                        }
                        else
                        {
                            myMinefield[x,diffT].uncoverSpace();
                            uncoverRow(x, diffT);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int diffB = y; diffB < Size; diffB++)//w dół od (x,y)
            {
                if (myMinefield[x,diffB].isDiscovered() == false)//funkcja odkrywa pola które sie są bombami lub poprzednio odkrytymi polami graniczącymi z miną
                {
                   
                    if (myMinefield[x,diffB].isMine()==false)
                    {
                        if (diffB > 0 && myMinefield[x, diffB-1].checkBorder() > 0)
                        {
                            break;
                        }
                        else
                        {
                            myMinefield[x, diffB].uncoverSpace();
                            uncoverRow(x, diffB);
                        }
                        
                    }
                    else
                    { 
                        break;
                    }
                }
            }
        }
        void uncoverRow(int x, int y)//odkrywa poziomy pas planszy planszy przecinający punkt aż do znalezienia pola sąsiadującego z miną
        {
            {
                for (int diffL = x; diffL >= 0; diffL--)//na lewo od (x,y)
                {
                    if (myMinefield[diffL,y].isDiscovered() == false)//funkcja odkrywa pola które sie są bombami lub poprzednio odkrytymi polami graniczącymi z miną
                    {
                        if (myMinefield[diffL,y].isMine()==false)
                        {
                            if (diffL < Size- 1 && myMinefield[diffL+1,y].checkBorder() > 0)
                            {
                                break;
                            }
                            else
                            {
                                myMinefield[diffL, y].uncoverSpace();
                                uncoverEmptySpaces(diffL, y);
                            }
                        }                      
                        else
                        {
                            break;
                        }
                    }
                   
                }
                for (int diffR = x; diffR < Size; diffR++)//na prawo od (x,y)
                {
                    if (myMinefield[diffR,y].isDiscovered() == false)//funkcja odkrywa pola które sie są bombami lub poprzednio odkrytymi polami graniczącymi z miną
                    {
                        if (myMinefield[diffR,y].isMine() ==false)
                        {
                            if (diffR > 0 && myMinefield[diffR - 1, y].checkBorder() > 0)
                            {
                                break;
                            }
                            else
                            {
                                myMinefield[diffR, y].uncoverSpace();
                                uncoverEmptySpaces(diffR, y);
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
