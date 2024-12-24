
namespace Policlinic.Server.Dto;

/// <summary>
/// DTO для представления количества пациентов и их ФИО.
/// </summary>
public class CountPatientDto
{
    /// <summary>
    /// Количество пациентов.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество пациента.
    /// </summary>
    public string Fio { get; set; }
}
