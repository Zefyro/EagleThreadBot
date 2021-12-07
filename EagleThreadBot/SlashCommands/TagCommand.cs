﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

using EagleThreadBot.Common;

namespace EagleThreadBot.SlashCommands
{
	public class TagCommand : ApplicationCommandModule
	{
		[SlashCommand("tag", "Fetches a tag from meta and posts it.")]
		public async Task Tag(InteractionContext ctx,
			[Option("TagId", "Tag ID as found on the eaglecord meta repository.")]
			String tag)
		{
			await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource, new());

			// Get the tag list from cache
			TagIndex TagList = Program.TagList;

			String url = "";
			Boolean isEmbed = false;
			Boolean isPaged = false;
			try
			{
				for (UInt32 i = 0; i < TagList.index.Length; i++)
				{
					if (TagList.index[i].identifier == tag
						|| TagList.index[i].aliases.Contains(tag))
					{
						url = TagList.index[i].url;

						if (TagList.index[i].isEmbed)
                        {
							isEmbed = true;
							if (TagList.index[i].isPaged)
								isPaged = true;
						}
						break;
					}
				}
			}
			catch
			{
				await ctx.FollowUpAsync(new()
				{
					Content = "The specified tag could not be found.",
					IsEphemeral = true
				});
				return;
			}
			if (url == "")
			{
				await ctx.FollowUpAsync(new()
				{
					Content = $"Could not fetch url for {tag}. Try again.",
					IsEphemeral = true
				});
				return;
			}
			String Tag = await Program.HttpClient.GetStringAsync($"{Program.Configuration.TagUrl}{url}");

			if (isEmbed)
            {
				DiscordEmbedBuilder embed = new();
				if (!isPaged)
                {
					embed.Description = Tag;

					await ctx.EditResponseAsync(new() { Content = "<https://github.com/ExaInsanity/eaglecord-meta/blob/main/" + $"{url}>" });
					await ctx.Channel?.SendMessageAsync(embed: embed);
				}
                else
                {
					await ctx.EditResponseAsync(new() { Content = "<https://github.com/ExaInsanity/eaglecord-meta/blob/main/" + $"{url}>" });
					IEnumerable<Page> pages = Program.Interactivity.GeneratePagesInEmbed(Tag, SplitType.Line, embed);
					await ctx.Channel?.SendPaginatedMessageAsync(ctx.Member, pages);
				}
            }
            else
            {
				await ctx.EditResponseAsync(new() { Content = Tag });
            }

        }
	}
}