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


        public Element()//default ctor;
        {
            Mine = false;
            Flag = false;
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
        public void printMine()
        {
            if (Mine == true)
            {
                Console.Write("*");
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

        public override string ToString()
        {
            if (Flag == true)
            {
                return (">");
            }
            else
            {
                return (" ");
            }
        }

    }
}
