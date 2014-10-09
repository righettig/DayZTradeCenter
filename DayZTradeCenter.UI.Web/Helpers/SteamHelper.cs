using System.Linq;
using PortableSteam;

namespace DayZTradeCenter.UI.Web.Helpers
{
    public static class SteamHelper
    {
        private const int DayZAppId = 221100;

        public static bool DoIHaveDayZ(long steamId)
        {
            SteamWebAPI.SetGlobalKey("26DF222A39656E4F4B38BA61B7E57744");

            var me =
                SteamIdentity.FromSteamID(steamId);

            var myGames =
                SteamWebAPI
                    .General()
                    .IPlayerService()
                    .GetOwnedGames(me)
                    .GetResponse().Data
                    .Games;

            if (myGames != null && myGames.Any(g => g.AppID == DayZAppId))
            {
                return true;
            }

            return false;
        }
    }
}