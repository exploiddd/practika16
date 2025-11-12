using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choo
{
    //задание 1
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    //задание 2
    public class Counter
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Counter(string name, int value)
        {
            Name = name;
            Value = value;
        }
        public static Counter operator +(Counter counter, int increment)
        {
            return new Counter(counter.Name, counter.Value + increment);
        }
        public object this[string key]
        {
            get
            {
                if (key.ToLower() == "value")
                    return Value;
                throw new ArgumentException("Неизвестный ключ: " + key);
            }
            set
            {
                if (key.ToLower() == "value" && value is int intValue)
                    Value = intValue;
                else
                    throw new ArgumentException("Неизвестный ключ или неверный тип значения");
            }
        }
        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }

    //задание 3
    public class Temperature
    {
        public double Celsius { get; set; }

        public Temperature(double celsius)
        {
            Celsius = celsius;
        }
        public static bool operator >(Temperature t1, Temperature t2)
        {
            return t1.Celsius > t2.Celsius;
        }
        public static bool operator <(Temperature t1, Temperature t2)
        {
            return t1.Celsius < t2.Celsius;
        }
        public static bool operator ==(Temperature t1, Temperature t2)
        {
            if (ReferenceEquals(t1, t2)) return true;
            if (t1 is null || t2 is null) return false;
            return t1.Celsius == t2.Celsius;
        }
        public static bool operator !=(Temperature t1, Temperature t2)
        {
            return !(t1 == t2);
        }
        public override bool Equals(object obj)
        {
            if (obj is Temperature other)
                return this.Celsius == other.Celsius;
            return false;
        }
        public override int GetHashCode()
        {
            return Celsius.GetHashCode();
        }
        public override string ToString()
        {
            return $"{Celsius}°C";
        }
    }

    //задние 4
    public class Book
    {
        private string[] chapters;

        public Book(int maxChapters = 10)
        {
            chapters = new string[maxChapters];
        }
        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= chapters.Length)
                    throw new IndexOutOfRangeException("Неверный индекс главы");
                return chapters[index] ?? "Глава не задана";
            }
            set
            {
                if (index < 0 || index >= chapters.Length)
                    throw new IndexOutOfRangeException("Неверный индекс главы");
                chapters[index] = value;
            }
        }
        public override string ToString()
        {
            var result = new System.Text.StringBuilder();
            for (int i = 0; i < chapters.Length; i++)
            {
                if (!string.IsNullOrEmpty(chapters[i]))
                {
                    result.AppendLine($"{i + 1}. {chapters[i]}");
                }
            }
            return result.ToString().TrimEnd();
        }
    }

    //задание5
    public class Vector
    {
        private double[] components;
        public Vector(params double[] components)
        {
            this.components = components;
        }
        public static double operator *(Vector v1, Vector v2)
        {
            if (v1.components.Length != v2.components.Length)
                throw new ArgumentException("Векторы должны иметь одинаковую размерность");
            double result = 0;
            for (int i = 0; i < v1.components.Length; i++)
            {
                result += v1.components[i] * v2.components[i];
            }
            return result;
        }
        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= components.Length)
                    throw new IndexOutOfRangeException($"Неверный индекс. Допустимый диапазон: 0-{components.Length - 1}");
                return components[index];
            }
            set
            {
                if (index < 0 || index >= components.Length)
                    throw new IndexOutOfRangeException($"Неверный индекс. Допустимый диапазон: 0-{components.Length - 1}");
                components[index] = value;
            }
        }
        public override string ToString()
        {
            return $"[{string.Join(",", components)}]";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //вывод 1 
            var p1 = new Point(1, 2);
            var p2 = new Point(3, 4);
            Console.WriteLine(p1 + p2);
            Console.WriteLine(p2 - p1);
            Console.WriteLine();

            //вывод 2
            var c = new Counter("Счётчик", 10);
            c = c + 5;
            Console.WriteLine(c);
            c["value"] = 20;
            Console.WriteLine(c["value"]);
            Console.WriteLine();

            //вывод 3
            var t1 = new Temperature(25);
            var t2 = new Temperature(30);
            Console.WriteLine(t1);
            Console.WriteLine(t2 > t1);
            Console.WriteLine(t1 == new Temperature(25));
            Console.WriteLine();

            //вывод 4
            var book = new Book();
            book[0] = "Введение";
            book[1] = "Глава 1";
            Console.WriteLine(book[0]);
            Console.WriteLine(book);
            Console.WriteLine();

            //вывод 5
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(4, 5, 6);
            Console.WriteLine(v1 * v2); 
            v1[1] = 10;
            Console.WriteLine(v1);     

        }
    }
}