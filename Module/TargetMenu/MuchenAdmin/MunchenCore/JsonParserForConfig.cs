using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.TargetMenu.MuchenAdmin
{
    internal class JsonParserForConfig : DefaultContractResolver
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x000336D0 File Offset: 0x000318D0
		internal JsonParserForConfig(string gat)
		{
			this.duBcqS761V = gat;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000299C File Offset: 0x00000B9C
		protected override IList<JsonProperty> CreateProperties(Type gaty, MemberSerialization gay2)
		{
			return base.CreateProperties(gaty, gay2).Where(new Func<JsonProperty, bool>(this.RLQcb23lCr)).ToList<JsonProperty>();
		} 
		private bool RLQcb23lCr(JsonProperty gaty)
		{
			return string.Compare(gaty.PropertyName, this.duBcqS761V, StringComparison.OrdinalIgnoreCase) != 0;
		}

		// Token: 0x04000511 RID: 1297
		private readonly string duBcqS761V;
	}
}
