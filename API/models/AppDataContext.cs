using Microsoft.EntityFrameworkCore;

namespace API.models;

public class AppDataContext : DbContext
{
    public DbSet<Tarefa> Tarefas {get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}
