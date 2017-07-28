using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace ZNTR_Urgot.Modes {
    public sealed class Harass :ModeBase {
        public override bool ShouldBeExecuted() {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute() {

            if (!SpellManager.ShouldCast()) {
                return;
            }

            const int range = 1100;

            var enemies = EntityManager.Heroes.Enemies.Where(n => n.IsValidTarget(range));
            var selectedTarget = TargetSelector.GetTarget(range, DamageType.Physical);
            var allTargets =
                new[] { selectedTarget }.Concat(enemies.Where(n => n.Index != selectedTarget.Index).OrderByDescending(n => Damage.TotalDamage(SpellSlot.Q, n) / n.Health)).Where(n => n.IsValidTarget());

            if (selectedTarget == null && !enemies.Any()) {
                return;
            }

            var hitchance_high = EloBuddy.SDK.Enumerations.HitChance.High;

            var isQTickedAndReady = Config.HarassMenu["UseQ"].Cast<CheckBox>().CurrentValue && Player.CanUseSpell(SpellSlot.Q) == SpellState.Ready;

            if (isQTickedAndReady) {
                foreach (var target in allTargets) {
                    if (Q.GetPrediction(target).HitChance >= hitchance_high) {
                        Q.Cast(Q.GetPrediction(target).CastPosition);
                    }
                }
            }
        }
    }
}
