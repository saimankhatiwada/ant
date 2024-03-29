using Invoice.Application.Abstractions.Messaging;
using Invoice.Domain.Abstractions;
using Invoice.Domain.Invoices;

namespace Invoice.Application.Invoices.RemoveInvoiceItem;

internal sealed class RemoveInvoiceItemCommandHandler : ICommandHandler<RemoveInvoiceItemCommand>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveInvoiceItemCommandHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveInvoiceItemCommand request, CancellationToken cancellationToken)
    {
        Domain.Invoices.Invoice? invoice = await _invoiceRepository.GetByIdAsync(new InvoiceId(request.InvoiceId), cancellationToken);

        if(invoice is null)
        {
            return Result.Failure(InvoiceErrors.InvoiceNotFound);
        }

        Result result = invoice.RemoveCartItem(new InvoiceItemId(request.InvoicetemId));

        if(result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
