using Driver.Common;
using Driver.Common.Models;
using Moneyon.Common.Extensions;

namespace Driver.Domain.Entities;

[ObjectMapper]
public class DocumentMapper : AutoMapper.Profile
{
    public DocumentMapper()
    {

        CreateMap<DocumentCreateDto, Document>();
        
        CreateMap<Document, DocumentDto>()
            .ForMember(model => model.Description, opt => opt.MapFrom(x => x.Description ?? x.Type.GetDescription()));

        CreateMap<Document, DocumentWithContentDto>()
            .ForMember(model => model.Content, opt => opt.MapFrom(x => x.Content.Value));

        CreateMap<Document, DocumentDetailDto>()
            .ForMember(model => model.Value, opt => opt.MapFrom(x => x.Content.Value));

        CreateMap<IEnumerable<Document>, IEnumerable<DocumentWithContentDto>>();

    }
}