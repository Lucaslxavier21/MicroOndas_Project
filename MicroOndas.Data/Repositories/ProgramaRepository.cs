using Dapper;
using MicroOndas_Project.Context;
using MicroOndas_Project.Interfaces;
using MicroOndas_Project.Models;

namespace Microondas.Data.Repositories
{
    public class ProgramaRepository : IProgramaRepository
    {
        private readonly DapperContext _context;
        public ProgramaRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<ProgramaModel> AdicionarPrograma(ProgramaModel model)
        {
            var query = @"
            INSERT INTO ProgramasAquecimento (Nome, Alimento, Tempo, Potencia, Caractere, Instrucoes)
            VALUES (@Nome, @Alimento, @Tempo, @Potencia, @Caractere, @Instrucoes);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstAsync<ProgramaModel>(query, model);
        }
        public async Task<bool> VerificarCaractereDuplicadoAsync(string caractere)
        {
            var query = "SELECT COUNT(1) FROM ProgramasAquecimento WHERE Caractere = @Caractere";
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(query, new { Caractere = caractere });
            return count > 0;
        }

        public async Task<IEnumerable<ProgramaModel>> BuscarTodosProgramas()
        {
            var query = "SELECT * FROM ProgramasAquecimento";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<ProgramaModel>(query);
        }
        public async Task<ProgramaModel> ObterProgramaPorNome(string nome)
        {
            var query = "SELECT * FROM ProgramasAquecimento WHERE Nome = @nome";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<ProgramaModel>(query, new { Nome = nome });
        }
    }
}
