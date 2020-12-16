using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data.Extensions
{
    public abstract class CompositeCommandBase<T> : ICommand<T>
    {
        protected IList<ICommand<T>> Children { get; private set; }
        public CompositeCommandBase()
        {
            Children = new List<ICommand<T>>();
        }
        public void Execute(T parameter)
        {
            foreach (var command in Children)
            {
                command.Execute(parameter);
            }
        }
    }
}
