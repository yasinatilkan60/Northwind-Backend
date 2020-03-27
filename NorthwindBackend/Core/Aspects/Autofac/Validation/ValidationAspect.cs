using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect:MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) // Gönderilen validatorType bir IValidator türünde değil ise
            {
                throw new Exception(AspectMessages.WrongValidationType);
            }
            _validatorType = validatorType;
        }
        // !! OnBefore'ı burada override edeceğiz.
        protected override void OnBefore(IInvocation invocation) 
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // Reflection ile IValidator türünde bir instance üretildi.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];// ihtiyacım olan karşılaştıracağım nesne. (Örneğin Product)
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // invocation method demektir. Metodun parametrelerine bak ve filtre işlemini uygula. (Product !!product!!)
            //entities birden fazla olabilir.
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
