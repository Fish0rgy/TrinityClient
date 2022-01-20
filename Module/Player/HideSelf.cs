using Area51.SDK;
using UnityEngine;
using VRC.Core;


namespace Area51.Module.Exploit
{
    class HideSelf : BaseModule
    {

        public HideSelf() : base("Hide Self", "Unloads your Avatar", Main.Instance.PlayerButton, null, true) { }
        public override void OnDisable()
        {
            PlayerWrapper.ClearAssets();
            PlayerWrapper.ShowSelf(true);
        }
        public override void OnEnable()
        {
            PlayerWrapper.ShowSelf(false);
        }
    }
}