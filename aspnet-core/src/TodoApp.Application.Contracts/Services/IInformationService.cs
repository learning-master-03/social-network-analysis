using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace TodoApp
{
    public interface IInformationService : IApplicationService
    {
         Task<ServerInfoDto> GetServerInfoAsync();

    }
}
