using Asp.Versioning;
using Invoice.Api.Utils;
using Invoice.Application.Invoices.AddInvoiceItem;
using Invoice.Application.Invoices.CreateInvoice;
using Invoice.Application.Invoices.GetAllInvoice;
using Invoice.Application.Invoices.GetAllInvoiceWithItem;
using Invoice.Application.Invoices.InvoiceReport;
using Invoice.Application.Invoices.RemoveInvoiceItem;
using Invoice.Domain.Abstractions;
using Invoice.Infrastructure.Athorization;
using IronPdf;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Api.Invoices;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/invoice")]
public class InvoiceController : ControllerBase
{
    private readonly ISender _sender;

    public InvoiceController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("all")]
    [HasPermission(Permission.InvoiceGetAll)]
    public async Task<IActionResult> GetAllInvoice(CancellationToken cancellationToken)
    {
        var query = new GetAllInvoiceQuery();

        Result<IReadOnlyList<InvoiceResponse>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("all-with-item")]
    [HasPermission(Permission.InvoiceGetAll)]
    public async Task<IActionResult> GetAllInvoiceWithItem(CancellationToken cancellationToken)
    {
        var query = new GetAllInvoiceWithItemQuery();

        Result<IReadOnlyList<InvoiceWithItemResponse>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("create")]
    [HasPermission(Permission.InvoiceCreation)]
    public async Task<IActionResult> CreateInvoice(string InvoiceName, CancellationToken cancellationToken)
    {
        var command = new CreateInvoiceCommand(InvoiceName);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{id}/invoice-items")]
    [HasPermission(Permission.InvoiceItemInsertion)]
    public async Task<IActionResult> AddInvoiceItem(
        Guid id,
        [FromBody] AddInvoiceItemRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddInvoiceItemCommand(
            id,
            request.Name,
            request.Quantity,
            request.Amount,
            request.Currency);

        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("{id}/invoice-items/{invoiceItemId}")]
    [HasPermission(Permission.InvoiceItemDeletion)]
    public async Task<IActionResult> RemoveCartItem(
        Guid id,
        Guid invoiceItemId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveInvoiceItemCommand(id, invoiceItemId);

        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpGet("invoice-report/{id}")]
    [HasPermission(Permission.InvoiceReportGeneration)]
    public async Task<IActionResult> GenerateInvoiceReport(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new GenerateInvoiceReportCommand(id);

        Result<PdfDocument> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? File(result.Value.BinaryData, "application/pdf", $"invoice-{Guid.NewGuid()}.pdf") : BadRequest(result.Error);
    }
}
