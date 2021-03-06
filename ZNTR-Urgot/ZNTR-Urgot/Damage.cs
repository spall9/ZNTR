﻿using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;

namespace ZNTR_Urgot {
    static class Damage {
        private static readonly Dictionary<SpellSlot, int[]> BaseDamage = new Dictionary<SpellSlot, int[]>();
        private static readonly Dictionary<SpellSlot, float[]> BonusDamage = new Dictionary<SpellSlot, float[]>();

        static Damage() {
            // Urgot Base Damage
            BaseDamage.Add(SpellSlot.Q, new[] { 25, 70, 115, 160, 205 });
            BaseDamage.Add(SpellSlot.W, new[] { 75, 120, 165, 210, 255 });
            BaseDamage.Add(SpellSlot.E, new[] { 60, 100, 140, 180, 220 });
            BaseDamage.Add(SpellSlot.R, new[] { 50, 175, 300 });

            BonusDamage.Add(SpellSlot.Q, new[] { 0.70f, 0.70f, 0.70f, 0.70f, 0.70f });
            BonusDamage.Add(SpellSlot.W, new[] { 0.6f, 0.6f, 0.6f, 0.6f, 0.6f });
            BonusDamage.Add(SpellSlot.E, new[] { 0.50f, 0.50f, 0.50f, 0.50f, 0.50f });
            BonusDamage.Add(SpellSlot.R, new[] { 0.50f, 0.50f, 0.50f });
        }


        public static float CalculateDamage(SpellSlot slot, Obj_AI_Base unit) {
            if (slot == SpellSlot.Internal) {
                return Player.Instance.CalculateDamageOnUnit(unit, DamageType.Physical, unit.MaxHealth * 0.08f) - unit.FlatHPRegenMod * 4;
            }
            // Hier noch einiges machen
            var spellLevel = Player.GetSpell(slot).Level;
            var attackDamage = Program.Urgot.TotalAttackDamage;

            var baseDmg = BaseDamage[slot];
            var bonusDmg = BonusDamage[slot];

            if (spellLevel == 0) {
                return 0;
            }

            return Player.Instance.CalculateDamageOnUnit(unit, DamageType.Physical,
                baseDmg[spellLevel - 1] + bonusDmg[spellLevel - 1] * attackDamage);
        }

        public static float TotalDamage(SpellSlot slot, Obj_AI_Base unit) {
            return CalculateDamage(slot, unit) + CalculateDamage(SpellSlot.Internal, unit);
        }

        public static bool Killable(this Obj_AI_Base target, SpellSlot slot) {
            return TotalDamage(slot, target) >= target.Health;
        }

        public static float GetIgniteDamage() {
            return (10 + Player.Instance.Level * 4) * 5;
        }


    }
}
