﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FiltersTypes
{
    public class ArtistFilter
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public string[] Properties { get; set; }
    }
}
