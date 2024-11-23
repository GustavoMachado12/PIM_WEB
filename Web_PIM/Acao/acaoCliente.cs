using Web_PIM.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Linq;

namespace Web_PIM.Acoes
{
    public class acaoCliente
    {
        conexao con = new conexao();


        //CONSULTA CLIENTE FISICO 
        public List<mCliente> consultaClienteF()
        {
            List<mCliente> ClienteLista = new List<mCliente>();

            SqlCommand cmd = new SqlCommand("EXEC pSelectClienteF_Descripto", con.OpenConnection());
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                ClienteLista.Add(
                        new mCliente
                        {
                            id = Convert.ToInt32(dr["Codigo"]),
                            nome = Convert.ToString(dr["Nome"]),
                            email = Convert.ToString(dr["E-mail"]),
                            endereco = Convert.ToString(dr["Endereco"]),
                            telefone = Convert.ToString(dr["Telefone"]),
                            documento = Convert.ToString(dr["CPF"])
                        });
            }
            con.CloseConnection();

            return ClienteLista;
        }

        //DELETA CLIENTE FISICO
        public bool deletaClienteF(int id)
        {
            SqlCommand cmd = new SqlCommand("pExcluiCliente @CodCliente", con.OpenConnection());
            cmd.Parameters.AddWithValue("@CodCliente", id);

            int i = cmd.ExecuteNonQuery();
            con.CloseConnection();

            if (i >= 1)
                return true;
                
            else
                return false;
        }

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

        //PEGA ULTIMO CADASTRO DO LOGIN
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

        //CONSULTA PELO ID
        public mCliente consultaClientePorId(int id)
        {
            mCliente cliente = null;

            SqlCommand cmd = new SqlCommand("EXEC pSelectClienteF_PorID @CodCliente", con.OpenConnection());
            cmd.Parameters.AddWithValue("@CodCliente", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cliente = new mCliente
                    {
                        id = Convert.ToInt32(dr["Codigo"]),
                        nome = dr["Nome"].ToString(),
                        email = dr["E-mail"].ToString(),
                        telefone = dr["Telefone"].ToString(),
                        documento = dr["CPF"].ToString(), 
                        endereco = dr["Endereco"].ToString(),
                    };
                }
            }

            SepararEndereco(cliente);

            con.CloseConnection();

            return cliente;
        }

        //ATUALIZA CLIENTE FISICO
        public mCliente atualizaClienteF(mCliente cliente)
        {
            SqlCommand cmd = new SqlCommand("pAlteraClienteF", con.OpenConnection());


            return cliente;
        }

        //SEPARA ENDERECO
        public void SepararEndereco(mCliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.endereco))
                throw new ArgumentException("O endereço completo não pode ser vazio ou nulo.");
            var partes = cliente.endereco.Split(',');

            if (partes.Length < 5)
                throw new ArgumentException("Formato de endereço inválido.");

            cliente.cep = partes[0].Trim();
            cliente.logradouro = partes[1].Trim();
            cliente.bairro = partes[2].Trim();
            cliente.cidade = partes[3].Trim();
            cliente.estado = partes[4].Trim();

            if (cliente.logradouro.Contains(" "))
            {
                var logradouroPartes = cliente.logradouro.Split(' ');
                if (int.TryParse(logradouroPartes.Last(), out int numero))
                {
                    cliente.numLogradouro = numero;
                    cliente.logradouro = string.Join(" ", logradouroPartes.Take(logradouroPartes.Length - 1));
                }
            }

            if (partes.Length > 5)
            {
                cliente.complemento = partes[5].Trim();
            }
        }
    }
}
