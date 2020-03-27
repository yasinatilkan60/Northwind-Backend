using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Constants;

namespace Business.BusinessAspect.Autofac
{
    public class SecuredOperation:MethodInterception
    {
        private string[] _roles; // private field'a _ verilir.
        private IHttpContextAccessor _httpContextAccessor;
        public SecuredOperation(string roles)
        {

            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        // Artık kişinin claimlerine erişebileceğimiz bir yapı bulunmaktadır.
        protected override void OnBefore(IInvocation invocation) // Operasyon başlamadan önce bu çalışsın.
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            // her bir rolü gez. Role sahipse sıkıntı yok.
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return; // varsa olduğu gibi return et. Yani bırak devam etsin.
                }
                throw new Exception(Messages.AuthorizationDenied);
            }


        }
    }
}
