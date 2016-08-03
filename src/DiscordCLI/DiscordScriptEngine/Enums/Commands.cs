namespace DScriptEngine {
    public static class Commands {
        public enum Command {
            //Helpers 
            ShowHelp,
            RunScript,
            //Server Commands
            UseServer,
            ShowServer,
            CreateServer,
            RemoveServer,
            EditServer,
            SaveServer,
            CreateInvite,
            //Text channel commands
            ShowText,
            CreateText,
            RemoveText,
            EditText,
            SaveText,
            //Voice channel commands
            ShowVoice,
            CreateVoice,
            RemoveVoice,
            EditVoice,
            SaveVoice,
            //User commands
            ShowUsers,
            BanUser,
            KickUser,
            SaveUsers,
            //Role commands
            ShowRoles,
            CreateRole,
            RemoveRole,
            EditRole,
            SaveRole,
            Error,
        }
    }
}
