using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult // IResult içindeki success ve message var.
    {
        // success ve message'a ek olarak.
        T Data { get; }
    }
}
