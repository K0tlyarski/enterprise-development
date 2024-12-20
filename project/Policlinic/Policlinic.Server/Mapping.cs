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
        CreateMap<Doctor, DoctorGetDto>();
        CreateMap<Doctor, DoctorPostDto>();
        CreateMap<DoctorPostDto, Doctor>();
        CreateMap<Patient, PatientGetDto>();
        CreateMap<Patient, PatientPostDto>();
        CreateMap<PatientPostDto, Patient>();
        CreateMap<Reception, ReceptionDto>();
        CreateMap<ReceptionDto, Reception>();
        CreateMap<Specialization, SpecializationDto>();
        CreateMap<SpecializationDto, Specialization>();
    }
}