using System.ComponentModel.DataAnnotations;

namespace Policlinic.Domain;

/// <summary>
/// Represents a patient entity in the Policlinic domain.
/// </summary>
public class Patient
{
    /// <summary>
    /// Gets or sets the unique identifier for the patient.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the passport number of the patient.
    /// </summary>
    public long Passport { get; set; }

    /// <summary>
    /// Gets or sets the full name (FIO) of the patient.
    /// </summary>
    public string Fio { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date of birth of the patient.
    /// </summary>
    public DateTime BirthDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Gets or sets the address of the patient.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of receptions associated with the patient.
    /// </summary>
    public List<Reception> Receptions { get; set; } = new List<Reception>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Patient"/> class.
    /// </summary>
    public Patient() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Patient"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier for the patient.</param>
    /// <param name="passport">The passport number of the patient.</param>
    /// <param name="fio">The full name (FIO) of the patient.</param>
    /// <param name="birthDate">The date of birth of the patient.</param>
    /// <param name="address">The address of the patient.</param>
    /// <param name="receptions">The list of receptions associated with the patient.</param>
    public Patient(
        int id,
        long passport,
        string fio,
        DateTime birthDate,
        string address,
        List<Reception> receptions)
    {
        Id = id;
        Passport = passport;
        Fio = fio;
        BirthDate = birthDate;
        Address = address;
        Receptions = receptions;
    }
}
