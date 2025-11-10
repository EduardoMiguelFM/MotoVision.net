namespace MotoVision.Application.DTOs
{
    public class UsuarioPatioDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Funcao { get; set; }
        public int PatioId { get; set; }
    }
}

