using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Common.Log;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Service.LegalEntities.Core.Domain;
using Lykke.Service.LegalEntities.Core.Exceptions;
using Lykke.Service.LegalEntities.Core.Services;
using Lykke.Service.LegalEntities.Extensions;
using Lykke.Service.LegalEntities.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.LegalEntities.Controllers
{
    [Route("api/[controller]")]
    public class SwiftCredentialsController : Controller
    {
        private readonly ISwiftCredentialsService _swiftCredentialsService;
        private readonly ILog _log;

        public SwiftCredentialsController(
            ISwiftCredentialsService swiftCredentialsService,
            ILog log)
        {
            _swiftCredentialsService = swiftCredentialsService;
            _log = log;
        }

        /// <summary>
        /// Returns swift credentials.
        /// </summary>
        /// <param name="swiftCredentialsId">The swift credentials id.</param>
        /// <response code="200">The swift credentials.</response>
        /// <response code="404">Swift credentials not found.</response>
        [HttpGet]
        [Route("{swiftCredentialsId}")]
        [SwaggerOperation("SwiftCredentialsGetById")]
        [ProducesResponseType(typeof(SwiftCredentialsModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByIdAsync(string swiftCredentialsId)
        {
            SwiftCredentials swiftCredentials = await _swiftCredentialsService.GetByIdAsync(swiftCredentialsId);

            if (swiftCredentials == null)
                return NotFound();

            var model = Mapper.Map<SwiftCredentialsModel>(swiftCredentials);

            return Ok(model);
        }

        /// <summary>
        /// Adds swift credentials.
        /// </summary>
        /// <param name="model">The model that describe a swift credentials.</param>
        /// <response code="204">The swift credentials successfully added.</response>
        /// <response code="400">Invalid model.</response>
        [HttpPost]
        [SwaggerOperation("SwiftCredentialsAdd")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddAsync([FromBody] CreateSwiftCredentialsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse().AddErrors(ModelState));

            var swiftCredentials = Mapper.Map<SwiftCredentials>(model);

            try
            {
                await _swiftCredentialsService.AddAsync(swiftCredentials);
            }
            catch (Exception exception) when (exception is SwiftCredentialsAlreadyExistsException || exception is LegalEntityNotFoundException)
            {
                await _log.WriteWarningAsync(nameof(SwiftCredentialsController), nameof(AddAsync),
                    model.ToJson(), exception.Message);
                return BadRequest(exception.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Updates swift credentials.
        /// </summary>
        /// <param name="model">The model that describe a swift credentials.</param>
        /// <response code="204">The swift credentials successfully updated.</response>
        /// <response code="400">The swift credentials model is invalid.</response>
        /// <response code="404">The swift credentials not found.</response>
        [HttpPatch]
        [SwaggerOperation("SwiftCredentialsUpdate")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] EditSwiftCredentialsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse().AddErrors(ModelState));

            var swiftCredentials = Mapper.Map<SwiftCredentials>(model);

            try
            {
                await _swiftCredentialsService.UpdateAsync(swiftCredentials);
            }
            catch (SwiftCredentialsNotFoundException exception)
            {
                await _log.WriteWarningAsync(nameof(SwiftCredentialsController), nameof(UpdateAsync),
                    model.ToJson(), exception.Message);
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes swift credentials.
        /// </summary>
        /// <param name="swiftCredentialsId">The swift credentials id.</param>
        /// <response code="204">The swift credentials successfully deleted.</response>
        [HttpDelete]
        [Route("{swiftCredentialsId}")]
        [SwaggerOperation("SwiftCredentialsDelete")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(string swiftCredentialsId)
        {
            await _swiftCredentialsService.DeleteAsync(swiftCredentialsId);

            return NoContent();
        }
    }
}
