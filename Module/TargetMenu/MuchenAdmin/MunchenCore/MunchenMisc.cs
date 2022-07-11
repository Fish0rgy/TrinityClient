using Newtonsoft.Json;
using System; 
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Trinity.Module.TargetMenu.MuchenAdmin.MunchenCore;

namespace Trinity.Module.TargetMenu.MuchenAdmin
{
    internal class MunchenMisc
	{
		internal static readonly Dictionary<string, bewPwUL7OkVgMwtScoj> PtotrZ6kfh = new Dictionary<string, bewPwUL7OkVgMwtScoj>();
		private static bool qQpfksOMjQ = false;
		private static bool TuWfEpTvlt = false;
		private static UploadQueueConfig XZ4faxREhn;
		public static string uZpWpgK43o;
		public static void ChangePlayerCustomRankNamega(string customRankName, Il2CppSystem.Collections.Generic.List<KeyCode> pressedKeys, Text text)
		{
			if (customRankName.Length > 5000)
			{
				//VAA1ORN8vOf3yAPMNhb.dq3NUfGS9I("Player", "Custom rank too long", ConsoleColor.Gray, "ChangePlayerCustomRankName", 114);
				LogHandler.Popup("Warning", "Custom rank too long");
				return;
			}
			ChangePlayerCustomRankName(uZpWpgK43o, customRankName.Trim());
			if (PtotrZ6kfh.ContainsKey(uZpWpgK43o))
			{
				bewPwUL7OkVgMwtScoj bewPwUL7OkVgMwtScojd = PtotrZ6kfh[uZpWpgK43o];
				PtotrZ6kfh[uZpWpgK43o] = new bewPwUL7OkVgMwtScoj
				{
					QenLFj1bCj = true,
					YB1L3Mm0qM = customRankName.Trim(),
					DioL1XSmOQ = bewPwUL7OkVgMwtScojd.DioL1XSmOQ,
					H3vLvgjjPF = bewPwUL7OkVgMwtScojd.H3vLvgjjPF
				};
			}
			else
			{
				PtotrZ6kfh.Add(uZpWpgK43o, new bewPwUL7OkVgMwtScoj
				{
					QenLFj1bCj = true,
					YB1L3Mm0qM = customRankName.Trim(),
					DioL1XSmOQ = false,
					H3vLvgjjPF = default(Color)
				});
			}
			LogHandler.Popup("Munchen", "Player rank succesfully set");
		}
		public static void ChangePlayerCustomRankName(string playerId, string newCustomRankName)
		{
			TempUploadContainer item = new TempUploadContainer
			{
				wtH9T8kx1 = 3,
				sYneajU5d = playerId,
				ENi8eS5J3 = newCustomRankName
			};
			JJTfRfNFd7().UploadQueue.Enqueue(item);
			SaveUploadQueueConfig(false);
		}

		private void ChangePlayerCustomRankColor(string customRankColor, List<KeyCode> pressedKeys, Text text)
		{
			if (customRankColor.Length > 7)
			{
				//LogHandler.dq3NUfGS9I("Player", "Custom rank color too long", ConsoleColor.Gray, "ChangePlayerCustomRankColor", 154);
				LogHandler.Popup("Warning", "Custom rank color too long");
				return;
			}
			Color color;
			if (!ColorUtility.TryParseHtmlString(customRankColor.Trim(),out color))
			{
				LogHandler.Popup("Warning", "Color is invalid");
				return;
			}
			ChangePlayerCustomRankColor(uZpWpgK43o, customRankColor.Trim());
			if (PtotrZ6kfh.ContainsKey(uZpWpgK43o))
			{
				bewPwUL7OkVgMwtScoj bewPwUL7OkVgMwtScoj = PtotrZ6kfh[uZpWpgK43o];
				PtotrZ6kfh[uZpWpgK43o] = new bewPwUL7OkVgMwtScoj
				{
					QenLFj1bCj = bewPwUL7OkVgMwtScoj.QenLFj1bCj,
					YB1L3Mm0qM = bewPwUL7OkVgMwtScoj.YB1L3Mm0qM,
					DioL1XSmOQ = true,
					H3vLvgjjPF = color
				};
			}
			else
			{
				PtotrZ6kfh.Add(uZpWpgK43o, new bewPwUL7OkVgMwtScoj
				{
					QenLFj1bCj = false,
					YB1L3Mm0qM = string.Empty,
					DioL1XSmOQ = true,
					H3vLvgjjPF = color
				});
			}
			LogHandler.Popup("Munchen","Player rank color succesfully set");
		}

