
using System.Collections.Generic;
using System.Threading;
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
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The client swift credentials.</returns>
        Task<ClientSwiftCredentialsModel> GetClientSwiftCredentialsAsync(string clientId, string legalEntityId, string assetId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns a collection of legal entities.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of legal entities.</returns>
        Task<IReadOnlyList<LegalEntityModel>> GetLegalEntitiesAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns a legal entity.
        /// </summary>
        /// <param name="legalEntityId">The legal entity id.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The legal entity.</returns>
        Task<LegalEntityModel> GetLegalEntitiesByIdAsync(string legalEntityId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds a legal entity.
        /// </summary>
        /// <param name="model">The legal entity creation information.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task AddLegalEntityAsync(CreateLegalEntityModel model, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates a legal entity.
        /// </summary>
        /// <param name="model">The legal entity update information.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task UpdateLegalEntityAsync(EditLegalEntityModel model, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes a legal entity.
        /// </summary>
        /// <param name="legalEntityId">The legal entity id.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task DeleteLegalEntityAsync(string legalEntityId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns legal entity swift credentials.
        /// </summary>
        /// <param name="legalEntityId">The legal entity id.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collections of swift credentials of legal entity.</returns>
        Task<IReadOnlyList<SwiftCredentialsModel>> GetLegalEntitySwiftCredentialsAsync(string legalEntityId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns swift credentials.
        /// </summary>
        /// <param name="swiftCredentialsId">The swift credentials id.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The swift credentials.</returns>
        Task<ClientSwiftCredentialsModel> GetSwiftCredentialsByIdAsync(string swiftCredentialsId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds swift credentials.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <param name="model">The model that describe a swift credentials.</param>
        Task AddSwiftCredentialsAsync(CreateSwiftCredentialsModel model, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates swift credentials.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <param name="model">The model that describe a swift credentials.</param>
        Task UpdateSwiftCredentialsAsync(EditSwiftCredentialsModel model, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes swift credentials.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <param name="swiftCredentialsId">The swift credentials id.</param>
        Task DeleteSwiftCredentialsAsync(string swiftCredentialsId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
