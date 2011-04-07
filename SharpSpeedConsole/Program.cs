using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSpeed;


namespace SharpSpeedConsole
{
    class Program
    {
        private static SharpSpeedRepository repository;

        static void Main(string[] args)
        {
            repository = SharpSpeedRepository.Instance;

         //   TestProfile();

          //  TestRoutes();

      //      TestEntries();

            TestEntryPaging();

            TestEntrySince();

            Console.WriteLine("Press enter to continue");

            Console.ReadLine();
        }

        private static void TestEntrySince()
        {
            Console.WriteLine("ENTRIES DATES");
            var entries = repository.GetEntries("ben", since: 1293861600, until: 1296540000);

            Console.WriteLine(String.Format("Jan 2011 first {0}: {1}", entries.First().At, entries.First().Message));

            entries = repository.GetEntries("ben", since: 1262325600, until: 1265004000);

            Console.WriteLine(String.Format("JAn 2010 first {0}: {1}", entries.First().At, entries.First().Message));

        }

        private static void TestEntryPaging()
        {
            Console.WriteLine("ENTRIES PAGING");
            var entries = repository.GetEntries("ben", page:1);

            Console.WriteLine(String.Format("Page 1 first {0}", entries.First().Message));

            entries = repository.GetEntries("ben", page: 2);

            Console.WriteLine(String.Format("Page 2 first {0}", entries.First().Message));

        }

        private static void TestEntries()
        {
            Console.WriteLine("ENTRIES");
            var entries = repository.GetEntries("spaetzel");

            foreach (var curEntry in entries.Take(10))
            {
                Console.WriteLine(curEntry.Message);
            }

            TestComments(entries);

        }

        private static void TestComments(IEnumerable<Entry> entries)
        {
            Console.WriteLine("ENTRY COMMENTS");

            var entry = (from e in entries
                         where e.Comments.Count() > 0
                         select e).FirstOrDefault();

            foreach (var curComment in entry.Comments)
            {
                Console.WriteLine(String.Format("{0} {1}: {2}", curComment.User.Username, curComment.CreatedAt, curComment.Body));
            }
        }

        private static void TestProfile()
        {

            var person = repository.GetPerson("klake");

            Console.WriteLine(person.Goal);

            Console.WriteLine(String.Format("{0} {1} {2}", person.Username, person.DisplayName, person.Location));
        }

        private static void TestRoutes()
        {
            Console.WriteLine("ROUTES");
            var routes = repository.GetRoutes("klake");

            foreach (var curRoute in routes.Take(10))
            {
                Console.WriteLine(curRoute.Name);
            }

            TestPoints(routes);
        }

        private static void TestPoints(IEnumerable<Route> routes)
        {
            Console.WriteLine("POINTS");

            var route = routes.FirstOrDefault();

            var points = repository.GetRoute(route.Id);

            foreach (var curPoint in points)
            {
                Console.WriteLine("{0} {1}", curPoint[0], curPoint[1]);
            }
        }
    }
}
