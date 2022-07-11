using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Utilities;
using UnityEngine;
using VRC.Core;
using VRC.DataModel.Core;
using VRC.UI.Elements.Menus;

namespace Trinity.Module.TargetMenu.MuchenAdmin.MunchenCore
{
    internal class cRMLef3MSSFd6H8SHap
	{
		private static SelectedUserMenuQM Aeq3WfwEBo;
		internal string kCuLxF0EwR;
		internal static Bssd1xLohZE2831sTyO VKo392tTsZ()
		{
			APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
			return cRMLef3MSSFd6H8SHap.BBM38OocI1(SelectedPlayer.id);
        }
		internal static Bssd1xLohZE2831sTyO BBM38OocI1(object gay)
		{
			APIUser currentUser = APIUser.CurrentUser;
			if (gay == ((currentUser == null) ? null : currentUser.id))
			{
				return cRMLef3MSSFd6H8SHap.cTq3GjpIr1();
			}
			if (uCw26LLyNN5g4c6ce5J.OuFLzOjp4w.Count == 0)
			{
				return null;
			}
			foreach (KeyValuePair<string, Bssd1xLohZE2831sTyO> keyValuePair in uCw26LLyNN5g4c6ce5J.OuFLzOjp4w)
			{
				if (keyValuePair.Value.kCuLxF0EwR == gay)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}
		internal static Bssd1xLohZE2831sTyO cTq3GjpIr1()
		{
			if (uCw26LLyNN5g4c6ce5J.RFmtFdaSKn != null)
			{
				return uCw26LLyNN5g4c6ce5J.RFmtFdaSKn;
			}
			if (APIUser.CurrentUser != null && uCw26LLyNN5g4c6ce5J.OuFLzOjp4w.ContainsKey(APIUser.CurrentUser.displayName))
			{
				uCw26LLyNN5g4c6ce5J.RFmtFdaSKn = uCw26LLyNN5g4c6ce5J.OuFLzOjp4w[APIUser.CurrentUser.displayName];
				return uCw26LLyNN5g4c6ce5J.OuFLzOjp4w[APIUser.CurrentUser.displayName];
			}
			return null;
		}
		internal static SelectedUserMenuQM vgnFg8WE6F() => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local").GetComponent<SelectedUserMenuQM>();
	}
}
