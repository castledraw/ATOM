using AdminPortal.Api.Modules.Companies.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Companies.Queries;

public sealed record GetCompanySegmentsQuery(Guid CompanyId) : IQuery<IReadOnlyList<CustomerSegmentDto>>;

public sealed class GetCompanySegmentsHandler : IQueryHandler<GetCompanySegmentsQuery, IReadOnlyList<CustomerSegmentDto>>
{
    private readonly IReadRepository<CustomerSegment> _segmentRepository;

    public GetCompanySegmentsHandler(IReadRepository<CustomerSegment> segmentRepository)
    {
        _segmentRepository = segmentRepository;
    }

    public async Task<IReadOnlyList<CustomerSegmentDto>> Handle(GetCompanySegmentsQuery query, CancellationToken cancellationToken)
    {
        var segments = await _segmentRepository.ListAsync(s => s.CompanyId == query.CompanyId, cancellationToken);
        return segments
            .Select(s => new CustomerSegmentDto(s.Id, s.CompanyId, s.Code, s.Name, s.Description, s.IsDefault))
            .ToList();
    }
}
