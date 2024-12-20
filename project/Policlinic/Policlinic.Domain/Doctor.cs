using System;
using System.Collections.Generic;

namespace Policlinic.Domain
{

    public class Doctor
    {

        public int Id { get; set; }

   
        public string Fio { get; set; } = string.Empty;

  
        public DateTime BirthDate { get; set; } = DateTime.MinValue;

  
        public int WorkExperience { get; set; }

  
        public int SpecializationId { get; set; }

   
        public long Passport { get; set; }

        public List<Reception> Receptions { get; set; } = new List<Reception>();

  
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