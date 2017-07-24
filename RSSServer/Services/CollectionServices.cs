using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSSServer.Models;
using Microsoft.EntityFrameworkCore;

namespace RSSServer.Services
{
    public class CollectionServices
    {
        private DataContext context;

        public CollectionServices()
        {
            context = new DataContext();
        }

        public void CreateCollection(Collection collection)
        {
            //context.ChangeTracker.TrackGraph(collection, e => e.Entry.State = EntityState.Added);
            //context.Collections.Add(collection);
            context.AttachRange(collection.Owner);
            context.AddRange(collection);
            context.SaveChanges();
        }

        public void DeleteCollection(Collection collection)
        {
            //context.ChangeTracker.TrackGraph(collection, e => e.Entry.State = EntityState.Added);
            //context.Collections.Remove(collection);
            context.AttachRange(collection.Owner);
            context.RemoveRange(collection);
            context.SaveChanges();
        }

        public void UpdateCollection(Collection collection)
        {
            //context.ChangeTracker.TrackGraph(collection, e => e.Entry.State = EntityState.Added);
            //context.Collections.Add(collection);
            context.AttachRange(collection.Owner);
            context.UpdateRange(collection);
            context.SaveChanges();
        }

        public List<Collection> FindCollections(User currentUser)
        {
            return context.Collections.Include(t => t.Owner).Where(x => x.Owner.Login == currentUser.Login).ToList();
        }

        public Collection FindCollection(int Id)
        {
            return context.Collections.FirstOrDefault(x => x.Id == Id);
        }

    }
}
