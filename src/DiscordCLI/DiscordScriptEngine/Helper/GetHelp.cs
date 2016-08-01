using DScriptEngine.Enums;
using static DScriptEngine.Commands;

namespace DScriptEngine {
    internal class GetHelp {
        public static DResult ShowHelp() {
            var result = new DResult();
            var table = new TableWriter("Command", "Description");
            table.AddRow("Show-Help", "Shows this help page");
            table.AddRow("Clear", "Clears the input of the console");
            table.AddRow("Use-Server", "Selects the server to manage");
            table.AddRow("Show-Server", "Lists info about a spesified server or all joined servers");
            table.AddRow("Create-Server", "Create a new server");
            table.AddRow("Remove-Server", "Deletes a server");
            table.AddRow("Edit-Server", "Edit a existing server");
            table.AddRow("Save-Server", "Save a server to a deployment script");
            result.Message = table.WriteToString();
            result.Result = Result.Ok;
            return result;
        }
        public static DResult ShowHelpForCommand(Command cmd) {
            var result = new DResult();
            var table = new TableWriter("Parameter", "Description", "Required", "Type");
            string info = string.Empty;
            switch (cmd) {
                case Command.UseServer:
                    table.AddRow("Name", "Name of the server", "If Id is not set", "Text");
                    table.AddRow("Id", "Id of the server", "If Name is not set", "Number");
                    info = table.WriteToString();
                    break;
            }
            result.Result = Result.Ok;
            result.Message = info;
            return result;
        }

    }
}
