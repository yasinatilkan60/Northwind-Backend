using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; } // işlem başarılı mı ? Bu sadece read only'dir.
        string Message { get; } // Yapılan işlemin mesajı.
    }
}
