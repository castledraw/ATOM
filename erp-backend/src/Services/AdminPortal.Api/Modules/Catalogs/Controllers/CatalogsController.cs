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
    /// <summary>
    /// Catálogo de monedas disponibles.
    /// </summary>
    [HttpGet("currencies")]
    [ProducesResponseType(typeof(IReadOnlyList<CurrencyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CurrencyDto>>> GetCurrencies(
        [FromServices] IQueryHandler<GetCurrenciesQuery, IReadOnlyList<CurrencyDto>> handler,
        CancellationToken cancellationToken)
    {
        var currencies = await handler.Handle(new GetCurrenciesQuery(), cancellationToken);
        return Ok(currencies);
    }

    /// <summary>
    /// Catálogo de términos de pago.
    /// </summary>
    [HttpGet("payment-terms")]
    [ProducesResponseType(typeof(IReadOnlyList<PaymentTermDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PaymentTermDto>>> GetPaymentTerms(
        [FromServices] IQueryHandler<GetPaymentTermsQuery, IReadOnlyList<PaymentTermDto>> handler,
        CancellationToken cancellationToken)
    {
        var terms = await handler.Handle(new GetPaymentTermsQuery(), cancellationToken);
        return Ok(terms);
    }

    /// <summary>
    /// Idiomas soportados.
    /// </summary>
    [HttpGet("languages")]
    [ProducesResponseType(typeof(IReadOnlyList<LanguageDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LanguageDto>>> GetLanguages(
        [FromServices] IQueryHandler<GetLanguagesQuery, IReadOnlyList<LanguageDto>> handler,
        CancellationToken cancellationToken)
    {
        var languages = await handler.Handle(new GetLanguagesQuery(), cancellationToken);
        return Ok(languages);
    }

    /// <summary>
    /// Catálogo de industrias.
    /// </summary>
    [HttpGet("industries")]
    [ProducesResponseType(typeof(IReadOnlyList<IndustryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<IndustryDto>>> GetIndustries(
        [FromServices] IQueryHandler<GetIndustriesQuery, IReadOnlyList<IndustryDto>> handler,
        CancellationToken cancellationToken)
    {
        var industries = await handler.Handle(new GetIndustriesQuery(), cancellationToken);
        return Ok(industries);
    }
}
