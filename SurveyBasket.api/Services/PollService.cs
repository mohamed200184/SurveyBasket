



using Azure.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.api.Entities;
using System.Threading;

namespace SurveyBasket.api.Services
{
    public class PollService(ApplicationDbContext contexct) : IPollService
    {
        private readonly ApplicationDbContext _context= contexct;
         public async Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await  _context.polls
                .AsNoTracking()
                .ProjectToType<PollResponse>()
                .ToListAsync(cancellationToken);
        }


        public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
        {
           var poll = await _context.polls.FindAsync(id, cancellationToken);
                return poll is not null ? Result.Success(poll.Adapt<PollResponse>()) : Result.Failure<PollResponse>(PollErrors.PollNotFound);
        }

        public async Task<Result<PollResponse>> AddAsync(PollRequest request , CancellationToken cancellationToken = default)
        {
            var isExistingTitle = await _context.polls.AnyAsync(x => x.Title.ToLower() == request.Title.ToLower(), cancellationToken);
            if (isExistingTitle)
            {
                Result.Failure<PollResponse>(PollErrors.DublicatedPollTitle);
            }
            var poll = request.Adapt<Poll>();
            await _context.AddAsync(poll,  cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(poll.Adapt<PollResponse>());
        }

        public async Task<Result> UpdateAsync(int id, PollRequest request, CancellationToken cancellationToken = default)
        {
            var isExistingTitle = await _context.polls.AnyAsync(x => x.Title.ToLower() == request.Title.ToLower() && x.Id != id, cancellationToken);
            if (isExistingTitle)
            {
                Result.Failure<PollResponse>(PollErrors.DublicatedPollTitle);
            }
            var currentPoll = await _context.polls.FindAsync(id,cancellationToken);
            if (currentPoll is null)
                return Result.Failure(PollErrors.PollNotFound);


            currentPoll.Title = request.Title;
            currentPoll.Summary = request.Summary;
            currentPoll.startsAt = request.StartAt;
            currentPoll.EndsAt= request.EndsAt;
           await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var poll = await _context.polls.FindAsync(id, cancellationToken);
            if (poll is null)
                return Result.Failure(PollErrors.PollNotFound);

            _context.Remove(poll);
           await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            var poll = await _context.polls.FindAsync(id, cancellationToken);
            if (poll is null)
                return Result.Failure(PollErrors.PollNotFound);


            poll.IsPublished = !poll.IsPublished;
      
           await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<IEnumerable<PollResponse>> GetCurrentAsync(CancellationToken cancellationToken = default)
        {
                return await _context.polls
                .Where(x => x.IsPublished && x.startsAt <= DateOnly.FromDateTime(DateTime.UtcNow) && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow))
                .AsNoTracking()
                .ProjectToType<PollResponse>()
                .ToListAsync(cancellationToken);
        }
    }
}
