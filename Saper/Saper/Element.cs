using System;
using System.Collections.Generic;
using System.Text;

namespace Saper
{
    class Element
    {
        private bool _Mine;//Mina
        private bool _Flag;//Oznaczenie pola z miną
        private bool _Discovered;//czy zostało odkryte
        private int _MineBorder;//ilość min znajdujących się w r=1 od od elementu

        public bool Mine
        {
            get { return _Mine; }
            set { _Mine = value; }
        }
        public bool Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        public bool Discovered
        {
            get { return _Discovered; }
            set { _Discovered= value; }
        }
        private int MineBorder
        {
            get { return _MineBorder; }
            set { _MineBorder = value; }
        }


        public Element()//default ctor;
        {
            Mine = false;
            Flag = false;
            Discovered = false;
            MineBorder = 0;
        }
        public void SetFlag()//ustawianie flagi
        {
            Flag = true;
        }
        public void RemoveFlag()//usuwanie flagi
        {
            Flag = false;
        }
        public bool getFlag()
        {
            return Flag;
        }
        public bool getMine()
        {
            return Mine;
        }
        public void addMineBorder()
        {
            MineBorder++;
        }
        public int getMineBorder()
        {
            return MineBorder;
        }
        public void Print()//wyswietlanie pola
        {
            if (Flag == true)
            {
                Console.Write(">");
            }
            else
            {
                Console.Write(" ");
            }
        }
        public void printMine()//wyswietla status pola
        {
            if (Mine == true && Flag == true)
            {
                Console.Write("D");
            }
            else if(Mine == true && Flag == false)
            {
                Console.Write("*");
            }
            else if (Mine == false && Flag == true)
            {
                Console.Write("F");
            }
            else if (Mine == false && MineBorder>0)
            {
                Console.Write(MineBorder);
            }
            else 
            {
                Console.Write(" ");
            }
        }
        public void plantMine()//ustawienie miny na polu
        {
            Mine = true;
        }

        public void uncoverSpace()
        {
            Discovered = true;
        }

        public int checkBorder()
        {
            return MineBorder;
        }
        public override string ToString()
        {
            if (Flag == true)
            {
                return (">");
            }
            else if (MineBorder > 0&& Discovered==true)
            {
                return MineBorder.ToString();
            }
            else if(Discovered==true)
            {
                return (" ");
            }
            else
            {
                return ("~");
            }
        }

    }
}
