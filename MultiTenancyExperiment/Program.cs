using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Autofac;
using MultiTenancyExperiment.Autofac;
using MultiTenancyExperiment.Dal.Entities;
using MultiTenancyExperiment.Dal.Interfaces;

namespace MultiTenancyExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofacConfiguration.RegisterContainer();
            var context = AutofacConfiguration.Container.Resolve<IDatabaseContext>();

            //var noteEntity = new Note
            //{
            //    Author = "Doru",
            //    Id = Guid.NewGuid(),
            //    Message = "Tenant message",
            //    Timestamp = DateTime.UtcNow,
            //    Adendums = new List<Adendum>()
            //};

            //noteEntity.Adendums.Add(new Adendum
            //{
            //    Note = noteEntity,
            //    Id = Guid.NewGuid() ,
            //    AdendumContent = "some adendum content",
            //    NoteId = noteEntity.Id,
            //});

            ////noteEntity.Adendums.Add(new Adendum
            ////{
            ////    Note = noteEntity,
            ////    Id = Guid.NewGuid() ,
            ////    AdendumContent = "yet another adendum content",
            ////    NoteId = noteEntity.Id,
            ////});

            //context.Notes.Add(noteEntity);

            //context.SaveChanges();


            var notes = context.Notes.Include(x => x.Adendums).ToList();

            foreach (var note in notes)
            {
                Console.WriteLine("Note : {0} with tenant {1} and message {2} with {3} adendums", note.Id, note.Tenant, note.Message, note.Adendums.Count);
            }

            Console.ReadKey();
        }
    }
}
