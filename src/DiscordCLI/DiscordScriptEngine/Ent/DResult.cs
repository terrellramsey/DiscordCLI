using DScriptEngine.Enums;
using static DScriptEngine.Commands;

namespace DScriptEngine {
    internal class DResult {
        public Command ExecutedCommand { get; internal set; }
        public Result Result { get; internal set; }
        public string Message { get; internal set; }
    }
}
