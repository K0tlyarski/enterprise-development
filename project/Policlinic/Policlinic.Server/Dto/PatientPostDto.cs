
namespace Policlinic.Server.Dto;

/// <summary>
/// DTO для создания нового пациента.
/// </summary>
public class PatientPostDto
{
    /// <summary>
    /// Паспортные данные пациента.
    /// </summary>
    public long Passport { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество пациента.
    /// </summary>
    public string Fio { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения пациента.
    /// </summary>
    public DateTime BirthDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Адрес проживания пациента.
    /// </summary>
    public string Address { get; set; } = string.Empty;
}