		private void RemoveCustomRank()
		{
			CNO0G7JhI(uZpWpgK43o);
			if (PtotrZ6kfh.ContainsKey(uZpWpgK43o))
			{
				bewPwUL7OkVgMwtScoj bewPwUL7OkVgMwtScoj = PtotrZ6kfh[uZpWpgK43o];
				PtotrZ6kfh[uZpWpgK43o] = new bewPwUL7OkVgMwtScoj
				{
					QenLFj1bCj = false,
					YB1L3Mm0qM = string.Empty,
					DioL1XSmOQ = bewPwUL7OkVgMwtScoj.DioL1XSmOQ,
					H3vLvgjjPF = bewPwUL7OkVgMwtScoj.H3vLvgjjPF
				};
				LogHandler.Popup("Munchen","Player rank succesfully removed");
				return;
			}
			LogHandler.Popup("Warning", "Player doesn't have custom rank");
		}
		private void RemoveCustomColor()
		{
			rm8axgFAT(uZpWpgK43o);
			if (!PtotrZ6kfh.ContainsKey(uZpWpgK43o))
			{
				LogHandler.Popup("Warning", "Player doesn't have custom rank color");
				return;
			}
			bewPwUL7OkVgMwtScoj bewPwUL7OkVgMwtScoj = PtotrZ6kfh[uZpWpgK43o];
			PtotrZ6kfh[uZpWpgK43o] = new bewPwUL7OkVgMwtScoj
			{
				QenLFj1bCj = bewPwUL7OkVgMwtScoj.QenLFj1bCj,
				YB1L3Mm0qM = bewPwUL7OkVgMwtScoj.YB1L3Mm0qM,
				DioL1XSmOQ = false,
				H3vLvgjjPF = default(Color)
			};
			LogHandler.Popup("Munchen","Player rank color succesfully removed");
		}



















		 

		internal void rm8axgFAT(string gat)
		{
			TempUploadContainer item = new TempUploadContainer
			{
				wtH9T8kx1 = 6,
				sYneajU5d = gat
			};
			JJTfRfNFd7().UploadQueue.Enqueue(item);
			SaveUploadQueueConfig(false);
		}
		public static void CNO0G7JhI(string gay)
		{
			TempUploadContainer item = new TempUploadContainer
			{
				wtH9T8kx1 = 5,
				sYneajU5d = gay
			};
			JJTfRfNFd7().UploadQueue.Enqueue(item);
			SaveUploadQueueConfig(false);
		}
		// Token: 0x0600001B RID: 27 RVA: 0x00004714 File Offset: 0x00002914
		internal void ChangePlayerCustomRankColor(string playerId, string newCustomRankColor)
		{
			TempUploadContainer item = new TempUploadContainer
			{
				wtH9T8kx1 = 4,
				sYneajU5d = playerId,
				nbMBinOyk = newCustomRankColor
			};
			JJTfRfNFd7().UploadQueue.Enqueue(item); 
		}
		internal static UploadQueueConfig JJTfRfNFd7()
		{
			if (XZ4faxREhn == null)
			{
				klBfoHPUiA();
			}
			return XZ4faxREhn;
		}
		internal static void klBfoHPUiA()
		{
			 
			XZ4faxREhn = new UploadQueueConfig();
			SaveUploadQueueConfig(false);
		}
		internal static readonly JsonSerializerSettings mLCftAss4N = new JsonSerializerSettings
		{
			ReferenceLoopHandling = (ReferenceLoopHandling)1,
			ContractResolver = new JsonParserForConfig("normalized")
		};
		internal static void SaveUploadQueueConfig(bool forceSave = false)
		{
			 
			if (!TuWfEpTvlt || forceSave)
			{
				qQpfksOMjQ = false;
				TuWfEpTvlt = true; 
				try
				{
					File.WriteAllText(UploadQueueConfig.ConfigLocation, JsonConvert.SerializeObject(XZ4faxREhn, (Formatting)1, mLCftAss4N));
				}
				catch (Exception e2)
				{
					//VAA1ORN8vOf3yAPMNhb.Exception("Config", "SaveUploadQueueConfig - Save", e2, "SaveUploadQueueConfig", 736);
				}
				TuWfEpTvlt = false;
				return;
			}
			qQpfksOMjQ = true;
		}












		public static void SetPlayerRank()
		{
			Bssd1xLohZE2831sTyO player = cRMLef3MSSFd6H8SHap.VKo392tTsZ();
			uZpWpgK43o = player.kCuLxF0EwR;
			Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) =>
			{
				MunchenMisc.ChangePlayerCustomRankNamega(str, l, txt);

			});
			UIU.OpenKeyboardPopup("Change Munchen Rank", "Enter Rank...", keyboardAction);
		}
		public static void RemoveRank()
		{
			Bssd1xLohZE2831sTyO player = cRMLef3MSSFd6H8SHap.VKo392tTsZ();
			if (player != null)
			{
				uZpWpgK43o = (string)player.kCuLxF0EwR;
				Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) => 
				{ 
					 if(str == "yes" || str == "Yes" || str == "YES")
                    { 
							CNO0G7JhI(uZpWpgK43o);
							if (PtotrZ6kfh.ContainsKey(uZpWpgK43o))
							{
								bewPwUL7OkVgMwtScoj bewPwUL7OkVgMwtScoj = PtotrZ6kfh[uZpWpgK43o];
								PtotrZ6kfh[uZpWpgK43o] = new bewPwUL7OkVgMwtScoj
								{
									QenLFj1bCj = false,
									YB1L3Mm0qM = string.Empty,
									DioL1XSmOQ = bewPwUL7OkVgMwtScoj.DioL1XSmOQ,
									H3vLvgjjPF = bewPwUL7OkVgMwtScoj.H3vLvgjjPF
								};
								LogHandler.Popup("Munchen","Player rank succesfully removed");
								return;
							}
						LogHandler.Popup("Warning", "Player doesn't have custom rank"); 
					} 
				});
				UIU.OpenKeyboardPopup("Change Munchen Rank", "Enter Rank...", keyboardAction);
				return;
			}
            else
			{
				LogHandler.Popup("Warning", "Player not found");
			} 
		}
	}
}
