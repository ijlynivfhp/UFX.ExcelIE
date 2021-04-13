using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts.interfaces;
using UFX.ExcelIE.Application.Contracts.interfaces.ExcelIE;

namespace UFX.ExcelIE.Application.Services
{
    public partial class ConsumerService
    {
        private readonly IExcelIEService _excelIEService;

        private readonly ILogger<ConsumerService> _logger;
        public ConsumerService(IExcelIEService excelIEService, ILogger<ConsumerService> logger)
        {
            _excelIEService = excelIEService;
            _logger = logger;
        }
    }
}
