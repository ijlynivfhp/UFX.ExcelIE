using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.EntityFrameworkCore.UnitOfWork;
using UFX.Infra.Interfaces;

namespace UFX.ExcelIE.Domain.Interfaces.ExcelIE
{
    public partial interface IExcelIEDomainService : IMyScoped
    {
        Guid NewGuid();
    }
}
