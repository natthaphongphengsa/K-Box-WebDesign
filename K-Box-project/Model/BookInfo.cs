﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K_Box_project.Model
{
    public class BookInfo
    {
        public string type {get;set;}
        public string text { get; set; }
        public string ImagesUrl { get; set; }
        public int reaprice { get; set; }
        public DateTime datetime { get; set; }
        public bool student { get; set; }
    }
}
