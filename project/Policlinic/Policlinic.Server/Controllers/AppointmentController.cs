using Microsoft.AspNetCore.Mvc;
using Policlinic.Server.Dto;
using Policlinic.Domain.Repositories;
using AutoMapper;
using Policlinic.Domain;

namespace Policlinic.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController(IRepository<Reception, int> repositoryAppointment, IRepository<Doctor, int> repositoryDoctor, IRepository<Patient, int> repositoryPatient, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вернуть все приёмы
    /// </summary>
    /// <returns>Список <see cref="ReceptionDto"/></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReceptionDto>>> Get()
    {
        var receptions = await repositoryAppointment.GetAll();
        var dtoList = mapper.Map<IEnumerable<ReceptionDto>>(receptions);
        return Ok(dtoList);
    }

    /// <summary>
    /// Вернуть приём по идентификатору
    /// </summary>
    /// <param name="id">идентификатор приёма</param>
    /// <returns><see cref="ReceptionDto"/></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ReceptionDto>> Get(int id)
    {
        var reception = await repositoryAppointment.Get(id);
        if (reception == null) return NoContent();

        var dto = mapper.Map<ReceptionDto>(reception);
        return Ok(dto);
    }

    /// <summary>
    /// Добавить новый приём
    /// </summary>
    /// <param name="value">объект <see cref="ReceptionDto"/></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReceptionDto value)
    {
        var patient = await repositoryPatient.Get(value.PatientId);
        if (patient == null) return NoContent();

        var doctor = await repositoryDoctor.Get(value.DoctorId);
        if (doctor == null) return NoContent();

        var reception = mapper.Map<Reception>(value);
        reception.PatientId = value.PatientId;
        reception.DoctorId = value.DoctorId;
        reception.DateAndTime = value.DateAndTime;
        reception.Status = value.Status;

        await repositoryAppointment.Post(reception);
        return Ok();
    }

    /// <summary>
    /// Обновить приём по идентификатору
    /// </summary>
    /// <param name="id">идентификатор приёма</param>
    /// <param name="value">объект <see cref="ReceptionDto"/></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ReceptionDto value)
    {
        var patient = await repositoryPatient.Get(value.PatientId);
        if (patient == null) return NoContent();

        var doctor = await repositoryDoctor.Get(value.DoctorId);
        if (doctor == null) return NoContent();

        var reception = mapper.Map<Reception>(value);
        reception.PatientId = value.PatientId;
        reception.DoctorId = value.DoctorId;
        reception.DateAndTime = value.DateAndTime;
        reception.Status = value.Status;

        return await repositoryAppointment.Put(reception, id) ? Ok() : NoContent();
    }

    /// <summary>
    /// Удалить приём по идентификатору
    /// </summary>
    /// <param name="id">идентификатор приёма</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await repositoryAppointment.Delete(id)) return NoContent();
        return NoContent();
    }
}
