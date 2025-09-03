using System.Text;

public  class FileService { 

    public static bool DirectoryValidation(string directoryPath)
    {


        if (Directory.Exists(directoryPath))
        {
            Console.WriteLine($"\nO diretório '{directoryPath}' foi encontrado. Processando...");

            return true;

        }
        else
        {
            Console.WriteLine($"\nErro: O diretório '{directoryPath}' não foi encontrado. Por favor, tente novamente.");
            return false;
        }

    }


    public static async Task ProcessDirectoryAsync(string path)
    {
        try {  
  
            string[] files = Directory.GetFiles(path);

            if (files.Length == 0)
            {
                Console.WriteLine("O diretório está vazio.");
                return;
            }

            
            var processingTasks = new List<Task<string>>();

     
            foreach (string filePath in files)
            {
                processingTasks.Add(ProcessFileAsync(filePath));


            }

            string[] results = await Task.WhenAll(processingTasks);

           
            string finalReport = string.Join(Environment.NewLine, results);


            string fatherPath = Path.Combine(path, "..");

            string reportPath = Path.Combine(fatherPath, "RelatorioProcessamento.txt");

            await SaveReportAsync(reportPath, finalReport);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n ERRO ao processar o diretório: {ex.Message}");
        }
    }

    private static async Task SaveReportAsync(string filePath, string content)
    {
        try
        {
            await File.WriteAllTextAsync(filePath, "Arquivo       Linhas       Palavras \n");
            await File.AppendAllTextAsync(filePath, content);
            Console.WriteLine($"\nRelatório salvo com sucesso em: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[Erro] Falha ao salvar o relatório: {ex.Message}");
        }
    }






    private static int CountWords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }

  
        char[] delimiters = new char[] { ' ', '\r', '\n', '\t', ',', '.', '!', '?', ';', ':', '-', '(', ')' };

    
        string[] words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        return words.Length;
    }



    private static async Task<string> ProcessFileAsync(string filePath)
    {
        try
        {

            string content = await File.ReadAllTextAsync(filePath);
            int wordCount = CountWords(content);
            string[] lines = await File.ReadAllLinesAsync(filePath);
            int lineCount = lines.Length;



            Console.WriteLine($"\nProcessando");
            Console.WriteLine($"Concluído : {Path.GetFileName(filePath)} Linhas :{lineCount} - palavras : {wordCount}  ---\n");

            return $"\n{Path.GetFileName(filePath)}   Linhas :{lineCount} -   Palavras : {wordCount}  ---\n";



        }
        catch (Exception ex)
        {
            return ($"[Erro] Falha ao processar {Path.GetFileName(filePath)}: {ex.Message}");
        }


    }


}