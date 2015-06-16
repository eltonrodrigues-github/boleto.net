using BoletoNet;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BoletoController : Controller
    {
        // GET: Boleto
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {

            var cedente       = new Cedente()
            {
                Codigo        = "1111111",
                CPFCNPJ       = "123.456.789-01",
                Nome          = "PAULO FREIRE - FOUR FREIRES INF.",
                ContaBancaria = new ContaBancaria("1000", "", "22507", "6")
            };

            var sacado = new Sacado()
            {
                CPFCNPJ    = form["cpf"],
                Nome       = form["nome"],
                Endereco   = new Endereco()
                {
                    End    = form["end"],
                    Bairro = form["bairro"],
                    Cidade = form["cidade"],
                    UF     = form["uf"],
                    CEP    = form["cep"]
                }

            };

            var boleto = new Boleto()
            {
                DataVencimento   = Convert.ToDateTime(form["vencimento"]),
                ValorBoleto      = Convert.ToDecimal(form["valor"]),
                NossoNumero      = "22222222",
                NumeroDocumento  = "B20005446",
                Carteira         = "109",
                Cedente          = cedente,
                Sacado           = sacado,
                EspecieDocumento = new EspecieDocumento_Itau("99"),
                Instrucoes       = new List<IInstrucao>() { new Instrucao_Itau() { Descricao = "Não receber após o vencimento" } },
            };

            var boleto_bancario = new BoletoBancario()
            {
                CodigoBanco               = 341,
                Boleto                    = boleto,
                MostrarCodigoCarteira     = false,
                MostrarComprovanteEntrega = false
            };

            boleto_bancario.Boleto.Valida();

            ViewBag.Boleto = boleto_bancario.MontaHtmlEmbedded();


            return View(form);
        }
    }
}