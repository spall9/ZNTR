using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace ZNTR_Urgot.Modes {
    public sealed class Combo :ModeBase {
        public override int Delay {
            get { return Game.Ping / 2; }
        }

        public override bool ShouldBeExecuted() {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute() {
            if (!SpellManager.ShouldCast(false)) {
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

            #region Combo logic

            var isQTickedAndReady = Config.ComboMenu["comboQ"].Cast<CheckBox>().CurrentValue &&
                       Player.CanUseSpell(SpellSlot.Q) == SpellState.Ready;
            var isWTickedAndReady = Config.ComboMenu["comboW"].Cast<CheckBox>().CurrentValue &&
                       Player.CanUseSpell(SpellSlot.W) == SpellState.Ready;
            var isETickedAndReady = Config.ComboMenu["comboE"].Cast<CheckBox>().CurrentValue &&
                       Player.CanUseSpell(SpellSlot.E) == SpellState.Ready;
            var isRTickedAndReady = Config.ComboMenu["comboR"].Cast<CheckBox>().CurrentValue &&
                       Player.CanUseSpell(SpellSlot.R) == SpellState.Ready;


            var pred_Q = Q.GetPrediction(selectedTarget);
            var pred_E = E.GetPrediction(selectedTarget);
            var pred_R = R.GetPrediction(selectedTarget);


            // If Enemy in W Range and E is likely to hit
            if (isWTickedAndReady && isETickedAndReady) {
                foreach (var target in allTargets) {
                    // Wenn W schon läuft und E trifft, caste E
                    if (Program.Urgot.HasBuff("UrgotW") && target.Distance(ObjectManager.Player) <= W.Range - 50 && E.GetPrediction(target).HitChance >= hitchance_high) {
                        E.Cast(E.GetPrediction(target).CastPosition);
                        return;
                    }

                    // Wenn W nicht läuft, enemy in Range und E trifft, W und E
                    if (!Program.Urgot.HasBuff("UrgotW") && target.Distance(ObjectManager.Player) <= W.Range - 50 && E.GetPrediction(target).HitChance >= hitchance_high) {
                        W.Cast();
                        E.Cast(E.GetPrediction(target).CastPosition);

                    }

                    if (isQTickedAndReady) {
                        if (Q.GetPrediction(target).HitChance >= hitchance_high) {
                            Q.Cast(Q.GetPrediction(target).CastPosition);
                        }

                    }
                }
            }

            // Wenn Q wahrscheinlich trifft, caste Q, wenn nicht dann medium Q
            if (isQTickedAndReady) {
                foreach (var target in allTargets) {
                    if (Q.GetPrediction(target).HitChance >= hitchance_high) {
                        Q.Cast(Q.GetPrediction(target).CastPosition);
                    }
                }
            }


            // Säuberung buff
            if (isWTickedAndReady) {
                foreach (var target in allTargets) {
                    if (Program.Urgot.HasBuff("UrgotW") && target.Distance(ObjectManager.Player) <= W.Range - 50) {
                        return;
                    }
                    if (target.Distance(ObjectManager.Player) <= W.Range - 50 && W.IsReady()) {
                        W.Cast();
                    }

                }
            }

            if (isETickedAndReady) {
                foreach (var target in allTargets) {
                    if (E.GetPrediction(target).HitChance >= hitchance_high) {
                        E.Cast(E.GetPrediction(target).CastPosition);
                    }
                }
            }


                /*foreach (var buff in Program.Urgot.Buffs) {
                    System.Console.WriteLine("BuffName: {0}", buff.Name);
                }*/



            if (isRTickedAndReady) {
                foreach (var target in allTargets) {
                    if (target.HealthPercent <= 25) {
                        if (R.GetPrediction(target).HitChance >= hitchance_high) {
                            R.Cast(R.GetPrediction(target).CastPosition);
                            R.Cast(R.GetPrediction(target).CastPosition);
                        }
                    }
                }
            }

        }
        #endregion
    }

}


