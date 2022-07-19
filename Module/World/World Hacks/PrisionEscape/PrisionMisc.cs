using Trinity.Utilities;
using Trinity.SDK;

using UnityEngine;
using VRC.Udon;

namespace Trinity.Module.World.World_Hacks.ZombieTag
{
    class PrisionMisc
    {
        public static bool Check()
        { 
            string WORLDID = "";
            return RoomManager.Method_Public_Static_String_0().Contains(WORLDID); //Method_Internal_Static_String_0
        }
        public static void ZombieMod(string udonevent)
        {
            bool CheckWorldID = Check();
            if (CheckWorldID)
            {
                foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    bool GameLogic = gameObject.name.Contains("Game Logic");
                    if (GameLogic)
                    {
                        gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, udonevent);
                    }
                }
            }
        } 
    }
}
/*
 * MakeHuman
 * MakeZombie
 * PlayerTagged
 * OpenDoorInBox
 * CloseDoor
 * GameStatePlaying
 * OpenDoorInBox
 * OpenDoor
 * QuitGame
 * HumansWin
 * EndGameLocal
 * ResetGame
 * StartGame
 * ZombiesWin
 * GameStateSetup
 * UnlockZombieDoors
 * ZombieLeft
 * */
