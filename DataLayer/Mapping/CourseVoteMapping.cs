using DataLayer.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Mapping
{
    public class CourseVoteMapping:IEntityTypeConfiguration<CourseVote>
    {
        public void Configure(EntityTypeBuilder<CourseVote> builder)
        {
            builder.HasOne(c => c.User)
                .WithMany() // Assuming a one-to-many relationship
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
