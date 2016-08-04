using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DScriptEngine.Commands;

namespace DScriptEngine.Syntax {
    public class SyntaxCommand {
        public static List<string> AutoCompleteCommands = new List<string>()
         {
            //Helpers
            "Show-Help",
            "Clear",
            //Server Commands
            "Use-Server",
            "Show-Server",
            "Create-Server",
            "Remove-Server",
            "Edit-Server",
            "Save-Server",
            "Create-Invite",
            //Text channel commands
            "Show-Text",
            "Create-Text",
            "Remove-Text",
            "Edit-Text",
            "Save-Text",
            //Voice channel commands
            "Show-Voice",
            "Create-Voice",
            "Remove-Voice",
            "Edit-Voice",
            "Save-Voice",
            //User commands
            "Show-Users",
            "Ban-User",
            "Kick-User",
            "Save-Users",
            //Role commands
            "Show-Roles",
            "Create-Role",
            "Remove-Role",
            "Edit-Role",
            "Save-Role",
            //Scripting
            "Load-Script",
        };
        #region Server
        public static List<string> ServerParameters = new List<string>()
        {
            "Name",
            "Region",
            "Image",
        };
        #endregion
        #region Text
        public static List<string> TextChannelPermissions = new List<string>() {

        };
        #endregion
        #region Voice
        public static List<string> VoiceChannelPermissions = new List<string>() {

        };
        public static List<Tuple<string, Command[]>> Parameters = new List<Tuple<string, Command[]>>()
        {
            new Tuple<string, Command[]>("Name", new Command[]
            { Command.UseServer, Command.ShowServer, Command.CreateServer,
                Command.RemoveServer, Command.EditServer, Command.SaveServer }),
            new Tuple<string, Command[]>("Id", new Command[]
            { Command.UseServer, Command.ShowServer, Command.RemoveServer,
                Command.EditServer, Command.SaveServer }),
            new Tuple<string, Command[]>("Region", new Command[]
            { Command.CreateServer, Command.EditServer}),
            new Tuple<string, Command[]>("Image", new Command[]
            {Command.CreateServer, Command.EditServer}),
             new Tuple<string, Command[]>("NewName", new Command[]
            {Command.EditServer, Command.EditRole, Command.EditText, Command.EditVoice}),
              new Tuple<string, Command[]>("Output", new Command[]
            {Command.SaveRole, Command.SaveServer, Command.SaveText, Command.SaveUsers,
            Command.SaveVoice}),
             new Tuple<string, Command[]>("MaxAge", new Command[]
            { Command.CreateInvite }),
             new Tuple<string, Command[]>("MaxUsers", new Command[]
            { Command.CreateInvite }),
             new Tuple<string, Command[]>("TempMember", new Command[]
            { Command.CreateInvite }),
             new Tuple<string, Command[]>("Xkcd", new Command[]
            { Command.CreateInvite }),
             new Tuple<string, Command[]>("Color", new Command[]
            { Command.CreateRole, Command.EditRole }),
        };
        public static List<Tuple<CommandParameter, Command[]>> ParameterList = new List<Tuple<CommandParameter, Command[]>>()
{
            new Tuple<CommandParameter, Command[]>(CommandParameter.Name, new Command[]
            { Command.UseServer, Command.ShowServer, Command.CreateServer,
                Command.RemoveServer, Command.EditServer, Command.SaveServer }),
            new Tuple<CommandParameter, Command[]>(CommandParameter.Id, new Command[]
            { Command.UseServer, Command.ShowServer, Command.RemoveServer,
                Command.EditServer, Command.SaveServer }),
            new Tuple<CommandParameter, Command[]>(CommandParameter.Region, new Command[]
            { Command.CreateServer, Command.EditServer}),
            new Tuple<CommandParameter, Command[]>(CommandParameter.Image, new Command[]
            {Command.CreateServer, Command.EditServer}),
             new Tuple<CommandParameter, Command[]>(CommandParameter.NewName, new Command[]
            {Command.EditServer, Command.EditRole, Command.EditText, Command.EditVoice}),
              new Tuple<CommandParameter, Command[]>(CommandParameter.Output, new Command[]
            {Command.SaveRole, Command.SaveServer, Command.SaveText, Command.SaveUsers,
            Command.SaveVoice}),

        };
        #endregion
    }
}
