using AutoMapper;
using Lykke.Service.LegalEntities.Core.Domain;

namespace Lykke.Service.LegalEntities.AzureRepositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LegalEntity, LegalEntityEntity>(MemberList.Source)
                .ForSourceMember(src => src.Id, opt => opt.Ignore());

            CreateMap<LegalEntityEntity, LegalEntity>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey));
            
            CreateMap<SwiftCredentials, SwiftCredentialsEntity>(MemberList.Source)
                .ForSourceMember(src => src.Id, opt => opt.Ignore())
                .ForSourceMember(src => src.AssetId, opt => opt.Ignore())
                .ForSourceMember(src => src.LegalEntityId, opt => opt.Ignore());
            
            CreateMap<SwiftCredentialsEntity, SwiftCredentials>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey));
        }
    }
}
