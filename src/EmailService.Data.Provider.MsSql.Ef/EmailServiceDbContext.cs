using System.Reflection;
using UniversityHelper.EmailService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UniversityHelper.Core.EFSupport.Provider;

namespace UniversityHelper.EmailService.Data.Provider.MsSql.Ef;

/// <summary>
/// A class that defines the tables and its properties in the database of MessageService.
/// </summary>
public class EmailServiceDbContext : DbContext, IDataProvider
{
  public EmailServiceDbContext(DbContextOptions<EmailServiceDbContext> options)
    : base(options)
  {
  }

  public DbSet<DbEmail> Emails { get; set; }
  public DbSet<DbUnsentEmail> UnsentEmails { get; set; }
  public DbSet<DbModuleSetting> ModuleSettings { get; set; }

  void IBaseDataProvider.Save()
  {
    SaveChanges();
  }

  async Task IBaseDataProvider.SaveAsync()
  {
    await SaveChangesAsync();
  }

  public void EnsureDeleted()
  {
    Database.EnsureDeleted();
  }

  public object MakeEntityDetached(object obj)
  {
    Entry(obj).State = EntityState.Detached;
    return Entry(obj).State;
  }

  public bool IsInMemory()
  {
    return Database.IsInMemory();
  }

  // Fluent API is written here.
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("UniversityHelper.EmailService.Models.Db"));
  }
}
