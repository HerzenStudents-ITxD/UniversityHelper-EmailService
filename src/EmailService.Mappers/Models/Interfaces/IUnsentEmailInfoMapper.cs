using UniversityHelper.EmailService.Models.Db;
using UniversityHelper.EmailService.Models.Dto.Models;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.EmailService.Mappers.Models.Interfaces;

[AutoInject]
public interface IUnsentEmailInfoMapper
{
  UnsentEmailInfo Map(DbUnsentEmail dbUnsentEmail);
}
