using Fbarbosa.Models;
using System.Data;
using System.Data.SqlClient;

namespace Fbarbosa.Acoes
{
    public class acaoLogin
    {
        conexao con = new conexao();


        public void TestaLogin(mLogin cmLogin)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                "EXEC pVerificaDadosLogin @login, @senha", con.OpenConnection());
                cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = cmLogin.login;
                cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = cmLogin.senha;

                Console.WriteLine(cmd);


                SqlDataReader r;
                r = cmd.ExecuteReader();
                try
                {
                    if (r.HasRows)
                    {
                        while (r.Read())
                        {
                            cmLogin.name = Convert.ToString(r["Nome"]);
                            cmLogin.nvlAcesso = Convert.ToString(r["Nivel"]);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }




                con.CloseConnection();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

        }


    }
}
