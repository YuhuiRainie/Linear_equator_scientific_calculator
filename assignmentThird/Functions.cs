using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using static assignmentThird.operatorsAndNumber;

namespace assignmentThird
{
    class Functions
    {
        public Functions()
        {
        }
        // check if there is argument inputed or not
        public static string[] HaveArg(string[] argument)
        {
            if (argument.Length == 0)
            {
                Console.WriteLine("please input argument");
                argument = Console.ReadLine().Split(' ');
                throw new System.ArgumentException("Equation must contain numbers");
            }
            else if (argument[0] != "calc ")
            {
                Console.WriteLine("please input keyword calc with a space");
                argument = Console.ReadLine().Split(' ');
                throw new System.ArgumentException("Equation must start with keyword 'calc'");
            }
            return argument;
        }

        // check if the equation has variables or '='
        public static bool FormalEquation(char[] arg)
        {

            if (arg.Contains('=') && (arg.Contains('X') || arg.Contains('x')))
            {
                return true;
            }
            else
            {
                Console.WriteLine("please input valid equations");
                throw new System.ArgumentException("Equation must has 'X' or''x'and '='");

            }
        }
        // check the input number is int or not
        public static void CheckInt(string str)
        {
            string[] split;
            split = str.Split('x', 'X', '+', '-', '*', '/', '=');
            for (int LoopCon = 0; LoopCon < split.Length; LoopCon++)
            {
                if (split[LoopCon].Equals(""))
                {
                    continue;
                }
                else if (CheckNum(split[LoopCon]) == true)
                {
                    continue;

                }
                else
                {
                    throw new System.ArgumentException("Parameter must all be int");
                }
            }
        }
        // check if the string is operators
        public static bool IndexOf(char ch)
        {
            if (ch == '*' || ch == '-' || ch == '+' || ch == '/' || ch == '=')
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        // transfer the char array into arraylist
        public static ArrayList Transfer(char[] ch)
        {
            ArrayList EquString = new ArrayList();
            string s = "";
            for (int i = 0; i < ch.Length; i++)
            {
                if (!IndexOf(ch[i]))
                {
                    s = string.Concat(s, ch[i]);//not a operator
                }
                else
                {
                    EquString.Add(s);
                    s = "";
                    EquString.Add("" + ch[i]);
                }

            }
            if (s != "")
            {
                EquString.Add(s);
            }
            
            return EquString;

        }

        // if there is negative contained equations like -6-3
        public ArrayList ForNegativeNum(ArrayList arr)
        {
            string str = "0";

            if (arr[0].ToString().Equals(""))
            {
                arr.Remove(arr[0]);
                arr.Insert(0, str);
            }

            return arr;
        }
        //check a string is a number
        public static bool CheckNum(string s)
        {
            int result;
            bool success = Int32.TryParse(s, out result);
            if (success == true)
            {
                return true;
            }

            return false;
        }
        // check if there is equation like "x/9"
        public bool HasXwithDiv(ArrayList arr)
        {
            bool has = false;
            for (int LoopCon = 0; LoopCon < arr.Count; LoopCon++)
            {
                if (arr.Count < 3)
                {
                    has = false;
                }
                else if (arr.Count >= 3)
                {
                    if ((arr[0].ToString().Contains("x") || arr[0].ToString().Contains("X")) && arr[1].ToString().Equals("/"))
                    {
                        has = true;
                    }
                    else
                    {
                        has = false;
                    }
                }
            }
            return has;
        }
        // firstly simplify the equation like "x/9"
        public void SimplicationVar(ArrayList left, ArrayList right)
        {
            int myIndex = 0;
            if (HasXwithDiv(left))
            {
                myIndex = left.IndexOf("/");
                right.Add("*");
                right.Add(left[myIndex + 1].ToString());
                left.Remove(left[myIndex].ToString());
                left.Remove(left[myIndex].ToString());

            }
            else if (HasXwithDiv(right))
            {
                myIndex = right.IndexOf("/");
                left.Add("*");
                left.Add(right[myIndex + 1].ToString());
                right.Remove(right[myIndex].ToString());
                right.Remove(right[myIndex].ToString());

            }

        }
        // if there is * or/ in equation, simplify the equation first
        public ArrayList SimplicationNum(ArrayList arr)
        {
            int result = 0;
            operatorsAndNumber N = new operatorsAndNumber();
            for (int Loopcon = 0; Loopcon < arr.Count; Loopcon++)
            {
                if ((arr[Loopcon].ToString().Equals("*") || arr[Loopcon].ToString().Equals("/")) && CheckNum(arr[Loopcon - 1].ToString()) && CheckNum(arr[Loopcon + 1].ToString()))
                {
                    if (arr[Loopcon].ToString().Equals("*"))
                    {
                        Operator o = N.CheckOperators("*");
                        result = N.calculation(Int32.Parse(arr[Loopcon - 1].ToString()), Int32.Parse(arr[Loopcon + 1].ToString()), o);
                        arr.Remove(arr[Loopcon - 1]);
                        arr.Remove(arr[Loopcon - 1]);
                        arr.Remove(arr[Loopcon - 1]);
                        arr.Insert(Loopcon - 1, result);
                    }
                    else if (arr[Loopcon].ToString().Equals("/"))
                    {
                        Operator o = N.CheckOperators("/");
                        result = N.calculation(Int32.Parse(arr[Loopcon - 1].ToString()), Int32.Parse(arr[Loopcon + 1].ToString()), o);
                        arr.Remove(arr[Loopcon - 1]);
                        arr.Remove(arr[Loopcon - 1]);
                        arr.Remove(arr[Loopcon - 1]);
                        arr.Insert(Loopcon - 1, result);
                    }
                }
                else if (arr[Loopcon].ToString().Equals("*") && CheckNum(arr[Loopcon - 1].ToString()) && (arr[Loopcon + 1].ToString().Equals("x") || arr[Loopcon + 1].ToString().Equals("X")))
                {
                    string strResult = string.Concat(arr[Loopcon - 1].ToString(), arr[Loopcon + 1]);
                    arr.Remove(arr[Loopcon - 1]);
                    arr.Remove(arr[Loopcon - 1]);
                    arr.Remove(arr[Loopcon - 1]);
                    arr.Insert(Loopcon - 1, strResult);
                }
            }
            return arr;
        }



        // move all number too right arraylist
        public ArrayList MoveNum(ref ArrayList Left, ref ArrayList Right)
        {
            string add = "+";
            string sub = "-";

            for (int LoopVar = 0; LoopVar < Left.Count; LoopVar++)
            {
                if (CheckNum(Left[LoopVar].ToString()) == true)
                {
                    if (LoopVar == 0)
                    {
                        if (Left.Count == 1)
                        {
                            Right.Add(sub);
                            Right.Add(Left[0]);
                            Left.Add("0");
                            Left.Remove(Left[0]);

                        }
                        else if (Left[1].ToString().Equals(add))
                        {
                            Right.Add(sub);
                            Right.Add(Left[0]);
                            Left.Remove(Left[0]);
                            Left.Remove(Left[0]);
                            if (Left.Count == 0)
                            {
                                Left.Add("0");
                            }
                        }
                        else if (Left[1].ToString().Equals(sub))
                        {
                            Right.Add(sub);
                            Right.Add(Left[0]);
                            Left.Remove(Left[0]);
                            Left.Insert(0, string.Concat(Left[LoopVar].ToString(), Left[LoopVar + 1].ToString()));

                            Left.Remove(Left[1]);
                            Left.Remove(Left[1]);
                            if (Left.Count == 0)
                            {
                                Left.Add("0");
                            }
                        }


                    }
                    else if (Left[LoopVar - 1].ToString().Equals(add) || Left[LoopVar - 1].ToString().Equals(sub))
                    {
                        if (Left[LoopVar - 1].ToString().Equals(add))
                        {
                            Right.Add(sub);
                            Right.Add(Left[LoopVar]);
                            Left.Remove(Left[LoopVar - 1]);
                            Left.Remove(Left[LoopVar - 1]);
                            if (Left.Count == 0)
                            {
                                Left.Add("0");
                            }

                        }
                        else if (Left[LoopVar - 1].ToString().Equals(sub))
                        {
                            Right.Add(add);
                            Right.Add(Left[LoopVar]);
                            Left.Remove(Left[LoopVar - 1]);
                            Left.Remove(Left[LoopVar - 1]);
                            if (Left.Count == 0)
                            {
                                Left.Add("0");
                            }

                        }
                    }
                    return Right;
                }
                continue;
            }
            return Left;
        }

        // move all the x in right side to left side
        public ArrayList MoveVariables(ArrayList Right, ArrayList Left)
        {
            string add = "+";
            string sub = "-";

            for (int LoopVar = 0; LoopVar < Right.Count; LoopVar++)
            {
                if (Right[LoopVar].ToString().Contains("x") || Right[LoopVar].ToString().Contains("X"))
                {
                    if (LoopVar == 0)
                    {
                        if (Right.Count == 1)
                        {
                            Left.Add(sub);
                            Left.Add(Right[0]);
                            Right.Remove(Right[0]);
                            Right.Add("0");
                        }
                        else if (Right[1].ToString().Equals(add))
                        {
                            Left.Add(sub);
                            Left.Add(Right[0]);
                            Right.Remove(Right[0]);
                            Right.Remove(Right[0]);
                            if (Right.Count == 0)
                            {
                                Right.Add("0");
                            }
                        }
                        else if (Right[1].ToString().Equals(sub))
                        {
                            Left.Add(sub);
                            Left.Add(Right[0]);
                            Right.Insert(0, string.Concat(Right[LoopVar + 1].ToString(), Right[LoopVar + 2].ToString()));
                            Right.Remove(Right[1]);
                            Right.Remove(Right[1]);
                            Right.Remove(Right[1]);
                            if (Right.Count == 0)
                            {
                                Right.Add("0");
                            }
                        }


                    }
                    else if (Right[LoopVar - 1].ToString().Equals(add) || Right[LoopVar - 1].ToString().Equals(sub))
                    {
                        if (Right[LoopVar - 1].ToString().Equals(add))
                        {
                            Left.Add(sub);
                            Left.Add(Right[LoopVar]);
                            Right.Remove(Right[LoopVar - 1]);
                            Right.Remove(Right[LoopVar - 1]);
                            if (Right.Count == 0)
                            {
                                Right.Add("0");
                            }

                        }
                        else if (Right[LoopVar - 1].ToString().Equals(sub))
                        {
                            Left.Add(add);
                            Left.Add(Right[LoopVar]);
                            Right.Remove(Right[LoopVar - 1]);
                            Right.Remove(Right[LoopVar - 1]);
                            if (Right.Count == 0)
                            {
                                Right.Add("0");
                            }

                        }
                    }
                    return Right;
                }
                continue;
            }
            return Left;

        }

        // calculate numbers
        public ArrayList Calculate(ArrayList arr)
        {
            int result = 0;
            operatorsAndNumber N = new operatorsAndNumber();
            if (arr.Contains("*") || arr.Contains("/"))
            {
                for (int DivMulLoop = 0; DivMulLoop < arr.Count; DivMulLoop++)
                {
                    if (arr[DivMulLoop].ToString().Equals("*") && CheckNum(arr[DivMulLoop - 1].ToString()) && CheckNum(arr[DivMulLoop + 1].ToString()))
                    {
                        Operator o = N.CheckOperators("*");
                        result = N.calculation(Int32.Parse(arr[DivMulLoop - 1].ToString()), Int32.Parse(arr[DivMulLoop + 1].ToString()), o);
                        arr.Remove(arr[DivMulLoop - 1]);
                        arr.Remove(arr[DivMulLoop - 1]);
                        arr.Remove(arr[DivMulLoop - 1]);
                        arr.Insert(DivMulLoop - 1, result);
                    }
                    else if (arr[DivMulLoop].ToString().Equals("/") && CheckNum(arr[DivMulLoop - 1].ToString()) && CheckNum(arr[DivMulLoop + 1].ToString()))
                    {
                        Operator o = N.CheckOperators("/");
                        result = N.calculation(Int32.Parse(arr[DivMulLoop - 1].ToString()), Int32.Parse(arr[DivMulLoop + 1].ToString()), o);
                        arr.Remove(arr[DivMulLoop - 1]);
                        arr.Remove(arr[DivMulLoop - 1]);
                        arr.Remove(arr[DivMulLoop - 1]);
                        arr.Insert(DivMulLoop - 1, result);
                    }
                }

            }
            else if (arr.Contains("+") || arr.Contains("-"))
            {
                for (int AddSubLoop = 0; AddSubLoop < arr.Count; AddSubLoop++)
                {
                    if (arr[AddSubLoop].ToString().Equals("+") && CheckNum(arr[AddSubLoop - 1].ToString()) && CheckNum(arr[AddSubLoop + 1].ToString()))
                    {
                        Operator o = N.CheckOperators("+");
                        result = N.calculation(Int32.Parse(arr[AddSubLoop - 1].ToString()), Int32.Parse(arr[AddSubLoop + 1].ToString()), o);

                        arr.Remove(arr[AddSubLoop - 1]);
                        arr.Remove(arr[AddSubLoop - 1]);
                        arr.Remove(arr[AddSubLoop - 1]);

                        arr.Insert(AddSubLoop - 1, result);
                    }
                    else if (arr[AddSubLoop].ToString().Equals("-") && CheckNum(arr[AddSubLoop - 1].ToString()) && CheckNum(arr[AddSubLoop + 1].ToString()))
                    {
                        Operator o = N.CheckOperators("-");
                        result = N.calculation(Int32.Parse(arr[AddSubLoop - 1].ToString()), Int32.Parse(arr[AddSubLoop + 1].ToString()), o);
                        arr.Remove(arr[AddSubLoop - 1]);
                        arr.Remove(arr[AddSubLoop - 1]);
                        arr.Remove(arr[AddSubLoop - 1]);
                        arr.Insert(AddSubLoop - 1, result);
                    }
                }

            }
            return arr;
        }
        // deal with two variables calculation
        public ArrayList CalculateVariables(ArrayList arr)
        {
            string str = "";
            ArrayList newLeft = new ArrayList();
            ArrayList NewnewLeft = new ArrayList();
            for (int ReadArrLoop=0; ReadArrLoop<arr.Count;ReadArrLoop++)
            {
                str += arr[ReadArrLoop];
            }
            char[] ch = str.ToCharArray();
            
            
            if (ch.Length == 4)
            {
                if (char.IsNumber(ch[0]))
                {
                    newLeft.Add(ch[0].ToString());
                    newLeft.Add(ch[2].ToString());
                    newLeft.Add("1");
                    Calculate(newLeft);
                  
                    NewnewLeft.Add(string.Concat(newLeft[0].ToString(), "x"));
                }
                else if (char.IsLetter(ch[0]))
                {
                    newLeft.Add("1");
                    newLeft.Add(ch[1].ToString());
                    newLeft.Add(ch[2].ToString());
                    Calculate(newLeft);
                    NewnewLeft.Add(string.Concat(newLeft[0].ToString(), "x"));
                }
            }
            else if (ch.Length == 5)
            {
                newLeft.Add(ch[0].ToString());
                newLeft.Add(ch[2].ToString());
                newLeft.Add(ch[3].ToString());
                Calculate(newLeft);
                NewnewLeft.Add(string.Concat(newLeft[0].ToString(), "x"));
            }
            else
            {
                return arr;
            }
           
            return NewnewLeft;
        }
        //present result
        public ArrayList Result(ArrayList left, ArrayList right)
        {
            ArrayList Result = new ArrayList();
            Result.Add(right[0]);
            string str = "";
            for (int LoopCon = 0; LoopCon < left.Count; LoopCon++)
            {
                str += left[LoopCon].ToString();
            }
            char[] ch = str.ToCharArray();
            if (ch.Length == 1)
            {
                Result.Add("/");
                Result.Add("1");

            }
            else if (ch.Length == 2)
            {
                if (ch[0].Equals('-'))
                {
                    Result.Add("/");
                    Result.Add("-1");
                }
                else if (CheckNum(ch[0].ToString()))
                {
                    Result.Add("/");
                    Result.Add(ch[0]);
                }

            }
            else if (ch.Length == 3)
            {
                if (ch[0].Equals('-') && (ch[2].Equals('x') || ch[2].Equals('X')))
                {
                    Result.Add("/");
                    Result.Add(string.Concat(ch[0].ToString(), ch[1].ToString()));
                }
            }
            return Result;
        }


    }

}

