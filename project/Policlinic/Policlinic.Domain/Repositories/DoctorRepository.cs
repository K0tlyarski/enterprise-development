using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Policlinic.Domain.Repositories
{
    /// <summary>
    /// Репозиторий докторов
    /// </summary>
    public class DoctorRepository : IRepository<Doctor, int>
    {
        private readonly PoliclinicDbContext _context;

        public DoctorRepository(PoliclinicDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Вернуть всех докторов
        /// </summary>
        /// <returns><see cref="Doctor"/></returns>
        public async Task<List<Doctor>> GetAll() => await _context.Doctors.ToListAsync();

        /// <summary>
        /// Вернуть доктора по идентификатору
        /// </summary>
        /// <param name="id">идентификатор доктора</param>
        /// <returns><see cref="Doctor"/></returns>
        public async Task<Doctor?> Get(int id) => await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

        /// <summary>
        /// Удалить доктора по идентификатору
        /// </summary>
        /// <param name="id">идентификатор доктора</param>
        /// <returns>false при ошибке,true иначе</returns>
        public async Task<bool> Delete(int id)
        {
            var deletedDoctor = await Get(id);

            if (deletedDoctor == null) return false;

            _context.Doctors.Remove(deletedDoctor);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Добавить доктора
        /// </summary>
        /// <param name="newObj">объект класса доктора</param>
        public async Task Post(Doctor newObj)
        {
            await _context.Doctors.AddAsync(newObj);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Изменить доктора по идентификатору
        /// </summary>
        /// <param name="newObj">объект класса доктора</param>
        /// <param name="id">идентификатор доктора</param>
        /// <returns>false при ошибке,true иначе</returns>
        public async Task<bool> Put(Doctor newObj, int id)
        {
            var oldDoctor = await Get(id);

            if (oldDoctor == null) return false;

            oldDoctor.Fio = newObj.Fio;
            oldDoctor.BirthDate = newObj.BirthDate;
            oldDoctor.Passport = newObj.Passport;
            oldDoctor.WorkExperience = newObj.WorkExperience;
            oldDoctor.SpecializationId = newObj.SpecializationId;

            _context.Doctors.Update(oldDoctor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
