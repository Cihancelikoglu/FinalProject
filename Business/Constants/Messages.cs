using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Core.Entities.Concrete;

namespace Business.Constants
{
    //Constants : Sabit demek proje sabitlerimizi burada tutarız
    public static class Messages
    {
        public static string CheckIfCategoryLimitExceded = "Kategori limiti aşıldığı için yeni kategori eklenemiyor.";
        public static string ProductCountOfCategoryError = "Aynı kategoriden en fazla 10 adet eklenebilir.";
        public static string ProductNameAlreadyExists = "Aynı isimde bir kayıt mevcut";
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string ProductListed = "Ürünler Listelendi";
        public static string MaintenanceTime = "Sistem Bakım Zamanı";
        public static string UnitPriceInvalid = "Fiyat Hatalı";
        public static string AuthorizationDenied = "Yetkiniz Yok";
        public static string UserRegistered = "Kayıt Oldu";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Parola Hatalı";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UserAlreadyExists = "Kullanıcı Mevcut";
        public static string AccessTokenCreated = "Token Oluşturuldu";
    }
}
