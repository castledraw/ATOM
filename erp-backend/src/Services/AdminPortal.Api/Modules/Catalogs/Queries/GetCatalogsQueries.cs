using AdminPortal.Api.Modules.Catalogs.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Catalogs.Queries;

public sealed record GetCurrenciesQuery : IQuery<IReadOnlyList<CurrencyDto>>;
public sealed record GetPaymentTermsQuery : IQuery<IReadOnlyList<PaymentTermDto>>;
public sealed record GetLanguagesQuery : IQuery<IReadOnlyList<LanguageDto>>;
public sealed record GetIndustriesQuery : IQuery<IReadOnlyList<IndustryDto>>;

public sealed class GetCurrenciesHandler : IQueryHandler<GetCurrenciesQuery, IReadOnlyList<CurrencyDto>>
{
    private readonly IReadRepository<Currency> _repository;

    public GetCurrenciesHandler(IReadRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CurrencyDto>> Handle(GetCurrenciesQuery query, CancellationToken cancellationToken)
    {
        var items = await _repository.ListAsync(cancellationToken);
        return items.Select(c => new CurrencyDto(c.Id, c.Code, c.Name, c.Symbol, c.DecimalPlaces)).ToList();
    }
}

public sealed class GetPaymentTermsHandler : IQueryHandler<GetPaymentTermsQuery, IReadOnlyList<PaymentTermDto>>
{
    private readonly IReadRepository<PaymentTerm> _repository;

    public GetPaymentTermsHandler(IReadRepository<PaymentTerm> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PaymentTermDto>> Handle(GetPaymentTermsQuery query, CancellationToken cancellationToken)
    {
        var items = await _repository.ListAsync(cancellationToken);
        return items.Select(p => new PaymentTermDto(p.Id, p.Code, p.Name, p.Days, p.Description)).ToList();
    }
}

public sealed class GetLanguagesHandler : IQueryHandler<GetLanguagesQuery, IReadOnlyList<LanguageDto>>
{
    private readonly IReadRepository<Language> _repository;

    public GetLanguagesHandler(IReadRepository<Language> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<LanguageDto>> Handle(GetLanguagesQuery query, CancellationToken cancellationToken)
    {
        var items = await _repository.ListAsync(cancellationToken);
        return items.Select(l => new LanguageDto(l.Code, l.Name, l.IsDefault)).ToList();
    }
}

public sealed class GetIndustriesHandler : IQueryHandler<GetIndustriesQuery, IReadOnlyList<IndustryDto>>
{
    private readonly IReadRepository<Industry> _repository;

    public GetIndustriesHandler(IReadRepository<Industry> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<IndustryDto>> Handle(GetIndustriesQuery query, CancellationToken cancellationToken)
    {
        var items = await _repository.ListAsync(cancellationToken);
        return items.Select(i => new IndustryDto(i.Id, i.Code, i.Name, i.Description)).ToList();
    }
}
