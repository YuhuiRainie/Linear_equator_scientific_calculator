using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace assignmentThird
{
    class Program
    {
        static void Main(string[] args)
        {
            

                string equation = "";
                args[0] = "calc ";
               // string equation get full equation
                for (int LoopVar = 1; LoopVar <args.Length; LoopVar++)
                {
                    equation += args[LoopVar];
                }
                
                Functions functions = new Functions();
             


                //check if the equation is validate,has x,= or not, input is int or not

                Functions.HaveArg(args);
                char[] Equation = equation.ToCharArray();
                Functions.FormalEquation(Equation);
                Functions.CheckInt(equation);
                ArrayList EquArrL = Functions.Transfer(Equation);

              // split the arrylist into left side arraylist and right side arraylist by"=";
                ArrayList Left = new ArrayList();
                ArrayList Right = new ArrayList();
                string equal = "=";
                int myIndex = EquArrL.IndexOf(equal);

                for (int Loop = 0; Loop < myIndex; Loop++)
                {
                    Left.Add(EquArrL[Loop]);
                }
               

                for (int LoopVar = myIndex + 1; LoopVar < EquArrL.Count; LoopVar++)
                {
                    Right.Add(EquArrL[LoopVar]);
                }
                

                // add negative number functions
                ArrayList newRight = functions.ForNegativeNum(Right);
                functions.ForNegativeNum(Left);
             

                //simplify "x/6"contained equation
                functions.SimplicationVar(Left, newRight);
               

                // if there is "*" or "/" in equation, to deal with these first, to simplify the equation

                functions.SimplicationNum(newRight);
                functions.SimplicationNum(Left);
               

                //move all numbers in left to right side

                functions.MoveNum(ref Left, ref newRight);





                //move variables to left side
                do
                {
                    functions.MoveVariables(newRight, Left);

                }
                while (Right.Contains("x") || Right.Contains("X"));

                
                //move numbers again if there is still numbers in left side

                functions.MoveNum(ref Left, ref newRight);

                functions.MoveNum(ref Left, ref newRight);

            //calculate the left variables side

            ArrayList newLeft = functions.CalculateVariables(Left);
           
         
            
            //calculate right sides numbers
            do
                {
                    functions.Calculate(newRight);
                }
                while (newRight.Count > 1);

          

            //present the result
           
            ArrayList Result = functions.Result(newLeft, newRight);
            functions.Calculate(Result);
            foreach (var element in Result) { Console.WriteLine($"x={element}"); }


            Console.Read();


        }
    }
}

