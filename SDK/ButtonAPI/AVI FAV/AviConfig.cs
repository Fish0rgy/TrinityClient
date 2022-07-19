using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.SDK.ButtonAPI.AVI_FAV
{
    internal class AviConfig
    {
		internal void SaveAvatarConfig()
		{
			string contents = JsonConvert.SerializeObject(this, (Formatting)1);
			File.WriteAllText("Trinity/AvatarConfig.json", contents);
		}
		 
		internal static AviConfig LoadAvatarFav()
		{
			bool check = !File.Exists("Trinity/AvatarConfig.json");
			AviConfig result;
			if (check)
			{
				result = new AviConfig();
			}
			else
			{
				AviConfig.Instance = JsonConvert.DeserializeObject<AviConfig>(File.ReadAllText("Trinity/AvatarConfig.json"));
				result = AviConfig.Instance;
			}
			return result;
		}
		 

		public static AviConfig Instance;
	}
}
