using Trinity.Utilities;
using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Among_Us
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
                LogHandler.Log(LogHandler.Colors.Green, "Found Dead Body Now Reporting", false, false);
                A_Misc.AmongUsMod("OnBodyWasFound");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, $"No Dead Body Found {ex.ToString()}", false, false);
            }
        }
    }
}
