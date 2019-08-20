using App.Basic.Domain.AggregateModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Basic.Infrastructure.EntityConfigurations
{
    class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Ignore(b => b.Name);
            builder.Property(x => x.FirstName);
            builder.Property(x => x.LastName);
            builder.Property(x => x.Description);
            builder.Property(x => x.Password);
            builder.Property(x => x.Mail);
            builder.Property(x => x.Phone);
            builder.Property(x => x.Active);
            builder.Property(x => x.Creator);
            builder.Property(x => x.Modifier);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.ModifiedTime);
            builder.Property(x => x.LanguageTypeId).HasDefaultValue(0);
            builder.Property(x => x.LegalPerson).HasDefaultValue(0);
            builder.HasIndex(x => x.FirstName);
            builder.HasIndex(x => x.Mail);
            builder.HasIndex(x => x.LegalPerson);
            builder.HasIndex(x => x.SystemRoleId);
            builder.Metadata.FindNavigation(nameof(Account.Organization)).SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Metadata.FindNavigation(nameof(Account.OwnRoles)).SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany(x => x.OwnRoles).WithOne(s => s.Account).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
