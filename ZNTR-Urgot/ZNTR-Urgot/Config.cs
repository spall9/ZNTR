using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace ZNTR_Urgot
{
    static class Config
    {
        public static Menu UrgotMenu { get; set; }

        public static Menu ComboMenu { get; set; }
        public static Menu HarassMenu { get; set; }
        public static Menu KillstealMenu { get; set; }
        public static Menu LaneClearMenu { get; set; }

        public static Menu UltimateMenu { get; set; }
        public static Menu DrawMenu { get; set; }

        static Config()
        {
            Chat.Print("<font color='#006622'><b>ZNTR-Urgot</b></font> loaded. Please report back with feedback to <font color='#006622'><b>ZNTR-Urgot</b></font>!");

            UrgotMenu = MainMenu.AddMenu("ZNTR-Urgot", "ZNTR_Urgot");
            UrgotMenu.AddGroupLabel("Welcome to ZNTR-Urgot,");
            UrgotMenu.AddSeparator(5);
            UrgotMenu.AddLabel("For bugs, errors and suggestions, please visit my EB Thread.");
            UrgotMenu.AddSeparator(5);
            UrgotMenu.AddLabel("Have Fun! - Onicuppac");

            // Combo Child
            ComboMenu = UrgotMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.Add("comboQ", new CheckBox("Use Q"));
            ComboMenu.Add("comboW", new CheckBox("Use W"));
            ComboMenu.Add("comboE", new CheckBox("Use E"));
            ComboMenu.Add("comboR", new CheckBox("Use R"));

            
            

            // Harass Child
            HarassMenu = UrgotMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.Add("UseQ", new CheckBox("Use Q"));

            // Ultimate Child
            UltimateMenu = UrgotMenu.AddSubMenu("Ultimate", "Ultimate");
            UltimateMenu.Add("ksWithR", new CheckBox("Try to steal Kills with R"));
            UltimateMenu.Add("hitchanceR", new Slider("R Hitchance (1 = High, 2 = on Immobile)", 1, 1, 2));


            // KillSteal Child
            /*KillstealMenu = UrgotMenu.AddSubMenu("KillSteal", "KillSteal");
            KillstealMenu.Add("UseQ", new CheckBox("Use Q to Killsteal"));
            KillstealMenu.Add("UseW", new CheckBox("Use W to Killsteal"));
            KillstealMenu.Add("UseE", new CheckBox("Use E to Killsteal"));
            KillstealMenu.Add("UseR", new CheckBox("Use R to Killsteal"));
            KillstealMenu.Add("UseIgnite", new CheckBox("Use Ignite to Killsteal"));*/

            // LaneClear Child
            /*
            LaneClearMenu = UrgotMenu.AddSubMenu("Farming", "Farming");
            LaneClearMenu.Add("UseW", new CheckBox("Use W to clear wave"));
            LaneClearMenu.Add("UseE", new CheckBox("Use E to clear wave"));
            LaneClearMenu.Add("minEnemiesToW", new Slider("Min. Enemies to Use W", 3, 1, 6));
            LaneClearMenu.Add("minEnemiesToE", new Slider("Min. Enemies to Use E", 3, 1, 6));
            LaneClearMenu.Add("Wclearmana", new Slider("W mana to clear %", 50, 0, 100));
            LaneClearMenu.Add("Eclearmana", new Slider("E mana to clear %", 50, 0, 100));*/

            // DrawMenu Child
            DrawMenu = UrgotMenu.AddSubMenu("Drawings", "Drawings");
            DrawMenu.Add("DrawQ", new CheckBox("Draw Q range"));
            DrawMenu.Add("DrawW", new CheckBox("Draw W range"));
            DrawMenu.Add("DrawE", new CheckBox("Draw E range"));
            DrawMenu.Add("DrawR", new CheckBox("Draw R range"));
            //DrawMenu.Add("DrawWpred", new CheckBox("Draw Q prediction"));
        }

        public static void Initialize()
        {
        }
    }
}
