using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK.Patching;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;

namespace Trinity.SDK.ButtonAPI
{
    internal class QMActionMenu
	{
		private static List<QMActionMenu.Page> customPages = new List<QMActionMenu.Page>();
		private static List<QMActionMenu.Button> mainMenuButtons = new List<QMActionMenu.Button>();
		public static ActionMenu activeActionMenu;
		public static Texture2D ToggleOffTexture;
		public static Texture2D ToggleOnTexture;
		public enum BaseMenu
		{
			MainMenu = 1
		}
		public class Page
		{
			public Page(QMActionMenu.BaseMenu baseMenu, string buttonText, Texture2D buttonIcon = null)
			{
				if (baseMenu == QMActionMenu.BaseMenu.MainMenu)
				{
					this.menuEntryButton = new QMActionMenu.Button(QMActionMenu.BaseMenu.MainMenu, buttonText, delegate ()
					{
						this.OpenMenu(null);
					}, buttonIcon);
				}
			}
			//public Page(QMActionMenu.Page basePage, string buttonText, Texture2D buttonIcon = null)
			//{
			//	QMActionMenu.Page <> 4__this = this;
			//	this.previousPage = basePage;
			//	this.menuEntryButton = new QMActionMenu.Button(this.previousPage, buttonText, delegate ()
			//	{
			//		<> 4__this.OpenMenu(basePage);
			//	}, buttonIcon);
			//}
			public void OpenMenu(QMActionMenu.Page currentPage)
			{
    //            QMActionMenu.GetActionMenuOpener().field_Public_ActionMenu_0.Method_Public_Page_Action_Action_Texture2D_String_0(delegate ()
				//{
				//	//using (List<Button>.Enumerator enumerator = this.buttons.GetEnumerator())
				//	//{
				//	//	while (enumerator.MoveNext())
				//	//	{
				//	//		QMActionMenu.Button btn = enumerator.Current;
				//	//		if (!btn.IsVisible)
				//	//		{
				//	//			break;
				//	//		}
				//	//		PedalOption pedalOption = QMActionMenu.GetActionMenuOpener().field_Public_ActionMenu_0.Method_Private_PedalOption_0();
				//	//		pedalOption.Method_Public_set_Void_String_0(btn.ButtonText);
				//	//		Func<bool> func = delegate ()
				//	//									   {
				//	//										   btn.ButtonAction();
				//	//										   return true;
				//	//									   };
				//	//		pedalOption.field_Public_Func_1_Boolean_0 = func;
				//	//		pedalOption.Method_Public_set_Void_Boolean_0(btn.IsEnabled);
				//	//		if (btn.ButtonIcon != null)
				//	//		{
				//	//			pedalOption.Method_Public_set_Void_Texture2D_0(btn.ButtonIcon);
				//	//		}
				//	//		btn.currentPedalOption = pedalOption;
				//	//	}
				//	//}
				//}, null, null, null);
			}
			public List<QMActionMenu.Button> buttons = new List<QMActionMenu.Button>();
			public QMActionMenu.Page previousPage;
			public QMActionMenu.Button menuEntryButton;
		}
		public class Button
		{
			public Button(QMActionMenu.BaseMenu baseMenu, string buttonText, Action buttonAction, Texture2D buttonIcon = null)
			{
				this.ButtonText = buttonText;
				this.ButtonAction = buttonAction;
				this.ButtonIcon = buttonIcon;
				if (baseMenu == QMActionMenu.BaseMenu.MainMenu)
				{
					QMActionMenu.mainMenuButtons.Add(this);
				}
			}
			public Button(QMActionMenu.Page basePage, string buttonText, Action buttonAction, Texture2D buttonIcon = null)
			{
				this.ButtonText = buttonText;
				this.ButtonAction = buttonAction;
				this.ButtonIcon = buttonIcon;
				basePage.buttons.Add(this);
			}
			public void SetButtonText(string newText)
			{
				//this.ButtonText = newText;
				//if (this.currentPedalOption != null)
				//{
				//	this.currentPedalOption.Method_Public_set_Void_String_0(newText);
				//}
			}
			public void SetIcon(Texture2D newTexture)
			{
				//this.ButtonIcon = newTexture;
				//if (this.currentPedalOption != null)
				//{
				//	this.currentPedalOption.Method_Public_set_Void_Texture2D_0(newTexture);
				//}
			}
			public void SetEnabled(bool enabled)
			{
				//this.IsEnabled = enabled;
				//if (this.currentPedalOption != null)
				//{
				//	this.currentPedalOption.Method_Public_set_Void_Boolean_0(!enabled);
				//}
			}
			public void SetVisible(bool visible)
			{
				this.IsVisible = visible;
			}
			public string ButtonText;
			public bool IsEnabled;
			public bool IsVisible = true;
			public Action ButtonAction;
			public Texture2D ButtonIcon;
			public PedalOption currentPedalOption;
		}
		public static void OnUiManagerInit()
		{
			SerpentPatch.Instance.Patch(typeof(ActionMenu).GetMethods().FirstOrDefault((MethodInfo it) => XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
			{
				if (jt.Type == null)
				{
					UnityEngine.Object @object = (UnityEngine.Object)jt.ReadAsObject();
					return ((@object != null) ? @object.ToString() : null) == "Emojis";
				}
				return false;
			})), null, new HarmonyMethod(typeof(QMActionMenu).GetMethod("OpenMainPage", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null);
			QMActionMenu.ToggleOffTexture = Resources.Load<Texture2D>("GUI_Toggle_OFF");
			QMActionMenu.ToggleOnTexture = Resources.Load<Texture2D>("GUI_Toggle_ON");
		}
		private static ActionMenuOpener GetActionMenuOpener()
		{
			if (!ActionMenuDriver.field_Public_Static_ActionMenuDriver_0.field_Public_ActionMenuOpener_0.field_Private_Boolean_0 && ActionMenuDriver.field_Public_Static_ActionMenuDriver_0.field_Public_ActionMenuOpener_1.field_Private_Boolean_0)
			{
				return ActionMenuDriver.field_Public_Static_ActionMenuDriver_0.field_Public_ActionMenuOpener_1;
			}
			if (ActionMenuDriver.field_Public_Static_ActionMenuDriver_0.field_Public_ActionMenuOpener_0.field_Private_Boolean_0 && !ActionMenuDriver.field_Public_Static_ActionMenuDriver_0.field_Public_ActionMenuOpener_1.field_Private_Boolean_0)
			{
				return ActionMenuDriver.field_Public_Static_ActionMenuDriver_0.field_Public_ActionMenuOpener_0;
			}
			return null;
		}
	}
}
