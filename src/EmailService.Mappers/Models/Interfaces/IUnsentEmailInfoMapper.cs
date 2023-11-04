﻿using HerzenHelper.EmailService.Models.Db;
using HerzenHelper.EmailService.Models.Dto.Models;
using HerzenHelper.Core.Attributes;

namespace HerzenHelper.EmailService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IUnsentEmailInfoMapper
  {
    UnsentEmailInfo Map(DbUnsentEmail dbUnsentEmail);
  }
}
