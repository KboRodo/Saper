using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Saper
{
    class Minefield
    {
        private int Mines { get; set; }
        private int Flags { get; set; }

        private int DefusedMines { get; set; }
        private int Size { get; set; }//roziar wybranej planszy
        private Element[,] myMinefield=new Element[10,10];

        private string[] Alphabet = { " ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };


        public Minefield(int size, int mines)//konstruktor pola
        {
            Mines = mines;
            Flags = mines;
            Size = size;
            DefusedMines = 0;
            for(int i=0; i<size; i++)
            {
                for(int j=0; j<size; j++)
                {
                    myMinefield[i, j] = new Element();
                }
            }
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
                    Console.Write(myMinefield[j,i]);//tu ma sie wyswietlac pole                                        
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

        public void uncoverSpace(int x, int y)
        {
            myMinefield[x, y].uncoverSpace();
        }
        public void setFlag(int x, int y)//ustawianie flagi na polu
        {         
            if (Flags > 0)
            {
                myMinefield[x, y].SetFlag();

                if (myMinefield[x, y].getMine() == true)
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

        public bool getMine(int x, int y)//sprawdza czy na polu jest mina
        {
            return myMinefield[x, y].getMine();
        }

        public bool isDiscovered(int x, int y)
        {
            return myMinefield[x, y].isDiscovered();
        }

        public void removeFlag(int x, int y)//usuwanie flagi z pola
        {
            if (myMinefield[x, y].getMine() == true)
            {
                DefusedMines--;
            }
            if (myMinefield[x, y].getFlag() == true)
            {
                Flags++;
                myMinefield[x, y].RemoveFlag();
            }
        }

        public int getSize()//zwraca rozmiar planszy
        {
            return Size;
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

        public void populateMinefield()//metoda spawnujące miny w losowych polach planszy
        {
            Random randomNumber = new Random();
            int x, y;
            for(int i=0; i<Mines; i++)//Dla kazdej miny wybierane są losowe koordynaty pola mieszczącego się w zakresie planszy
            {
                x = randomNumber.Next(0, Size);
                y = randomNumber.Next(0, Size);
                while (myMinefield[x, y].Mine == true)//losowe pole wybierane jest do momentu aż zostanie znalezione pole, na którym nie ma miny
                {
                    x = randomNumber.Next(0, Size);
                    y = randomNumber.Next(0, Size);
                }
                myMinefield[x, y].Mine = true;//na polu zostanie zespawnowana mina

                for(int m=-1; m<2; m++)
                {
                    for(int j=-1; j<2; j++)
                    {
                        if ((x + m) < Size && (x+m)>=0 && (y + j) < Size && (y+j)>=0)//pola otaczające minę zostaną o tym "powiadomione"
                        {
                            if (myMinefield[x + m, y + j].Mine == false)
                            {
                                myMinefield[x + m, y + j].addMineBorder();
                            }
                        }
                    }
                }
            }
        }
    }
}
