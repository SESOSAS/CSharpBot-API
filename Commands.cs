using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBot_API
{
    public class Commands
    {

        //How to create commands
        public static void help(MessageCreateEventArgs messageEvent, string[] args)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = "Commands",
                Description = $"{DBot.prefix}help\n{DBot.prefix}ping",
                Color = DiscordColor.Green,
            };
            messageEvent.Message.Channel.SendMessageAsync(embed: embed);
        }

        public static void ping(MessageCreateEventArgs messageEvent, string[] args)
        {
            messageEvent.Message.Channel.SendMessageAsync($"pong!");
        }

        public static void verify(MessageCreateEventArgs messageEvent, string[] args)
        {
            if (messageEvent.Guild.GetMemberAsync(messageEvent.Author.Id).Result.Roles.Any(role => role.Id == ulong.Parse(DBot.VerificationRoleID)))
            {
                DiscordRole role = messageEvent.Guild.GetRole(ulong.Parse(DBot.VerificationRoleID));
                messageEvent.Guild.GetMemberAsync(messageEvent.Author.Id).Result.GrantRoleAsync(role);
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Verified!",
                    Description = $"You have got verified on this Server!",
                    Color = DiscordColor.Gold,
                };
                messageEvent.Channel.SendMessageAsync(embed: embed);
            }
            else
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = "ERROR!",
                    Description = $"You've already been verified!",
                    Color = DiscordColor.Red,
                };
                messageEvent.Channel.SendMessageAsync(embed: embed);
            }
        }

    }
}
