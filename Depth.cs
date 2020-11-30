using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

List<string> GenericArr = new List<string>();

File.Delete("res.txt");

Stopwatch watch = Stopwatch.StartNew();
watch.Start();

Console.Write("Введите путь:");

string readWithConsole = Console.ReadLine();
IEnumerable<string> currnetDirArrFile = Directory.GetDirectories(readWithConsole);
string[] arrF = Directory.GetFiles(readWithConsole);

GenericArr.AddRange(arrF);

Console.WriteLine("\n\nВыполнение алгоритма");

Parallel.Invoke(() => GetDir(currnetDirArrFile));

Console.WriteLine("Алгоритм завершил работу\n\n");

GenericArr.Sort();

Console.WriteLine("Запись в файл ...");

await File.WriteAllLinesAsync("res.txt", GenericArr);

Console.WriteLine("Запись завершена.\n\n");

watch.Stop();

Console.WriteLine($"Выполнение программы заняло: {watch.ElapsedMilliseconds / 1000} сек.");
Console.ReadKey();

IEnumerable<string> GetDir(IEnumerable<string> dir)
{
    List<string> arrResult = new List<string>();

    foreach (string item in dir)
    {
        try
        {
            GenericArr.AddRange(Directory.GetFiles(item));
        }
        catch { Console.WriteLine("Не прочитаны файлы"); }
        try
        {
            arrResult.AddRange(GetDir(Directory.GetDirectories(item).ToList()));
        }
        catch { Console.WriteLine("Не удачная попытка спуститься вниз"); }
    }
    return arrResult;
}