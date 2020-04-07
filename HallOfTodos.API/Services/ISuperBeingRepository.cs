using HallOfTodos.API.Entities;
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
        SuperBeingPower GetPowerById(int PowerId);
        SuperBeing GetSuperBeingById(int superBeingId);
        bool SuperBeingExists(int superBeingId);
        int DeletePower(int PowerId);
        SuperBeingPowerCreateUpdateDto CreatePower(int SuperBeingId, SuperBeingPowerCreateUpdateDto createPowerDto);
    }
}
