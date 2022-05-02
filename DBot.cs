using DSharpPlus;
using System;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

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
        /// All Guilds
        /// </summary>
        public static DiscordGuild[] ArrayGuilds { get; set; }

        /// <summary>
        /// All Channels
        /// </summary>
        public static DiscordChannel[] ArrayChannels { get; set; }

        /// <summary>
        /// Client Prefix
        /// </summary>
        public static string prefix = "!";

        /// <summary>
        /// Verification Role
        /// </summary>
        public static string VerificationRoleID = "1234";

        /// <summary>
        /// Client Online?!
        /// </summary>
        public static bool IsOnline { get; private set; }


        /// <summary>
        /// Start the client.
        /// </summary>
        public static async void ClientStart()
        {
            client.MessageCreated += async (c, e) =>
            {
                if (e.Message.Content.StartsWith(prefix))
                {
                    
                    string[] args = e.Message.Content.Split(' ');
                    string command = args[0].Replace(prefix, "");
                    Type type = typeof(Commands);
                    MethodInfo info = type.GetMethod(command);
                    info.Invoke(null, new object[] { e, args });
                }
            };

            client.Ready += async (c, e) => {

                IsOnline = true;

                ArrayGuilds = new List<DiscordGuild>(client.Guilds.Values).ToArray();
            };

            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        static void Timer_Tick()
        {

        }

        /// <summary>
        /// Stop the client.
        /// </summary>
        public static async void ClientStop()
        {
            await client.DisconnectAsync();
            IsOnline = false;
        }

        /// <summary>
        /// Send Message as Client.
        /// </summary>
        /// <param name="channel">Channel ID (Channel you want to send the message to).</param>
        /// <param name="text">Message you want to send.</param>
        public static async void ClientSendMessage(string channel, string text)
        {
            if(client.GetChannelAsync(ulong.Parse(channel)).Result.Type == ChannelType.Text)
            {
                await client.SendMessageAsync(await client.GetChannelAsync(ulong.Parse(channel)), text);
            }
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
        /// </summary>4
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
                default:
                    break;
            }

            activity.Name = text;
            activity.ActivityType = activityType;
            if(url != String.Empty && url.Contains("https://")) activity.StreamUrl = url;
            await client.UpdateStatusAsync(activity, UserStatus.Online);
        }

        /// <summary>
        /// Channel Name that will be fetched by DBot.ChannelName("ChannelID")
        /// </summary>
        public static string FetchedChannelName { get; private set; }

        /// <summary>
        /// Fetch channel name by id
        /// </summary>
        /// <param name="text">Channel id as string</param>
        public static async void ChannelName(string text)
        {
            if (text == null)
                return;
            if (text == String.Empty)
                return;
            FetchedChannelName = client.GetChannelAsync(ulong.Parse(text)).Result.Name; 

        }
    }
}
