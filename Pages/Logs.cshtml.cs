using Docker_Update_Center.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Docker_Update_Center.Pages
{
    public class LogsModel : PageModel
    {
        public string Logs { get; set; } = "";

        public IActionResult OnGet(string id)
        {
            Console.WriteLine($"Получен id: {id}");

            if (string.IsNullOrEmpty(id)) //если id пустой – отправка на гл страницу
            {
                return RedirectToPage("/Index");
            }

            Logs = DockerService.GetContainerLogs(id); //иначе получаем логи
            return Page();
        }
    }
}
