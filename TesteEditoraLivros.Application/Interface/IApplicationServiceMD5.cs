using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEditoraLivros.Application.Interface
{
    public interface IApplicationServiceMD5
    {
        string CreateMD5(string word);
        bool MD5IsMatch(string word, string hash);

    }
}
