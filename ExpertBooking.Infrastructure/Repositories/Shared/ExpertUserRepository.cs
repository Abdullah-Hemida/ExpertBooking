
using ExpertBooking.Core.Entities;
using ExpertBooking.Core.IRepositories.Shared;
using ExpertBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExpertBooking.Infrastructure.Repositories.Shared
{
    public class ExpertUserRepository : IExpertUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ExpertUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Expert?> GetByIdAsync(Guid userId)
        {
            return await _context.Experts.FindAsync(userId);
        }

        public async Task<Expert?> GetByIdWithCategoryAndDocumentsAsync(Guid userId)
        {
            return await _context.Experts
                .Include(e => e.Category)
                .Include(e => e.ExpertDocuments)
                .FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task CreateAsync(Expert expert)
        {
            _context.Experts.Add(expert);
            await _context.SaveChangesAsync();
        }
    }

}


