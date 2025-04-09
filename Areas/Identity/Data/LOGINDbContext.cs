using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMDT.Areas.Identity.Data;

namespace TMDT.Data;

public class LOGINDbContext : IdentityDbContext<LOGINUser>
{
    public LOGINDbContext(DbContextOptions<LOGINDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
		// Customize the ASP.NET Identity model and override the defaults if needed.
		// For example, you can rename the ASP.NET Identity table names and more.
		// Add your customizations after calling base.OnModelCreating(builder);
		builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

		builder.Entity<IdentityRole>().HasData(
			new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
			new IdentityRole { Name = "User", NormalizedName = "USER" },
			new IdentityRole { Name = "Author", NormalizedName = "AUTHOR" }
		);
	}
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<LOGINUser>
{
	public void Configure(EntityTypeBuilder<LOGINUser> builder)
	{
		builder.Property(u => u.Firstname).HasMaxLength(255);
		builder.Property(u => u.Lastname).HasMaxLength(255);

	}
}