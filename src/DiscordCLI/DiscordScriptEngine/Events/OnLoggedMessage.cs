using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScriptEngine {
    public class OnLoggedMessage : EventArgs {
        
        #region Internal Constructors
        public OnLoggedMessage(DResult result)
        {
            Result = result;
        }

        public DResult Result { get; }

        #endregion
    }
}
