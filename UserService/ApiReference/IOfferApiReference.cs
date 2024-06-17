using Refit;

namespace UserService.ApiReference
{
    public interface IOfferApiReference
    {
        [Delete("/users/{userId}")]
        Task<bool> DeleteOffersByUserId([AliasAs("userId")] Guid userId);

        [Delete("/users/{userId}/offer/{offerId}")]
        Task<bool> DeleteOffer([AliasAs("userId")] Guid userId, [AliasAs("offerId")] long offerId);
    }
}