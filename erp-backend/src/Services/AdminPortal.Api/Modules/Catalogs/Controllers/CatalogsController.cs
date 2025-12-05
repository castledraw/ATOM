using AdminPortal.Api.Modules.Catalogs.Commands;
using AdminPortal.Api.Modules.Catalogs.Models;
using AdminPortal.Api.Modules.Catalogs.Queries;
using ERP.Shared.Application.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace AdminPortal.Api.Modules.Catalogs.Controllers;

[ApiController]
[Route("api/catalogs")]
[Produces("application/json")]
public class CatalogsController : ControllerBase
{
    private readonly IQueryHandler<GetCurrenciesQuery, IReadOnlyList<CurrencyDto>> _currencyQuery;
    private readonly IQueryHandler<GetPaymentTermsQuery, IReadOnlyList<PaymentTermDto>> _paymentTermsQuery;
    private readonly IQueryHandler<GetLanguagesQuery, IReadOnlyList<LanguageDto>> _languagesQuery;
    private readonly IQueryHandler<GetIndustriesQuery, IReadOnlyList<IndustryDto>> _industriesQuery;
    private readonly ICommandHandler<CreateCurrencyCommand, CurrencyDto> _createCurrency;
    private readonly ICommandHandler<CreatePaymentTermCommand, PaymentTermDto> _createPaymentTerm;
    private readonly ICommandHandler<CreateLanguageCommand, LanguageDto> _createLanguage;
    private readonly ICommandHandler<CreateIndustryCommand, IndustryDto> _createIndustry;

    public CatalogsController(
        IQueryHandler<GetCurrenciesQuery, IReadOnlyList<CurrencyDto>> currencyQuery,
        IQueryHandler<GetPaymentTermsQuery, IReadOnlyList<PaymentTermDto>> paymentTermsQuery,
        IQueryHandler<GetLanguagesQuery, IReadOnlyList<LanguageDto>> languagesQuery,
        IQueryHandler<GetIndustriesQuery, IReadOnlyList<IndustryDto>> industriesQuery,
        ICommandHandler<CreateCurrencyCommand, CurrencyDto> createCurrency,
        ICommandHandler<CreatePaymentTermCommand, PaymentTermDto> createPaymentTerm,
        ICommandHandler<CreateLanguageCommand, LanguageDto> createLanguage,
        ICommandHandler<CreateIndustryCommand, IndustryDto> createIndustry)
    {
        _currencyQuery = currencyQuery;
        _paymentTermsQuery = paymentTermsQuery;
        _languagesQuery = languagesQuery;
        _industriesQuery = industriesQuery;
        _createCurrency = createCurrency;
        _createPaymentTerm = createPaymentTerm;
        _createLanguage = createLanguage;
        _createIndustry = createIndustry;
    }

    /// <summary>
    /// Catálogo de monedas disponibles.
    /// </summary>
    [HttpGet("currencies")]
    [ProducesResponseType(typeof(IReadOnlyList<CurrencyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CurrencyDto>>> GetCurrencies(CancellationToken cancellationToken)
    {
        var currencies = await _currencyQuery.Handle(new GetCurrenciesQuery(), cancellationToken);
        return Ok(currencies);
    }

    /// <summary>
    /// Registra una moneda.
    /// </summary>
    [HttpPost("currencies")]
    [ProducesResponseType(typeof(CurrencyDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<CurrencyDto>> CreateCurrency([FromBody] CreateCurrencyCommand command, CancellationToken cancellationToken)
    {
        var result = await _createCurrency.Handle(command, cancellationToken);
        return Created(string.Empty, result);
    }

    /// <summary>
    /// Catálogo de términos de pago.
    /// </summary>
    [HttpGet("payment-terms")]
    [ProducesResponseType(typeof(IReadOnlyList<PaymentTermDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PaymentTermDto>>> GetPaymentTerms(CancellationToken cancellationToken)
    {
        var terms = await _paymentTermsQuery.Handle(new GetPaymentTermsQuery(), cancellationToken);
        return Ok(terms);
    }

    /// <summary>
    /// Registra un término de pago.
    /// </summary>
    [HttpPost("payment-terms")]
    [ProducesResponseType(typeof(PaymentTermDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<PaymentTermDto>> CreatePaymentTerm([FromBody] CreatePaymentTermCommand command, CancellationToken cancellationToken)
    {
        var term = await _createPaymentTerm.Handle(command, cancellationToken);
        return Created(string.Empty, term);
    }

    /// <summary>
    /// Idiomas soportados.
    /// </summary>
    [HttpGet("languages")]
    [ProducesResponseType(typeof(IReadOnlyList<LanguageDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LanguageDto>>> GetLanguages(CancellationToken cancellationToken)
    {
        var languages = await _languagesQuery.Handle(new GetLanguagesQuery(), cancellationToken);
        return Ok(languages);
    }

    /// <summary>
    /// Agrega un idioma.
    /// </summary>
    [HttpPost("languages")]
    [ProducesResponseType(typeof(LanguageDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<LanguageDto>> CreateLanguage([FromBody] CreateLanguageCommand command, CancellationToken cancellationToken)
    {
        var language = await _createLanguage.Handle(command, cancellationToken);
        return Created(string.Empty, language);
    }

    /// <summary>
    /// Catálogo de industrias.
    /// </summary>
    [HttpGet("industries")]
    [ProducesResponseType(typeof(IReadOnlyList<IndustryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<IndustryDto>>> GetIndustries(CancellationToken cancellationToken)
    {
        var industries = await _industriesQuery.Handle(new GetIndustriesQuery(), cancellationToken);
        return Ok(industries);
    }

    /// <summary>
    /// Registra una industria.
    /// </summary>
    [HttpPost("industries")]
    [ProducesResponseType(typeof(IndustryDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<IndustryDto>> CreateIndustry([FromBody] CreateIndustryCommand command, CancellationToken cancellationToken)
    {
        var industry = await _createIndustry.Handle(command, cancellationToken);
        return Created(string.Empty, industry);
    }
}
