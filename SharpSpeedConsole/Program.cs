using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSpeed;


namespace SharpSpeedConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = SharpSpeedRepository.Instance;

            var person = repository.GetPerson("klake");

            Console.WriteLine(person.Goal);

            Console.WriteLine(String.Format("{0} {1} {2}", person.Username, person.DisplayName, person.Location));

            Console.WriteLine("ROUTES");
            var routes = repository.GetRoutes("klake");

            foreach (var curRoute in routes.Take(10))
            {
                Console.WriteLine(curRoute.Name);
            }

            Console.WriteLine("POINTS");

            var route = routes.FirstOrDefault();

            var points = repository.GetRoute(route.Id);

            foreach (var curPoint in points)
            {
                Console.WriteLine("{0} {1}", curPoint[0], curPoint[1]);
            }

            Console.WriteLine("ENTRIES");
            var entries = repository.GetEntries("spaetzel");

            foreach (var curEntry in entries.Take(10))
            {
                Console.WriteLine(curEntry.Message);
            }

            Console.WriteLine("ENTRY COMMENTS");

            var entry = (from e in entries
                        where e.Comments.Count() > 0
                        select e).FirstOrDefault();

            foreach (var curComment in entry.Comments)
            {
                Console.WriteLine(String.Format("{0} {1}: {2}", curComment.User.Username, curComment.CreatedAt, curComment.Body));
            }


            Console.ReadLine();
        }
    }
}
