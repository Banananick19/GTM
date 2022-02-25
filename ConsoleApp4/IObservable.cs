using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal interface IObservable<T>
    {
        void AddObserver(IObserver<T> o);
        void RemoveObserver(IObserver<T> o);
        void NotifyObservers();
    }
}
