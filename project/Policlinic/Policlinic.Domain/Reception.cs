using System.ComponentModel.DataAnnotations;

namespace Policlinic.Domain;

/// <summary>
/// Represents a reception entity in the Policlinic domain.
/// </summary>
public class Reception
{
    /// <summary>
    /// Gets or sets the unique identifier for the reception.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the reception.
    /// </summary>
    public DateTime DateAndTime { get; set; }

    /// <summary>
    /// Gets or sets the status of the reception.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier of the doctor associated with the reception.
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the patient associated with the reception.
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// Gets or sets the conclusion or notes for the reception.
    /// </summary>
    public string Conclusion { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="Reception"/> class.
    /// </summary>
    public Reception() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Reception"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier for the reception.</param>
    /// <param name="dateAndTime">The date and time of the reception.</param>
    /// <param name="status">The status of the reception.</param>
    /// <param name="doctorId">The unique identifier of the doctor associated with the reception.</param>
    /// <param name="patientId">The unique identifier of the patient associated with the reception.</param>
    /// <param name="conclusion">The conclusion or notes for the reception.</param>
    public Reception(
        int id,
        DateTime dateAndTime,
        string status,
        int doctorId,
        int patientId,
        string conclusion)
    {
        Id = id;
        DateAndTime = dateAndTime;
        Status = status;
        DoctorId = doctorId;
        PatientId = patientId;
        Conclusion = conclusion;
    }
}
