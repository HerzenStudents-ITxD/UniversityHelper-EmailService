using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HerzenHelper.EmailService.Models.Dto.Enums
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum StatusType
  {
    Sent = 0,
    Read = 1,
    Removed = 2
  }
}
