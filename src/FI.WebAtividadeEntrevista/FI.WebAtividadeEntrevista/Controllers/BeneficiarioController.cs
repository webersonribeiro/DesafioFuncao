using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        [HttpPost]
        public JsonResult Listar(long IdCliente)
        {
            try
            {
                List<Beneficiario> beneficiarios = new BoBeneficiario().Pesquisa(IdCliente);             

                return Json(new
                {
                    Result = "OK",
                    Records = beneficiarios,
                    TotalRecordCount = beneficiarios.Count
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }


        [HttpPost]
        public JsonResult Salvar(List<Beneficiario> beneficiarios)
        {
            try
            {
                BoBeneficiario boBeneficiario = new BoBeneficiario();
                boBeneficiario.Salvar(beneficiarios);
                
                return Json(new
                {
                    Result = "OK"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }
   
    }
}