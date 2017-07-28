using System;
using EloBuddy;
using EloBuddy.SDK;

namespace ZNTR_Urgot.Modes
{
    public abstract class ModeBase
    {
        protected Spell.Skillshot Q
        {
            get { return SpellManager.Q; }
        }

        protected Spell.Active W
        {
            get { return SpellManager.W; }
        }

        protected Spell.Skillshot E
        {
            get { return SpellManager.E; }
        }

        protected Spell.Skillshot R
        {
            get { return SpellManager.R; }
        }

        protected int Time { get; private set; }

        public virtual int Delay
        {
            get { return Game.Ping; }
        }

        public bool IsReady()
        {
            return Environment.TickCount > Time + Delay;
        }

        public void SetDelay()
        {
            Time = Environment.TickCount;
        }

        public void RemoveDelay()
        {
            Time = 0;
        }

        public virtual void Draw()
        {
        }

        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}