using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.MovieAndChill
{
    class TargetMCTeleport : BaseModule
    {
        public TargetMCTeleport() : base("Respawn Loop", "Targeted Teleport", Main.Instance.MoveAndChillSettings, QMButtonIcons.CreateSpriteFromBase64(Serpent.clientLogo), true, true) { }
        public override void OnEnable()
        {
            try
            {
                MelonLoader.MelonCoroutines.Start(gay());
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Force Respawned {SelectedPlayer.displayName}", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Respawn Loop");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
        public override void OnDisable()
        {
            MelonLoader.MelonCoroutines.Stop(gay());
        }
        IEnumerator gay()
        {
            while (toggled)
            {
                UdonExploitManager.ObjectEvent("Door Room 1 OPEN", "Teleport", 1);
                yield return new WaitForSecondsRealtime(0.2f);
            }
            yield break;
        }
    }
}
