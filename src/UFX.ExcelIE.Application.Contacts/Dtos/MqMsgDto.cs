using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Domain.Shared.Enums;
using UFX.ExcelIE.Domain.Shared.Enums.RabbitMq;
using UFX.Infra.Attributes;

namespace UFX.ExcelIE.Application.Contracts.Dtos
{
    public class MqMsgDto
    {
        /// <summary>
        /// 消息类型，拉取或推送
        /// </summary>
        public MqMsgType Type { get; set; } = MqMsgType.Push;
        /// <summary>
        /// 租户id
        /// </summary>
        [Required]
        public Guid TntId { get; set; }
        /// <summary>
        /// 消息id,直接使用FxApiQueue的id
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// 导出模板编码
        /// </summary>
        [Required]
        public string TemplateCode { get; set; }
        /// <summary>
        /// 导出模板参数
        /// </summary>
        [Required]
        public string TemplateParams { get; set; }
    }
}
