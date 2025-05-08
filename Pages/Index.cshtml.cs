using Docker_Update_Center.Models;
using Docker_Update_Center.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Docker_Update_Center.Pages;

public class IndexModel : PageModel
{
    public List<ContainerInfo>? Containers { get; set; }

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(string id)
    {
        ServerTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        Containers = DockerService.GetContainers();
        Console.WriteLine($"Количество контейнеров… {Containers.Count}");
        Console.WriteLine($"Id контейнера: {id}");
    }

    public string? ServerTime { get; set; }

    public ActionResult OnPostRestart(string id)
    {
        DockerService.RestartContainer(id);
        return RedirectToPage();
    }
}
