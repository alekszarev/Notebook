using Microsoft.EntityFrameworkCore;
using Notebook.Data.Models;
using Notebook.Data.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Data
{
    public interface IDatabase
    {
        DbSet<Appointment> Appointments { get; set; }
        DbSet<Day> Days { get; set; }
        DbSet<Hour> Hours { get; set; }
        DbSet<Month> Months { get; set; }
        DbSet<Note> Notes { get; set; }
        DbSet<Year> Years { get; set; }
        DbSet<NotebookCalendar> Calendar { get; set; }
        DbSet<Procedure> Procedures { get; set; }

        DbSet<UserBuffer> UserBuffers { get; set; }
        int SaveChanges();
    }
}
