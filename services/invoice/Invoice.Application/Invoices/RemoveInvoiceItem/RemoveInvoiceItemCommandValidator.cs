using FluentValidation;

namespace Invoice.Application.Invoices.RemoveInvoiceItem;
internal sealed class RemoveInvoiceItemCommandValidator : AbstractValidator<RemoveInvoiceItemCommand>
{
    public RemoveInvoiceItemCommandValidator()
    {
        RuleFor(c => c.InvoiceId)
            .NotEmpty()
            .WithMessage("Invoice id cannot be empty.")
            .Must(invoiceId => Guid.TryParse(invoiceId.ToString(), out _))
            .WithMessage("Invalid identifier.");

        RuleFor(c => c.InvoicetemId)
            .NotEmpty()
            .WithMessage("Invoice item id cannot be empty.")
            .Must(invoiceItemId => Guid.TryParse(invoiceItemId.ToString(), out _))
            .WithMessage("Invalid identifier.");
    }
}
