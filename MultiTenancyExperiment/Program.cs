using System;
using System.Linq;
using Autofac;
using MultiTenancyExperiment.Autofac;
using MultiTenancyExperiment.Dal.Interfaces;

namespace MultiTenancyExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofacConfiguration.RegisterContainer();
            var context = AutofacConfiguration.Container.Resolve<IDatabaseContext>();

            //context.Notes.Add(new Note
            //{
            //    Author = "Doru",
            //    Id = Guid.NewGuid(),
            //    Message = "Tenant message",
            //    Timestamp = DateTime.UtcNow
            //});

            //context.SaveChanges();

            
            var notes = context.Notes.ToList();

            foreach (var note in notes)
            {
                Console.WriteLine("Note : {0} with tenant {1} and message {2}", note.Id, note.Tenant, note.Message);
            }

            Console.ReadKey();
        }
    }
}
