
namespace Policlinic.Server.Dto;

/// <summary>
/// DTO для представления данных о специализации.
/// </summary>
public class SpecializationDto
{
    /// <summary>
    /// Уникальный идентификатор специализации.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название специализации.
    /// </summary>
    public string NameSpecialization { get; set; } = string.Empty;
}
