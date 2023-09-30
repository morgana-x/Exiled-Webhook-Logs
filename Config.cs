using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace SCPDiscordWebhookModerationBasic
{
    public class TranslationsPlayer
    {
        public string Joined { get; set; } = "{player} has joined the game!";
        public string Left { get; set; } = "{player} has left the server!";
        public string Kicked { get; set; } = "{player} was kicked from the server! Reason: {reason}";
        public string Banned { get; set; } = "{player} was banned from the server! Type: {type}";
        public string Escaped { get; set; } = "{player} has escaped and became {newrole}";

        public string Killed { get; set; } = "{victim} was killed by {attacker} Damagetype: {damagetype}";

        public string Died { get; set; } = "{victim} Died! Damagetype: {damagetype}";

        public string HandCuffed { get; set; } = "{victim} was handcuffed by {attacker}";

        public string RemovedHandcuffs { get; set; } = "{attacker} removed {victim}'s handcuffs";

        public string Revived { get; set; } = "{player} was revived as a zombie by {doctor}!";
    }
    public class TranslationsServer
    {
        public string WaitingForPlayers { get; set; } = "Waiting for players";
        public string RoundStarted { get; set; } = "A new round has started!";
        public string RoundEnded { get; set; } = "Round ended! Winner: {winner} Roundtime:{roundtime}";
        public string RoundRestarting { get; set; } = "Round is restarting";
        public string WarheadDetonated { get; set; } = "The Alpha Warhead was detonated!";
        public string TeamRespawning { get; set; } = "Team {team} is spawning!";

        public string Decontaminating { get; set; } = "Decontamination sequence has begun!";



    }
    
    public class Translations
    {
        public TranslationsPlayer player { get; set; } = new TranslationsPlayer();
        public TranslationsServer server { get; set; } = new TranslationsServer();

    }
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        public string webhookGeneral { get; set; }
        public string webhookTeamkill { get; set; }
        public string webhookModeration { get; set; }
        public Translations translations { get; set; } = new Translations();
    }

}