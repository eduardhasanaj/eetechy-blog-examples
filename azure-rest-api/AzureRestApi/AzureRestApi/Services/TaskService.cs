using AzureRestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureRestApi.Services
{
  public class TaskService : ITaskService
  {
    private readonly RepositoryContext _repoContext;

    /// <summary>
    /// Initializes Task Service.
    /// </summary>
    /// <param name="repositoryContext"></param>
    public TaskService(RepositoryContext repositoryContext)
    {
      _repoContext = repositoryContext;
    }

    /// <summary>
    /// Create a task async
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public async Task<TaskModel> CreateAsync(TaskModel task)
    {
      var result = await _repoContext.AddAsync(task);
      await _repoContext.SaveChangesAsync();

      return result.Entity;
    }

    /// <summary>
    /// Update a task async.
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public async Task UpdateAsync(TaskModel task)
    {
      var entity = await GetByIdAsync(task.Id);

      entity.SyncWith(task);

      await _repoContext.SaveChangesAsync();
    }

    /// <summary>
    /// Delete a task async.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TaskModel> DeleteAsync(int id)
    {
      var entity = await GetByIdAsync(id);

      _repoContext.Remove(entity);

      await _repoContext.SaveChangesAsync();

      return entity;
    }

    /// <summary>
    /// Get a task by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TaskModel> GetByIdAsync(int id)
    {
      var entity = _repoContext.Tasks.FirstOrDefault(item => item.Id == id);
      if (entity == null)
      {
        throw new ServiceException(404, "Resource not found");
      }

      return entity;
    }

    /// <summary>
    /// Get a collection with tasks.
    /// </summary>
    /// <returns></returns>
    public async Task<List<TaskModel>> GetTasksAsync()
    {
      var list = await _repoContext.Tasks.ToListAsync();
      return list;
    }
  }
}
