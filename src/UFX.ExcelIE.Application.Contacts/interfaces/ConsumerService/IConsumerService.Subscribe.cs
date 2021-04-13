using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Application.Contracts.interfaces
{
    public partial interface IConsumerService
    {
        Task Consumer(string t);
    }
}
