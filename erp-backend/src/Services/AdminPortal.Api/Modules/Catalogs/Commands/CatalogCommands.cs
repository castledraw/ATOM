using AdminPortal.Api.Modules.Catalogs.Models;
using ERP.Shared.Application.CQRS;
using ERP.Shared.Domain.Abstractions;
using ERP.Shared.Domain.Entities;

namespace AdminPortal.Api.Modules.Catalogs.Commands;

public sealed record CreateCurrencyCommand(string Code, string Name, string Symbol, int DecimalPlaces) : ICommand<CurrencyDto>;
public sealed record CreatePaymentTermCommand(string Code, string Name, int Days, string Description) : ICommand<PaymentTermDto>;
public sealed record CreateLanguageCommand(string Code, string Name, bool IsDefault) : ICommand<LanguageDto>;
public sealed record CreateIndustryCommand(string Code, string Name, string Description) : ICommand<IndustryDto>;

public class CatalogCommandHandler :
    ICommandHandler<CreateCurrencyCommand, CurrencyDto>,
    ICommandHandler<CreatePaymentTermCommand, PaymentTermDto>,
    ICommandHandler<CreateLanguageCommand, LanguageDto>,
    ICommandHandler<CreateIndustryCommand, IndustryDto>
{
    private readonly IRepository<Currency> _currencies;
    private readonly IRepository<PaymentTerm> _paymentTerms;
    private readonly IRepository<Language> _languages;
    private readonly IRepository<Industry> _industries;

    public CatalogCommandHandler(
        IRepository<Currency> currencies,
        IRepository<PaymentTerm> paymentTerms,
        IRepository<Language> languages,
        IRepository<Industry> industries)
    {
        _currencies = currencies;
        _paymentTerms = paymentTerms;
        _languages = languages;
        _industries = industries;
    }

    public async Task<CurrencyDto> Handle(CreateCurrencyCommand command, CancellationToken cancellationToken)
    {
        var currency = new Currency(Guid.NewGuid(), command.Code, command.Name, command.Symbol, command.DecimalPlaces);
        await _currencies.AddAsync(currency, cancellationToken);
        return new CurrencyDto(currency.Id, currency.Code, currency.Name, currency.Symbol, currency.DecimalPlaces);
    }

    public async Task<PaymentTermDto> Handle(CreatePaymentTermCommand command, CancellationToken cancellationToken)
    {
        var paymentTerm = new PaymentTerm(Guid.NewGuid(), command.Code, command.Name, command.Days, command.Description);
        await _paymentTerms.AddAsync(paymentTerm, cancellationToken);
        return new PaymentTermDto(paymentTerm.Id, paymentTerm.Code, paymentTerm.Name, paymentTerm.Days, paymentTerm.Description);
    }

    public async Task<LanguageDto> Handle(CreateLanguageCommand command, CancellationToken cancellationToken)
    {
        var language = new Language(command.Code, command.Name, command.IsDefault);
        await _languages.AddAsync(language, cancellationToken);
        return new LanguageDto(language.Code, language.Name, language.IsDefault);
    }

    public async Task<IndustryDto> Handle(CreateIndustryCommand command, CancellationToken cancellationToken)
    {
        var industry = new Industry(Guid.NewGuid(), command.Code, command.Name, command.Description);
        await _industries.AddAsync(industry, cancellationToken);
        return new IndustryDto(industry.Id, industry.Code, industry.Name, industry.Description);
    }
}
