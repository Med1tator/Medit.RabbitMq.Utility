using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medit.RabbitMq.Utility.Enums
{
    public enum MsgConsumeResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Accept = 1,
        /// <summary>
        /// 重试
        /// </summary>
        Retry = 2,
        /// <summary>
        /// 抛弃
        /// </summary>
        Reject = 3
    }
}
