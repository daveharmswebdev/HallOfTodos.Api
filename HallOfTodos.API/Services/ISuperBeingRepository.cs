﻿using HallOfTodos.API.Entities;
using HallOfTodos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Services
{
    public interface ISuperBeingRepository
    {
        IEnumerable<SuperBeingPower> GetPowers(int SuperBeingId);
        int DeletePower(int PowerId);
        SuperBeingPowerCreateUpdateDto CreatePower(int SuperBeingId, SuperBeingPowerCreateUpdateDto createPowerDto);
    }
}
