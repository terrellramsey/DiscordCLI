using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScriptEngine {
    internal class DString : IVariable {
        private string value { get; set; }
        private string name { get; set; }
        public DString(string name, string value) {
            this.name = name;
            this.value = value;
        }
        public string GetName() {
            return name;
        }

        public dynamic GetValue() {
            return value;
        }
    }
}
