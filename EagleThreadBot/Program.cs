using System;
using System.IO;
using System.Threading.Tasks;

using DSharpPlus;

using Newtonsoft.Json;

namespace EagleThreadBot
{
	internal static class Program
	{
		public static Config Configuration { get; private set; }
		public static DiscordClient Client { get; private set; }

		internal static async Task Main(String[] args)
		{
			StreamReader reader = new("./config.json");
			Configuration = JsonConvert.DeserializeObject<Config>(reader.ReadToEnd());
			reader.Close();

			Client = new(new()
			{
				Token = Configuration.Token,
				TokenType = TokenType.Bot
			});

			await Client.ConnectAsync();
		}
	}
}
