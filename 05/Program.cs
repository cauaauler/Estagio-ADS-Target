Console.WriteLine("Digite a palavra que deseja inverter:");
string input = Console.ReadLine();

char[] caracteres = input.ToCharArray();

char[] novo = new char[caracteres.Length];
for (int i = 0; i < caracteres.Length; i++)
{
    novo[caracteres.Length - 1 - i] = caracteres[i];
}
Console.WriteLine("Palavra invertida: " + new string(novo));