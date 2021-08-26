using System;

namespace App.Extensions
{
    public static class ObjectExtensions
    {
        private const ConsoleColor DefaultColor = ConsoleColor.DarkBlue;

        public static void Dump<T>(this T obj, string description, ConsoleColor color = DefaultColor)
        {
            var dump = ObjectDumper.Dump(obj);
            ConsoleColor.Gray.WriteLine($"{description} :");
            color.WriteLine(dump);
        }
    }
}
