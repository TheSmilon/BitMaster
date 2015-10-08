using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Utils;
using Color = System.Drawing.Color;
using SharpDX;

namespace BitMasterYi
{
    class Program
    {
        public static Spell.Targeted Q;
        public static Spell.Active W;
        public static Spell.Active E;
        public static Spell.Active R;

        public static Menu Menu,
            DrawMenu,
            ComboMenu;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnStart;
            Bootstrap.Init(null);
            Game.OnUpdate += Game_OnUpdate;
        }

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        private static void Game_OnStart(EventArgs args)
        {
            if (_Player.ChampionName != "MasterYi")
                return;
           
            Chat.Print("MasterYi © 2015");

            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R, 500);

            Menu = MainMenu.AddMenu("BitMaster", "bitMaster");
            Menu.AddSeparator();
            Menu.AddLabel("Combo Master Yi By TheSmilon © 2015");

            DrawMenu = Menu.AddSubMenu("Draws", "bitMasterDraw");
            DrawMenu.Add("drawDisable", new CheckBox("Desabilitar todos os Draws", true));
            ComboMenu = Menu.AddSubMenu("Combo", "bitMasterCombo");
            ComboMenu.Add("comboQ", new CheckBox("Use o Q no Combo", true));            
            ComboMenu.Add("comboE", new CheckBox("Use o E no Combo", true));
            ComboMenu.Add("comboR", new CheckBox("Use o R no Combo", true));
        }

        public static void Game_OnUpdate(EventArgs args)
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            var comboQ = ComboMenu["Qcombo"].Cast<CheckBox>().CurrentValue;
            var comboE = ComboMenu["Ecombo"].Cast<CheckBox>().CurrentValue;
            var comboR = ComboMenu["Rcombo"].Cast<CheckBox>().CurrentValue;

            if (target == null)
                return;

            if (target.HasBuffOfType(BuffType.Stun) || target.HasBuffOfType(BuffType.Snare))

                if (Q.IsReady() && Q.IsReady() && !target.IsZombie && target.IsValidTarget(Q.Range))
                {
                    Q.Cast(target);
                }
            if (comboE && E.IsReady() && !target.IsZombie && target.IsValidTarget(150))
            {
                E.Cast();
            }
            if (comboR && R.IsReady() && !target.IsZombie && target.IsValidTarget(R.Range))
            {
                R.Cast();
            }
        }
    }
}