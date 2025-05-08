using System.Diagnostics; //чтобы запускать процессы (bash, docker etc)
using System.Linq.Expressions;
using System.Text.Json; //для сериализации и десериализации json
using Docker_Update_Center.Models; //для работы с моделями

namespace Docker_Update_Center.Services;

public class DockerService
{
    public static List<ContainerInfo> GetContainers()
    {
        List<ContainerInfo> containers = new(); // создаем список контейнеров

        var process = new Process()
        {
            StartInfo = new ProcessStartInfo //StartInfo – что запустить и с какими параметрами
            {
                FileName = "zsh", //используем bash для запуска команд
                Arguments = "-c \"docker ps --format '{{json .}}'\"", //форматируем вывод в json
                // -с (от слова command) – выполнить строку как команду
                RedirectStandardOutput = true, //вывод команды в коде, а не в консоли
                UseShellExecute = false, //обязательно, чтобы читать вывод
                CreateNoWindow = true, //не создавать новое окно консоли
            },
        };

        process.Start();
        while (!process.StandardOutput.EndOfStream) //пока команда docker ps не завершилась
        {
            var line = process.StandardOutput.ReadLine(); //читаем строку вывода из консоли
            if (!string.IsNullOrWhiteSpace(line)) //если строка не пустая
            {
                try
                { //десериализуем строку в объект ContainerInfo
                    var container = JsonSerializer.Deserialize<ContainerInfo>(line);
                    if (container != null) //если десериализация прошла успешно
                    {
                        containers.Add(container);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при разборе строки: {line}\n{ex.Message}");
                }
            }
        }
        Console.WriteLine($"Загружено контейнеров: {containers.Count}");
        return containers;
    }

    //принимает id контейнера как строку
    public static bool RestartContainer(string id)
    {
        //создание процесса с bash командой
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "zsh",
                Arguments = $"-c \"docker restart {id}\"",
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
            },
        };
        //проверка ExitCode
        process.Start();
        process.WaitForExit();
        return process.ExitCode == 0;
    }

    public static string GetContainerLogs(string id)
    {
        try
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "zsh",
                    Arguments = $"-c \"docker logs {id}\"",
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                },
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd(); //нельзя использовать while, т.к метод сам читает строку до конца
            process.WaitForExit();
            return output;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при разборе логов контейнера: {id}\n{ex.Message}");
            return $"Не удалось получить логи контейнера {id}";
        }
    }
}
