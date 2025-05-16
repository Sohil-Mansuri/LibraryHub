using LibraryHub.Core.Entity;

namespace LibraryHub.Core.Repository
{
    public interface ILibraryRepository
    {
        Task AddAsync(LibraryInfo library);
        Task<List<LibraryInfo>> GetNearbyAsync(double lat, double lng, double radius);
        Task<List<LibraryInfo>> GetNearbyAsyncv2(double lat, double lng, double radius);
        Task ImportAsync(List<LibraryInfo> libraries);
    }
}
