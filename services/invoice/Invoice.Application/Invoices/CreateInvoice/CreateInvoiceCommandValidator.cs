using FluentValidation;

namespace Invoice.Application.Invoices.CreateInvoice;
internal sealed class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(c => c.InvoiceName)
            .NotEmpty()
            .WithMessage("Invoice name cannot be empty.");
    }
}
