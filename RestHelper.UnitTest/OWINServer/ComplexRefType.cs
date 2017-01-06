using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcSolvoRestHelper.UnitTest.OWINServer
{
    public class ComplexRefType
    {
        public string VariableStr { get; set; }
        public int VariableInt { get; set; }
        public bool VariableBool { get; set; }
        public DateTime VariableDateTime { get; set; }
    }


    public class NestedComplexRefType
    {
        public string OuterVariableStr { get; set; }
        public ComplexRefType InnerVariableComplex { get; set; }
    }


}
