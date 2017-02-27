using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
    public class Operations : IOperations
    {
        //private int num1 = 10;
        //protected int num2 = 20;
        //public int num3 = 30;
        //internal int num4 = 40;
        //public int MyProperty { get; set; }
        public string name
        {
            get
            {
                return "MathLib";
            }
        }

        public int diff()
        {
            return Math.Abs(-30);
        }

        public int sum()
        {
            return 20;
        }

        public int valueOfProp()
        {
            return 200;
        }
    }
}
