using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data.Extensions
{
    public interface ICommand<T>
    {
        void Execute(T parameter);
    }
}
