using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Utils;
using ZNTR_Urgot.Modes;
using EloBuddy.SDK;
using SharpDX;
using System.Linq;
using EloBuddy.SDK.Menu.Values;

namespace ZNTR_Urgot {
    public static class ModeManager {
        private static List<ModeBase> Modes { get; set; }

        static ModeManager() {
            Modes = new List<ModeBase>();

            Modes.AddRange(new ModeBase[]
            {
                //new LaneClear(),
                new Combo(),
                new Harass(),
                //new Ignite(), 
            });

            Game.OnTick += OnTick;
            Drawing.OnDraw += OnDraw;
        }

        public static void Initialize() {
        }

        private static void OnTick(EventArgs args) {
            /*
            foreach (var buff in Program.Urgot.Buffs) {
                Console.WriteLine("BuffName: {0}, displayname: {1}, start: {2}, end {3}", buff.Name, buff.DisplayName, buff.StartTime, buff.IsActive);
                if (buff.Name.ToString().Contains("UrgotPassiveZone1"))
                {
                   
                }
               
            }*/

            var enemies = EntityManager.Heroes.Enemies.Where(n => n.IsValidTarget(SpellManager.R.Range));
            var selectedTarget = TargetSelector.GetTarget(SpellManager.R.Range, DamageType.Physical);
            var allTargets = new[] { selectedTarget }.Concat(enemies.Where(n => n.Index != selectedTarget.Index).OrderByDescending(n => Damage.TotalDamage(SpellSlot.Q, n) / n.Health)).Where(n => n.IsValidTarget());

            if (selectedTarget == null && !enemies.Any()) {
                return;
            }



           


            //Drawing.DrawLine(Drawing.WorldToScreen(Player.Instance.Position), Drawing.WorldToScreen(selectedTarget.Position), 4f, System.Drawing.Color.Green);

            // KS with R, hitchance High
            if (Config.UltimateMenu["ksWithR"].Cast<CheckBox>().CurrentValue && Player.GetSpell(SpellSlot.R).IsLearned) {
                if (Config.UltimateMenu["hitchanceR"].Cast<Slider>().CurrentValue == 1) {
                    if (SpellManager.R.GetPrediction(selectedTarget).HitChance >= HitChance.High && selectedTarget.HealthPercent <= 25) {
                        SpellManager.R.Cast(SpellManager.R.GetPrediction(selectedTarget).CastPosition);
                        SpellManager.R.Cast(SpellManager.R.GetPrediction(selectedTarget).CastPosition); // gesund?
                    }
                }
                if (Config.UltimateMenu["hitchanceR"].Cast<Slider>().CurrentValue == 2) {
                    if (SpellManager.R.GetPrediction(selectedTarget).HitChance >= HitChance.Immobile && selectedTarget.HealthPercent <= 25) {
                        SpellManager.R.Cast(SpellManager.R.GetPrediction(selectedTarget).CastPosition);
                        SpellManager.R.Cast(SpellManager.R.GetPrediction(selectedTarget).CastPosition); // gesund?
                    }
                }
            }


            Modes.ForEach(mode => {
                try {
                    if (mode.IsReady() && mode.ShouldBeExecuted()) {
                        mode.SetDelay();
                        mode.Execute();
                    }
                }
                catch (Exception e) {
                    Logger.Log(LogLevel.Error, "Error executing mode '{0}'\n{1}", mode.GetType().Name, e);
                }
            });
        }

        private static void OnDraw(EventArgs args) {
            Modes.ForEach(mode => {
                try {
                    mode.Draw();
                }
                catch (Exception e) {
                    Logger.Log(LogLevel.Error, "Error executing mode '{0}'\n{1}", mode.GetType().Name, e);
                }
            });
        }
    }
}
