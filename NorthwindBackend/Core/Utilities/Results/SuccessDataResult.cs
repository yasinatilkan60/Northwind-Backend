using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {
        }
        public SuccessDataResult(T data) : base(data, true)
        {
        }
        public SuccessDataResult(string message) : base(default, true, message) // Data'yı default olarak geçebilirim. (Kullanmasak da olurdu.)
        {

        }
        public SuccessDataResult():base(default,true) // message'ı geçmeyebilirim. (Kullanmasak da olurdu.)
        {

        }

       
    }
}
