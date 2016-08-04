using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScriptEngine {
    internal class DULong : IVariable {
        private ulong value { get; set; }
        private string name { get; set; }
        public DULong(string name, ulong value) {
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
