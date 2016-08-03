using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScriptEngine {
    internal class DArray : IVariable {
        private string[] value { get; set; }
        private string name { get; set; }

        public DArray(string name, string[] value) {
            this.name = name;
            this.value = value;
        }

        public string Name => name;
        public string[] Value => value;
    }
}
