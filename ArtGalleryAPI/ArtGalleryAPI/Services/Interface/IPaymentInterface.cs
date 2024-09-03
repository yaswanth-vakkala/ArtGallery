using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IPaymentInterface
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment>? GetPaymentByIdAsync(Guid paymentId);

        Task<Payment>? UpdatePaymentAsync(Guid paymentId, UpdatePaymentDto updatedPayment);

        Task<bool> DeletePaymentAsync(Guid paymentId);
        Task<Payment> CreatePaymentAsync(Payment payment);
    }
}
