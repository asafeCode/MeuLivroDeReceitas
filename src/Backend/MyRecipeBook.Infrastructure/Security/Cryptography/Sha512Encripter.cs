﻿using System.Security.Cryptography;
using System.Text;
using MyRecipeBook.Domain.Security.Cryptography;

namespace MyRecipeBook.Infrastructure.Security.Cryptography;

public class Sha512Encripter : IPasswordEncripter
{
    private readonly string _additionalKey;
    public Sha512Encripter(string additionalKey) => _additionalKey = additionalKey;
    public string Encrypt(string password)
    {
        
        var newPassword = $"{password}{_additionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword); // Pega os bytes da Senha
        var hashBytes = SHA512.HashData(bytes); // Encripta os bytes, e retorna uma lista de bytes

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
        
    }
}