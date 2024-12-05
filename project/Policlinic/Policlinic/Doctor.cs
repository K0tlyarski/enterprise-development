using System;
using System.Collections.Generic;

namespace Policlinic
{
    /// <summary>
    /// Doctor describes a doctor
    /// </summary>
    public class Doctor
    {
        /// <summary>
        /// Id is an int typed value of the doctor's id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fio is a string typed value for storing the name, surname and patronymic of the doctor
        /// </summary>
        public string Fio { get; set; } = string.Empty;

        /// <summary>
        /// BirthDate is a datetime value of a doctor's birthday
        /// </summary>
        public DateTime BirthDate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// WorkExperience is an int typed value of the doctor's work experience
        /// </summary>
        public int WorkExperience { get; set; }

        /// <summary>
        /// SpecializationId is an int typed value for storing the id of a specialization
        /// </summary>
        public int SpecializationId { get; set; }

        /// <summary>
        /// Passport is a long int typed value of the passport series and number
        /// </summary>
        public long Passport { get; set; }

        /// <summary>
        /// Receptions is a list of receptions
        /// </summary>
        public List<Reception> Receptions { get; set; } = new List<Reception>();

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Doctor() { }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="id">Doctor's id </param>
        /// <param name="fio">Doctor's FIO</param>
        /// <param name="birthDate">Doctor's birth date</param>
        /// <param name="workExperience">Doctor's work experience</param>
        /// <param name="specializationId">Specialization's id</param>
        /// <param name="passport">Doctor's number of passport</param>
        /// <param name="receptions">Receptions</param>
        public Doctor(int id, string fio, DateTime birthDate, int workExperience, long passport, List<Reception> receptions, int specializationId)
        {
            Id = id;
            Fio = fio;
            BirthDate = birthDate;
            WorkExperience = workExperience;
            SpecializationId = specializationId;
            Passport = passport;
            Receptions = receptions;
        }
    }
}