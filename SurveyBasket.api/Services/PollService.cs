



using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.api.Entities;
using System.Threading;

namespace SurveyBasket.api.Services
{
    public class PollService(ApplicationDbContext contexct) : IPollService
    {
        private readonly ApplicationDbContext _context= contexct;
         public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await  _context.polls.AsNoTracking().ToListAsync(cancellationToken);
        }


        public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.polls.FindAsync(id, cancellationToken);
        }

        public async Task<Poll> AddAsync(Poll poll , CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(poll,  cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return poll;
        }

        public async Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken = default)
        {
            var currentPoll = await GetAsync(id, cancellationToken);
            if (currentPoll is null)
                return false;


            currentPoll.Title = poll.Title;
            currentPoll.Summary = poll.Summary;
            currentPoll.startsAt = poll.startsAt;
            currentPoll.EndAt=poll.EndAt;
            _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var poll = await GetAsync(id, cancellationToken);
            if (poll is null)
                return false;

            _context.Remove(poll);
            _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            var poll = await GetAsync(id, cancellationToken);
            if (poll is null)
                return false;


            poll.IsPublished = !poll.IsPublished;
      
            _context.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
