using ClinicSoftware.Models;

namespace ClinicSoftware.Repositories.Interfaces
{
    public interface IProcedureRepository
    {
        Task AddProcedure(Procedure procedure);
        Task EditProcedure(Procedure procedure);
        Task DeleteProcedure(int procedureId);
        Task<Procedure> GetProcedureById(int procedureId);
        IEnumerable<Procedure> Procedures { get; }
    }
}
