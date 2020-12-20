using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    // Hata olduğu zaman dönecek bir middleware.
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next; // next sıradaki middleware anlamına gelir.
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                // hata olursa.
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internel Server Error";
            // bir istisna oluştur. (Bize gelen Exception ile birlikte.)
            if (e.GetType() == typeof(ValidationException)) // Hatanın tipi Validasyon ise;
            {
                message = e.Message;
            }
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
