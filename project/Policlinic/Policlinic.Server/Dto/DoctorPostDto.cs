namespace Policlinic.Server.Dto;


public class DoctorPostDto
{

    public long Passport { get; set; }

    public string Fio { get; set; } = string.Empty;
 
    public DateTime BirthDate { get; set; } = DateTime.MinValue;

    public int WorkExperience { get; set; }
 
    public int SpecializationId { get; set; }
}