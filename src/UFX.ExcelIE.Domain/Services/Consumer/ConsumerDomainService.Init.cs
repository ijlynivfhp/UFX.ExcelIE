using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Domain.Services
{
    public partial class ConsumerService
    {

        private readonly ILogger<ConsumerService> _logger;
        public ConsumerService( ILogger<ConsumerService> logger)
        {
            _logger = logger;
        }
    }
}
