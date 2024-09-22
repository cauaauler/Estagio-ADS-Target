using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Digite um número:");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int valor))
        {
            Console.WriteLine("Por favor, digite um número válido.");
            return;
        }

        int a = 0, b = 1;

        // Verifica se o número é 0 ou 1, já que 0 e 1 são números de fibonacci
        if (valor == 0 || valor == 1)
        {
            Console.WriteLine($"O número {valor} pertence à sequência de Fibonacci.");
            return;
        }

        while (b < valor)
        {
            int next = a + b;
            a = b;
            b = next;
        }

        // Gera a sequência até que b seja maior ou igual a valor
        if (b != valor)
        {
            Console.WriteLine($"O número {valor} não pertence à sequência de Fibonacci.");
            return;
        }

        Console.WriteLine($"O número {valor} pertence à sequência de Fibonacci.");
    }
}
