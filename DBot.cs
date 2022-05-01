using DSharpPlus;
using System;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace CSharpBot_API
{
    public class DBot
    {
        static DiscordClient client = new DiscordClient(new DiscordConfiguration()
        {
            Token = "Your-Token",
            TokenType = TokenType.Bot,
        });

        /// <summary>
        /// Client Prefix
        /// </summary>
        public static string prefix = "!";


        /// <summary>
        /// Start the client.
        /// </summary>
        public static async void ClientStart()
        {
            client.MessageCreated += async (c, e) =>
            {
                if (e.Message.Content.StartsWith(prefix))
                {
                    string[] ags = e.Message.Content.Split(' ');
                    string command = ags[0].Replace(prefix, "");

                    switch (command)
                    {
                        case "help":
                            await e.Channel.SendMessageAsync("No help!");
                            break;
                        default:
                            break;
                    }
                }
            };
            client.Ready += async (c, e) => {

                await c.UpdateStatusAsync();
            };

            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        /// <summary>
        /// Stop the client.
        /// </summary>
        public static async void ClientStop()
        {
            await client.DisconnectAsync();
        }

        /// <summary>
        /// Send Message as Client.
        /// </summary>
        /// <param name="channel">Channel ID (Channel you want to send the message to).</param>
        /// <param name="text">Message you want to send.</param>
        public static async void ClientSendMessage(string channel, string text)
        {
            await client.SendMessageAsync(await client.GetChannelAsync(ulong.Parse(channel)), text);
        }

        /// <summary>
        /// Update client.
        /// </summary>
        public static async void ClientUpdate()
        {
            await client.UpdateCurrentUserAsync();
        }

        /// <summary>
        /// Change client rpc
        /// </summary>
        /// <param name="rpcMode">RPC mode by int [0 = Playing | 1 = Streaming | 2 = ListeningTo | 3 = Watching | 4 = Watching | 5 = Competing]</param>
        /// <param name="text">RPC Text</param>
        /// <param name="url">RPC StreamURL</param>
        public static async void ClientChangeRPC(int rpcMode, string text, string url)
        {
            DiscordActivity activity = new DiscordActivity();

            ActivityType activityType = ActivityType.Playing;
            switch (rpcMode)
            {
                case 0:
                    activityType = ActivityType.Playing;
                    break;
                case 1:
                    activityType = ActivityType.Streaming;
                    break;
                case 2:
                    activityType = ActivityType.ListeningTo;
                    break;
                case 3:
                    activityType = ActivityType.Watching;
                    break;
                case 4:
                    activityType = ActivityType.Custom;
                    break;
                case 5:
                    activityType = ActivityType.Competing;
                    break;
            }

            activity.Name = text;
            activity.ActivityType = activityType;
            activity.StreamUrl = url;
            await client.UpdateStatusAsync(activity);
        }
    }
}
