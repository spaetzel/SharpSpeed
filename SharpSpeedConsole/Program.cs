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

            var person = repository.GetPerson("spaetzel");

            Console.WriteLine(person.Goal);

        }
    }
}
