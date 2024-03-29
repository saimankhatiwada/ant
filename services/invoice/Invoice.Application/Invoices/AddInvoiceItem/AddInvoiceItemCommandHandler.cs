using Invoice.Application.Abstractions.Messaging;
using Invoice.Domain.Abstractions;
using Invoice.Domain.Invoices;

namespace Invoice.Application.Invoices.AddInvoiceItem;

internal sealed class AddInvoiceItemCommandHandler : ICommandHandler<AddInvoiceItemCommand>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddInvoiceItemCommandHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddInvoiceItemCommand request, CancellationToken cancellationToken)
    {
        Domain.Invoices.Invoice? invoice = await _invoiceRepository.GetByIdAsync(new InvoiceId(request.InvoiceId), cancellationToken);

        if(invoice is null)
        {
            return Result.Failure(InvoiceErrors.InvoiceNotFound);
        }

        invoice.AddInvoiceItem(
            new Name(request.Name), 
            Quantity.Create(request.Quantity).Value, 
            new Money(request.Amount, Currency.FromCode(request.Currency)));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
