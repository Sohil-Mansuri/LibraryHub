using LibraryHub.Core.Entity;

namespace LibraryHub.Core.Repository
{
    public interface ILibraryRepository
    {
        Task AddAsync(LibraryInfo library, CancellationToken cancellationToken = default);
        Task<List<LibraryInfo>> GetNearbyAsync(double lat, double lng, double radius, CancellationToken cancellationToken = default);
        Task<List<LibraryInfo>> GetNearbyAsyncv2(double lat, double lng, double radius, CancellationToken cancellationToken = default);
        Task ImportAsync(List<LibraryInfo> libraries, CancellationToken cancellationToken = default);
    }
}
