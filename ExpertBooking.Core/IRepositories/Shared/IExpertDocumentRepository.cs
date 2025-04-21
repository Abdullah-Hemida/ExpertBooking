
using ExpertBooking.Core.Entities;


namespace ExpertBooking.Core.IRepositories.Shared
{
    public interface IExpertDocumentRepository
    {
        Task AddAsync(ExpertDocument document);
        Task<List<string>> GetCertificationUrlsByExpertIdAsync(Guid expertId);
    }
}


