﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Classes
{
    class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Upc { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }
        public string IsStock { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

    }
}
