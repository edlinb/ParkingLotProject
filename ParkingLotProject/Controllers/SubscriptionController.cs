using Microsoft.AspNetCore.Mvc;
using ParkingLotProject.Models;
using ParkingLotProject.Repositories;
using System.Runtime.CompilerServices;

namespace ParkingLotProject.Controllers
{
    public class SubscriptionController
    {
        [ApiController]
        [Route("api/Subscriptions")]

        public class SubscriptionsController : ControllerBase
        {
            private readonly SubscriptionsRepository _subscriptionsRepository;

            public SubscriptionsController(SubscriptionsRepository subscriptionsReporsitory)
            {
                _subscriptionsRepository = subscriptionsReporsitory;
            }

            [HttpGet("All")]
            public IActionResult GetAllSubscriptions()
            {
                try
                {
                    IEnumerable<Subscription> subscriptions = _subscriptionsRepository.GetAll();
                    return Ok(subscriptions);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to retrieve all subscriber:{ex.Message}");
                }
            }

            [HttpGet("{id}")]

            public IActionResult GetSubscriptionsId(int id)
            {
                try
                {
                    var subscriptions = _subscriptionsRepository.GetById(id);
                    return Ok(subscriptions);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to get id:{ex.Message}");
                }


            }
            [HttpPost]
            public IActionResult CreateSubscriptions(Subscription subscription)
            {
                try
                {
                    _subscriptionsRepository.AddSubscription(subscription);
                    return Ok("Subscription was created succesfully.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to create subscription: {ex.Message}");
                }
            }
            [HttpPut]
            public IActionResult UpdateSubscriptions(Subscription subscription)
            {
                try
                {
                    _subscriptionsRepository.UpdateSubscription(subscription);
                    return Ok("Subscription was updated succesfully.");
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to update subscription: {ex.Message}");
                }
            }
            [HttpDelete]
            public IActionResult SoftDeleteSubscriptions(int code)
            {
                try
                {
                    _subscriptionsRepository.SoftDelete(code);
                    return Ok("Subscription deleted succesfully.");
                }
                catch (ArgumentException ex)
                { 
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to update subscription: {ex.Message}");
                }
            }


        }
    }
}