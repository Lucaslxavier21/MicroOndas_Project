using MicroOndas_Project.Interfaces;
using MicroOndas_Project.Models;

namespace MicroOndas_Project.Services
{
    public class ProgramaService
    {
        private readonly IProgramaRepository _repository;
        private static List<ProgramaModel> _programasPreDefinidos;
        public ProgramaService(IProgramaRepository repository)
        {
            _repository = repository;
            _programasPreDefinidos = new List<ProgramaModel>
        {
            new ProgramaModel
            {
                Nome = "Pipoca",
                Alimento = "Pipoca (de micro-ondas)",
                Tempo = 180, // 3 minutos
                Potencia = 7,
                Caractere = "*",
                Instrucoes = "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."
            },
            new ProgramaModel
            {
                Nome = "Leite",
                Alimento = "Leite",
                Tempo = 300, // 5 minutos
                Potencia = 5,
                Caractere = "#",
                Instrucoes = "Cuidado com aquecimento de líquidos. O choque térmico aliado ao movimento do recipiente pode causar fervura imediata, causando risco de queimaduras."
            },
            new ProgramaModel
            {
                Nome = "Carnes de boi",
                Alimento = "Carne em pedaço ou fatias",
                Tempo = 840, // 14 minutos
                Potencia = 4,
                Caractere = "@",
                Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."
            },
            new ProgramaModel
            {
                Nome = "Frango",
                Alimento = "Frango (qualquer corte)",
                Tempo = 480, // 8 minutos
                Potencia = 7,
                Caractere = "%",
                Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."
            },
            new ProgramaModel
            {
                Nome = "Feijão",
                Alimento = "Feijão congelado",
                Tempo = 480, // 8 minutos
                Potencia = 9,
                Caractere = "&",
                Instrucoes = "Deixe o recipiente destampado. Em casos de plástico, cuidado ao retirar o recipiente, pois o mesmo pode perder resistência em altas temperaturas."
            }
        };
        }
        public async Task<IEnumerable<ProgramaModel>> ObterTodosProgramasAsync()
        {
            var listaRetorno = new List<ProgramaModel>();

            if (_programasPreDefinidos != null)
                listaRetorno.AddRange(_programasPreDefinidos);

            var listaProgramas = await _repository.BuscarTodosProgramas();

            if (listaProgramas != null)
                listaRetorno.AddRange(listaProgramas);

            return listaRetorno;
        }
        public async Task<bool> ValidarCaractereAsync(string caractere)
        {
            var caractereExistente = await _repository.VerificarCaractereDuplicadoAsync(caractere);

            if (caractereExistente)
                throw new ArgumentException("Caractere já está em uso ou é inválido.");

            return caractereExistente;
        }
        public async Task<ProgramaModel> AdicionarPrograma(ProgramaModel model)
        {
            if (model.Tempo < 1 || model.Tempo > 120)
                throw new ArgumentException("Tempo deve estar entre 1 e 120 segundos.");

            if (model.Potencia < 1 || model.Potencia > 10)
                throw new ArgumentException("Potência deve estar entre 1 e 10.");

            await ValidarCaractereAsync(model.Caractere);

            return await _repository.AdicionarPrograma(model);
        }
        public async Task<ProgramaModel> ObterProgramaPorNome(string nome)
        {
            var listaProgramas = await _repository.BuscarTodosProgramas();

            var programa = listaProgramas.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            var programaPreDefinido = _programasPreDefinidos.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (programa == null && programaPreDefinido != null)
            {
                programa = new ProgramaModel
                {
                    Nome = programaPreDefinido.Nome,
                    Alimento = programaPreDefinido.Alimento,
                    Tempo = programaPreDefinido.Tempo,
                    Potencia = programaPreDefinido.Potencia,
                    Caractere = programaPreDefinido.Caractere,
                    Instrucoes = programaPreDefinido.Instrucoes
                };
            }

            return programa;
        }
    }
}
