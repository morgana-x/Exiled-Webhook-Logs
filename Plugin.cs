using Exiled.API.Features;
using Exiled.Events.Handlers;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
//using System.Text.Json;

namespace SCPDiscordWebhookModerationBasic
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "morgana";

        public override string Name => "Discord Moderation Webhooks";

        public override string Prefix => Name;

        public static Plugin Instance;

        private EventHandlers _handlers;

        public override void OnEnabled()
        {
            Instance = this;

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            Instance = null;

            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _handlers = new EventHandlers();
            Exiled.Events.Handlers.Player.Joined += _handlers.OnJoined;
            Exiled.Events.Handlers.Player.Left += _handlers.OnLeft;
            Exiled.Events.Handlers.Player.Dying += _handlers.OnDying;
            Exiled.Events.Handlers.Player.Kicked += _handlers.OnKicked;
            Exiled.Events.Handlers.Player.Banned += _handlers.OnBanned;
            Exiled.Events.Handlers.Player.Escaping += _handlers.OnEscaping;
            Exiled.Events.Handlers.Server.RoundStarted += _handlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += _handlers.OnRoundEnded;
            Exiled.Events.Handlers.Warhead.Detonated += _handlers.OnWarheadDetonated;
            Exiled.Events.Handlers.Server.RestartingRound += _handlers.OnRestartingRound;
            Exiled.Events.Handlers.Player.ChangingRole += _handlers.OnChangingRole;
            Exiled.Events.Handlers.Server.WaitingForPlayers += _handlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RespawningTeam += _handlers.OnRespawningTeam;
            Exiled.Events.Handlers.Player.Verified += _handlers.OnVerified;
            Exiled.Events.Handlers.Player.Spawned += _handlers.OnSpawned;
            Exiled.Events.Handlers.Player.Handcuffing += _handlers.OnHandcuffing;
            Exiled.Events.Handlers.Player.RemovingHandcuffs += _handlers.OnRemovingHandcuffs;
            Exiled.Events.Handlers.Map.Decontaminating += _handlers.Decontaminating;
           
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.Joined -= _handlers.OnJoined;
            Exiled.Events.Handlers.Player.Left -= _handlers.OnLeft;
            Exiled.Events.Handlers.Player.Dying -= _handlers.OnDying;
            Exiled.Events.Handlers.Player.Kicked -= _handlers.OnKicked;
            Exiled.Events.Handlers.Player.Banned -= _handlers.OnBanned;
            Exiled.Events.Handlers.Player.Escaping -= _handlers.OnEscaping;
            Exiled.Events.Handlers.Server.RoundStarted -= _handlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= _handlers.OnRoundEnded;
            Exiled.Events.Handlers.Warhead.Detonated -= _handlers.OnWarheadDetonated;
            Exiled.Events.Handlers.Server.RestartingRound -= _handlers.OnRestartingRound;
            Exiled.Events.Handlers.Player.ChangingRole -= _handlers.OnChangingRole;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= _handlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RespawningTeam -= _handlers.OnRespawningTeam;
            Exiled.Events.Handlers.Player.Verified -= _handlers.OnVerified;
            Exiled.Events.Handlers.Player.Spawned -= _handlers.OnSpawned;
            Exiled.Events.Handlers.Player.Handcuffing -= _handlers.OnHandcuffing;
            Exiled.Events.Handlers.Player.RemovingHandcuffs -= _handlers.OnRemovingHandcuffs;
            Exiled.Events.Handlers.Map.Decontaminating -= _handlers.Decontaminating;
            _handlers = null;
        }
        private readonly HttpClient httpClient = new HttpClient();

        private async void WebHookSender(string url, string payload)
        {
            Log.Debug("Sending " + payload + " to " + url);
            // Create the request content
            var content = new StringContent($"{{\"content\": \"{payload}\"}}", Encoding.UTF8, "application/json");
            // Send the request to the webhook using HttpClient
            HttpResponseMessage response = await Plugin.Instance.httpClient.PostAsync(url, content);
            return;
        }
        public void SendWebHookBasic(string url, string message)
        {
            WebHookSender(url, message);
        }
    }
}