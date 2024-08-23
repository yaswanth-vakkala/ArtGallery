using ArtGalleryAPI.CustomExceptions;
using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Implementation;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var payments = await paymentService.GetAllPaymentsAsync();
                return Ok(payments);
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

        [HttpPost]

        public async Task<IActionResult> AddPayment([FromBody] AddPaymentDto payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var newPayment = new Payment
                {
                    PaymentMethod = payment.PaymentMethod,
                    Amount = payment.Amount,
                    PaymentDate = DateTime.UtcNow,
                };
                await paymentService.CreatePaymentAsync(newPayment);
                var locationUri = Url.Action("GetPaymentById", new { paymentId = newPayment.PaymentId });
                return Created(locationUri, newPayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayment([FromBody] UpdatePaymentDto updatedPayment)
        {
            try
            {
                var result = await paymentService.UpdatePaymentAsync(updatedPayment);
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
















