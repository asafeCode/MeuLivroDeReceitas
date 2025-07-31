using AutoMapper;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.Services.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestUserRegisterJson, Domain.Entities.User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
     // AutoMapper, Mapeie todas as propriedades e ignore 'Password'
    }
}

