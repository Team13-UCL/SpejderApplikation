using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// Unit class represents a unit or group within the application
namespace SpejderApplikation.Model
{
    public class Unit
    {
        public int _unitID { get; private set; } // Unique identifier for the unit
        public string Description { get; set; } // Description of the unit
        public string UnitName { get; set; } // Name of the unit
        public string Link { get; set; } // Link to additional information about the unit

        // Constructor to initialize all properties
        public Unit(int unitID, string unitName, string description, string link)
        {
            _unitID = unitID;
            UnitName = unitName;
            Description = description;
            Link = link;           
        }

        // Default constructor with default values
        public Unit() : this(0, "Enhed", "", null) { }        

        // Updates the unit ID
        public void UpdateID(int ID)
        {
            _unitID = ID;
        }
    }
}
