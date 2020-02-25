using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {
        }
        public ErrorDataResult(T data) : base(data, false)
        {
        }
        public ErrorDataResult(string message) : base(default, false, message) // Data'yı default olarak geçebilirim. (Kullanmasak da olurdu.)
        {
        }
        public ErrorDataResult() : base(default, false) // message'ı geçmeyebilirim. (Kullanmasak da olurdu.)
        {
        }
    }
}
