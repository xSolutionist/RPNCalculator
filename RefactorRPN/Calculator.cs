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

            Dictionary<string, Func<double, double, double>> _inputDictionary = InitializeCalculator();

            CalculateInput(input, _inputDictionary);
            Push(Isnumeric, command);
            Clear(input);
            Quit(input);

        }

        private Dictionary<string, Func<double, double, double>> InitializeCalculator()
        {
            return new()
            {
                { Ops.EnglishAdd, _logic.AddLogic },
                { Ops.NumericAdd, _logic.AddLogic },
                { Ops.EnglishSubtract, _logic.SubtractLogic },
                { Ops.SwedishSubtract, _logic.SubtractLogic },
                { Ops.NumericSubtract, _logic.SubtractLogic },
                { Ops.EnglishDivide, _logic.DivideLogic },
                { Ops.SwedishDivide, _logic.DivideLogic },
                { Ops.NumericDivide, _logic.DivideLogic },
                { Ops.EnglishMultiply, _logic.MultiplyLogic },
                { Ops.SwedishMultiply, _logic.MultiplyLogic },
                { Ops.NumericMultiply, _logic.MultiplyLogic }

            };
        }

        private void CalculateInput(string? input, Dictionary<string, Func<double, double, double>> _inputDictionary)
        {
            if (_stack.Count > 1 && _inputDictionary.ContainsKey(input))
            {

                var method = _inputDictionary.ContainsKey(input)
               ? _inputDictionary[input]
               : null;
                Calculate(method);
            }
        }

        private void Push(bool Isnumeric, double command)
        {
            if (Isnumeric)
            {
                _stack.Push(command);
            }
        }

        private void Clear(string? input)
        {
            if (input == "c")
            {
                _stack.Clear();
            }
        }

        private static void Quit(string? input)
        {
            if (input == "q")
            {
                Environment.Exit(1);
            }
        }

        private void Print()
        {
            var results = _io.Format(_stack);
            _io.Write(results);
        }

        private T Calculate<T>(T x, T y, Func<T, T, T> del)
        {
            var result = del(x, y);
            return result;
        }
        private void Calculate(Func<double, double, double> doFunction)
        {
            if (_stack.Count > 0)
            {
                var first = _stack.Pop();
                var second = _stack.Pop();
                var result = doFunction(first, second);
               _stack.Push(result);
            }
        }
    }
}
