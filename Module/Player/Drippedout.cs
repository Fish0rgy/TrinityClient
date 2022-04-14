using Trinity.SDK;
using Trinity.SDK.ButtonAPI;

namespace Trinity.Module.Player
{ 
    class DrippedOut : BaseModule
    {
        public DrippedOut() : base("Drippedout", "Don't be a bitch, press it nigga!", Main.Instance.PlayerButton, QMButtonIcons.CreateSpriteFromBase64(Serpent.drippedout), false, false) { }

        public override void OnEnable()
        {
          
                PlayerWrapper.ChangeAvatar("avtr_1c727bf4-77b7-4b20-888b-4b81fc88d37e");
                LogHandler.LogDebug("[Swag] -> Good Job fag, shouldn't of ran that exe ;) jk");
        }
    }
}

