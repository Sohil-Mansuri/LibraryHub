using LibraryHub.Core.Entity;

namespace LibraryHub.Core.Repository
{
    public interface ILibraryRepository
    {
        Task AddLibrary(LibraryInfo library);
        Task<List<LibraryInfo>> GetNearbyLibrariesAsync(double lat, double lng, double radius);
        Task<List<LibraryInfo>> GetNearbyLibrariesAsyncV2(double lat, double lng, double radius);
        Task ImportLibraries(List<LibraryInfo> libraries);
    }
}
