using AutoMapper;
using Policlinic.Domain;
using Policlinic.Server.Dto;
namespace Policlinic.Server;
/// <summary>
/// MappingProfile to map Dto objects on Domain objects and  backwards
/// </summary>
public class Mapping : Profile
{
    /// <summary>
    /// Constructor for MappingProfile
    /// </summary>
    public Mapping()
    {
        // Маппинг доменной модели Doctor на DTO для получения данных
        CreateMap<Doctor, DoctorGetDto>().ReverseMap(); 

        // Маппинг для создания нового доктора
        CreateMap<DoctorPostDto, Doctor>();

        // Аналогично для Patient
        CreateMap<Patient, PatientGetDto>().ReverseMap();
        CreateMap<PatientPostDto, Patient>();

        // Маппинг для приемов
        CreateMap<Reception, ReceptionDto>().ReverseMap();

        // Маппинг для специализаций
        CreateMap<Specialization, SpecializationDto>().ReverseMap();
    }
}