using Microsoft.AspNetCore.Mvc;

namespace Giffinator.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class ThumbnailController : ControllerBase
{
    private readonly ILogger<ThumbnailController> _logger;

    public ThumbnailController(ILogger<ThumbnailController> logger)
    {
        _logger = logger;
    }

}