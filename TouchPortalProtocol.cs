using CorpseLib.Json;
using CorpseLib.Network;
using CorpseLib.Serialize;
using CorpseLib.Web.Http;

namespace CorpseRemote.TouchPortal
{
    public class TouchPortalProtocol(string id) : JsonProtocol
    {
        public static TouchPortalProtocol? NewConnection(string pluginUUID)
        {
            TouchPortalProtocol protocol = new(pluginUUID);
            TCPAsyncClient twitchIRCClient = new(protocol, URI.Build("ws").Host("localhost").Port(12136).Build());
            twitchIRCClient.Start();
            return protocol;
        }

        private readonly string m_ID = id;

        protected override void OnClientConnected()
        {
            Send(new JsonObject()
            {
                { "type", "pair" },
                { "id", m_ID }
            });
        }

        protected override void OnReceive(JsonObject receivedEvent)
        {
            if (receivedEvent.TryGet("action", out string? actionID) &&
                receivedEvent.TryGet("event", out string? @event) &&
                receivedEvent.TryGet("context", out string? context) &&
                receivedEvent.TryGet("device", out string? device) &&
                receivedEvent.TryGet("payload", out JsonObject? payload) &&
                payload!.TryGet("settings", out JsonObject? settings) &&
                payload.TryGet("coordinates", out JsonObject? coordinates) &&
                coordinates!.TryGet("column", out int? column) &&
                coordinates.TryGet("row", out int? row) &&
                payload.TryGet("isInMultiAction", out bool? isInMultiAction))
            {

            }
        }
    }
}
