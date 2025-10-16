using Microsoft.EntityFrameworkCore;
using SurveyBasket.api.Contracts.Questions;
using SurveyBasket.api.Contracts.Votes;

namespace SurveyBasket.api.Services
{
    public class VoteService(ApplicationDbContext context

        ) : IVoteService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result> AddAsync(int pollId, string userId, VoteRequest request, CancellationToken cancellationToken = default)
        {
            var hasVotes = await _context
             .Votes
             .AnyAsync
             (x => x.PollId == pollId && x.UserId == userId, cancellationToken);


            if (hasVotes)
                return Result.Failure(VoteErrors.DublicatedVote);

            var pollIsExists = await _context.polls
                .AnyAsync(x => x.Id == pollId && x.startsAt <= DateOnly.FromDateTime(DateTime.UtcNow) && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);

            if (!pollIsExists)
                return Result.Failure(PollErrors.PollNotFound);


            var avalibleQuestions = await _context.Questions
                .Where(x => x.PollId == pollId && x.IsActive)
                .Select(q => q.Id)
                .ToListAsync(cancellationToken);

            if (!request.Answers.Select(x => x.QuestionId).SequenceEqual(avalibleQuestions))
                return Result.Failure(VoteErrors.InvalidQuestion);

            var vote = new Vote
            {
                PollId = pollId,
                UserId = userId,
                VoteAnswers = request.Answers.Adapt<IEnumerable<VoteAnswer>>().ToList()


            };

            await _context.AddAsync(vote, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    } }

