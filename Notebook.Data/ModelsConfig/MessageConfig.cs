using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Data.ModelsConfig
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> message)
        {
            message.HasKey(x => x.MessageId);
            
            message.HasOne(x=>x.Conversation)
                .WithMany(x=>x.Messages)
                .HasForeignKey(x=>x.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}