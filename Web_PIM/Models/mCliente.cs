using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_PIM.Models
{
    public class mCliente
    {
        [DisplayName("Código")]
        public int id { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("E-mail")]
        public string email { get; set;}

        [DisplayName("Endereço")]
        public string endereco { get; set;}

        [DisplayName("Telefone")]
        public string telefone { get; set;}

        [DisplayName("Documento")]
        public string documento { get; set;}



        [DisplayName("Login")]
        public string login { get; set;}

        [DisplayName("Senha")]
        public string senha { get; set;}

        [DisplayName("Confirma Senha")]
        public string confirmaSenha { get; set;}

    }
}
