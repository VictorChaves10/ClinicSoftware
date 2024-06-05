using ClinicSoftware.Context;
using ClinicSoftware.Models;
using ClinicSoftware.Repositories.Interfaces;

namespace ClinicSoftware.Repositories
{
    public class ProcedureRepository : IProcedureRepository
    {

        private readonly AppDbContext _context;

        public ProcedureRepository(AppDbContext context)
        {
            _context = context;
        }

        //Get all Employees
        public IEnumerable<Procedure> Procedures => _context.Procedures ?? throw new InvalidOperationException("Não foi encontrado nenhum procedimento.");


        //Add new Employee
        public async Task AddProcedure(Procedure procedure)
        {
            if (procedure == null)
            {
                throw new ArgumentNullException(nameof(procedure), "Objeto procedimento não pode ser nulo.");
            }

            try
            {
                _context.Procedures.Add(procedure);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar adicionar o procedimento.", ex);
            }
        }

        //Edit Employee
        public async Task EditProcedure(Procedure procedure)
        {
            if (procedure == null)
            {
                throw new ArgumentNullException(nameof(procedure), "Objeto procedimento não pode ser nulo.");
            }

            var existingProcedure = await _context.Procedures.FindAsync(procedure.ProcedureId) ?? throw new InvalidOperationException("Não foi localizado o procedimento.");

            _context.Entry(existingProcedure).CurrentValues.SetValues(procedure);

            await _context.SaveChangesAsync();
        }

        //Delete a Employee
        public async Task DeleteProcedure(int procedureId)
        {
            var existingProcedure = await _context.Procedures.FindAsync(procedureId) ?? throw new InvalidOperationException("Não foi localizado o procedimento.");

            _context.Procedures.Remove(existingProcedure);

            await _context.SaveChangesAsync();
        }

        //Get Employee by Id
        public async Task<Procedure> GetProcedureById(int procedureId)
        {
            var existingProcedure = await _context.Procedures.FindAsync(procedureId) ?? throw new InvalidOperationException("Não foi localizado o procedimento.");

            return  existingProcedure;
        }

       
    }
}
