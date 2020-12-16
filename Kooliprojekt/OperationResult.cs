using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt
{
    public class OperationResult
    {
        public IList<string> Errors { get; private set; } = new List<string>();

        public bool HasErrors
        {
            get { return Errors.Count > 0; }
        }

        public OperationResult AddError(string error)
        {
            Errors.Add(error);

            return this;
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }

        public OperationResult<T> AddError(string error)
        {
            Errors.Add(error);

            return this;
        }
    }
}
