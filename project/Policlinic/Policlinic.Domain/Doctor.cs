namespace Policlinic.Domain;

/// <summary>
/// Represents a doctor entity in the Policlinic domain.
/// </summary>
public class Doctor
{
    /// <summary>
    /// Gets or sets the unique identifier for the doctor.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the full name (FIO) of the doctor.
    /// </summary>
    public string Fio { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date of birth of the doctor.
    /// </summary>
    public DateTime BirthDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Gets or sets the years of work experience the doctor has.
    /// </summary>
    public int WorkExperience { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the doctor's specialization.
    /// </summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Gets or sets the passport number of the doctor.
    /// </summary>
    public long Passport { get; set; }

    /// <summary>
    /// Gets or sets the list of receptions associated with the doctor.
    /// </summary>
    public List<Reception> Receptions { get; set; } = new List<Reception>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Doctor"/> class.
    /// </summary>
    public Doctor() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Doctor"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier for the doctor.</param>
    /// <param name="fio">The full name (FIO) of the doctor.</param>
    /// <param name="birthDate">The date of birth of the doctor.</param>
    /// <param name="workExperience">The years of work experience the doctor has.</param>
    /// <param name="passport">The passport number of the doctor.</param>
    /// <param name="receptions">The list of receptions associated with the doctor.</param>
    /// <param name="specializationId">The unique identifier for the doctor's specialization.</param>
    public Doctor(
        int id,
        string fio,
        DateTime birthDate,
        int workExperience,
        long passport,
        List<Reception> receptions,
        int specializationId)
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
