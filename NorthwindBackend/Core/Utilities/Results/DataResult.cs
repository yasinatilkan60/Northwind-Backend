using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T> // Result'tan farkı data'sının olmasıdır.
    {
        // base ile success ve message "Result'a" gönderilir. 
        public DataResult(T data,bool success, string message) : base(success,message) // Data'da gönderilirse burası çalışır.
        {
            Data = data;
        }
        public DataResult(T data,bool success):base(success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
