using AutoMapper;
using DAL.Dtos;
using DAL.Dtos.HospitalsDTO;
using DAL.Entities;

namespace PL.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DonorDto, Donor>().ReverseMap();
            CreateMap<PatientDto, Patient>().ReverseMap();
            CreateMap<RequestDto, Request>().ReverseMap();
            CreateMap<FollowUpFormDto, FollowUpForm>().ReverseMap();
            CreateMap<Hospital, GetHospitalDTO>();
        }
    }
}
