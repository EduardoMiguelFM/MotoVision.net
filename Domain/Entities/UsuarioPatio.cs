namespace MotoVision.Domain.Entities
{
    public class UsuarioPatio
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Funcao { get; set; }
        public int PatioId { get; set; }
        public required Patio Patio { get; set; }
    }
}

