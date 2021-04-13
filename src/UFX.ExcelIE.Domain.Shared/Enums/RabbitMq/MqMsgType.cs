using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Domain.Shared.Enums.RabbitMq
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MqMsgType
    {
        /// <summary>
        /// 推送
        /// </summary>
        Push = 1,
        /// <summary>
        /// 拉取
        /// </summary>
        Pull = 2,
    }
}
