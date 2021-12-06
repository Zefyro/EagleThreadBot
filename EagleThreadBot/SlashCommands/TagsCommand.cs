using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

using EagleThreadBot.Common;

namespace EagleThreadBot.SlashCommands
{
    public class TagsCommand : ApplicationCommandModule
    {
        private static DiscordEmbedBuilder embed = new DiscordEmbedBuilder();

        // Get the tag list from cache
        private static TagIndex TagList = Program.GetTagList();

        [SlashCommand("tags", "Fetches a list of tags from meta and posts them.")]
        public async Task Tags(InteractionContext ctx)
        {
            String description = "List of all tags & their aliases.";
            for (UInt32 i = 0; i < TagList.index.Length; i++)
            {
                String b = "Aliases: *none*";
                if (TagList.index[i].aliases.Length >= 1)
                {
                    b = "Aliases: `" + String.Join("`, `", TagList.index[i].aliases) + "`";
                }
                String tag = "\n\n**" + TagList.index[i].identifier + "**\n" + b;
                description += tag;
            }
            embed.Title = "Tags";
            //embed.Description = description;
            //await ctx.CreateResponseAsync(embed: embed);

            IEnumerable<Page> pages = Program.Interactivity.GeneratePagesInEmbed(description, SplitType.Line, embed);
            await ctx.Channel?.SendPaginatedMessageAsync(ctx.Member, pages);
        }
    }
}
