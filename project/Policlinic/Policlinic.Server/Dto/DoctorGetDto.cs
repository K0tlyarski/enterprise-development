namespace Policlinic.Server.Dto;


public class DoctorGetDto
{

    public int Id { get; set; }
 
    public long Passport { get; set; }
 
    public string Fio { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; } = DateTime.MinValue;

    public int WorkExperience { get; set; }

    public int SpecializationId { get; set; }
}