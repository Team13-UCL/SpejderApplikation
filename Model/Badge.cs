using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Model
{
    internal class Badge
    {
        private int _badgeID;
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public string Link { get; set; }
        public Badge() { }
        public Badge(int id, string name, string description, byte[] picture, string link)
        {
            _badgeID = id;
            Name = name;
            Description = description;
            Picture = picture;
            Link = link;
        }
        public Badge(Badge badge)
        {
            if (badge == null) throw new ArgumentNullException(nameof(badge));
            this._badgeID = badge._badgeID;
            this.Name = badge.Name;
            this.Description = badge.Description;
            this.Picture = badge.Picture;
            this.Link = badge.Link;
        }
    }
}
