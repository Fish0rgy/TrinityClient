using Trinity.SDK;
using Trinity.Utilities;
using VRC.DataModel;

namespace Trinity.Module.Player
{
    class HeadFlipper : BaseModule
    {

        public HeadFlipper() : base("HeadFlipper", "Fuck your desktop neck", Main.Instance.PlayerButton, null, true, false) { }
        private NeckRange orgin;

        public override void OnEnable()
        {
            orgin = VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0;
            VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = new NeckRange(float.MinValue, float.MaxValue, 0f);
            MenuUI.Log("PLAYER: <color=green>Head Movement Unlocked</color>");
        }

        public override void OnDisable()
        {
            MenuUI.Log("PLAYER: <color=red>Head Movement Reset</color>");
            VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = orgin;
        }
    }
}
