using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string url = "https://raw.githubusercontent.com/creater22/Programma/master/Optimization/Keys.txt";

        string fileContent = await GetContentFromGitHubAsync(url);
        if (fileContent == null)
        {
            Console.WriteLine("Не удалось получить содержимое файла");
            return;
        }

        string[] keys = fileContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine($"Найдено ключей: {keys.Length}");

        int validCount = 0;
        foreach (var key in keys)
        {
            if (CheckKey(key))
            {
                validCount++;
                Console.WriteLine($"Ключ прошел проверку: {key}");
            }
            else
            {
                Console.WriteLine($"Ключ не прошел проверку: {key}");
            }
        }

        Console.WriteLine($"Общее количество подходящих ключей: {validCount}");
    }

    static async Task<string> GetContentFromGitHubAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                return await client.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении файла: {ex.Message}");
                return null;
            }
        }
    }

    static bool CheckKey(string key)
    {
        // Ваша логика проверки ключа
        // Например, попытка расшифровать тестовое сообщение или другая логика
        return true; // Заглушка
    }
}