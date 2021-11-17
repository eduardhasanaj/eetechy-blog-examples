using AzureRestApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureRestApi.Services
{
  public interface ITaskService
  {
    Task<TaskModel> CreateAsync(TaskModel task);
    Task<TaskModel> GetByIdAsync(int id);

    Task<List<TaskModel>> GetTasksAsync();

    Task UpdateAsync(TaskModel task);

    Task<TaskModel> DeleteAsync(int id);
  }
}
