using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Service.LegalEntities.Core.Services;
using Lykke.Service.LegalEntities.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.LegalEntities.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Returns client swift credentials.
        /// </summary>
        /// <returns>The client swift credentials.</returns>
        /// <param name="clientId">The client id.</param>
        /// <param name="legalEntityId">The legal entity id.</param>
        /// <param name="assetId">The asset id.</param>
        /// <response code="200">The client swift credentials.</response>
        /// <response code="400">Invalid request parameters.</response>
        /// <response code="404">Swift credentials not found.</response>
        [HttpGet]
        [Route("{clientId}/swiftcredentials")]
        [SwaggerOperation("ClientsGetSwiftCredentials")]
        [ProducesResponseType(typeof(ClientSwiftCredentialsModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSwiftCredentialsAsync(string clientId, string legalEntityId, string assetId)
        {
            if (string.IsNullOrEmpty(legalEntityId))
                return BadRequest(ErrorResponse.Create("Legal entity id required"));

            if (string.IsNullOrEmpty(assetId))
                return BadRequest(ErrorResponse.Create("Asset id required"));

            var clientSwiftCredentials = await _clientService.GetSwiftCredentialsAsync(clientId, legalEntityId, assetId);

            if (clientSwiftCredentials == null)
                return NotFound();
            
            var model = Mapper.Map<ClientSwiftCredentialsModel>(clientSwiftCredentials);

            return Ok(model);
        }
    }
}
