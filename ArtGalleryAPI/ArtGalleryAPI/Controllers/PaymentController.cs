using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Implementation;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentInterface paymentService;

        public PaymentController(IPaymentInterface paymentService)
        {
            this.paymentService = paymentService;
        }

        /// <summary>
        /// creates a payment record in db
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var payments = await paymentService.GetAllPaymentsAsync();
                List<PaymentDto> result = new List<PaymentDto>();
                foreach (Payment payment in payments)
                {
                    result.Add(
                        new PaymentDto()
                        {
                            PaymentId = payment.PaymentId,
                            Amount = payment.Amount,
                            PaymentDate = payment.PaymentDate,
                            PaymentMethod = payment.PaymentMethod,
                            Status = payment.Status,

                        }
                        );
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{paymentId:Guid}")]
        public async Task<IActionResult> GetPaymentById([FromRoute] Guid paymentId)
        {
            try
            {
                var payment = await paymentService.GetPaymentByIdAsync(paymentId);
                if (payment == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(payment);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// creates a payment record in db
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreatePayment(AddPaymentDto payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var newPayment = new Payment()
                {
                    Amount = payment.Amount,
                    PaymentDate = payment.PaymentDate,
                    PaymentMethod = payment.PaymentMethod,
                    Status = "Success"
                };

                var res = await paymentService.CreatePaymentAsync(newPayment);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{paymentId:Guid}")]
        public async Task<IActionResult> UpdatePayment([FromRoute] Guid paymentId, [FromBody] UpdatePaymentDto updatedPayment)
        {
            try
            {
                var result = await paymentService.UpdatePaymentAsync(paymentId, updatedPayment);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{paymentId:Guid}")]
        public async Task<IActionResult> DeletePayment([FromRoute] Guid paymentId)
        {
            try
            {
                var deleteStatus = await paymentService.DeletePaymentAsync(paymentId);
                return Ok(deleteStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
