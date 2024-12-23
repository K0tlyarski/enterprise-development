using System.ComponentModel.DataAnnotations;

namespace Policlinic.Domain;

/// <summary>
/// Represents a specialization entity in the Policlinic domain.
/// </summary>
public class Specialization
{
    /// <summary>
    /// Gets or sets the unique identifier for the specialization.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the specialization.
    /// </summary>
    public string NameSpecialization { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of doctors associated with the specialization.
    /// </summary>
    public List<Doctor>? Doctors { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Specialization"/> class.
    /// </summary>
    public Specialization() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Specialization"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier for the specialization.</param>
    /// <param name="nameSpecialization">The name of the specialization.</param>
    public Specialization(int id, string nameSpecialization)
    {
        Id = id;
        NameSpecialization = nameSpecialization;
    }
}
