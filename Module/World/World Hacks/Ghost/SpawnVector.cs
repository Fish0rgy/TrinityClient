using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Module.World.World_Hacks.Ghost
{
    internal class SpawnVector : BaseModule
    {
        public SpawnVector() : base("Spawn Vector", "", Main.Instance.GhostButton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Killed Everyone", false, false);
                //GhostMisc.spawnObject("Reward_Vector");
                //GhostMisc.SpawnVectorHouse();
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
