using AutoMapper;
using Lykke.Service.LegalEntities.Core.Domain;
using Lykke.Service.LegalEntities.Models;

namespace Lykke.Service.LegalEntities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LegalEntity, LegalEntityModel>(MemberList.Source);
            CreateMap<CreateLegalEntityModel, LegalEntity>(MemberList.Destination);
            CreateMap<EditLegalEntityModel, LegalEntity>(MemberList.Destination);

            CreateMap<SwiftCredentials, SwiftCredentialsModel>(MemberList.Source);
            CreateMap<CreateSwiftCredentialsModel, SwiftCredentials>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<EditSwiftCredentialsModel, SwiftCredentials>(MemberList.Destination)
                .ForMember(dest => dest.AssetId, opt => opt.Ignore())
                .ForMember(dest => dest.LegalEntityId, opt => opt.Ignore());

            CreateMap<ClientSwiftCredentials, ClientSwiftCredentialsModel>(MemberList.Source);
        }
    }
}
