using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;


namespace ZNTR_Urgot {
    static class Program {

        public static AIHeroClient Urgot => Player.Instance;

        static void Main(string[] args) {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args) {
            if (Urgot.Hero != Champion.Urgot) {
                return;
            }

            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();

            Drawing.OnDraw += OnDraw;
        }

        private static void OnDraw(EventArgs args) {
            if (Urgot.IsDead) {
                return;
            }


            if (Config.DrawMenu["drawQ"].Cast<CheckBox>().CurrentValue && Player.GetSpell(SpellSlot.Q).IsLearned) {
                EloBuddy.SDK.Rendering.Circle.Draw(SharpDX.Color.Red, SpellManager.Q.Range, Player.Instance);
            }

            if (Config.DrawMenu["drawW"].Cast<CheckBox>().CurrentValue && Player.GetSpell(SpellSlot.W).IsLearned) {
                EloBuddy.SDK.Rendering.Circle.Draw(SharpDX.Color.Red, SpellManager.W.Range, Player.Instance);
            }

            if (Config.DrawMenu["drawE"].Cast<CheckBox>().CurrentValue && Player.GetSpell(SpellSlot.E).IsLearned) {
                EloBuddy.SDK.Rendering.Circle.Draw(SharpDX.Color.Red, SpellManager.E.Range+50, Player.Instance);
            }

            if (Config.DrawMenu["drawR"].Cast<CheckBox>().CurrentValue && Player.GetSpell(SpellSlot.R).IsLearned) {
                EloBuddy.SDK.Rendering.Circle.Draw(SharpDX.Color.Red, SpellManager.R.Range, Player.Instance);
            }

            if (TargetSelector.GetTarget(SpellManager.Q.Range, DamageType.Physical) != null) {
                var targetQ = TargetSelector.GetTarget(SpellManager.Q.Range, DamageType.Physical);

                /*
            if (Config.DrawMenu["DrawQpred"].Cast<CheckBox>().CurrentValue) {
                if (targetQ == null || targetQ.IsAlly) { return; }

                Drawing.DrawCircle(SpellManager.Q.GetPrediction(targetQ).CastPosition, SpellManager.Q.Width, System.Drawing.Color.DarkGray);

            }*/
            }

            // Printe AttackDamage onScreen
            //Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(30, -30), System.Drawing.Color.White, Urgot.TotalAttackDamage.ToString(), 2);

            /*
            if (TargetSelector.SelectedTarget != null) {
                var currTarget = TargetSelector.SelectedTarget;
                if (currTarget.IsDead) {return;}
                Drawing.DrawLine(Drawing.WorldToScreen(Player.Instance.Position), Drawing.WorldToScreen(currTarget.Position), 4f, System.Drawing.Color.Red);
                // For Q
                if (SpellManager.Q.GetPrediction(currTarget).HitChance >= HitChance.High) {
                    Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(30, -30), System.Drawing.Color.Green, "High Q", 10);
                } else { Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(30, -30), System.Drawing.Color.Red, "Low Q", 10); }
                // For E
                if (SpellManager.E.GetPrediction(currTarget).HitChance >= HitChance.High) {
                    Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(30, -45), System.Drawing.Color.Green, "High E", 10);
                } else { Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(30, -45), System.Drawing.Color.Red, "Low E", 10); }
                // For R
                if (SpellManager.E.GetPrediction(currTarget).HitChance >= HitChance.High) {
                    Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(30, -60), System.Drawing.Color.Green, "High", 10);
                } else { Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(30, -60), System.Drawing.Color.Red, "Low", 10); }
            }
            */

            // EloBuddy.SDK.Rendering.Circle.Draw(Color.CornflowerBlue, 175, Player.Instance); Q AoE
        }
    }
}
