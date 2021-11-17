using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorRPN
{
    public class Calculator
    {
        private readonly LogicHandler _logic;
        private readonly IOProvider _io;
        private readonly Stack<double> _stack;

        public Calculator(IOProvider io, Stack<double> stack, LogicHandler logic)
        {
            _logic = logic;
            _io = io;
            _stack = stack;
        }
       

        public void AskForCalculation()
        {
            Print();

            string? input = _io.Read();
            bool Isnumeric = double.TryParse(input, out double command);

            if (_stack.Count > 1)
            {

                switch (input)
                {
                    case Ops.EnglishAdd:
                    case Ops.SwedishAdd:
                    case Ops.NumericAdd:
                        DoLogic(_logic.AddLogic);
                        break;

                    case Ops.EnglishSubtract:
                    case Ops.SwedishSubtract:
                    case Ops.NumericSubtract:
                        DoRpnLogic(_logic.SubtractLogic);
                        break;

                    case Ops.EnglishDivide:
                    case Ops.SwedishDivide:
                    case Ops.NumericDivide:
                        DoRpnLogic(_logic.DivideLogic);
                        break;

                    case Ops.EnglishMultiply:
                    case Ops.SwedishMultiply:
                    case Ops.NumericMultiply:
                        DoLogic(_logic.MultiplyLogic);
                        break;

                    case "q": return;

                    case "c":
                        _stack.Clear();
                        break;

                    default:
                        _io.Write("Illegal command, ignored");
                        break;
                }

            }

            if (Isnumeric)
            {
                _stack.Push(command);
            }


        }

        private void Print()
        {
            var results = _io.Format(_stack);
            _io.Write(results);
        }

        private T DoLogic<T>(T x, T y, Func<T, T, T> del)
        {
            var result = del(x, y);
            return result;
        }
        private double DoLogic(Func<double, double, double> doFunction)
        {
            if (_stack.Count > 0)
            {
                var first = _stack.Pop();
                var second = _stack.Pop();
                var result = doFunction(first, second);
                _stack.Push(result);
                return result;
            }
            return 0;
        }

        private void DoRpnLogic(Func<double, double, double> function)
        {
            double reversedDigit = _stack.Pop();
            _stack.Push(function(_stack.Pop(), reversedDigit));
        }
    }
}
