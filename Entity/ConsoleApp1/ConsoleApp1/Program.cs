using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApp1
{
    public class Entity
    {
        public int Id;
        public int ParentId;
        public string Name;
      
    }

    class Program
    {
        public static IDictionary<int, List<Entity>> ToGroupByParent(List<Entity> entities)
           => entities
              // Группировка
              .GroupBy(e => e.ParentId)
              // Начинаем формировать справочник 
              //  g.Key - это значит что зв основу ключа берем поле группировки
              // g.Select(k => k) - это значит что каждый эдемент мы формируем в коллекцию(IEnumerable)
              .ToDictionary(g => g.Key, g => g.Select(k => k).ToList());
        static void Main(string[] args)
        {
            List<Entity> list = new List<Entity>
            {
                new Entity { Id = 1, ParentId = 0, Name = "Root entity" },
                new Entity { Id = 2, ParentId = 1, Name = "Child of 1 entity" },
                new Entity { Id = 3, ParentId = 1, Name = "Child of 1 entity" },
                new Entity { Id = 4, ParentId = 2, Name = "Child of 2 entity" },
                new Entity { Id = 5, ParentId = 4, Name = "Child of 4 entity" }
            };

            var result = ToGroupByParent(list);

            foreach (var item in result)
            {
                Console.WriteLine(item.Key);
                foreach (var entity in item.Value)
                {
                    Console.WriteLine(entity.Name);
                }               
                Console.WriteLine();
            }
        }
    }
}
    




