using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorRPN
{
    public class IOProvider
    {
        private readonly Func<string?> _inputProvider;
        private readonly Action<string> _outputProvider;

        public IOProvider(Func<string?> inputProvider, Action<string> outputProvider)
        {
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;
        }

        public string Format(Stack<double> stack)
        {
            StringBuilder b = new StringBuilder();
            if (stack.Count > 0)
            {

                b.Append('[');
                for (int i = stack.Count - 1; ; i--)
                {
                    b.Append(stack.ElementAt(i));
                    if (i == 0)
                        return b.Append(']').ToString();
                    b.Append(", ");
                }
            }
            _outputProvider("Commands: q c + - * / number");
            _outputProvider("[]");
            return "";
        }
        public void Write(string message)
        {
            _outputProvider(message);
        }

        public string? Read()
        {
            return _inputProvider();
        }
    }
}
