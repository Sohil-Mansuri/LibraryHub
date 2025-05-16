using LibraryHub.Core.Entity;

namespace LibraryHub.Core.Repository
{
    public interface ILibraryRepository
    {
        Task AddLibrary(LibraryInfo library);

        Task ImportLibraries(List<LibraryInfo> libraries);
    }
}
