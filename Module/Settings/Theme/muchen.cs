using Area51.Events;
using System.Threading.Tasks;
using UnityEngine;

namespace Area51.Module.Settings.Theme
{
    class munchen : BaseModule, OnUpdateEvent
    {
        public munchen() : base("Hide Tab", "Hides/Shows Munchen", Main.Instance.SettingsButtonTheme, null, true, false) { }

        public override void OnEnable()
        {
            Main.Instance.OnUpdateEvents.Add(this);   
        }
        public override void OnDisable()
        {
            Main.Instance.OnUpdateEvents.Remove(this);
            GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_MünchenClientMünchenClient2/").active = true;

        }

        public void OnUpdate()
        {
            try
            {
                GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_MünchenClientMünchenClient2/").active = false;
            }
            catch
            {

            }
        }
    }
}

