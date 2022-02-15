using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Data.Models;

namespace Notebook.Data.ModelsConfig
{
    public class UserBufferConfigUserConfig : IEntityTypeConfiguration<UserBuffer>
    {
        public void Configure(EntityTypeBuilder<UserBuffer> appUser)
        {
            appUser.HasKey(x => x.UserBuferId);
        }
    }
}
