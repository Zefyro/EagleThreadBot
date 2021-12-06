using System;
using System.Linq;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.SlashCommands;

using EagleThreadBot.Common;

namespace EagleThreadBot.SlashCommands
{
	[SlashModuleLifespan(SlashModuleLifespan.Singleton)]
	public class TagCommand : ApplicationCommandModule
	{
		[SlashCommand("tag", "Fetches a tag from meta and posts it.")]
		public async Task Tag(InteractionContext ctx, 
			[Option("TagId", "Tag ID as found on the eaglecord meta repository.")]
			String tag)
		{
			// Get the tag list from cache
			TagIndex TagList = Program.GetTagList();

			String url = "";
			try
			{
				for (UInt32 i = 0; i < TagList.index.Length; i++)
				{
					if (TagList.index[i].identifier == tag
						|| TagList.index[i].aliases.Contains(tag))
					{
						url = TagList.index[i].url;
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
            await ctx.CreateResponseAsync(content: await Program.httpClient.GetStringAsync($"{Program.Configuration.TagUrl}{url}"));
		}
	}
}
