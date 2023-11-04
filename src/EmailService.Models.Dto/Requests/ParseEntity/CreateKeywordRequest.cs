using HerzenHelper.EmailService.Models.Dto.Enums;

namespace HerzenHelper.EmailService.Models.Dto.Requests.ParseEntity
{
  public record CreateKeywordRequest
  {
    public string Keyword { get; set; }
    public ServiceName ServiceName { get; set; }
    public string EntityName { get; set; }
    public string PropertyName { get; set; }
  }
}
