using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
  public  class Program
    {
        static void Main(string[] args)
        {
            var moudelA = new Item("Module A");
            var moudelC = new Item("Module C",moudelA);
            var moudelB = new Item("Module B", moudelC);
            var moudelE = new Item("Module E", moudelB);
            var moudelD = new Item("Module D", moudelE);
            var unsorted = new[]{moudelE, moudelA, moudelD, moudelB, moudelC};
          
            var sorted = Sort(unsorted, x => x.Dependencies);

          
               
            foreach (var item in sorted)
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadLine();
        }
        public static IList<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        {
            var sorted = new List<T>(0);
            var visited = new Dictionary<T, bool>();
            foreach (var item in source)
            {
                Visit(item, getDependencies, sorted, visited);
            }
            return sorted;
        }
        public class A
        {
            public string k { get; set; }
        }
        public static void Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);
            //如果已经访问过该顶点，则直接返回
            if (alreadyVisited)
            {
                //如果处理的为当前节点，则说明存在循环引用
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found");
                }
            }
            else
            {
                //正在处理当前节点
                visited[item] = true;
                //获得所有依赖项
                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        //遍历访问
                        Visit(dependency, getDependencies, sorted, visited);
                    }
                }
                //处理完成配置为false
                visited[item] = false;
                sorted.Add(item);
            }
        }
    }
    public class Item
    {
        //条目名称
        public string Name { get; private set; }
        //依赖项
        public Item[] Dependencies { get; private set; }
        public Item(string name, params Item[] dependencies)
        {
            Name = name;
            Dependencies = dependencies;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
