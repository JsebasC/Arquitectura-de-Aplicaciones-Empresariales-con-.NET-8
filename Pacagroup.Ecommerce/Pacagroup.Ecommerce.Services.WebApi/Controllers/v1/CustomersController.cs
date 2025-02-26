using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Features;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersApplication _customersApplication;

        public CustomersController(ICustomersApplication customersApplication)
        {
            _customersApplication = customersApplication;
        }

        #region Metodos Sincronos
        [HttpPost()]
        public IActionResult Insert([FromBody] CustomerDto customersDto)
        {
            if (customersDto == null)
                return BadRequest();

            var response = _customersApplication.Insert(customersDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest();
        }

        [HttpPut()]
        public IActionResult Update([FromBody] CustomerDto customersDto)
        {
            if (customersDto == null)
                return BadRequest();

            var response = _customersApplication.Update(customersDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest();
        }

        [HttpDelete("{customerId}")]
        public IActionResult Delete(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = _customersApplication.Delete(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest();
        }

        [HttpGet("{customerId}")]
        public IActionResult Get(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = _customersApplication.Get(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest();
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var response = _customersApplication.GetAll();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest();
        }

        #endregion

        #region Metodos Asincronos

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] CustomerDto customersDto)
        {
            if (customersDto == null)
                return BadRequest();

            var response = await _customersApplication.InsertAsync(customersDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest();
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomerDto customersDto)
        {
            if (customersDto == null)
                return BadRequest();

            var response = await _customersApplication.UpdateAsync(customersDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest();
        }

        [HttpDelete("DeleteAsync/{customerId}")]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = await _customersApplication.DeleteAsync(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest();
        }

        [HttpGet("GetAsync/{customerId}")]
        public async Task<IActionResult> GetAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = await _customersApplication.GetAsync(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest();
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customersApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest();
        }

        #endregion
    }
}
