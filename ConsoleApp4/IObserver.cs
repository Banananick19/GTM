using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectInput;

namespace ConsoleApp4
{
    internal interface IObserver<T>
    { 
       void Update(T updateData);
    }

}
