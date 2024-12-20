namespace Policlinic.Server.Dto;

public class ReceptionDto
{

    public int Id { get; set; }

    public DateTime DateAndTime { get; set; }
 
    public string Status { get; set; } = string.Empty;
 
    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public string Conclusion { get; set; } = string.Empty;
}