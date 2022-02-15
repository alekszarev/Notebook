using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notebook.Data.Common;
using Notebook.Data.Models;
using Notebook.Data.Models.Calendar;
using System.Reflection;

namespace Notebook.Data
{
    public class NotebookContext : IdentityDbContext<User, Role, string>, IDatabase
    {
        public NotebookContext()
        {

        }
        public NotebookContext(DbContextOptions<NotebookContext> options)
            : base(options)
        {

        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<NotebookCalendar> Calendar { get; set; }
        public DbSet<Procedure> Procedures { get; set; }

        public DbSet<UserBuffer> UserBuffers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Seed();

        }

        public override int SaveChanges()
        {
         
            return base.SaveChanges();
        }

        //private void ApplyNotebookElementInfo()
        //{
        //    var changedEntries = this.ChangeTracker
        //        .Entries()
        //        .Where(e =>
        //            e.Entity is INotebookInformation &&
        //            (e.State == EntityState.Added || e.State == EntityState.Modified));

        //    foreach (var entry in changedEntries)
        //    {
        //        var entity = (INotebookInformation)entry.Entity;
        //        if (entry.State == EntityState.Added && entity.CreatedOn == default)
        //        {
        //            entity.CreatedOn = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        //        }
        //        else
        //        {
        //            entity.ModifiedOn = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        //        }
        //    }
        //}
        //private void UpdateSoftDeleteStatuses()
        //{
        //    foreach (var entry in ChangeTracker.Entries())
        //    {
               
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.CurrentValues["IsDelete"] = false;
        //                break;
        //            case EntityState.Deleted:
        //                entry.State = EntityState.Modified;
        //                entry.CurrentValues["IsDelete"] = true;
        //                entry.CurrentValues["DeletedOn"] = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        //                break;
        //        }
        //    }
        //}
    }
}
