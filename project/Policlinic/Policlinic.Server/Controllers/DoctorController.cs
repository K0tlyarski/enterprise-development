using Microsoft.AspNetCore.Mvc;
using Policlinic.Server.Dto;
using Policlinic.Domain.Repositories;
using AutoMapper;
using Policlinic.Domain;

namespace Policlinic.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController(IRepository<Doctor, int> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вернуть всех докторов
    /// </summary>
    /// <returns>Список <see cref="DoctorGetDto"/></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoctorGetDto>>> Get()
    {
        var doctors = await repository.GetAll();
        var dtoList = mapper.Map<IEnumerable<DoctorGetDto>>(doctors);
        return Ok(dtoList);
    }

    /// <summary>
    /// Вернуть доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns><see cref="DoctorGetDto"/></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorGetDto>> Get(int id)
    {
        var doctor = await repository.Get(id);
        if (doctor == null) return NoContent();

        var dto = mapper.Map<DoctorGetDto>(doctor);
        return Ok(dto);
    }

    /// <summary>
    /// Добавить доктора
    /// </summary>
    /// <param name="value">объект <see cref="DoctorPostDto"/></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DoctorPostDto value)
    {
        var doctor = mapper.Map<Doctor>(value);

        var minDate = new DateTime(1900, 1, 1);
        var maxDate = DateTime.Today;
        if (value.BirthDate < minDate || value.BirthDate > maxDate)
            return BadRequest("Invalid birth date");

        await repository.Post(doctor);
        return Ok();
    }

    /// <summary>
    /// Изменить доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <param name="value">объект <see cref="DoctorPostDto"/></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] DoctorPostDto value)
    {
        var doctor = mapper.Map<Doctor>(value);

        var minDate = new DateTime(1900, 1, 1);
        var maxDate = DateTime.Today;
        if (value.BirthDate < minDate || value.BirthDate > maxDate)
            return BadRequest("Invalid birth date");

        return await repository.Put(doctor, id) ? Ok() : NoContent();
    }

    /// <summary>
    /// Удалить доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await repository.Delete(id)) return NoContent();
        return NoContent();
    }
}
