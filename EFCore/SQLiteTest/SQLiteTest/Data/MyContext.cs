using Microsoft.EntityFrameworkCore;
using SQLiteTest.Models;

namespace SQLiteTest.Data;

public class MyContext : DbContext
{
	public MyContext(DbContextOptions<MyContext> options):
		base(options)
	{
	}

	public DbSet<TestModel> TestModel { get; set; }
	public DbSet<UserModel> UserModel { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
}
