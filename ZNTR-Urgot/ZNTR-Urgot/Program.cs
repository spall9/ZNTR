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
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Obj_AI_Base.OnSpellCast += Obj_AI_Base_OnSpellCast;
            MissileClient.OnCreate += MissileClient_OnCreate;
            Obj_SpellMissile.OnCreate += Obj_SpellMissile_OnCreate;



        }

        private static void Obj_SpellMissile_OnCreate(GameObject sender, EventArgs args) {
            if (sender.IsMe) {
                Console.WriteLine("MEOWOWOW:");
            }
        }


        // Code zum getten von SpellData!
        private static void MissileClient_OnCreate(GameObject sender, EventArgs args) {
            var missile = sender as MissileClient;

            if (missile != null && missile.SpellCaster.IsMe) {
                Console.WriteLine("Missile created:");
                Console.WriteLine("Name " + missile.SData.Name);
                Console.WriteLine("Speed " + missile.SData.MissileSpeed);
                Console.WriteLine("CastType " + missile.SData.CastType);
                Console.WriteLine("CooldownTime " + missile.SData.CooldownTime);
                Console.WriteLine("LineMissileDelayDestroyAtEndSeconds " + missile.SData.LineMissileDelayDestroyAtEndSeconds);
                Console.WriteLine("MinSpeed " + missile.SData.MissileMinSpeed);
                Console.WriteLine("MaxSpeed " + missile.SData.MissileMaxSpeed);
                Console.WriteLine("Width " + missile.SData.LineWidth);
                Console.WriteLine("Delay " + missile.SData.CastTime);
                Console.WriteLine("CastRange " + missile.SData.CastRange);
                Console.WriteLine("CastRangeOverride " + missile.SData.CastRangeDisplayOverride);



            }
        }

        private static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args) {
            // Try to get E data

        }

        // Bei ausführung eines Spells
        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args) {

            if (sender.IsMe) {
                var spell = args.SData;
                if (spell == null) { return; }
                Console.WriteLine("Spell: " + spell.Name + ", AmmoRechargeTime: " + args.SData.AmmoRechargeTime);
                Console.WriteLine("-------------------------------");
            }
        }

        private static void OnDraw(EventArgs args) {
            if (Urgot.IsDead) {
                return;
            }



            if (Config.DrawMenu["AutoAttackRange"].Cast<CheckBox>().CurrentValue) {
                // AutoAttackRange 217.293491873 from each leg for stuff
                EloBuddy.SDK.Rendering.Circle.Draw(SharpDX.Color.White, 420, Player.Instance.Position);
            }

            Vector3 vector3PlayerStart = new Vector3(Player.Instance.Position.X, Player.Instance.Position.Y, Player.Instance.Position.Z);

            // LEG DIRECTIONS TO W RANGE FOR AREA
            Vector3 vector3PlayerEnd1 = new Vector3(Player.Instance.Position.X + (-130), Player.Instance.Position.Y + (465), Player.Instance.Position.Z);
            Vector3 vector3PlayerEnd2 = new Vector3(Player.Instance.Position.X + (340), Player.Instance.Position.Y + (350), Player.Instance.Position.Z);
            Vector3 vector3PlayerEnd3 = new Vector3(Player.Instance.Position.X + (470), Player.Instance.Position.Y + (-120), Player.Instance.Position.Z);
            Vector3 vector3PlayerEnd4 = new Vector3(Player.Instance.Position.X + (130), Player.Instance.Position.Y + (-470), Player.Instance.Position.Z);
            Vector3 vector3PlayerEnd5 = new Vector3(Player.Instance.Position.X + (-345), Player.Instance.Position.Y + (-345), Player.Instance.Position.Z);
            Vector3 vector3PlayerEnd6 = new Vector3(Player.Instance.Position.X + (-470), Player.Instance.Position.Y + (125), Player.Instance.Position.Z);

            // LEG DIRECTIONS + HALF SHIT THINGY STUFF AREA K?
            Vector3 vec1 = Vector3.Lerp(vector3PlayerEnd1, vector3PlayerEnd2, 0.5f);
            Vector3 vec2 = Vector3.Lerp(vector3PlayerEnd2, vector3PlayerEnd3, 0.5f);    
            Vector3 vec3 = Vector3.Lerp(vector3PlayerEnd3, vector3PlayerEnd4, 0.5f);
            Vector3 vec4 = Vector3.Lerp(vector3PlayerEnd4, vector3PlayerEnd5, 0.5f);
            Vector3 vec5 = Vector3.Lerp(vector3PlayerEnd5, vector3PlayerEnd6, 0.5f);
            Vector3 vec6 = Vector3.Lerp(vector3PlayerEnd6, vector3PlayerEnd1, 0.5f);

            // LEG DIRECTIONS TO AUTO RANGE FOR DRAW
            Vector3 legDirectionShort1 = new Vector3(Player.Instance.Position.X + (-110), Player.Instance.Position.Y + (405), Player.Instance.Position.Z);
            Vector3 legDirectionShort2 = new Vector3(Player.Instance.Position.X + (298), Player.Instance.Position.Y + (298), Player.Instance.Position.Z);
            Vector3 legDirectionShort3 = new Vector3(Player.Instance.Position.X + (405), Player.Instance.Position.Y + (-108), Player.Instance.Position.Z);
            Vector3 legDirectionShort4 = new Vector3(Player.Instance.Position.X + (110), Player.Instance.Position.Y + (-405), Player.Instance.Position.Z);
            Vector3 legDirectionShort5 = new Vector3(Player.Instance.Position.X + (-298), Player.Instance.Position.Y + (-298), Player.Instance.Position.Z);
            Vector3 legDirectionShort6 = new Vector3(Player.Instance.Position.X + (-410), Player.Instance.Position.Y + (110), Player.Instance.Position.Z);
            
            // Write MouseData compared to PlayerPos
            //Vector2 currMouse = new Vector2(Game.ActiveCursorPos.X, Game.ActiveCursorPos.Y);
            //Vector2 currMouseToPlayer = new Vector2(Game.ActiveCursorPos.X - Player.Instance.Position.X, Game.ActiveCursorPos.Y - Player.Instance.Position.Y);
            //Console.WriteLine("Mausdaten: X -> " + currMouseToPlayer.X + " --- Y -> " + currMouseToPlayer.Y);



            if (Config.DrawMenu["LegTriggerArea"].Cast<CheckBox>().CurrentValue) {
                // Leg1 + halber umfang eines sechstels
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.White, 1f, vector3PlayerStart, vec1);
                // Leg2 + halber umfang eines sechstels
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.White, 1f, vector3PlayerStart, vec2);
                // Leg3 + halber umfang eines sechstels
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.White, 1f, vector3PlayerStart, vec3);
                // Leg4 + halber umfang eines sechstels
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.White, 1f, vector3PlayerStart, vec4);
                // Leg5 + halber umfang eines sechstels
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.White, 1f, vector3PlayerStart, vec5);
                // Leg6 + halber umfang eines sechstels
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.White, 1f, vector3PlayerStart, vec6);
            }

            if (Config.DrawMenu["LegDirection"].Cast<CheckBox>().CurrentValue) {
                // Leg1
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.Blue, vector3PlayerStart, legDirectionShort1);
                // Leg2
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.Blue, vector3PlayerStart, legDirectionShort2);
                // Leg3
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.Blue, vector3PlayerStart, legDirectionShort3);
                // Leg4
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.Blue, vector3PlayerStart, legDirectionShort4);
                // Leg5
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.Blue, vector3PlayerStart, legDirectionShort5);
                // Leg6
                EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.Blue, vector3PlayerStart, legDirectionShort6);
            }

            /* Drawing.DrawLine(Player.Instance.Position.X, Player.Instance.Position.Z, Player.Instance.Position.X + 30, Player.Instance.Position.Z + 30, 4f, System.Drawing.Color.YellowGreen);
             Drawing.DrawLine(Drawing.WorldToScreen(Player.Instance.Position), Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(-200, 0), 4f, System.Drawing.Color.Red);*/

            // EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.BlanchedAlmond, 2f, Drawing.WorldToScreen(Player.Instance.Position), Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(150, 70));

            /* Drawing.DrawLine(Drawing.WorldToScreen(Player.Instance.Position), Drawing.WorldToScreen(Player.Instance.Position)  - new Vector2(0, -200), 4f, System.Drawing.Color.Red);
             Drawing.DrawLine(Drawing.WorldToScreen(Player.Instance.Position), Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(0, 200), 4f, System.Drawing.Color.Red);
             Drawing.DrawLine(Drawing.WorldToScreen(Player.Instance.Position), Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(200, 0), 4f, System.Drawing.Color.Red);
             Drawing.DrawLine(Drawing.WorldToScreen(Player.Instance.Position), Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(-200, 0), 4f, System.Drawing.Color.Red);*/
            /*EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.BlanchedAlmond, 2f, Drawing.WorldToScreen(Player.Instance.Position), Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(-30, -150));

            EloBuddy.SDK.Rendering.Line.DrawLine(System.Drawing.Color.BlanchedAlmond, 2f, Drawing.WorldToScreen(Player.Instance.Position), Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(-150, -20));*/

            if (Config.DrawMenu["drawQ"].Cast<CheckBox>().CurrentValue && Player.GetSpell(SpellSlot.Q).IsLearned) {
                EloBuddy.SDK.Rendering.Circle.Draw(SharpDX.Color.Red, SpellManager.Q.Range, Player.Instance);
            }


            if (Config.DrawMenu["drawQ"].Cast<CheckBox>().CurrentValue && Player.GetSpell(SpellSlot.Q).IsLearned) {
                EloBuddy.SDK.Rendering.Circle.Draw(SharpDX.Color.Red, SpellManager.Q.Range, Player.Instance);
            }

            if (Config.DrawMenu["drawW"].Cast<CheckBox>().CurrentValue && Player.GetSpell(SpellSlot.W).IsLearned) {
                EloBuddy.SDK.Rendering.Circle.Draw(SharpDX.Color.Red, SpellManager.W.Range, Player.Instance);
            }

            if (Config.DrawMenu["drawE"].Cast<CheckBox>().CurrentValue && Player.GetSpell(SpellSlot.E).IsLearned) {
                EloBuddy.SDK.Rendering.Circle.Draw(SharpDX.Color.Red, SpellManager.E.Range, Player.Instance);
            }

            if (Config.DrawMenu["drawR"].Cast<CheckBox>().CurrentValue && Player.GetSpell(SpellSlot.R).IsLearned) {
                EloBuddy.SDK.Rendering.Circle.Draw(SharpDX.Color.Red, 1525, Player.Instance);
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
