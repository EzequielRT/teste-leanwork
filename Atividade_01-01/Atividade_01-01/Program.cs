public class Program
{
    public static void Main(string[] args)
    {
        CalculaMaiorSequencia(1000000);
    }

    public static void CalculaMaiorSequencia(uint num)
    {
        uint numComMaiorSequencia = 0;
        uint maiorQtdSequencias = 0;

        for (uint i = 1; i <= num; i++)
        {
            uint qtdSequenciasAtual = CalculaQtdDeSequencias(i);
            if (qtdSequenciasAtual > maiorQtdSequencias)
            {
                numComMaiorSequencia = i;
                maiorQtdSequencias = qtdSequenciasAtual;
            }
        }

        Console.WriteLine($"Número com a maior sequência => [{numComMaiorSequencia:n} - {maiorQtdSequencias} sequências percorridas]");
    }

    public static uint Collatz(uint num)
    {
        if (num % 2 == 0)
            return num / 2;

        return num * 3 + 1;
    }

    public static uint CalculaQtdDeSequencias(uint num)
    {
        ushort qtdSequencias = 0;

        for (ushort i = 1; num != 1; i++)
        {
            num = Collatz(num);
            qtdSequencias += 1;
        }

        return qtdSequencias;
    }
}
