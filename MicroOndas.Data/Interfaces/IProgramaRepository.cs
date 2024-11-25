using MicroOndas_Project.Models;

namespace MicroOndas_Project.Interfaces
{
    public interface IProgramaRepository
    {
        Task<IEnumerable<ProgramaModel>> BuscarTodosProgramas();
        Task<ProgramaModel> ObterProgramaPorNome(string nome);
        Task<ProgramaModel> AdicionarPrograma(ProgramaModel model);
        Task<bool> VerificarCaractereDuplicadoAsync(string caractere);
    }
}
