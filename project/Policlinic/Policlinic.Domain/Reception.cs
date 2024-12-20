using System;
using System.ComponentModel.DataAnnotations;

namespace Policlinic.Domain
{

    public class Reception
    {
     
        [Key]
        public int Id { get; set; }

    
        public DateTime DateAndTime { get; set; }

    
        public string Status { get; set; } = string.Empty;

        public int DoctorId { get; set; }

      
        public int PatientId { get; set; }


        public string Conclusion { get; set; } = string.Empty;

        public Reception() { }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="id">Reception's id</param>
        /// <param name="dateAndTime">Reception's date and time</param>
        /// <param name="status">Reception's status</param>
        /// <param name="doctorId">Doctor's id</param>
        /// <param name="patientId">Patient's id</param>
        /// <param name="conclusion">Reception's conclusion</param>
        public Reception(int id, DateTime dateAndTime, string status, int doctorId, int patientId, string conclusion)
        {
            Id = id;
            DateAndTime = dateAndTime;
            Status = status;
            DoctorId = doctorId;
            PatientId = patientId;
            Conclusion = conclusion;
        }
    }
}