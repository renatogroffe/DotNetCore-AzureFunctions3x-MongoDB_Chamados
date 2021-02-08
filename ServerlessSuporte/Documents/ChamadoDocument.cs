using System;
using MongoDB.Bson;

namespace ServerlessSuporte.Documents
{
    public class ChamadoDocument
    {
        public ObjectId _id { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
    }
}