using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        // genelde yönetim constructor ile olur.
        public Result(bool success, string message):this(success) // this ile iki parametre gelirse tek parametreli ctor'da çalıştırılmıştır.
        {
            Message = message;
        }
        public Result(bool success) // Tek parametre gelirse sadece bu ctor çalıştırılır.
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
