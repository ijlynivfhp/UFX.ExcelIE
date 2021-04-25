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
        private readonly IExcelIEService excelService;
        public HomeController(IExcelIEService _excelService)
        {
            excelService = _excelService;
        }
        /// <summary>
        /// 发送消息(导出excel)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> PushExcelExportMsg(ExcelIEDto dto)
        {
            dto.LocalUrl = this.Request.GetLocalExportUrl();
            await excelService.PushExcelExportMsg(dto);
            return string.Empty;
        }
    }
}
