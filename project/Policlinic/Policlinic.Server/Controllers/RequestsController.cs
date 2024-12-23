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
public class RequestsController(IRepository<Doctor, int> repositoryDoctor, IRepository<Reception, int> repositoryAppointment, IRepository<Patient, int> repositoryPatient, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вывод всех докторов, опыт которых больше 10 лет
    /// </summary>
    /// <returns>Список <see cref="DoctorGetDto"/></returns>
    [HttpGet("experience-doctors")]
    public async Task<ActionResult<IEnumerable<DoctorGetDto>>> GetExperienceDoctors()
    {
        var doctors = await repositoryDoctor.GetAll();
        var experiencedDoctors = doctors.Where(d => d.WorkExperience > 10).ToList();
        if (!experiencedDoctors.Any()) return NoContent();

        var dtoList = mapper.Map<IEnumerable<DoctorGetDto>>(experiencedDoctors);
        return Ok(dtoList);
    }

    /// <summary>
    /// Вывод всех пациентов указанного доктора, сортировка по имени
    /// </summary>
    [HttpGet("patients-of-doctor/{id}")]
    public async Task<ActionResult<IEnumerable<PatientGetDto>>> GetPatientsOfDoctor(int id)
    {
        var receptions = await repositoryAppointment.GetAll();
        var patients = receptions.Where(a => a.DoctorId == id)
                                  .Select(a => a.PatientId)
                                  .Distinct()
                                  .ToList();

        if (!patients.Any()) return NoContent();

        var patientEntities = await Task.WhenAll(patients.Select(p => repositoryPatient.Get(p)));
        var dtoList = mapper.Map<IEnumerable<PatientGetDto>>(patientEntities.Where(p => p != null).OrderBy(p => p.Fio));
        return Ok(dtoList);
    }

    /// <summary>
    /// Вывод здоровых на данный момент пациентов
    /// </summary>
    [HttpGet("healthy-patients")]
    public async Task<ActionResult<IEnumerable<PatientGetDto>>> GetHealthyPatients()
    {
        var receptions = await repositoryAppointment.GetAll();
        var healthyPatients = receptions.Where(a => a.Conclusion == "Healthy")
                                         .Select(a => a.PatientId)
                                         .Distinct()
                                         .ToList();

        if (!healthyPatients.Any()) return NoContent();

        var patientEntities = await Task.WhenAll(healthyPatients.Select(p => repositoryPatient.Get(p)));
        var dtoList = mapper.Map<IEnumerable<PatientGetDto>>(patientEntities.Where(p => p != null));
        return Ok(dtoList);
    }

    /// <summary>
    /// Вывод количества приемов пациентов по врачам за месяц
    /// </summary>
    [HttpGet("count-appointment")]
    public async Task<IActionResult> GetCountAppointment()
    {
        var month = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var receptions = await repositoryAppointment.GetAll();

        var countByDoctor = receptions.Where(a => a.DateAndTime >= month)
                                       .GroupBy(a => a.DoctorId)
                                       .Select(g => new CountPatientDto
                                       {
                                           Fio = repositoryDoctor.Get(g.Key).Result?.Fio ?? "Unknown",
                                           Count = g.Count()
                                       })
                                       .OrderBy(c => c.Fio)
                                       .ToList();

        if (!countByDoctor.Any()) return NoContent();

        return Ok(countByDoctor);
    }

    /// <summary>
    /// Вывод топа заболеваний
    /// </summary>
    [HttpGet("disease-top")]
    public async Task<IActionResult> GetSpecializationTop()
    {
        var receptions = await repositoryAppointment.GetAll();

        var topDiseases = receptions.GroupBy(a => a.Conclusion)
                                     .OrderByDescending(g => g.Count())
                                     .Take(5)
                                     .Select(g => new Top5DiseasesDto
                                     {
                                         Conclusion = g.Key
                                     })
                                     .ToList();

        if (!topDiseases.Any()) return NoContent();

        return Ok(topDiseases);
    }

    /// <summary>
    /// Вывод пациентов, записанных к нескольким врачам, старше 30 лет, сортировка по дате рождения
    /// </summary>
    [HttpGet("patients-with-several-appointment")]
    public async Task<ActionResult<IEnumerable<PatientGetDto>>> GetPatientsWithSeveralAppointment()
    {
        var receptions = await repositoryAppointment.GetAll();
        var patientGroups = receptions.GroupBy(a => a.PatientId)
                                       .Where(g => g.Select(a => a.DoctorId).Distinct().Count() > 1)
                                       .Select(g => g.Key)
                                       .ToList();

        if (!patientGroups.Any()) return NoContent();

        var patientEntities = await Task.WhenAll(patientGroups.Select(p => repositoryPatient.Get(p)));
        var patientsOver30 = patientEntities.Where(p => p != null && DateTime.Now.Year - p.BirthDate.Year > 30)
                                            .OrderBy(p => p.BirthDate)
                                            .ToList();

        var dtoList = mapper.Map<IEnumerable<PatientGetDto>>(patientsOver30);
        return Ok(dtoList);
    }
}
