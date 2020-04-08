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
        // superbeing
        IEnumerable<SuperBeing> GetSuperBeings();
        SuperBeing GetSuperBeingById(int superBeingId);
        bool SuperBeingExists(int superBeingId);
        SuperBeing CreateSuperBeing(SuperBeing superBeing);
        void DeleteSuperBeing(int superBeingId);

        // powers
        IEnumerable<SuperBeingPower> GetPowers(int SuperBeingId);
        SuperBeingPower GetPowerById(int PowerId);
        int DeletePower(int PowerId);
        SuperBeingPowerCreateUpdateDto CreatePower(int SuperBeingId, SuperBeingPowerCreateUpdateDto createPowerDto);
    }
}
