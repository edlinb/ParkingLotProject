using Microsoft.AspNetCore.Mvc;
using ParkingLotProject.Models;
using ParkingLotProject.Repositories;

namespace ParkingLotProject.Controllers
{
    public class PricingPlanController
    {
        [ApiController]
        [Route("api/PricingPlan")]

        public class PricingPlanControlller(PricingPlansRepository pricingPlansRepository) : ControllerBase
        {
            private readonly PricingPlansRepository _pricingPlansRepository = pricingPlansRepository;

            [HttpPost]
            public IActionResult CreatePricingPlan([FromBody] PricingPlan pricingPlans)
            {
                try
                {
                    _pricingPlansRepository.CreatePricingPlan(pricingPlans);
                    return Ok("Pricing Plan created ssuccessfully");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to create pricing plan:{ex.Message}");
                }
            }
            [HttpGet]
            public IActionResult GetPricingPlans()
            {
                try
                {
                    _pricingPlansRepository.GetPricingPlans();
                    return Ok("Pricing Plan created successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to get pricing plan:{ex.Message}");
                }
            }
            [HttpPatch("{id}")]
            public IActionResult UpdatePricingPlan(int id, decimal hourlyPricing, decimal dailyPricing)
            {
                var pricingPlan = _pricingPlansRepository.GetPricingPlans().FirstOrDefault(p => p.Id == id);
                if (pricingPlan != null)
                {
                    _pricingPlansRepository.UpdatePricingPlan(id, hourlyPricing, dailyPricing);
                }
                return Ok();
            }

        }
    }
}