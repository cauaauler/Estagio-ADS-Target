using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Faturamento
{
    class Program
    {
        public class FaturamentoPorEstado
        {
            [JsonPropertyName("estado")]
            public string Estado { get; set; }

            [JsonPropertyName("valor")]
            public double Valor { get; set; }
        }

        public class Faturamento
        {
            [JsonPropertyName("faturamento")]
            public List<FaturamentoPorEstado> Estados { get; set; }
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

                Faturamento faturamentoPorEstado = JsonSerializer.Deserialize<Faturamento>(json);

                if (faturamentoPorEstado?.Estados == null || !faturamentoPorEstado.Estados.Any())
                {
                    throw new Exception("Dados de faturamento não encontrados ou inválidos no arquivo JSON.");
                }

                double valorTotal = faturamentoPorEstado.Estados.Sum(e => e.Valor);

                foreach (var estado in faturamentoPorEstado.Estados)
                {
                    double percentual = (estado.Valor / valorTotal) * 100;
                    Console.WriteLine($"Estado: {estado.Estado}, Valor: {estado.Valor}, Percentual: {percentual:F2}%");
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
}

