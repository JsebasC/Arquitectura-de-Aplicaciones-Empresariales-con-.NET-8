﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Features;
using Asp.Versioning;
using Microsoft.AspNetCore.Http.Timeouts;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v2
{
    [Authorize]
    [EnableRateLimiting("fixedWindow")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountsApplication _discountsApplication;

        public DiscountsController(IDiscountsApplication discountsApplication)
        {
            _discountsApplication = discountsApplication;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DiscountDto discountDto)
        {
            if (discountDto == null)
                return BadRequest();
            var response = await _discountsApplication.Create(discountDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DiscountDto discountDto)
        {
            var customerDtoExists = await _discountsApplication.Get(id);
            if (customerDtoExists.Data == null)
                return NotFound(customerDtoExists);

            if (discountDto == null)
                return BadRequest();
            var response = await _discountsApplication.Update(discountDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _discountsApplication.Delete(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("Get/{id}")]
        [RequestTimeout("CustomPolicy")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _discountsApplication.Get(id, HttpContext.RequestAborted);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _discountsApplication.GetAll(HttpContext.RequestAborted);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] int pageNumber, int pageSize)
        {
            var response = await _discountsApplication.GetAllWithPagination(pageNumber, pageSize);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}
