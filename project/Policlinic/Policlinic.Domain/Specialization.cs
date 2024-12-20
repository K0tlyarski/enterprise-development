using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Policlinic.Domain
{

    public class Specialization
    {

        [Key]
        public int Id { get; set; }

        public string NameSpecialization { get; set; } = string.Empty;

    
        public List<Doctor>? Doctors { get; set; }

        public Specialization() { }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="id">Specialization's id</param>
        /// <param name="nameSpecialization">Specialization's name</param>
        public Specialization(int id, string nameSpecialization)
        {
            Id = id;
            NameSpecialization = nameSpecialization;
        }
    }
}