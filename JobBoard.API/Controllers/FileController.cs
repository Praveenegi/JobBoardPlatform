using Microsoft.AspNetCore.Mvc;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    [HttpPost("resume")]
    public async Task<IActionResult> UploadResume(
        IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(
                "No file uploaded");
        }

        var uploadsFolder =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads",
                "resumes");

        if (!Directory.Exists(
            uploadsFolder))
        {
            Directory.CreateDirectory(
                uploadsFolder);
        }

        var fileName =
            Guid.NewGuid() +
            Path.GetExtension(
                file.FileName);

        var filePath =
            Path.Combine(
                uploadsFolder,
                fileName);

        using var stream =
            new FileStream(
                filePath,
                FileMode.Create);

        await file.CopyToAsync(
            stream);

        var fileUrl =
            $"/Uploads/resumes/{fileName}";

        return Ok(new
        {
            fileUrl
        });
    }
}