using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Models.Domain;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IPaymentInterface
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment>? GetPaymentByIdAsync(Guid paymentId);
        Task<Payment> CreatePaymentAsync(Payment newPayment);
        Task<Payment>? UpdatePaymentAsync(UpdatePaymentDto updatedPayment);

        Task<bool> DeletePaymentAsync(Guid PaymentId);
    }
}
