using AutoMapper;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.IOC;

namespace Driver.Service.Services;

[AutoRegister()]
public class DocumentService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper mapper;

    public DocumentService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<DocumentWithContentDto> ReadDocumentAsync(Guid documentGuid, CancellationToken cancellationToken = default)
    {
        var doc = await _unitOfWork.DocumentRepository.FirstOrDefaultAsync(filter: x => x.Guid == documentGuid,
                                                                           include: s => s.Include(p => p.Content),
                                                                           cancellationToken);
        return mapper.Map<DocumentWithContentDto>(doc);
    }

    public async Task<DocumentDto> UpsertDocumentAsync(DocumentCreateDto dto, CancellationToken cancellationToken = default)
    {
        var docToReplaceOrInsert = await dto.ToDocumentAsync();
        if (dto.PersonCode is not null)
        {
            var driver = await _unitOfWork.PersonRepository.FirstOrDefaultAsync(p => p.PersonCode == dto.PersonCode);
            docToReplaceOrInsert.PersonId = driver?.Id ?? null;
        }

        await _unitOfWork.DocumentRepository.InsertAsync(docToReplaceOrInsert);
        await _unitOfWork.CommitAsync();

        if (dto.AutoId is not null)
        {
            var auto = await _unitOfWork.AutoRepository.FirstOrDefaultAsync(filter: p => p.Id == dto.AutoId);
            if (auto is not null)
            {
                auto.DocumentId = docToReplaceOrInsert.Id;
                await _unitOfWork.CommitAsync();
            }
        }

        if (dto.PersonCode is not null)
        {
            var driver = await _unitOfWork.PersonRepository.FirstOrDefaultAsync(p => p.PersonCode == dto.PersonCode);
            if (driver is not null)
            {
                driver.DocumentId = docToReplaceOrInsert.Id;
                await _unitOfWork.CommitAsync();
            }

        }

        return mapper.Map<DocumentDto>(docToReplaceOrInsert);
    }
}