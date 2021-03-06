using System;
namespace API.Dominio {
    public class Cidade {
        public int id { get; set; }
        public long ibge { get; set; }
        public string uf { get; set; }
        public string nome_cidade { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string regiao { get; set; }

        public Cidade() { }
        public Cidade(long _ibge, string _uf, string _nome_cidade, decimal _longitude, decimal _latitude, string _regiao)
        {
            // Como � um API, � necess�rio fazer esta valida��o, caso seja acessado por um front que n�o implemente a valida��o de preenchimento de campos...
            if (_ibge <= 0 || _uf == null || _nome_cidade == null || _longitude == 0|| _latitude == 0 || _regiao == null)
            {
                throw new Exception("H� campos que n�o foram preenchidos!");
            }

            ibge = _ibge;
            uf = _uf;
            nome_cidade = _nome_cidade;
            longitude = _longitude;
            latitude = _latitude;
            regiao = _regiao;

        }

             
    }
}