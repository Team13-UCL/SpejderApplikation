﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Model
{
    public class Badge
    {
        public int _badgeID { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; } // Byte-array til billeder af gemte mærker
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
        // Kopi Konstruktor
        public Badge(Badge badge)
        {
            if (badge == null) throw new ArgumentNullException(nameof(badge)); // Fejlhåndtering af Null
            this._badgeID = badge._badgeID;
            this.Name = badge.Name;
            this.Description = badge.Description;
            this.Picture = badge.Picture;
            this.Link = badge.Link;
        }
        public void UpdateID(int ID)
        {
            _badgeID = ID;
        }

    }
}
