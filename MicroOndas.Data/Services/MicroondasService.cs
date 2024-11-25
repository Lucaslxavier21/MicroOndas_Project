namespace MicroOndas_Project.Services
{
    public class MicroondasService
    {
        private const int TEMPO_MINIMO = 1;
        private const int TEMPO_MAXIMO = 120;
        private const int POTENCIA_MINIMA = 1;
        private const int POTENCIA_MAXIMA = 10;
        private const int POTENCIA_PADRAO = 10;

        public bool ValidarTempo(int tempo)
        {
            return tempo >= TEMPO_MINIMO && tempo <= TEMPO_MAXIMO;
        }
        public bool ValidarPotencia(int potencia)
        {
            return potencia >= POTENCIA_MINIMA && potencia <= POTENCIA_MAXIMA;
        }
        public string ConverterTempo(int tempo)
        {
            if (!ValidarTempo(tempo))
            {
                throw new ArgumentException("O tempo deve estar entre 1 e 120 segundos.");
            }

            int minutos = tempo / 60;
            int segundos = tempo % 60;
            return $"{minutos:D2}:{segundos:D2}";
        }
        public string GerarStringAquecimento(int tempo, int potencia)
        {
            if (!ValidarTempo(tempo))
            {
                throw new ArgumentException("O tempo deve estar entre 1 e 120 segundos.");
            }

            if (!ValidarPotencia(potencia))
            {
                potencia = POTENCIA_PADRAO;
            }

            int quantidadeCarateres = tempo * potencia;
            return new string('.', quantidadeCarateres);
        }
    }
}
