using System;
using System.Linq;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace EagleThreadBot.SlashCommands
{
	public class CreateCommand : ApplicationCommandModule
	{
		[SlashCommand("create", "Creates a help thread for your problem.")]
		public async Task CreateThreadCommand(InteractionContext ctx,
			
			[Option("Subject", "Specifies the subject for your help thread.")]
			String subject)
		{
			if(!Program.Configuration.AllowedChannels.Contains(ctx.Channel.Id))
			{
				await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
				{
					IsEphemeral = true,
					Content = "Help threads cannot be created in this channel."
				});
			}

			await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

			DiscordThreadChannel thread = await ctx.Channel.CreateThreadAsync(subject, AutoArchiveDuration.Day, ChannelType.PublicThread);

			DiscordMessage message = await thread.SendMessageAsync(subject);

			if (Program.Configuration.ThreadPings)
				foreach(var roleid in Program.Configuration.ThreadRoles)
					await thread.SendMessageAsync($"<@&{roleid}>");

			await message.PinAsync();

			await ctx.FollowUpAsync(new()
			{
				IsEphemeral = true,
				Content = $"Thread about {subject} was created successfully."
			});
		}
	}
}
