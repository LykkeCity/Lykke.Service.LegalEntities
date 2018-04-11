using System;
using System.Collections.Generic;
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
    public class LegalEntitiesController : Controller
    {
        private readonly ILegalEntityService _legalEntityService;
        private readonly ISwiftCredentialsService _swiftCredentialsService;
        private readonly ILog _log;

        public LegalEntitiesController(
            ILegalEntityService legalEntityService,
            ISwiftCredentialsService swiftCredentialsService,
            ILog log)
        {
            _legalEntityService = legalEntityService;
            _swiftCredentialsService = swiftCredentialsService;
            _log = log;
        }
        
        /// <summary>
        /// Returns a collection of legal entities.
        /// </summary>
        /// <returns>A collection of legal entities.</returns>
        /// <response code="200">A collection of legal entities.</response>
        [HttpGet]
        [SwaggerOperation("LegalEntitiesGetAll")]
        [ProducesResponseType(typeof(IEnumerable<LegalEntityModel>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var legalEntities = await _legalEntityService.GetAllAsync();

            var model = Mapper.Map<IEnumerable<LegalEntityModel>>(legalEntities);

            return Ok(model);
        }

        /// <summary>
        /// Returns a legal entity.
        /// </summary>
        /// <param name="legalEntityId">The legal entity id.</param>
        /// <returns>The legal entity.</returns>
        /// <response code="200">The legal entity.</response>
        /// <response code="404">Legal entity not found.</response>
        [HttpGet]
        [Route("{legalEntityId}")]
        [SwaggerOperation("LegalEntitiesGetById")]
        [ProducesResponseType(typeof(LegalEntityModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByIdAsync(string legalEntityId)
        {
            var legalEntity = await _legalEntityService.GetByIdAsync(legalEntityId);

            if (legalEntity == null)
                return NotFound();

            var model = Mapper.Map<LegalEntityModel>(legalEntity);

            return Ok(model);
        }

        /// <summary>
        /// Adds a legal entity.
        /// </summary>
        /// <param name="model">The legal entity creation information.</param>
        /// <response code="200">The legal entity.</response>
        /// <response code="400">Invalid model or already exists.</response>
        [HttpPost]
        [SwaggerOperation("LegalEntitiesAdd")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddAsync([FromBody] CreateLegalEntityModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse().AddErrors(ModelState));

            try
            {
                var legalEntity = Mapper.Map<LegalEntity>(model);

                await _legalEntityService.AddAsync(legalEntity);
            }
            catch (LegalEntityAlreadyExistsException exception)
            {
                await _log.WriteWarningAsync(nameof(LegalEntitiesController), nameof(AddAsync),
                    model.ToJson(), exception.Message);
                return BadRequest(ErrorResponse.Create(exception.Message));
            }
            
            return NoContent();
        }

        /// <summary>
        /// Updates a legal entity.
        /// </summary>
        /// <param name="model">The legal entity update information.</param>
        /// <response code="204">Legal entity successfully updated.</response>
        /// <response code="400">Invalid model.</response>
        /// <response code="404">Legal entity not found.</response>
        [HttpPut]
        [SwaggerOperation("LegalEntitiesUpdate")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] EditLegalEntityModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse().AddErrors(ModelState));

            try
            {
                var legalEntity = Mapper.Map<LegalEntity>(model);

                await _legalEntityService.UpdateAsync(legalEntity);
            }
            catch (LegalEntityNotFoundException exception)
            {
                await _log.WriteWarningAsync(nameof(LegalEntitiesController), nameof(UpdateAsync),
                    model.ToJson(), exception.Message);
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a legal entity.
        /// </summary>
        /// <param name="legalEntityId">The legal entity id.</param>
        /// <response code="204">Legal entity successfully deleted.</response>
        /// <response code="400">One or more swift credentials associated with legal entity.</response>
        [HttpDelete]
        [Route("{legalEntityId}")]
        [SwaggerOperation("LegalEntitiesDelete")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync(string legalEntityId)
        {
            try
            {
                await _legalEntityService.DeleteAsync(legalEntityId);
            }
            catch (InvalidOperationException exception)
            {
                await _log.WriteWarningAsync(nameof(LegalEntitiesController), nameof(DeleteAsync),
                    new { legalEntityId }.ToJson(), exception.Message);
                return BadRequest(ErrorResponse.Create(exception.Message));
            }

            return NoContent();
        }

        /// <summary>
        /// Returns legal entity swift credentials.
        /// </summary>
        /// <param name="legalEntityId">The legal entity id.</param>
        /// <returns>A collections of swift credentials of legal entity.</returns>
        /// <response code="200">The collection of swift credentials.</response>
        [HttpGet]
        [Route("{legalEntityId}/swiftcredentials")]
        [SwaggerOperation("LegalEntitiesGetSwiftCredentials")]
        [ProducesResponseType(typeof(IEnumerable<SwiftCredentialsModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSwiftCredentialsAsync(string legalEntityId)
        {
            IEnumerable<SwiftCredentials> swiftCredentials =
                await _swiftCredentialsService.GetByLegalEntityIdAsync(legalEntityId);

            var model = Mapper.Map<IEnumerable<SwiftCredentialsModel>>(swiftCredentials);

            return Ok(model);
        }
    }
}
