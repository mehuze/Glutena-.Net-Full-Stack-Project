using Microsoft.AspNetCore.Identity;

namespace Glutena.Models
{
    public class TurkishIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email) =>
            new() { Code = nameof(DuplicateEmail), Description = $"'{email}' adresiyle zaten bir hesap var." };

        public override IdentityError DuplicateUserName(string userName) =>
            new() { Code = nameof(DuplicateUserName), Description = $"'{userName}' adresiyle zaten bir hesap var." };

        public override IdentityError InvalidEmail(string? email) =>
            new() { Code = nameof(InvalidEmail), Description = "Geçerli bir e-posta adresi girin." };

        public override IdentityError InvalidUserName(string? userName) =>
            new() { Code = nameof(InvalidUserName), Description = "E-posta adresi geçersiz karakterler içeriyor." };

        public override IdentityError PasswordTooShort(int length) =>
            new() { Code = nameof(PasswordTooShort), Description = $"Şifre en az {length} karakter olmalı." };

        public override IdentityError PasswordRequiresDigit() =>
            new() { Code = nameof(PasswordRequiresDigit), Description = "Şifrede en az bir rakam (0-9) olmalı." };

        public override IdentityError PasswordRequiresLower() =>
            new() { Code = nameof(PasswordRequiresLower), Description = "Şifrede en az bir küçük harf olmalı." };

        public override IdentityError PasswordRequiresUpper() =>
            new() { Code = nameof(PasswordRequiresUpper), Description = "Şifrede en az bir büyük harf olmalı." };

        public override IdentityError PasswordRequiresNonAlphanumeric() =>
            new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Şifrede en az bir özel karakter (!, ? vb.) olmalı." };

        public override IdentityError PasswordMismatch() =>
            new() { Code = nameof(PasswordMismatch), Description = "Şifre hatalı." };

        public override IdentityError DefaultError() =>
            new() { Code = nameof(DefaultError), Description = "Bilinmeyen bir hata oluştu, lütfen tekrar deneyin." };
    }
}
