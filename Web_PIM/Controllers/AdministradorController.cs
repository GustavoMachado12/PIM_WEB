using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public ActionResult EditaClienteF(int id)
        {
            var cliente = acCliente.consultaClientePorId(id); 
            if (cliente != null)
            {
                return View(cliente); 
            }
            else
            {

                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public JsonResult GetClienteById(int id)
        {
            try
            {
                var cliente = acCliente.consultaClientePorId(id); 
                if (cliente != null)
                {
                    return Json(new
                    {
                        success = true,
                        nome = cliente.nome,
                        email = cliente.email,
                        telefone = cliente.telefone,
                        documento = cliente.documento,
                        cep = cliente.cep,
                        logradouro = cliente.logradouro,
                        numLogradouro = cliente.numLogradouro,
                        bairro = cliente.bairro,
                        cidade = cliente.cidade,
                        estado = cliente.estado,
                        complemento = cliente.complemento
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Cliente não encontrado." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult excluiClienteF(int id)
        {
            acCliente.deletaClienteF(id);
            return View();
        }
    }
}