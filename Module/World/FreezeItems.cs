using Area51.Events;
using Area51.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.SDKBase;

namespace Area51.Module.World
{
    class FreezeItems : BaseModule, OnUpdateEvent
	{
		public FreezeItems() : base("FreezePickups", "No one besides you can use Pickups", Main.Instance.WorldButton, null, true, true)
        {
		}

		public override void OnEnable()
		{
			Main.Instance.OnUpdateEvents.Add(this);
		}

		public override void OnDisable()
		{
			Main.Instance.OnUpdateEvents.Remove(this);
		}

		public void OnUpdate()
		{
			for (int i = 0; i < WorldWrapper.vrc_Pickups.Length; i++)
			{
				Networking.SetOwner(Networking.LocalPlayer, WorldWrapper.vrc_Pickups[i].gameObject);
			}
		}
	}
}
