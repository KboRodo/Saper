using System;
using System.Collections.Generic;
using System.Text;

namespace Saper
{
    class Minefield
    {
        private int Mines { get; set; }
        private int Flags { get; set; }

        private int Size { get; set; }//roziar wybranej planszy
        private Element[,] myMinefield=new Element[10,10];

        private string[] Alphabet = { " ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };


        public Minefield(int size, int mines)
        {
            Mines = mines;
            Size = size;
            for(int i=0; i<size; i++)
            {
                for(int j=0; j<size; j++)
                {
                    myMinefield[i, j] = new Element();
                }
            }
        }
        //plant mines
        public void printMinefield()//wyswietlanie obrazu pola minowego z zaznaczonymi flagami
        {
            Console.Clear();//czyszczenie ekranu

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

        public void setFlag(int x, int y)//ustawianie flagi na polu
        {
            myMinefield[x, y].SetFlag();
        }
        public void removeFlag(int x, int y)//usuwanie flagi z pola
        {
            myMinefield[x, y].RemoveFlag();
        }
        public int getSize()
        {
            return Size;
        }
    }
}
