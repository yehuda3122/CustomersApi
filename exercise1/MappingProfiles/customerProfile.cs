namespace exercise1.MappingProfiles
{
    public class customerProfile : Profile
    {
        public customerProfile()
        {
            CreateMap<CustomerModel, CustomerDto>().ReverseMap();
        }
    }
}
