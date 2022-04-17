using Trinity.SDK;
using Trinity.Utilities;
namespace Trinity.Module.Player
{
    class LoudMic : BaseModule
    {
        public LoudMic() : base("LoudMic", "Microphone Go Brrrr", Main.Instance.PlayerButton, null, true, false) { }

        public override void OnEnable()
        {
            USpeaker.field_Internal_Static_Single_1 = float.MaxValue;
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_512k;
            MenuUI.Log("AUDIO: <color=green>Loud Mic On</color>");

        }

        public override void OnDisable()
        {
            USpeaker.field_Internal_Static_Single_1 = 1;
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_24K;
            MenuUI.Log("AUDIO: <color=red>Loud Mic Off</color>");
        }
    }
}
