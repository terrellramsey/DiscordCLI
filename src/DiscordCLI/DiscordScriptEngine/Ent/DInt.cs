using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScriptEngine {
    internal class DInt : IVariable {
        private int value { get; set; }
        private string name { get; set; }
        public DInt(string name, int value) {
            this.name = name;
            this.value = value;
        }
        public string Name => name;
        public int Value => value;
    }
}
