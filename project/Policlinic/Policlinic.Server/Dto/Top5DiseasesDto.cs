
namespace Policlinic.Server.Dto;

/// <summary>
/// DTO для представления данных о топ-5 наиболее частых заболеваниях.
/// </summary>
public class Top5DiseasesDto
{
    /// <summary>
    /// Заключение или описание заболевания.
    /// </summary>
    public string Conclusion { get; set; }
}
