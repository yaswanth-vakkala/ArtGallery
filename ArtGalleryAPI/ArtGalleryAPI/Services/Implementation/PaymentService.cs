using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Services.Interface;

namespace ArtGalleryAPI.Services.Implementation
{
    public class PaymentService : IPaymentInterface
    {
        private readonly ApplicationDbContext dbContext;

        public PaymentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            await dbContext.Payment.AddAsync(payment);
            await dbContext.SaveChangesAsync();
            return payment;
        }
    }
}
