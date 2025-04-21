
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpertBooking.Core.Entities;
using ExpertBooking.Core.IRepositories.Shared;
using ExpertBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpertBooking.Infrastructure.Repositories.Shared
{
    public class ExpertDocumentRepository : IExpertDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpertDocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ExpertDocument document)
        {
            await _context.ExpertDocuments.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetCertificationUrlsByExpertIdAsync(Guid expertId)
        {
            return await _context.ExpertDocuments
                .Where(d => d.ExpertId == expertId)
                .Select(d => d.FileUrl)
                .ToListAsync();
        }
    }
}



