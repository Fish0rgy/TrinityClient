using System.Collections.Generic;
using System.Collections;
using System.Linq;
using VRC.Management;
using VRC.SDKBase;
using VRC.Core;
using VRC;
using UnityEngine;
using MelonLoader;

namespace Trinity.Utilities
{
    public enum PlayerRankStatus : short
    {
        RANK_UNKNOWN = 0,
        RANK_LOCAL = 1,
        RANK_VISITOR = 2,
        RANK_NEWUSER = 3,
        RANK_USER = 4,
        RANK_KNOWN = 5,
        RANK_TRUSTED = 6,
        RANK_VETERAN = 7, 
        RANK_VRCHATTEAM = 8,
        RANK_FRIEND = 9
    } 

    internal enum TrustRanks
    {
        IGNORE,
        VETERAN,
        LEGENDARY,
        VRCHATTEAM
    }

    internal enum CloneType : short
    {
        CLONE_UNKNOWN = 0,
        CLONE_SKELETON = 1,
        CLONE_AVATAR = 2
    }
    internal class CustomRankInfo
    {
        internal bool customRankEnabled;
        internal string customRank;

        internal bool customRankColorEnabled;
        internal Color customRankColor;
    };
    public class PI
    {
        internal string id;
        internal string displayName;
        internal bool isLocalPlayer;
        internal bool isInstanceMaster;
        internal bool isVRChatStaff;
        internal bool isLegendaryUser;
        internal bool isVRUser;
        internal bool isQuestUser;

        internal bool blockedLocalPlayer;

        internal PlayerRankStatus rankStatus;

        private Renderer avatarRendererCache;

        internal Player player;
        internal VRCPlayerApi playerApi;
        internal VRCPlayer vrcPlayer;
        internal APIUser apiUser;
        internal VRCNetworkBehaviour networkBehaviour;

        internal int lastNetworkedUpdatePacketNumber;
        internal float lastNetworkedUpdateTime;
        internal int lagBarrier;

        internal GameObject customNameplateObject;
        internal RectTransform customNameplateTransform;
        internal TMPro.TextMeshProUGUI customNameplateText;
    }
    internal class PlayerUtils
    {
        internal static readonly Color vrchatTeamColor = new Color(0.2588235294117647f, 0.9607843137254902f, 0.9372549019607843f);
        internal static readonly Color legendaryUserColor = new Color(1.0f, 0.0f, 0.0f);
        internal static readonly Color veteranUserColor = new Color(1.0f, 0.4117647058823529f, 0.7058823529411765f);
        internal static readonly Color trustedUserColor = new Color(0.5058824f, 0.2627451f, 0.9019608f);
        internal static readonly Color knownUserColor = new Color(0.7764705882352941f, 0.5176470588235294f, 0.3882352941176471f);
        internal static readonly Color userUserColor = new Color(0.0666666666666667f, 0.792156862745098f, 0.5686274509803922f);
        internal static readonly Color newuserUserColor = new Color(0.0823529411764706f, 0.4666666666666667f, 0.992156862745098f);
        internal static readonly Color visitorUserColor = new Color(0.792156862745098f, 0.792156862745098f, 0.792156862745098f);

        internal static readonly Dictionary<string, PI> playerCachingList = new Dictionary<string, PI>();
        internal static readonly Dictionary<string, CustomRankInfo> playerCustomRank = new Dictionary<string, CustomRankInfo>();
        internal static readonly Dictionary<string, Color> playerColorCache = new Dictionary<string, Color>(); 

        internal static readonly Dictionary<string, GameObject> playerCloneList = new Dictionary<string, GameObject>();

        internal static PI localPlayerInfo = null;
        private static VRC_AnimationController playerAnimController;
        private static VRCVrIkController playerIkController;
        private static float capsuleHiderOffsetReset = 0.0f;

        private static bool refreshingPlayerColorCache = false;


        private static IEnumerator RefreshAllPlayerColorCacheEnumerator()
        {
            foreach (KeyValuePair<string, PI> playerInfo in playerCachingList.ToList())
            {
                playerColorCache[playerInfo.Value.displayName] = VRCPlayer.Method_Public_Static_Color_APIUser_0(playerInfo.Value.apiUser);

                yield return new WaitForEndOfFrame();
            }

            refreshingPlayerColorCache = false;

            yield break;
        }


        private static IEnumerator ToggleIKControllerEnumerator(bool state)
        {
            yield return new WaitForSeconds(2f);

            playerIkController.field_Private_Boolean_0 = !state;
        }

        internal static bool IsPlayerAvatarHidden(string id)
        {
            if (ModerationManager.prop_ModerationManager_0.field_Private_Dictionary_2_String_List_1_ApiPlayerModeration_0.ContainsKey(id) == false)
            {
                return false;
            }

            foreach (ApiPlayerModeration moderation in ModerationManager.prop_ModerationManager_0.field_Private_Dictionary_2_String_List_1_ApiPlayerModeration_0[id])
            {
                if (moderation.moderationType == ApiPlayerModeration.ModerationType.HideAvatar)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
