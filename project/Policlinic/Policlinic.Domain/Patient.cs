using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Policlinic.Domain
{

    public class Patient
    {
       
        [Key]
        public int Id { get; set; }

 
        public long Passport { get; set; }

     
        public string Fio { get; set; } = string.Empty;

       
        public DateTime BirthDate { get; set; } = DateTime.MinValue;

     
        public string Address { get; set; } = string.Empty;

     
        public List<Reception> Receptions { get; set; } = new List<Reception>();


        public Patient() { }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="id">Patient's id</param>
        /// <param name="passport">Patient's number of passport</param>
        /// <param name="fio">Patient's FIO</param>
        /// <param name="birthDate">Patient's birth date</param>
        /// <param name="address">Patient's address</param>
        /// <param name="receptions">Receptions</param>
        public Patient(int id, long passport, string fio, DateTime birthDate, string address, List<Reception> receptions)
        {
            Id = id;
            Passport = passport;
            Fio = fio;
            BirthDate = birthDate;
            Address = address;
            Receptions = receptions;
        }
    }
}
