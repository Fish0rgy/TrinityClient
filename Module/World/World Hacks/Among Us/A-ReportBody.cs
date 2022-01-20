using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Among_Us
{
    class A_ReportBody : BaseModule
    {
        public A_ReportBody() : base("Report Body", "", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Found Dead Body Now Reporting", false, false);
                A_Misc.AmongUsMod("OnBodyWasFound");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, $"No Dead Body Found {ex.ToString()}", false, false);
            }
        }
    }
}
