using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpejderApplikation.Model
{
    public class Unit
    {
        public int _unitID { get; private set; }
        public string Description { get; set; }
        public string UnitName { get; set; }
        public string Link { get; set; }
        public byte[] Picture { get; set; }
        public Unit(int unitID, string unitName, string description, string link, byte[] picture)
        {
            _unitID = unitID;
            UnitName = unitName;
            Description = description;
            Link = link;
            Picture = picture;
        }
        public Unit() : this(0, "Enhed", "", null, null) { }

        // Hjælpefunktion til at hente standardbilledet KFUM.PNG, hvilket er KFUMS logo
        private static byte[] GetDefaultPicture()
        {
            string filePath = Directory.GetCurrentDirectory();
            string fileName = "\\KFUM.png"; // har et basis KFUM mærke i projektets mappe
            if (File.Exists(filePath) == true)
            {
                return File.ReadAllBytes(string.Concat(filePath, fileName));
            }
            else
            {
                return new byte[0];
            }
        }
        public void UpdateID(int ID)
        {
            _unitID = ID;
        }
    }
}
