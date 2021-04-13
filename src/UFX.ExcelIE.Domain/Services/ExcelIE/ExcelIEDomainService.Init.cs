using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.EntityFrameworkCore.UnitOfWork;
using UFX.ExcelIE.Domain.Interfaces.ExcelIE;
using UFX.Infra.Interfaces;

namespace UFX.ExcelIE.Domain.Services.ExcelIE
{
    public partial class ExcelIEDomainService : IExcelIEDomainService
    {
        private readonly IUnitOfWork<SCMContext> _scmUnit;
        private readonly ISequentialGuid _sequentialGuid;

        public ExcelIEDomainService(IUnitOfWork<SCMContext> scmUnit, ISequentialGuid sequentialGuid)
        {
            _scmUnit = scmUnit;
            _sequentialGuid = sequentialGuid;
        }

        public Guid NewGuid()
        {
            return _sequentialGuid.MsSqlGuid();
        }
    }
}
