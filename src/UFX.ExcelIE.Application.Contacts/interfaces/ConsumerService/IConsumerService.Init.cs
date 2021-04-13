using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.Infra.Interfaces;

namespace UFX.ExcelIE.Application.Contracts.interfaces
{
    public partial interface IConsumerService : ICapSubscribe, IMyScoped
    {
    
    }
}
