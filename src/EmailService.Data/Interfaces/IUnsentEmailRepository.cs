using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.EmailService.Models.Db;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Requests;

namespace UniversityHelper.EmailService.Data.Interfaces;

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
