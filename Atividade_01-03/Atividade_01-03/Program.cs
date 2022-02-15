int[] primeiroarray = { 1, 3, 7, 29, 42, 98, 234, 93 };
int[] segundoarray = { 4, 6, 93, 7, 55, 32, 3 };

var resultado = primeiroarray.Except(segundoarray);

foreach (var num in resultado)
{
    Console.WriteLine(num);
}