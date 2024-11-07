using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Model
{
    internal class Unit
    {
        private int _unitID { get; set; }
        public string UnitName { get; set; }
        public string Link { get; set; }
        public Blob Picture { get; set; }
    }
}
