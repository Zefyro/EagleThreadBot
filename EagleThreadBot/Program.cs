using System;
using System.IO;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

using EagleThreadBot.SlashCommands;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace EagleThreadBot
{
	internal static class Program
	{
		public static Config Configuration { get; private set; }
		public static DiscordClient Client { get; private set; }
		public static SlashCommandsExtension Slashies { get; private set; }

		internal static async Task Main(String[] args)
		{
			StreamReader reader = new("./config.json");
			Configuration = JsonConvert.DeserializeObject<Config>(reader.ReadToEnd());
			reader.Close();

			Client = new(new()
			{
				Token = Configuration.Token,
				TokenType = TokenType.Bot,
				MinimumLogLevel = LogLevel.Information
			});

			await Client.ConnectAsync();

			Slashies = Client.UseSlashCommands();

			Slashies.RegisterCommands<CreateCommand>(Configuration.GuildId);
			Slashies.RegisterCommands<TagCommand>(Configuration.GuildId);

			Slashies.SlashCommandErrored += Slashies_SlashCommandErrored;

			await Task.Delay(-1);
		}

		private async static Task Slashies_SlashCommandErrored(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
		{
			await e.Context.FollowUpAsync(new()
			{
				Content = "An error occured. Please create an issue at <https://github.com/ExaInsanity/EagleThreadBot/issues/new> if this was unexpected.",
				IsEphemeral = true
			});

			Console.WriteLine($"{e.Exception}: {e.Exception.Message}\n{e.Exception.StackTrace}");
		}
	}
}
