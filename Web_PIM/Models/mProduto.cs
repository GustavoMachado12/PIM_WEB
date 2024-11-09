using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_PIM.Models
{
    public class mProduto
    {
        public int id { get; set; }

        public string nomeProduto { get; set; }

        public string valor {  get; set; }

        public string categoria { get; set; }

        public int quantidade { get; set; } 

        public string status { get; set;}
    }
}