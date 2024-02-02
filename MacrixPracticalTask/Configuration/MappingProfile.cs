using AutoMapper;
using MacrixPracticalTask.Models;
using MacrixPracticalTask.Models.DTO;

namespace MacrixPracticalTask.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<PersonForCreationDTO, Person>();
            CreateMap<PersonForUpdateDTO, Person>();
        }
    }
}
