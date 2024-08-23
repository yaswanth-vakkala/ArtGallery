using ArtGalleryAPI.CustomExceptions;
using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class PaymentService : IPaymentInterface
    {
        private readonly ApplicationDbContext dbContext;
        public PaymentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            var payments = await dbContext.Payment.ToListAsync();
            return payments;

        }

            public async Task<Payment>? GetPaymentByIdAsync(Guid paymentId)
            {
                var payment = await dbContext.Payment.SingleOrDefaultAsync(payment => payment.PaymentId == paymentId);
                return payment;
            }

            public async Task<Payment> CreatePaymentAsync(Payment newPayment)
            {
                await dbContext.Payment.AddAsync(newPayment);
                await dbContext.SaveChangesAsync();
                return newPayment;
            }

        public async Task<Payment>? UpdatePaymentAsync(UpdatePaymentDto updatedPayment)
        {
            var payment = await dbContext.Payment.SingleOrDefaultAsync(payment => payment.PaymentId == updatedPayment.PaymentId);
            if (payment == null)
            {
                return null;
            }
            else
            {
                dbContext.Entry(payment).CurrentValues.SetValues(updatedPayment);
                await dbContext.SaveChangesAsync();
                return payment;
            }
        }
        public async Task<bool> DeletePaymentAsync(Guid paymentId)
        {
            var payment = await dbContext.Payment.SingleOrDefaultAsync(payment => payment.PaymentId == paymentId);
            if (payment == null)
            {
                return false;
            }
            else
            {
                dbContext.Payment.Remove(payment);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}