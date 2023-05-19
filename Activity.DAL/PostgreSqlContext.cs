using System;
using Activity.DAL.Entity;
using Activity.DAL.Entity.GameGuild;
using Microsoft.EntityFrameworkCore;
using MyGo.Core;

namespace Activity.DAL
{
    public class PostgreSqlContext : DbContext
    {
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connnectStr = ConfigManager.getIntanse().GetConnStr("DefaultConnection");
            optionsBuilder.UseNpgsql(connnectStr);
        }

        public DbSet<UserInformation> UserInformation { get; set; }
       
        public DbSet<Entity.GameGuild.Games> Games { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<GameToken> GameTokens { get; set; }
        public DbSet<GameAccount> gameAccounts { get; set; }
        public DbSet<GameAccountToken> GameAccountTokens { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<UserGameAccount>  UserGameAccounts { get; set; }
        public DbSet<Activity.DAL.Entity.GameGuild.UserGame>  UserGames { get; set; }
        public DbSet<Activity.DAL.Entity.GameGuild.TokenReport>  tokenReports { get; set; }
        public DbSet<Activity.DAL.Entity.GameGuild.PropertyReport>  propertyReports { get; set; }
        public DbSet<Guild>  Guilds { get; set; }
        public DbSet<UserGuild>  UserGuilds { get; set; }
        public DbSet<DetailReportStepN>  detailReportStepNs { get; set; }


        public DbSet<News> news { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");//chọn vào dâtbase dbo
            builder.HasPostgresExtension("uuid-ossp");
            base.OnModelCreating(builder);
        }

        //public override int SaveChangesAsync()
        //{
        //    //ChangeTracker.DetectChanges();
        //    ChangeTracker.Clear();
        //    return base.SaveChaSaveChangesAsyncnges();
        //}
    }
}
