namespace MicroOndas_Project.Models
{
    public class ProgramaModel
    {
        public int IdPrograma { get; set; }
        public string Nome { get; set; }
        public string Alimento { get; set; }
        public int Tempo { get; set; } 
        public int Potencia { get; set; }
        public string Caractere { get; set; }
        public string Instrucoes { get; set; }
        public bool IsPreDefinido { get; set; }
    }
}
