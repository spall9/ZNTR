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
            Q = new Spell.Skillshot(SpellSlot.Q, 800, SkillShotType.Circular, 250, 1600, 175, DamageType.Physical) { AllowedCollisionCount = 300 };// spellspeed, delay etc nicht richtig
            W = new Spell.Active(SpellSlot.W, 490, DamageType.Physical);
            E = new Spell.Skillshot(SpellSlot.E, 425, SkillShotType.Linear, 250, null, 110, DamageType.Physical) { AllowedCollisionCount = 300 };
            R = new Spell.Skillshot(SpellSlot.R, 1525, SkillShotType.Linear, 250, 800, 100, DamageType.Physical){ AllowedCollisionCount = 0 };

            Q.MinimumHitChance = HitChance.Medium;
            E.MinimumHitChance = HitChance.Medium;
            R.MinimumHitChance = HitChance.Medium;
        }

        public static void Initialize() {
        }

        public static bool ShouldCast(bool allowAutos = true) {
            //return !Program.Urgot.Spellbook.IsCastingSpell || (!allowAutos || (Program.Urgot.Spellbook.IsAutoAttacking && Orbwalker.CanBeAborted));
            return true;
        }

        /*
        public static bool IsBlazed(this Obj_AI_Base target)
        {
            return target.HasBuff("BrandAblaze");
        }
        */
    }
}
