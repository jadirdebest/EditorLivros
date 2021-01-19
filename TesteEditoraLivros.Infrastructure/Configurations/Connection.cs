using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEditoraLivros.Infrastructure.Configurations
{
    //Gosto de criar cada coisa no seu devido lugar, aqui iria colocar todas minhas connections strings que for utilizar
    //Em condições normais de projeto eu ja teria uma lib propria pra conexao que iria ser consumida por qualquer projeto,
    //utilizo mto isso quando se possui um banco de dados centralizado onde varias aplicações persistem desse local.
    public class Connection
    {
        public static string SqlConnectionString { get => "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jadir\\source\\repos\\TesteEditoraLivros\\TesteEditoraLivros.Infrastructure\\DataBase\\DatabaseTeste.mdf;Integrated Security=True"; }
    }
}
