using Invoice.Application.Abstractions.Messaging;
using Invoice.Domain.Abstractions;
using Invoice.Domain.Invoices;

namespace Invoice.Application.Invoices.CreateInvoice;

internal sealed class CreateInvoiceCommandHandler : ICommandHandler<CreateInvoiceCommand, Guid>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = Domain.Invoices.Invoice.Create(new InvoiceName(request.InvoiceName));

        _invoiceRepository.Add(invoice);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return invoice.Id.Value;
    }

}
