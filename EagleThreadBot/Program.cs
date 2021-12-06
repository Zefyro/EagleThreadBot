using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

using DSharpPlus;
//using DSharpPlus.Interactivity;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
using EagleThreadBot.Common;
using EagleThreadBot.SlashCommands;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace EagleThreadBot
{
	internal partial class Program
	{
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

			// Create cache on startup
			await UpdateLocalCache();

			// Start cache timer
			StartTimer();
			
			Slashies = Client.UseSlashCommands();

			Slashies.RegisterCommands<CreateCommand>(Configuration.GuildId);
			Slashies.RegisterCommands<TagCommand>(Configuration.GuildId);
			Slashies.RegisterCommands<SuggestCommand>(Configuration.GuildId);
			Slashies.RegisterCommands<TagsCommand>(Configuration.GuildId);
			Slashies.RegisterCommands<CacheCommand>(Configuration.GuildId);

			Slashies.SlashCommandErrored += Slashies_SlashCommandErrored;

			await Task.Delay(-1);
		}

        private static void StartTimer()
        {
			// Get the cache interval from config (15+ minute cycles)
			Int32 timerInterval = 15;
			if (Configuration.UpdateCache < timerInterval)
				timerInterval = 15 * 60 * 1000;
			else
				timerInterval = Configuration.UpdateCache * 60 * 1000;

			// Update cache each cycle
			timer.Elapsed += Timer_Elapsed;
			timer.Interval = timerInterval;
			timer.AutoReset = true;
			timer.Enabled = true;
			timer.Start();
		}

        private static async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
			// Update cache
			await UpdateLocalCache();
        }

        private static async Task UpdateLocalCache()
        {
			
			if (File.Exists("./cache/index.json")
				&& File.GetLastWriteTimeUtc("./cache/index.json") 
				>= (DateTime.UtcNow + TimeSpan.FromMinutes(15)))
				return;

			if (!Directory.Exists("./cache"))
				Directory.CreateDirectory("./cache");
			if (!File.Exists("./cache/index.json"))
				File.Create("./cache/index.json");
			
			String index = await httpClient.GetStringAsync($"{Program.Configuration.TagUrl}index.json");

			String cache = File.ReadAllText("./cache/index.json");

			// Check if there are any changes
			if (cache != index)
			{
				// Store the index.json in cache
				File.WriteAllText("./cache/index.json", index);
				Client.Logger.LogInformation(new EventId(10, "Cache"), "Cache updated");
			}
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
		public static TagIndex GetTagList()
        {
			// Get the tag list from cache
			StreamReader sr = new("./cache/index.json");
			TagList = JsonConvert.DeserializeObject<TagIndex>(sr.ReadToEnd());
			sr.Close();
			return TagList;
		}
	}
}
