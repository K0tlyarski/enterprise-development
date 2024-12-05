using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Policlinic
{
    /// <summary>
    /// Specialization describes specializations of doctors
    /// </summary>
    public class Specialization
    {
        /// <summary>
        /// Id is an int typed value for storing Id of the specialization
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// NameSpecialization is a string typed value representing the name of specialization
        /// </summary>
        public string NameSpecialization { get; set; } = string.Empty;

        /// <summary>
        /// Doctors is a list of doctors
        /// </summary>
        public List<Doctor>? Doctors { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
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