
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.LegalEntities.Client.Models;

namespace Lykke.Service.LegalEntities.Client
{
    /// <summary>
    /// HTTP client for legal entities service.
    /// </summary>
    public interface ILegalEntitiesClient
    {
        /// <summary>
        /// Returns client swift credentials.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="legalEntityId">The legal entity id.</param>
        /// <param name="assetId">The asset id.</param>
        /// <returns>The client swift credentials.</returns>
        Task<ClientSwiftCredentialsModel> GetClientSwiftCredentialsAsync(string clientId, string legalEntityId, string assetId);

        /// <summary>
        /// Returns a collection of legal entities.
        /// </summary>
        /// <returns>A collection of legal entities.</returns>
        Task<IReadOnlyList<LegalEntityModel>> GetLegalEntitiesAllAsync();

        /// <summary>
        /// Returns a legal entity.
        /// </summary>
        /// <param name="legalEntityId">The legal entity id.</param>
        /// <returns>The legal entity.</returns>
        Task<LegalEntityModel> GetLegalEntitiesByIdAsync(string legalEntityId);

        /// <summary>
        /// Adds a legal entity.
        /// </summary>
        /// <param name="model">The legal entity creation information.</param>
        Task AddLegalEntityAsync(CreateLegalEntityModel model);

        /// <summary>
        /// Updates a legal entity.
        /// </summary>
        /// <param name="model">The legal entity update information.</param>
        Task UpdateLegalEntityAsync(EditLegalEntityModel model);

        /// <summary>
        /// Deletes a legal entity.
        /// </summary>
        /// <param name="legalEntityId">The legal entity id.</param>
        Task DeleteLegalEntityAsync(string legalEntityId);

        /// <summary>
        /// Returns legal entity swift credentials.
        /// </summary>
        /// <param name="legalEntityId">The legal entity id.</param>
        /// <returns>A collections of swift credentials of legal entity.</returns>
        Task<IReadOnlyList<SwiftCredentialsModel>> GetLegalEntitySwiftCredentialsAsync(string legalEntityId);

        /// <summary>
        /// Returns swift credentials.
        /// </summary>
        /// <param name="swiftCredentialsId">The swift credentials id.</param>
        /// <returns>The swift credentials.</returns>
        Task<ClientSwiftCredentialsModel> GetSwiftCredentialsByIdAsync(string swiftCredentialsId);

        /// <summary>
        /// Adds swift credentials.
        /// </summary>
        /// <param name="model">The model that describe a swift credentials.</param>
        Task AddSwiftCredentialsAsync(CreateSwiftCredentialsModel model);

        /// <summary>
        /// Updates swift credentials.
        /// </summary>
        /// <param name="model">The model that describe a swift credentials.</param>
        Task UpdateSwiftCredentialsAsync(EditSwiftCredentialsModel model);

        /// <summary>
        /// Deletes swift credentials.
        /// </summary>
        /// <param name="swiftCredentialsId">The swift credentials id.</param>
        Task DeleteSwiftCredentialsAsync(string swiftCredentialsId);
    }
}
