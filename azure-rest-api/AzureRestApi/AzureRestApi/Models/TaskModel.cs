using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AzureRestApi.Models
{
  public enum TaskStatus
  {
    Backlog,
    Pending,
    Failed,
    Done
  }

  [Table("tasks")]
  public class TaskModel
  {
    [JsonProperty("id")]
    [Column("id")]
    public int Id { get; set; }

    [JsonProperty("title")]
    [Column("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    [Column("description")]
    public string Description { get; set; }

    [JsonProperty("status")]
    [JsonConverter(typeof(StringEnumConverter))]
    [Column("status")]
    public TaskStatus Status { get; set; }
    
    [Column("start_time")]
    [JsonProperty("start_time")]
    public DateTime StartTime { get; set; }

    [JsonProperty("end_time")]
    [Column("end_time")]
    public DateTime EndTime { get; set; }

    public void SyncWith(TaskModel other)
    {
      Title = other.Title;
      Description = other.Description;
      Status = other.Status;
      StartTime = other.StartTime;
      EndTime = other.EndTime;
    }
  }
}
