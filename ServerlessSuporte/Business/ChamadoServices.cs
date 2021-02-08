using System;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ServerlessSuporte.Data;
using ServerlessSuporte.Models;
using ServerlessSuporte.Documents;
using ServerlessSuporte.Validators;

namespace ServerlessSuporte.Business
{
    public class ChamadoServices
    {
        private readonly ChamadoRepository _repository;

        public ChamadoServices(ChamadoRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Get()
        {
            return new OkObjectResult(
                _repository.ListAll().Select(c => new ChamadoDetalhado()
                {
                    Codigo = c.Codigo,
                    Nome = c.Nome,
                    Email = c.Email,
                    Descricao = c.Descricao
                }));
        }

        public IActionResult Insert(string strDadosChamado)
        {
            var dadosChamado = DeserializeDadosChamado(strDadosChamado);
            var resultado = DadosValidos(dadosChamado);
            resultado.Acao = "Inclusão de Chamado";
        
            if (resultado.Inconsistencias.Count == 0)
            {
                var dataAtual = DateTime.Now;
                _repository.Save(new ChamadoDocument()
                {
                    DataCadastro = dataAtual,
                    Codigo = $"{dataAtual:yyyyMMddHHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}",
                    Nome = dadosChamado.Nome,
                    Email = dadosChamado.Email,
                    Descricao = dadosChamado.Descricao
                });
                return new OkObjectResult(resultado);
            }
            else
                return new BadRequestObjectResult(resultado);
        }

        private Chamado DeserializeDadosChamado(string dados)
        {
            Chamado dadosChamado;
            try
            {
                dadosChamado = JsonSerializer.Deserialize<Chamado>(dados,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                dadosChamado = null;
            }

            return dadosChamado;
        }

        private Resultado DadosValidos(Chamado chamado)
        {
            var resultado = new Resultado();
            if (chamado == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do Chamado");
            }
            else
            {
                var validator = new ChamadoValidator().Validate(chamado);
                if (!validator.IsValid)
                {
                    foreach (var errors in validator.Errors)
                        resultado.Inconsistencias.Add(errors.ErrorMessage);
                }
            }

            return resultado;
        }
    }
}