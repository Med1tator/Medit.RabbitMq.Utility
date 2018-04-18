using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medit.RabbitMq.Utility.Configurations
{
    public class RabbitMqParameters
    {
        static RabbitMqParameters()
        {
            Dictionary<string, string> sections = ConfigurationManager.GetSection("RabbitMqParameters") as Dictionary<string, string>;
            Host = sections["Host"] ?? "";
            UserName = sections["UserName"] ?? "";
            Password = sections["Password"] ?? "";
            QueueName = sections["QueueName"] ?? "";
            IsDurable = (sections["QueueName"] ?? "true") == "true";
            VirtualHost = sections["VirtualHost"] ?? "";
            MaxRetry = int.Parse(sections["MaxRetry"] ?? "5");
        }

        public static string Host { get; set; }

        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string VirtualHost { get; set; }

        public static string QueueName { get; set; }

        public static bool IsDurable { get; set; }

        public static int MaxRetry { get; set; }
    }
}
