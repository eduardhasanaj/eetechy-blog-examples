using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureRestApi.Services;
using AzureRestApi.Models;
using AzureRestApi.Utils;
using Newtonsoft.Json.Converters;

namespace AzureRestApi
{
  public class TaskFunctions
  {
    public const string ParentPath = "tasks";

    private readonly ITaskService _taskService;

    private IsoDateTimeConverter dateTimeConverter = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddTH:mm:ss.fffZ" };

    public TaskFunctions(ITaskService taskService)
    {
      _taskService = taskService;
    }

    [FunctionName("CreateTask")]
    public async Task<IActionResult> CreateTask(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = ParentPath)] HttpRequest req,
        ILogger log)
    {
      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var task = JsonConvert.DeserializeObject<TaskModel>(requestBody);
      try
      {
        var result = await _taskService.CreateAsync(task);

        return new CreatedResult(result.Id.ToString(), result);
      }
      catch(Exception err)
      {
        log.LogError(err.ToString());
        return ResponseUtility.CreateErrorResult(err);
      }
    }

    [FunctionName("GetTasks")]
    public async Task<IActionResult> GetTasks(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = ParentPath)] HttpRequest req,
        ILogger log)
    {
      try
      {
        var result = await _taskService.GetTasksAsync();

        return new OkObjectResult(result);
      }
      catch (Exception err)
      {
        log.LogError(err.ToString());
        return ResponseUtility.CreateErrorResult(err);
      }
    }

    [FunctionName("GetTask")]
    public async Task<IActionResult> GetTask(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = ParentPath + "/{id}")] HttpRequest req,
        ILogger log, string id)
    {
      try
      {
        int idInt;
        if (!int.TryParse(id, out idInt)) 
        {
          return ResponseUtility.CreateErrorResult(new ServiceException(400, "id must be an integer"));
        }

        var result = await _taskService.GetByIdAsync(idInt);

        return new CreatedResult(result.Id.ToString(), result);
      }
      catch (Exception err)
      {
        log.LogError(err.ToString());
        return ResponseUtility.CreateErrorResult(err);
      }
    }

    [FunctionName("UpdateTask")]
    public async Task<IActionResult> UpdateTask(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = ParentPath + "/{id}")] HttpRequest req,
        ILogger log, string id)
    {
      try
      {
        int idInt;
        if (!int.TryParse(id, out idInt))
        {
          return ResponseUtility.CreateErrorResult(new ServiceException(400, "id must be an integer"));
        }

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var task = JsonConvert.DeserializeObject<TaskModel>(requestBody);
        task.Id = idInt;

        await _taskService.UpdateAsync(task);

        return new OkObjectResult(task);
      }
      catch (Exception err)
      {
        log.LogError(err.ToString());
        return ResponseUtility.CreateErrorResult(err);
      }
    }

    [FunctionName("DeleteTask")]
    public async Task<IActionResult> DeleteTask(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = ParentPath + "/{id}")] HttpRequest req,
        ILogger log, string id)
    {
      try
      {
        int idInt;
        if (!int.TryParse(id, out idInt))
        {
          return ResponseUtility.CreateErrorResult(new ServiceException(400, "id must be an integer"));
        }

        var result = await _taskService.DeleteAsync(idInt);
        return new OkObjectResult(result);
      }
      catch (Exception err)
      {
        log.LogError(err.ToString());
        return ResponseUtility.CreateErrorResult(err);
      }
    }
  }
}
