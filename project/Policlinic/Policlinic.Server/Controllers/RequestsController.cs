using Microsoft.AspNetCore.Mvc;
using Policlinic.Server.Dto;
using Policlinic.Domain.Repositories;
using AutoMapper;
using Policlinic.Domain;

namespace Policlinic.Server.Controllers;

/// <summary>
/// Класс для работы с запросами
/// </summary>
/// <param name="repositoryDoctor">репозиторий докторов</param>
/// <param name="repositoryAppointment">репозиторий посещений</param>
[Route("api/[controller]")]
[ApiController]
public class RequestsController(IRepository<Doctor, int> repositoryDoctor, IRepository<Reception, int> repositoryAppointment, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вывод всех докторов, опыт которых больше 10 лет
    /// </summary>
    /// <returns>Список <see cref="DoctorGetDto"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("experience-doctors")]
    public async Task<ActionResult<IEnumerable<DoctorGetDto>>> GetExperienceDoctors()
    {
        var doctors = await repositoryDoctor.GetAll();
        var experiencedDoctors = doctors.Where(d => d.WorkExperience > 10).ToList();
        var dtoList = mapper.Map<IEnumerable<DoctorGetDto>>(experiencedDoctors);
        return Ok(dtoList);
    }

    /// <summary>
    /// Вывод всех пациентов указанного доктора, сортировка по имени
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns>Список <see cref="PatientGetDto"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("patients-of-doctor/{id}")]
    public async Task<ActionResult<IEnumerable<PatientGetDto>>> GetPatientsOfDoctor(int id)
    {
        var receptions = await repositoryAppointment.GetAll();
        var patients = receptions.Where(a => a.DoctorId == id)
                                  .Select(a => new PatientGetDto
                                  {
                                      Id = a.PatientId,
                                      Fio = "Unknown", // Здесь должен быть реальный запрос к PatientRepository, если он доступен
                                      Passport = 0,
                                      BirthDate = DateTime.MinValue,
                                      Address = "Unknown"
                                  })
                                  .Distinct()
                                  .OrderBy(p => p.Fio)
                                  .ToList();
        return Ok(patients);
    }

    /// <summary>
    /// Вывод здоровых на данный момент пациентов
    /// </summary>
    /// <returns>Список <see cref="PatientGetDto"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("healthy-patients")]
    public async Task<ActionResult<IEnumerable<PatientGetDto>>> GetHealthyPatients()
    {
        var receptions = await repositoryAppointment.GetAll();
        var healthyPatients = receptions.Where(a => a.Conclusion == "Healthy")
                                         .Select(a => new PatientGetDto
                                         {
                                             Id = a.PatientId,
                                             Fio = "Unknown", // Здесь должен быть реальный запрос к PatientRepository, если он доступен
                                             Passport = 0,
                                             BirthDate = DateTime.MinValue,
                                             Address = "Unknown"
                                         })
                                         .Distinct()
                                         .ToList();
        return Ok(healthyPatients);
    }

    /// <summary>
    /// Вывод количества приемов пациентов по врачам за месяц
    /// </summary>
    /// <returns>Список количества приемов по врачам</returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("count-appointment")]
    public async Task<IActionResult> GetCountAppointment()
    {
        var month = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var receptions = await repositoryAppointment.GetAll();
        var countByDoctor = receptions.Where(a => a.DateAndTime >= month)
                                       .GroupBy(a => a.DoctorId)
                                       .Select(g => new CountPatientDto
                                       {
                                           Fio = "Doctor Unknown", // Здесь должен быть реальный запрос к DoctorRepository, если он доступен
                                           Count = g.Count()
                                       })
                                       .OrderBy(c => c.Fio)
                                       .ToList();
        return Ok(countByDoctor);
    }

    /// <summary>
    /// Вывод топа заболеваний
    /// </summary>
    /// <returns>Топ заболеваний</returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("disease-top")]
    public async Task<IActionResult> GetSpecializationTop()
    {
        var receptions = await repositoryAppointment.GetAll();
        var topDiseases = receptions.GroupBy(a => a.Conclusion)
                                     .Select(g => new Top5DiseasesDto
                                     {
                                         Conclusion = g.Key
                                     })
                                     .OrderByDescending(g => g.Conclusion)
                                     .Take(5)
                                     .ToList();
        return Ok(topDiseases);
    }

    /// <summary>
    /// Вывод пациентов, записаных к нескольким врачам, сортировка по дате рождения (старше 30 лет)
    /// </summary>
    /// <returns>Список <see cref="PatientGetDto"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("patients-with-several-appointment")]
    public async Task<ActionResult<IEnumerable<PatientGetDto>>> GetPatientsWithSeveralAppointment()
    {
        var today = DateTime.Now;
        var receptions = await repositoryAppointment.GetAll();
        var patients = receptions.Where(a => (today.Year - a.DateAndTime.Year) > 30) // Уточнение, если есть дата рождения
                                  .GroupBy(a => a.PatientId)
                                  .Where(g => g.Select(a => a.DoctorId).Distinct().Count() > 1)
                                  .Select(g => new PatientGetDto
                                  {
                                      Id = g.Key,
                                      Fio = "Unknown", // Здесь должен быть реальный запрос к PatientRepository, если он доступен
                                      Passport = 0,
                                      BirthDate = DateTime.MinValue,
                                      Address = "Unknown"
                                  })
                                  .OrderBy(p => p.BirthDate)
                                  .ToList();
        return Ok(patients);
    }
}
