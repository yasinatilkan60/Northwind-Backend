using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        // Object yerine IEntity'de geçebilirdim, fakat dto'da alabilmem için object kullandım.  
        public static void Validate(IValidator validator, object entity) // IValidator fluent validation içerisinden gelir. 
        {
            var result = validator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            } //  Burada bu işlemi business'da yapmak yerine burada yaparak merkezi yaklaşımı kullandık.
        }
    }
}
