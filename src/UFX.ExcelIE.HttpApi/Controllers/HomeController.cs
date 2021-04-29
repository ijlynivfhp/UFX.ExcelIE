using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts;
using UFX.ExcelIE.Application.Contracts.Dtos;
using UFX.ExcelIE.Application.Contracts.Dtos.Export;
using UFX.ExcelIE.Application.Contracts.Helper;
using UFX.ExcelIE.Application.Contracts.interfaces;
using UFX.ExcelIE.Application.Contracts.interfaces.ExcelIE;
using UFX.Infra.Responses;

namespace UFX.ExcelIE.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IExcelIEService _excelService;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IExcelIEService excelService,ILogger<HomeController> logger)
        {
            _excelService = excelService;
            _logger = logger;
        }
        /// <summary>
        /// 发送消息(导出excel)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> PushExcelExportMsg(ExcelIEDto dto)
        {
            try
            {
                dto.LocalUrl = this.Request.GetLocalExportUrl();
                await _excelService.PushExcelExportMsg(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.Message;
            }
            return string.Empty;
        }
    }
}
