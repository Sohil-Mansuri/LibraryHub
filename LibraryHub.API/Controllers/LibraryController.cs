using Library.Contracts.Library;
using LibraryHub.API.Mapper;
using LibraryHub.API.Model;
using LibraryHub.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.API.Controllers
{
    [Route("api/library/v1")]
    [ApiController]
    public class LibraryController(LibraryService libraryService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddLibrary(LibraryModel libraryModel)
        {
            await libraryService.AddLibrary(libraryModel.ToLibrary());
            return Ok(ApiResponse<string>.Success("library added"));
        }

        [HttpPost("import")]
        public async Task<IActionResult> BuikImport(List<LibraryModel> libraries)
        {
            await libraryService.ImportLibraries([.. libraries.Select(l => l.ToLibrary())]);
            return Ok(ApiResponse<string>.Success("libraries imported"));
        }
    }
}
