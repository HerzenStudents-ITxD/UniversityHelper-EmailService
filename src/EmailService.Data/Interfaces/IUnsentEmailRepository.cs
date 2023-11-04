using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HerzenHelper.EmailService.Models.Db;
using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Requests;

namespace HerzenHelper.EmailService.Data.Interfaces
{
  [AutoInject]
  public interface IUnsentEmailRepository
  {
    Task CreateAsync(DbUnsentEmail email);

    Task<DbUnsentEmail> GetAsync(Guid id);

    Task<List<DbUnsentEmail>> GetAllAsync(int totalSendingCountIsLessThen);

    Task<(List<DbUnsentEmail> unsentEmailes, int totalCount)> FindAsync(BaseFindFilter filter);

    Task<bool> RemoveAsync(DbUnsentEmail email);

    Task IncrementTotalCountAsync(DbUnsentEmail email);
  }
}
