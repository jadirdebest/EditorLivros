using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TesteEditoraLivros.Application.Interface;

namespace TesteEditoraLivros.Application.Service
{

    public class ApplicationServiceMD5 : IApplicationServiceMD5
    {
        public string CreateMD5(string word)
        {
            using(MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(word));
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    strBuilder.Append(data[i].ToString("x2"));
                }
                return strBuilder.ToString();
            }
        }

        public bool MD5IsMatch(string word, string hash)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var wordCript = CreateMD5(word);
                StringComparer compara = StringComparer.OrdinalIgnoreCase;

                if (0 == compara.Compare(wordCript, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
