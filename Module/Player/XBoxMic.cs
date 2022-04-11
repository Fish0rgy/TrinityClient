namespace Area51.Module.Player
{
    class XBoxMicd : BaseModule
    {
        public XBoxMicd() : base("XboxMic", "1v1 in COD bro", Main.Instance.PlayerButton, null, true, false) { }



        public override void OnEnable()
        {
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_8K;
        }

        public override void OnDisable()
        {

            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_24K;
        }


    }
}
