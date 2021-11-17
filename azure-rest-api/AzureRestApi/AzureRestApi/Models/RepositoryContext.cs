using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureRestApi.Models
{
  public class RepositoryContext : DbContext
  {
    public DbSet<TaskModel> Tasks { get; set; }

    public RepositoryContext(DbContextOptions<RepositoryContext> options)
      : base(options)
    {
      NpgsqlConnection.GlobalTypeMapper.MapEnum<Models.TaskStatus>();
    }
  }
}
