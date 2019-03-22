using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue<Point> p = new PriorityQueue<Point>();
            Point pp = new Point(3, 2, 3);
            p.Add(pp,3);
            p.Add(new Point(-5, 2, 3), -5);
            p.Add(new Point(8, 2, 3), 8);
            p.Add(new Point(5, 2, 3), 5);
            p.Add(new Point(4, 2, 3), 4);
            p.Add(new Point(10, 2, 3), 10);
            Console.WriteLine(p.ExtraxtMin().Item.Id);
            p.ChangePriority(pp, 9);
            Console.WriteLine(p.ExtraxtMin().Priority);
            Console.WriteLine(p.ExtraxtMin().Priority);
            Console.WriteLine(p.ExtraxtMin().Priority);
            Console.WriteLine(p.ExtraxtMin().Priority);
            Console.WriteLine(p.ExtraxtMin().Priority);
            //Console.WriteLine(p.ExtraxtMin().Item.Id);
            //Console.WriteLine(p.ExtraxtMin().Item.Id);
            Console.Read();
        }
    }
}
