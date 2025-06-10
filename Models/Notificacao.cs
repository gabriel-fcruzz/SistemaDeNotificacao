namespace SistemaDeNotificacao.Models
{
    public class Notificacao
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } // ou int se usar Id numérico
        public string Mensagem { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
