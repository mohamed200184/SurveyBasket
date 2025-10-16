using SurveyBasket.api.Contracts.Votes;

namespace SurveyBasket.api.Services
{
    public interface IVoteService
    {
        Task<Result>AddAsync(int pollId,string userId,VoteRequest voteRequest , CancellationToken cancellationToken = default);

    }
}
