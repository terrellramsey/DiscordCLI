namespace DScriptEngine {
    public static class MessageStore {
        public static string TextChannelCreated = "The text channel {0} was created on {1}";
        public static string NoServerSelected = "No server selected, select a server before preforming this action";
        public static string MissingParameter = "Missing parameter {0}";
        public static string WrongImageType = "The image must be a png or jpeg file";
        public static string FileNotFound = "The file {0} was not found";
        public static string ServerRegionNotFound = "The server region was not found";
        public static string ServerCreated = "The server {0} was created and given the id {1}";
        public static string ServerNotFound = "The server {0} was not found or you don't have permission to edit it";
        public static string IdNotValid = "{0} is not a valid id";
        public static string FileFormatNotValid = "{0} is not a supported DScript";
        public static string EmptyScriptFile = "No commands found in {0}";
        public static string RunningCommand = "Running {0}";
        public static string CommandNotFound = "The command {0} was not found";
        public static string FailedToCreateInvite = "Failed to create invite";
        public static string NotServerOwner = "You need to be the owner of the server to perform this action";
        public static string ServerDeleted = "The server {0} was deleted.";
        public static string FailedToCreateChannel = "Failed to create the channel {0} on {1}";
    }
}
