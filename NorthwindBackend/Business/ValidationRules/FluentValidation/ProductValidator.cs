using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    // Product Entity'sini doğrulayacak olan validation'ı yazalım.
    // Önemli !! Inheritance class ya da abstract class ile olabilir. Implement ise interface ile olacaktır.
    // Buradan veritabanına vs. bağlanmaya çalışılmaz. Sadece validation işlemi kullanılmalıdır. ( Business yok!)
    public class ProductValidator : AbstractValidator<Product> // ProductValidator'ın baseType'ı AbstractValidator'dur.
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).Length(2, 30);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When( p=> p.CategoryId == 1); // İçecek kategorisinde ise fiyat en az 10 tl olmalıdır. 
            RuleFor(p => p.ProductName).Must(StartsWithWithA);
        }

        private bool StartsWithWithA(string arg) // Kendi custom kurallarımızı da yazabiliriz.
        {
            return arg.StartsWith("A"); // büyük A ile başlamalı.
        }
    }
}
