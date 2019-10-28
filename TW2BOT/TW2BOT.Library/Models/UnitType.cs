using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW2BOT.Library.Models
{
    public enum UnitType
    {
        Spear, Sword, Axe, Archer,
        LightCav, HorseArcher, HeavyCav,
        Ram, Catapult,
        Berserker, Trebutchet,
        Noble, Paladin
    }

    public struct ArmyPreset
    {
        public UnitType unit;
        public int amount;

        public ArmyPreset(UnitType unit, int amount)
        {
            this.unit = unit;
            this.amount = amount;
        }
    }
}
