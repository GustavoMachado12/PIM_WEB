using Web_PIM.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Web_PIM.Acoes
{
    public class acaoCliente
    {
        conexao con = new conexao();


        //CADASTRA CLIENTE FISICO
        public void cadastraClienteF(mCliente cmCliente)
        {
            SqlCommand cmd = new SqlCommand("pCadastraClienteF", con.OpenConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.Add("@NomeCli", SqlDbType.VarChar).Value = cmCliente.nome;
                cmd.Parameters.Add("@EmailCli", SqlDbType.VarChar).Value = cmCliente.email;
                cmd.Parameters.Add("@EnderecoCli", SqlDbType.VarChar).Value = "Não Informado";
                cmd.Parameters.Add("@TelefoneCli", SqlDbType.VarChar).Value = cmCliente.telefone;
                cmd.Parameters.Add("@CPF_Cli", SqlDbType.VarChar).Value = cmCliente.documento;

                int linhasAfetadas = cmd.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    Console.WriteLine("Cliente cadastrado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Nenhuma alteração foi realizada.");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.CloseConnection();
            }

            pegaUltimoCadastro(cmCliente);
        }


        //CADASTRA CLIENTE JURIDICO
        public void cadastraClienteJ(mCliente cmCliente)
        {
            SqlCommand cmd = new SqlCommand("pCadastraClienteJ", con.OpenConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.Add("@NomeCli", SqlDbType.VarChar).Value = cmCliente.nome;
                cmd.Parameters.Add("@EmailCli", SqlDbType.VarChar).Value = cmCliente.email;
                cmd.Parameters.Add("@EnderecoCli", SqlDbType.VarChar).Value = "Não Informado";
                cmd.Parameters.Add("@TelefoneCli", SqlDbType.VarChar).Value = cmCliente.telefone;
                cmd.Parameters.Add("@CNPJ_Cli", SqlDbType.VarChar).Value = cmCliente.documento;

                int linhasAfetadas = cmd.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    Console.WriteLine("Cliente cadastrado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Nenhuma alteração foi realizada.");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.CloseConnection();
            }

            pegaUltimoCadastro(cmCliente);
        }


        //CADASTRA LOGIN 
        public void cadastraLogin(mCliente cmCliente)
        {
            SqlCommand cmd = new SqlCommand("pCadastraLoginCliente", con.OpenConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@pCdCliente", SqlDbType.Int).Value = cmCliente.id;
            cmd.Parameters.Add("@pLogin", SqlDbType.VarChar).Value = cmCliente.email;
            cmd.Parameters.Add("@pSenha", SqlDbType.VarChar).Value = cmCliente.senha;

            int linhasAfetadas = cmd.ExecuteNonQuery();

            if (linhasAfetadas > 0)
            {
                Console.WriteLine("Cliente cadastrado com sucesso.");
            }
            else
            {
                Console.WriteLine("Nenhuma alteração foi realizada.");
            }

            con.CloseConnection();
        }


        public void pegaUltimoCadastro(mCliente cmCliente)
        {
            SqlCommand cmd = new SqlCommand(
               "SELECT * FROM vVisualizaCliente ORDER BY Codigo DESC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", con.OpenConnection());
            Console.WriteLine(cmd);

            SqlDataReader r;
            r = cmd.ExecuteReader();

            if (r.HasRows)
            {
                while (r.Read())
                {
                    cmCliente.id = Convert.ToInt32(r["Codigo"]);
                }
            }

            con.CloseConnection();
        }


    }
}
