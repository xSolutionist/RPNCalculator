using RefactorRPN;
using System.Collections;
using System.Text;


var stack = new Stack<double>();
var io = new IOProvider(Console.ReadLine, Console.WriteLine);
var logic = new LogicHandler();
var calculator = new Calculator(io, stack, logic);

while (true)
{
    calculator.AskForCalculation();
}

