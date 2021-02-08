using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServerlessSuporte.Business;

namespace ServerlessSuporte
{
    public class Chamados
    {
        private readonly ChamadoServices _chamadoSvc;

        public Chamados(ChamadoServices chamadoSvc)
        {
            _chamadoSvc = chamadoSvc;
        }
        
        [FunctionName("Chamados")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Acessada a Function Chamados");
            log.LogInformation($"Operação: {req.Method}");

            string strDadosOriginais =
                new StreamReader(req.Body).ReadToEndAsync().Result;
            log.LogInformation($"Dados originais: {strDadosOriginais}");

            switch (req.Method)
            {
                case "GET":
                    return _chamadoSvc.Get();                
                case "POST":
                    return _chamadoSvc.Insert(strDadosOriginais);
            }

            return new NotFoundResult();
        }
    }
}
