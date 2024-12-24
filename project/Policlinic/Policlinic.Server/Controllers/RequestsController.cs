using Microsoft.AspNetCore.Mvc;
using Policlinic.Server.Dto;
using Policlinic.Domain.Repositories;
using AutoMapper;
using Policlinic.Domain;

namespace Policlinic.Server.Controllers;

/// <summary>
/// Класс для работы с запросами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RequestsController(IRepository<Doctor, int> repositoryDoctor, IRepository<Reception, int> repositoryAppointment, IRepository<Patient, int> repositoryPatient, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вывод всех докторов, опыт которых больше 10 лет
    /// </summary>
    /// <returns>Список <see cref="DoctorGetDto"/></returns>
    [HttpGet("experience-doctors")]
    public async Task<ActionResult<List<DoctorGetDto>>> GetExperienceDoctors()
    {
        var doctors = await repositoryDoctor.GetAll();
        var experiencedDoctors = doctors.Where(d => d.WorkExperience > 10).ToList();
        if (!experiencedDoctors.Any()) return NoContent();

        var dtoList = mapper.Map<List<DoctorGetDto>>(experiencedDoctors);
        return dtoList;
    }

    /// <summary>
    /// Вывод всех пациентов указанного доктора, сортировка по имени
    /// </summary>
    [HttpGet("patients-of-doctor/{id}")]
    public async Task<ActionResult<List<PatientGetDto>>> GetPatientsOfDoctor(int id)
    {
        var receptions = await repositoryAppointment.GetAll();
        var patients = receptions.Where(a => a.DoctorId == id)
                                  .Select(a => a.PatientId)
                                  .Distinct()
                                  .ToList();

        if (!patients.Any()) return NoContent();

        var patientEntities = await Task.WhenAll(patients.Select(p => repositoryPatient.Get(p)));
        var dtoList = mapper.Map<List<PatientGetDto>>(patientEntities.Where(p => p != null).OrderBy(p => p.Fio));
        return dtoList;
    }

    /// <summary>
    /// Вывод здоровых на данный момент пациентов
    /// </summary>
    [HttpGet("healthy-patients")]
    public async Task<ActionResult<List<PatientGetDto>>> GetHealthyPatients()
    {
        var receptions = await repositoryAppointment.GetAll();
        var healthyPatients = receptions.Where(a => a.Conclusion == "Healthy")
                                         .Select(a => a.PatientId)
                                         .Distinct()
                                         .ToList();

        if (!healthyPatients.Any()) return NoContent();

        var patientEntities = await Task.WhenAll(healthyPatients.Select(p => repositoryPatient.Get(p)));
        var dtoList = mapper.Map<List<PatientGetDto>>(patientEntities.Where(p => p != null));
        return dtoList;
    }

    /// <summary>
    /// Вывод количества приемов пациентов по врачам за месяц
    /// </summary>
    [HttpGet("count-appointment")]
    public async Task<ActionResult<List<CountPatientDto>>> GetCountAppointment()
    {
        var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var receptions = await repositoryAppointment.GetAll();

        var groupedByDoctor = receptions.Where(a => a.DateAndTime >= monthStart)
                                        .GroupBy(a => a.DoctorId)
                                        .ToList();

        var countByDoctor = new List<CountPatientDto>();

        foreach (var group in groupedByDoctor)
        {
            var doctor = await repositoryDoctor.Get(group.Key);
            countByDoctor.Add(new CountPatientDto
            {
                Fio = doctor?.Fio ?? "Unknown",
                Count = group.Count()
            });
        }

        if (!countByDoctor.Any()) return NoContent();

        return countByDoctor.OrderBy(c => c.Fio).ToList();
    }

    /// <summary>
    /// Вывод топа заболеваний
    /// </summary>
    [HttpGet("disease-top")]
    public async Task<ActionResult<List<Top5DiseasesDto>>> GetSpecializationTop()
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

        return topDiseases;
    }

    /// <summary>
    /// Вывод пациентов, записанных к нескольким врачам, старше 30 лет, сортировка по дате рождения
    /// </summary>
    [HttpGet("patients-with-several-appointment")]
    public async Task<ActionResult<List<PatientGetDto>>> GetPatientsWithSeveralAppointment()
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

        var dtoList = mapper.Map<List<PatientGetDto>>(patientsOver30);
        return dtoList;
    }
}
