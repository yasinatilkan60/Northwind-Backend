using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {
        //params ile her boyutta verilebilir, 0 bile verilebilir.
        public static IResult Run(params IResult[] logics) // logic dizisi parametre olarak gelsin.
        {
            foreach (var result in logics)
            {
                if (!result.Success)
                {
                    return result;
                }
            }
            return null; // hepsi success olursa null döner.
        }
    }
}
