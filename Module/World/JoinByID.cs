using Area51.SDK;

namespace Area51.Module.World
{
    class JoinByID : BaseModule
    {
        public JoinByID() : base("Force Join", "Make Sure To Copy A World ID To Your Clipboard Before Clicking", Main.Instance.WorldButton, SDK.ButtonAPI.QMButtonIcons.CreateSpriteFromBase64(Alien.rocket), false, false) { }

        public override void OnEnable()
        {
            string Negro = Misc.GetClipboard();
            string[] Nigger = Negro.Split(new char[] { ':' }); bool fat = Nigger.Length != 2;
            VRCFlowManager.prop_VRCFlowManager_0.Method_Public_Void_String_WorldTransitionInfo_Action_1_String_Boolean_0(Nigger[0] + ":" + Nigger[1]);
        }
    }
}
