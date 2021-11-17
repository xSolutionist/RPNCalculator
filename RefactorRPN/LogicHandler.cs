using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorRPN
{
    public class LogicHandler
    {
        public Func<double, double, double> SubtractLogic = (x, y) => x - y;
        public Func<double, double, double> DivideLogic = (x, y) => x / y;
        public Func<double, double, double> MultiplyLogic = (x, y) => x * y;
        public Func<double, double, double> AddLogic = (x, y) => x + y;
    }


}
