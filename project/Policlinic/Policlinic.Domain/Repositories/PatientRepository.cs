using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Policlinic.Domain.Repositories
{
    /// <summary>
    /// Репозиторий пациентов
    /// </summary>
    public class PatientRepository : IRepository<Patient, int>
    {
        private readonly PoliclinicDbContext _context;

        public PatientRepository(PoliclinicDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Вернуть всех пациентов
        /// </summary>
        /// <returns><see cref="Patient"/></returns>
        public async Task<List<Patient>> GetAll() => await _context.Patients.ToListAsync();

        /// <summary>
        /// Вернуть пациента по идентификатору
        /// </summary>
        /// <param name="id">идентификатор пациента</param>
        /// <returns><see cref="Patient"/></returns>
        public async Task<Patient?> Get(int id) => await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

        /// <summary>
        /// Удалить пациента по идентификатору
        /// </summary>
        /// <param name="id">идентификатор пациента</param>
        /// <returns>false при ошибке, true иначе</returns>
        public async Task<bool> Delete(int id)
        {
            var deletedPatient = await Get(id);

            if (deletedPatient == null) return false;

            _context.Patients.Remove(deletedPatient);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Добавить пациента
        /// </summary>
        /// <param name="newObj">объект класса пациента</param>
        public async Task Post(Patient newObj)
        {
            await _context.Patients.AddAsync(newObj);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Изменить пациента по идентификатору
        /// </summary>
        /// <param name="newObj">объект класса пациента</param>
        /// <param name="id">идентификатор пациента</param>
        /// <returns>false при ошибке, true иначе</returns>
        public async Task<bool> Put(Patient newObj, int id)
        {
            var oldPatient = await Get(id);

            if (oldPatient == null) return false;

            oldPatient.Fio = newObj.Fio;
            oldPatient.BirthDate = newObj.BirthDate;
            oldPatient.Address = newObj.Address;
            oldPatient.Passport = newObj.Passport;

            _context.Patients.Update(oldPatient);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
