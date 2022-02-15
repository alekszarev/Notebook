using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Data.Models.Calendar;

namespace Notebook.Data.ModelsConfig
{
    public class AppointmentConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {

            builder.HasKey(x => x.HourId);

            builder.HasOne(x => x.Hour)
                .WithOne(x => x.Appointment)
                .HasForeignKey<Appointment>(x => x.HourId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.UserBuffer)
               .WithMany(x => x.Appointments)
               .HasForeignKey(x => x.UserBufferId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Procedure)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.ProcedureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
