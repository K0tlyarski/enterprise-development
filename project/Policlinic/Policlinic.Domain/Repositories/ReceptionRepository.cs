using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Policlinic.Domain.Repositories
{
    /// <summary>
    /// Репозиторий приёмов
    /// </summary>
    public class ReceptionRepository : IRepository<Reception, int>
    {
        private readonly PoliclinicDbContext _context;

        public ReceptionRepository(PoliclinicDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Вернуть все приёмы
        /// </summary>
        /// <returns><see cref="Reception"/></returns>
        public async Task<List<Reception>> GetAll() =>
            await _context.Receptions
                .ToListAsync();

        /// <summary>
        /// Вернуть приём по идентификатору
        /// </summary>
        /// <param name="id">идентификатор приёма</param>
        /// <returns><see cref="Reception"/></returns>
        public async Task<Reception?> Get(int id) =>
            await _context.Receptions
                .FirstOrDefaultAsync(r => r.Id == id);

        /// <summary>
        /// Удалить приём по идентификатору
        /// </summary>
        /// <param name="id">идентификатор приёма</param>
        /// <returns>false при ошибке, true иначе</returns>
        public async Task<bool> Delete(int id)
        {
            var receptionToDelete = await Get(id);

            if (receptionToDelete == null) return false;

            _context.Receptions.Remove(receptionToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Добавить новый приём
        /// </summary>
        /// <param name="newObj">объект класса приёма</param>
        public async Task Post(Reception newObj)
        {
            await _context.Receptions.AddAsync(newObj);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновить приём по идентификатору
        /// </summary>
        /// <param name="newObj">объект класса приёма</param>
        /// <param name="id">идентификатор приёма</param>
        /// <returns>false при ошибке, true иначе</returns>
        public async Task<bool> Put(Reception newObj, int id)
        {
            var oldReception = await Get(id);

            if (oldReception == null) return false;

            oldReception.PatientId = newObj.PatientId;
            oldReception.Conclusion = newObj.Conclusion;
            oldReception.DateAndTime = newObj.DateAndTime;
            oldReception.DoctorId = newObj.DoctorId;
            oldReception.Status = newObj.Status;

            _context.Receptions.Update(oldReception);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
