using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcSolvo.Interface
{
    internal interface ICloneable<T>
    {
        T Clone();
    }
}
