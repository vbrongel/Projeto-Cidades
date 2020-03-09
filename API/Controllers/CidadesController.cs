using API.Dominio;
using System.Collections.Generic;
using System.Linq;
using API.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        // GET api/values
        private readonly CidadesContext _context;

        public CidadesController(CidadesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CidadeViewModel>> Get()
        {
            try
            {
                return _context.Cidades.Select(cidade =>
                    new CidadeViewModel()
                    {
                        id = cidade.id,
                        ibge = cidade.ibge,
                        uf = cidade.uf,
                        nome = cidade.nome_cidade,
                        longitude = cidade.longitude,
                        latitude = cidade.latitude,
                        regiao = cidade.regiao

                    }).ToArray();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CidadeViewModel> Get(int id)
        {
            try
            {
                var cidade = _context.Cidades.FirstOrDefault(c => c.id == id);
                var cidadeViewModel = new CidadeViewModel();
                cidadeViewModel.id = cidade.id;
                cidadeViewModel.ibge = cidade.ibge;
                cidadeViewModel.uf = cidade.uf;
                cidadeViewModel.nome = cidade.nome_cidade;
                cidadeViewModel.longitude = cidade.longitude;
                cidadeViewModel.latitude = cidade.latitude;
                cidadeViewModel.regiao = cidade.regiao;
                return cidadeViewModel;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<CidadeViewModel> Post([FromBody] CidadeViewModel cidadePayload)
        {
            try
            {
                var cidade = new Cidade(cidadePayload.ibge, cidadePayload.uf.ToUpper(), cidadePayload.nome.ToUpper(),
                       cidadePayload.longitude, cidadePayload.latitude, cidadePayload.regiao.ToUpper());
                var ibgeIgual = _context.Cidades.FirstOrDefault(c => c.ibge == cidade.ibge);
                if (ibgeIgual != null)
                {
                    var error = new CidadeViewModel("O IBGE já existe!");
                    return error;
                }
                var cidadeJaExiste = _context.Cidades.FirstOrDefault(c => c.uf == cidade.uf && c.nome_cidade == cidade.nome_cidade);
                if (cidadeJaExiste != null)
                {
                    var error = new CidadeViewModel("Esta cidade já existe!");
                    return error;
                }
                _context.Cidades.Add(cidade);
                _context.SaveChanges();
                var newViewModel = new CidadeViewModel(cidade.id, cidade.ibge, cidade.uf, cidade.nome_cidade,
                    cidade.longitude, cidade.latitude, cidade.regiao);
                return newViewModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [HttpGet]
        [Route("reportUf")]
        public List<CidadeViewModel> GerarRelatorioCidadesPorUf()
        {
            try
            {
                List<CidadeViewModel> listaCidades = new List<CidadeViewModel>();
                List<string> listaUf = new List<string>();
                var cidades = _context.Cidades.Select(cidade =>
                    new CidadeViewModel()
                    {

                        uf = cidade.uf


                    }).ToList();
                cidades?.ForEach(cidade =>
                {
                    if (!listaUf.Contains(cidade.uf))
                    {
                        listaUf.Add(cidade.uf);
                        CidadeViewModel dados = new CidadeViewModel();
                        dados.uf = cidade.uf;
                        dados.quantidadeCidadesPorUf = _context.Cidades.Where(c => c.uf == cidade.uf).Count();
                        listaCidades.Add(dados);
                    }


                });
                return listaCidades;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpGet]
        [Route("reportRegiao")]
        public List<CidadeViewModel> GerarRelatorioCidadesPorRegiao()
        {
            try
            {
                List<CidadeViewModel> listaCidades = new List<CidadeViewModel>();
                List<string> listaRegiao = new List<string>();
                var cidades = _context.Cidades.Select(cidade =>
                    new CidadeViewModel()
                    {

                        uf = cidade.uf,
                        regiao = cidade.regiao

                    }).ToList();
                cidades?.ForEach(cidade =>
                {
                    if (!listaRegiao.Contains(cidade.uf))
                    {
                        listaRegiao.Add(cidade.uf);
                        CidadeViewModel dados = new CidadeViewModel();
                        dados.uf = cidade.uf;
                        dados.regiao = cidade.regiao;
                        dados.quantidadeCidadesPorRegiao = _context.Cidades.Where(c => c.regiao == cidade.regiao).Count();
                        listaCidades.Add(dados);
                    }


                });
                return listaCidades;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }





        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                var cidade = _context.Cidades.FirstOrDefault(c => c.id == id);
                if (cidade == null)
                {
                    throw new Exception("Not found!");
                }
                _context.Remove(cidade);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }

    public class CidadeViewModel
    {
        public int id { get; set; }
        public long ibge { get; set; }
        public string uf { get; set; }
        public string nome { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string regiao { get; set; }
        public int quantidadeCidadesPorUf { get; set; }
        public int quantidadeCidadesPorRegiao { get; set; }

        public string error { get; set; }

        public CidadeViewModel() { }


        public CidadeViewModel(string _error)
        {
            error = _error;
        }
        public CidadeViewModel(int _id, long _ibge, string _uf, string _nome, decimal _longitude, decimal _latitude, string _regiao)
        {
            id = _id;
            ibge = _ibge;
            uf = _uf;
            nome = _nome;
            longitude = _longitude;
            latitude = _latitude;
            regiao = _regiao;
            error = "";
        }
    }
}


