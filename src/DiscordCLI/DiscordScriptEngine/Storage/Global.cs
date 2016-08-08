using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DScriptEngine {
    public class Global {
        public static DiscordClient client { get; set; } = new DiscordClient();
        public static Server activeServer { get; set; }
        public static Logger logger { get; set; }
    }
}
