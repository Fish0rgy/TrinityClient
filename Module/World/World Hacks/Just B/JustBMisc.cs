using UnityEngine;
using UnityEngine.SceneManagement;

namespace Area51.Module.World.World_Hacks.Just_B
{
    static class JustBMisc
    {

        public static void EnterRoom1(string location, float x, float y, float z)
        {
            GetLocalPlayer().transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(x, y, z);
        }
        internal static GameObject GetLocalPlayer()
        {
            foreach (GameObject Player in GetAllGameObjects())
            {
                bool FindPlayer = Player.name.StartsWith("VRCPlayer[Local]");
                if (FindPlayer)
                {
                    return Player;
                }
            }
            return new GameObject();
        }
        internal static GameObject[] GetAllGameObjects()
        {
            return SceneManager.GetActiveScene().GetRootGameObjects();
        }
        /* FORMAT
		 * JustBMisc.RoomEnter("Room 1",-223.7f -11.2f 149.8f);
		 * JustBMisc.RoomEnter("Room 2",-211.2f 55.7f -90.4f);
		 * JustBMisc.RoomEnter("Room 3",-124.6f -11.2f 149.8f);
		 * JustBMisc.RoomEnter("Room 4",-111.2f 55.7f -90.4f);
		 * JustBMisc.RoomEnter("Room 5",-24.2f, -11.4f, 150.7f);
		 * JustBMisc.RoomEnter("Room 6", -11.6f, 55.2f, -90.8f);
		 * Room 7 (VIP = different code lol idk what to do here Position 56.9625 62.8633 -0.5625)
		 */
    }
}
