using System;
using System.Diagnostics;

namespace MinefieldLibrary
{        enum mineStatus//status miny na polu
         {
            Armed,//istnieje, moze wybuchnąć
            Flagged,//jest oznaczona flagą
            None,//nie istnieje
         }
        class Element
        {
            mineStatus mineOnField=new mineStatus();
            private bool Flag;//czy jest odkryte 
            private bool _Discovered;//czy zostało odkryte
            private int _MineBorder;//ilość min znajdujących się w r=1 od od elementu
           
            public bool Discovered
            {
                get { return _Discovered; }
                set { _Discovered = value; }
            }
            private int MineBorder
            {
                get { return _MineBorder; }
                set { _MineBorder = value; }
            }


            public Element()//default ctor;
            {
                mineOnField = mineStatus.None;
                Discovered = false;
                MineBorder = 0;
            }
            public void SetFlag()//ustawianie flagi
            {
                Flag = true;
                if (mineOnField == mineStatus.Armed)
                {
                    mineOnField = mineStatus.Flagged;
                }
            }
            public void RemoveFlag()//usuwanie flagi
            {
                Flag = false;
                if (mineOnField == mineStatus.Flagged)
                {
                    mineOnField = mineStatus.Armed;
                }
            }
            public bool getFlag()
            {
                return Flag;
            }
            public bool isMine()//zwraca prawde gdy na polu jest mina
            {
                if (mineOnField != mineStatus.None)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                Debug.WriteLine("Printing mine");
                if (mineOnField==mineStatus.Flagged)
                {
                    Console.Write("D");
                }
                else if (mineOnField == mineStatus.Armed)
                {
                    Console.Write("*");
                }
                else if (mineOnField == mineStatus.None && getFlag() == true)
                {
                    Console.Write("F");
                }
                else if (mineOnField == mineStatus.None && MineBorder > 0)
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
                mineOnField=mineStatus.Armed;
            }

            public void uncoverSpace()
            {
                Discovered = true;
            }

            public bool isDiscovered()
            {
                return Discovered;
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
                else if (MineBorder > 0 && Discovered == true)
                {
                    return MineBorder.ToString();
                }
                else if (Discovered == true)
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
