int[] numeros = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };

Func<int, bool> numImpar = num => num % 2 != 0;

var resultado = numeros.Where(numImpar);

foreach (var num in resultado)
{
    Console.WriteLine(num);
}