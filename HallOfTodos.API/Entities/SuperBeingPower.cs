﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Entities
{
    public class SuperBeingPower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PowerType { get; set; }
        public int SuperBeingId { get; set; }
    }
}
