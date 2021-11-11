namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'50d55eff-5590-4ec1-b104-49ec2e94c372', N'admin@vidly.com', 0, N'APQjLmQxYStKUD4bPAQThqBo0mRBfaN4ETy1dBoBb7koJGdgOZRoWw1j5qz6jMfeog==', N'cfde9482-c7d6-467c-980f-5c22a1b55342', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                  INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'84d6513a-6203-48f2-860e-b8cec047d709', N'guest@vidly.com', 0, N'AJK0N707pxpfdBWOO00byKh1BoeFcvI+ELI9hpXc8BL3Ry2sjrol275rPf+fSaX7vg==', N'8187f61a-8150-4e21-8858-fdf84d9dfb9a', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'eba82001-3beb-452c-8f9e-462b8ed3a3e1', N'CanManageMovies')
                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'50d55eff-5590-4ec1-b104-49ec2e94c372', N'eba82001-3beb-452c-8f9e-462b8ed3a3e1')
");
        }
        
        public override void Down()
        {
        }
    }
}
