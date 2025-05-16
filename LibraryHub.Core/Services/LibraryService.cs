using LibraryHub.Core.Entity;
using LibraryHub.Core.Repository;

namespace LibraryHub.Core.Services
{
    public class LibraryService(ILibraryRepository libraryRepository)
    {
        public Task AddLibrary(LibraryInfo library)  => libraryRepository.AddLibrary(library);

        public Task ImportLibraries(List<LibraryInfo> libraries) => libraryRepository.ImportLibraries(libraries);
    }
}
