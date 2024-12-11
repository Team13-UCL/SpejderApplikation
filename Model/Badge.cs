using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

// Badge class represents achievements or badges in the application
namespace SpejderApplikation.Model
{
    public class Badge
    {
        public int _badgeID { get; private set; } // Unique identifier for the badge
        public string Name { get; set; } // Name of the badge
        public string Description { get; set; } // Description of the badge
        public byte[] Picture { get; set; } // Byte-array for storing badge images
        public string Link { get; set; } // Link for more information about the badge

        public Badge() { }

        // Constructor to initialize all properties
        public Badge(int id, string name, string description, byte[] picture, string link)
        {
            _badgeID = id;
            Name = name;
            Description = description;
            Picture = picture;
            Link = link;
        }
        // Copy constructor for creating a duplicate badge
        public Badge(Badge badge)
        {
            if (badge == null) throw new ArgumentNullException(nameof(badge)); // Fejlhåndtering af Null
            this._badgeID = badge._badgeID;
            this.Name = badge.Name;
            this.Description = badge.Description;
            this.Picture = badge.Picture;
            this.Link = badge.Link;
        }

        // Updates the badge ID
        public void UpdateID(int ID)
        {
            _badgeID = ID;
        }

    }
}
