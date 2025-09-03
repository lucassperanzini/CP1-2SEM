using System;
using System.IO;
using System.Threading.Tasks;

public class Program
{
    static async Task Main(string[] args)
    {
 
        string directoryPath;
            
    
        while (true)
        {
                Console.WriteLine("Por favor, digite o caminho do diretório que deseja acessar:");
                directoryPath = Console.ReadLine();

            if (FileService.DirectoryValidation(directoryPath))
            {
                break;

            }
            continue;
        }

     
        await FileService.ProcessDirectoryAsync(directoryPath);

  

        Console.WriteLine("\nProcesso concluído. Pressione qualquer tecla para sair.");
        Console.ReadKey();
    }


}