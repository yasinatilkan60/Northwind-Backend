using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages // Her seferinde new'lenmesine gerek olmayan yapıdır ve bu nedenle static olarak tanımlanmıştır.
    {
        public static string ProductAdded = "Ürün başarıyla eklendi.";
        public static string ProductDeleted = "Ürün başarıyla silindi.";
        public static string ProductUpdated = "Ürün başarıyla güncellendi.";

        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Şifre hatalı.";
        public static string SucccesfulLogin = "Sisteme giriş başarılıdır.";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcuttur.";
        public static string UserRegistered = "Kullanıcı başarı ile kayıt edildi.";
        public static string AccessTokenCreated = "Access Token başarı ile oluşturuldu.";
    }
}
