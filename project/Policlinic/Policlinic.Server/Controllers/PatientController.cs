using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Policlinic.Server.Dto;
using Policlinic.Domain.Repositories;
using Policlinic.Domain;

namespace Policlinic.Server.Controllers;

/// <summary>
/// Класс для работы с данными пациентов
/// </summary>
/// <param name="repository">репозиторий пациентов</param>
/// <param name="mapper"></param>
[Route("api/[controller]")]
[ApiController]
public class PatientController(IRepository<Patient, int> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вернуть всех пациентов
    /// </summary>
    /// <returns>Список <see cref="PatientGetDto"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientGetDto>>> Get()
    {
        var patients = await repository.GetAll();
        var dtoList = mapper.Map<IEnumerable<PatientGetDto>>(patients);
        return Ok(dtoList);
    }

    /// <summary>
    /// Вернуть пациента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пациента</param>
    /// <returns><see cref="PatientGetDto"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<PatientGetDto>> Get(int id)
    {
        var patient = await repository.Get(id);
        if (patient == null) return NotFound();

        var dto = mapper.Map<PatientGetDto>(patient);
        return Ok(dto);
    }

    /// <summary>
    /// Добавить пациента
    /// </summary>
    /// <param name="value">объект <see cref="PatientPostDto"/></param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PatientPostDto value)
    {
        var minDate = new DateTime(1900, 1, 1);
        var maxDate = DateTime.Today;
        if (value.BirthDate < minDate || value.BirthDate > maxDate)
            return BadRequest("Invalid birth date");

        var patient = mapper.Map<Patient>(value);
        await repository.Post(patient);
        return Ok();
    }

    /// <summary>
    /// Изменить пациента по идентификатору
    /// </summary>
    /// <param name="id">идентификатор пациента</param>
    /// <param name="value">объект <see cref="PatientPostDto"/></param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PatientPostDto value)
    {
        var minDate = new DateTime(1900, 1, 1);
        var maxDate = DateTime.Today;
        if (value.BirthDate < minDate || value.BirthDate > maxDate)
            return BadRequest("Invalid birth date");

        var patient = mapper.Map<Patient>(value);
        return await repository.Put(patient, id) ? Ok() : NotFound();
    }

    /// <summary>
    /// Удалить пациента по идентификатору
    /// </summary>
    /// <param name="id">идентификатор пациента</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await repository.Delete(id) ? Ok() : NotFound();
    }
}
