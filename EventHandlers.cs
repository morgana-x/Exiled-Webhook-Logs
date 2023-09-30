using Exiled.API.Extensions;
using Exiled.Events.EventArgs;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using System.Text.Json.Nodes;
using Exiled.API.Features;
using PluginAPI;
using System;
using System.Linq;
using Exiled.Events.EventArgs.Map;

namespace SCPDiscordWebhookModerationBasic
{

    public class EventHandlers
    {
        /*public string getProfileImage(Player pl)
        {
            return "https://static.wikia.nocookie.net/scp-secret-laboratory-official/images/5/51/MTF.PNG/revision/latest?cb=20200910084106";
        }
        public string embedImage(string  url )
        {
            return "![p](" + url + ")";
        }

        public string formatPlayer(Player pl, string value = "", bool inline = false)
        {
            string playerString = Plugin.Instance.Config.playerThingStructure;
            playerString = playerString.Replace("{name}", embedImage(getProfileImage(pl)) + pl.DisplayNickname);
            playerString = playerString.Replace("{steamid}", pl.RawUserId);
            playerString = playerString.Replace("{value}", value);
            playerString = playerString.Replace("{inline}", inline.ToString());
            return playerString;
        }*/

        public string formatPlayer(Player pl)
        {
            return pl.DisplayNickname + "(" + pl.UserId + ")(" + pl.Role.Name + ")";
        }
      
        public void OnJoined(JoinedEventArgs ev)
        {
   
        }
        public void OnLeft(LeftEventArgs ev)
        {
            string translated = Plugin.Instance.Config.translations.player.Left;
            translated = translated.Replace("{player}", formatPlayer(ev.Player));
            Plugin.Instance.SendWebHookBasic( Plugin.Instance.Config.webhookGeneral, ev.Player.DisplayNickname + "(" + ev.Player.UserId + ")(" + ev.Player.Role.Name + ") Left the game!");
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;
            string translated = Plugin.Instance.Config.translations.player.Escaped;
            translated = translated.Replace("{player}", formatPlayer(ev.Player));
            translated = translated.Replace("{newrole}", ev.Player.Role.Name);
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, translated);
        }
        public void OnKicked(KickedEventArgs ev)
        {
            string translated = Plugin.Instance.Config.translations.player.Kicked;
            translated = translated.Replace("{player}", formatPlayer(ev.Player));
            translated = translated.Replace("{reason}", ev.Reason);
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookModeration, translated);
        }
        public void OnSpawned(SpawnedEventArgs ev)
        {
            
        }

        public void OnHandcuffing(HandcuffingEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;
            string translated = Plugin.Instance.Config.translations.player.HandCuffed;
            translated = translated.Replace("{victim}", formatPlayer(ev.Target));
            translated = translated.Replace("{attacker}", formatPlayer(ev.Player));
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, translated);
        }
        public void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;
            string translated = Plugin.Instance.Config.translations.player.RemovedHandcuffs;
            translated = translated.Replace("{victim}", formatPlayer(ev.Target));
            translated = translated.Replace("{attacker}", formatPlayer(ev.Player));
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, translated);
        }
        public void OnBanned(BannedEventArgs ev)
        {
            string translated = Plugin.Instance.Config.translations.player.Banned;
            translated = translated.Replace("{player}", formatPlayer(ev.Target));
            translated = translated.Replace("{admin}", formatPlayer(ev.Player));
            translated = translated.Replace("{type}", ev.Type.ToString());
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookModeration, translated);
        }
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.IsAllowed && ev.Reason == Exiled.API.Enums.SpawnReason.Revived && ev.NewRole == PlayerRoles.RoleTypeId.Scp0492)
            {
                string translated = Plugin.Instance.Config.translations.player.Revived;
                translated = translated.Replace("{player}", formatPlayer(ev.Player));
                Player doctor = Player.List.Where(x => (x.Role.Type == PlayerRoles.RoleTypeId.Scp0492)).FirstOrDefault();
                if (doctor == null)
                    doctor = ev.Player;
                translated = translated.Replace("{doctor}", formatPlayer(doctor));
                Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, translated);
            }
            //Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhook_General, ev.Player.DisplayNickname + "(" + ev.Player.Role.Name + ") role is changing to " + ev.NewRole.GetFullName());
        }
        public void OnDying(DyingEventArgs ev)
        {
           
            if (ev.Attacker != null && ev.Attacker != ev.Player)
            {
                string translatedteamkill = Plugin.Instance.Config.translations.player.Killed;
                translatedteamkill = translatedteamkill.Replace("{victim}", formatPlayer(ev.Player));
                translatedteamkill = translatedteamkill.Replace("{attacker}", formatPlayer(ev.Attacker));
                translatedteamkill = translatedteamkill.Replace("{damagetype}", ev.DamageHandler.Type.ToString());
                
                Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, translatedteamkill);
                if (ev.Attacker.Role.Side == ev.Player.Role.Side)
                {
                    Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookTeamkill, translatedteamkill);
                }
                return;
            }
            string translated = Plugin.Instance.Config.translations.player.Died;
            translated = translated.Replace("{victim}", formatPlayer(ev.Player));
            translated = translated.Replace("{attacker}", formatPlayer(ev.Attacker));
            translated = translated.Replace("{damagetype}", ev.DamageHandler.Type.ToString());
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, translated);
        }
        public void OnRoundStarted()
        {
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, Plugin.Instance.Config.translations.server.RoundStarted);
        }
      

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            string translated = Plugin.Instance.Config.translations.server.RoundEnded;
            translated = translated.Replace("{winner}", ev.LeadingTeam.ToString());
            translated = translated.Replace("{roundtime}", Round.ElapsedTime.TotalMinutes.ToString() + " minutes");
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, translated);
        }

        public void OnWarheadDetonated()
        {
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, Plugin.Instance.Config.translations.server.WarheadDetonated);
        }
        public void OnRestartingRound()
        {
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, Plugin.Instance.Config.translations.server.RoundRestarting);
        }
        public void OnWaitingForPlayers()
        {
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, Plugin.Instance.Config.translations.server.WaitingForPlayers);
        }
        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            string translated = Plugin.Instance.Config.translations.server.TeamRespawning;
            translated = translated.Replace("{team}", ev.NextKnownTeam.ToString());
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, translated);
        }
        public void OnVerified(VerifiedEventArgs ev)
        {
            string translated = Plugin.Instance.Config.translations.player.Joined;
            translated = translated.Replace("{player}", formatPlayer(ev.Player));
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, translated);
        }
        public void Decontaminating(DecontaminatingEventArgs ev)
        {
            if (!ev.IsAllowed)
            {
                return;
            }
            

            string translated = Plugin.Instance.Config.translations.server.Decontaminating;
            Plugin.Instance.SendWebHookBasic(Plugin.Instance.Config.webhookGeneral, translated);
        }
    }
}