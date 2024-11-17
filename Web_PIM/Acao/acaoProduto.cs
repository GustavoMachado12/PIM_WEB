using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using Web_PIM.Models;
using Web_PIM.Acoes;

namespace Web_PIM.Acao
{
    public class acaoProduto
    {
        conexao con = new conexao();

        public List<mProduto> PegaTodosProdutos()
        {
            List<mProduto> ProdutoLista = new List<mProduto>();

            SqlCommand cmd = new SqlCommand("EXEC pSelectProduto", con.OpenConnection());
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            con.CloseConnection();

            foreach (DataRow dr in dt.Rows)
            {
                ProdutoLista.Add(
                        new mProduto
                        {
                            idProduto = Convert.ToString(dr["Codigo"]),
                            nomeProduto = Convert.ToString(dr["Nome"]),
                            categoria = Convert.ToString(dr["Categoria"]),
                            valor = Convert.ToString(dr["Valor"]),
                            quantidade = Convert.ToInt32(dr["Quantidade"]),
                            fotoProduto = Convert.ToString(dr["Foto"])
                        }) ;
            }
            return ProdutoLista;
        }
    }
}