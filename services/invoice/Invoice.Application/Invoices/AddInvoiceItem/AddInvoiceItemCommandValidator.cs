using FluentValidation;
using Invoice.Domain.Invoices;

namespace Invoice.Application.Invoices.AddInvoiceItem;

internal sealed class AddInvoiceItemCommandValidator : AbstractValidator<AddInvoiceItemCommand>
{
    public AddInvoiceItemCommandValidator()
    {
        RuleFor(c => c.InvoiceId)
            .NotEmpty()
            .WithMessage("Invoice id cannot be empty.")
            .Must(invoiceId => Guid.TryParse(invoiceId.ToString(), out _))
            .WithMessage("Invalid identifier.");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");

        RuleFor(c => c.Quantity)
            .NotEmpty()
            .WithMessage("Quantity cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");

        RuleFor(c => c.Amount)
            .NotEmpty()
            .WithMessage("Amount cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");

        RuleFor(c => c.Currency)
            .NotEmpty()
            .WithMessage("Currency cannot be empty")
            .Must(DoesCurrencyExist)
            .WithMessage("Invalid currency");

    }

    private bool DoesCurrencyExist(string code)
    {
        return !string.IsNullOrWhiteSpace(Currency.CheckCode(code).Code);
    }
}
