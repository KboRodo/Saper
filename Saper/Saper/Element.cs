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
        public void SetFlag()
        {
            Flag = true;
        }
        public void RemoveFlag()
        {
            Flag = false;
        }
        public void getFlag()
        {
            if(Flag==false)
            {
                Console.WriteLine("false");
            }
            else
            {
                Console.WriteLine("true");
            }
        }
        public void Print()
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
