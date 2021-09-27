using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.SlashCommands;

using EagleThreadBot.Common;

using Newtonsoft.Json;

namespace EagleThreadBot.SlashCommands
{
	public class TagCommand : ApplicationCommandModule
	{
		[SlashCommand("tag", "Fetches a tag from meta and posts it.")]
		public async Task Tag(InteractionContext ctx, 
			[Option("TagId", "Tag ID as found on the eaglecord meta repository.")]
			String tag)
		{
			await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

			HttpClient client = new();

			String index = await client.GetStringAsync($"{Program.Configuration.TagUrl}index.json");

			List<TagIndex> indices = JsonConvert.DeserializeObject<List<TagIndex>>(index);

			String url;

			try
			{
				url = (from i in indices
					   where i.Identifier == tag || i.Aliases.Contains(tag)
					   select i.Url)?.First();
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

			await ctx.FollowUpAsync(new()
			{
				Content = await client.GetStringAsync($"{Program.Configuration.TagUrl}{url}")
			});
		}
	}
}
