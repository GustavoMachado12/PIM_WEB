using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_PIM.Acoes;
using Web_PIM.Models;

namespace Web_PIM.Controllers
{
    public class AdministradorController : Controller
    {
        conexao con = new conexao();
        acaoCliente acCliente = new acaoCliente();


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ConsultaClienteF()
        {
            if (Session["Administrador"] != null)
            {
                ViewBag.Clientes = acCliente.consultaClienteF();
                return View(new mCliente());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult excluiClienteF(int id)
        {
            acCliente.deletaClienteF(id);
            return View();
        }
    }
}