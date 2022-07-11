using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Module.TargetMenu.MuchenAdmin
{
    internal class UploadQueueConfig
    {
		[JsonIgnore]
		public static readonly string ConfigLocation = "MünchenClient\\Config\\UploadQueue.json";

		// Token: 0x040008C2 RID: 2242
		public int ConfigVersion = 1;

		// Token: 0x040008C3 RID: 2243
		public Queue<TempUploadContainer> UploadQueue = new Queue<TempUploadContainer>();
	}
}
