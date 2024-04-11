using DataLayer.Entities.Course;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Mapping
{
    public class UserCommentMapping: IEntityTypeConfiguration<CourseComment>
    {
        public void Configure(EntityTypeBuilder<CourseComment> builder)
        {
            builder.HasOne(c => c.User)
                .WithMany() // Assuming a one-to-many relationship
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
