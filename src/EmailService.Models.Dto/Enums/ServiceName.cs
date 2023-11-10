using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UniversityHelper.EmailService.Models.Dto.Enums
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum ServiceName
  {
    UserService,
    ProjectService
  }
}
