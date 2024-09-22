using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    public class FaturamentoDia
    {
        [JsonPropertyName("dia")]
        public int Dia { get; set; }

        [JsonPropertyName("valor")]
        public double Valor { get; set; }
    }

    public class FaturamentoMensal
    {
        [JsonPropertyName("faturamento_diario")]
        public List<FaturamentoDia> FaturamentoDiario { get; set; }
    }

    static void Main()
    {
        try
        {
            // Lê o arquivo JSON
            string json = File.ReadAllText("faturamento.json");

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new Exception("O arquivo JSON está vazio.");
            }

            FaturamentoMensal faturamentoMensal = JsonSerializer.Deserialize<FaturamentoMensal>(json);

            // Verifica se os dados foram carregados corretamente
            if (faturamentoMensal?.FaturamentoDiario == null || !faturamentoMensal.FaturamentoDiario.Any())
            {
                throw new Exception("Dados de faturamento não encontrados ou inválidos no arquivo JSON.");
            }

            // Filtra os dias com faturamento acima de 0
            var faturamentoValido = faturamentoMensal.FaturamentoDiario
                                    .Where(f => f.Valor > 0)
                                    .Select(f => f.Valor)
                                    .ToList();

            if (faturamentoValido.Count > 0)
            {
                // Calcula o menor, maior e a média
                double menorFaturamento = faturamentoValido.Min();
                double maiorFaturamento = faturamentoValido.Max();
                double mediaMensal = faturamentoValido.Average();

                // Conta o número de dias com faturamento superior à média
                int diasAcimaDaMedia = faturamentoValido.Count(f => f > mediaMensal);

                // Exibe os resultados
                Console.WriteLine($"Menor faturamento: {menorFaturamento}");
                Console.WriteLine($"Maior faturamento: {maiorFaturamento}");
                Console.WriteLine($"Número de dias com faturamento acima da média: {diasAcimaDaMedia}");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Arquivo JSON não encontrado. Certifique-se de que o arquivo 'faturamento.json' está no local correto.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Erro ao processar o arquivo JSON. Verifique se o formato do arquivo está correto.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
