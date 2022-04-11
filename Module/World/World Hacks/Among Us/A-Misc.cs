using UnityEngine;
using VRC.Udon;

namespace Area51.Module.World.World_Hacks.Among_Us
{
    class A_Misc
    {
        public static bool Check()
        {
            string WORLDID = "";
            return RoomManager.Method_Public_Static_String_0().Contains(WORLDID);
        }
        public static void AmongUsMod(string udonevent)
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
 * OnLocalPlayerCompletedTask = complete all task
 * SyncCloseVoting = close voting
 * OnBodyWasFound = report body
 * Btn_SkipVoting = skip voting
 * StartMeeting = call meeting
 * Btn_Start = start game
 * SyncAbort = abort game
 * SyncVictoryM = murder win
 * SyncVictoryB = bystanders win
 * KillLocalPlayer = kill all 
 */