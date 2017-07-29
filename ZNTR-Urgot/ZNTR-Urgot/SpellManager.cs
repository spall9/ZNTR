using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace ZNTR_Urgot {
    public static class SpellManager {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Skillshot R { get; private set; }

        static SpellManager() {
            Q = new Spell.Skillshot(SpellSlot.Q, 800, SkillShotType.Circular, 0, 500, 175, DamageType.Physical) { AllowedCollisionCount = int.MaxValue };// spellspeed, delay etc nicht richtig
            W = new Spell.Active(SpellSlot.W, 490, DamageType.Physical);
            E = new Spell.Skillshot(SpellSlot.E, 475, SkillShotType.Linear, 250, null, 110, DamageType.Physical) { AllowedCollisionCount = int.MaxValue };
            R = new Spell.Skillshot(SpellSlot.R, 1600, SkillShotType.Linear, 0 , 2150, 80, DamageType.Physical){ AllowedCollisionCount = 0 };

        }

        public static void Initialize() {
        }

        public static bool ShouldCast(bool allowAutos = true) {
            return !Program.Urgot.Spellbook.IsCastingSpell || (!allowAutos || (Program.Urgot.Spellbook.IsAutoAttacking && Orbwalker.CanBeAborted));
        }

       
    }
}
