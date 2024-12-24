
namespace Policlinic.Server.Dto;

/// <summary>
/// DTO для получения данных о докторе.
/// </summary>
public class DoctorGetDto
{
    /// <summary>
    /// Уникальный идентификатор доктора.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Паспортные данные доктора.
    /// </summary>
    public long Passport { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество доктора.
    /// </summary>
    public string Fio { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения доктора.
    /// </summary>
    public DateTime BirthDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Стаж работы доктора (в годах).
    /// </summary>
    public int WorkExperience { get; set; }

    /// <summary>
    /// Идентификатор специализации доктора.
    /// </summary>
    public int SpecializationId { get; set; }
}
