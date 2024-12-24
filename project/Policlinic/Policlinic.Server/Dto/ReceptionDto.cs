
namespace Policlinic.Server.Dto;

/// <summary>
/// DTO для представления данных о приеме.
/// </summary>
public class ReceptionDto
{
    /// <summary>
    /// Уникальный идентификатор приема.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время приема.
    /// </summary>
    public DateTime DateAndTime { get; set; }

    /// <summary>
    /// Статус приема.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор доктора, проводившего прием.
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// Идентификатор пациента, участвующего в приеме.
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// Заключение, сделанное по итогам приема.
    /// </summary>
    public string Conclusion { get; set; } = string.Empty;
}
