namespace ProdutosApi.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Endpoint { get; set; }
        public string Metodo { get; set; }
        public DateTime DataHora { get; set; }
        public string IP { get; set; }
    }
}
