using ArtGalleryAPI.Models.Domain;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IPaymentInterface
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
    }
}
