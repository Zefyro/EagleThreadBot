using System.Net.Http;
using System.Timers;

using DSharpPlus;
using DSharpPlus.SlashCommands;

using EagleThreadBot.Common;

namespace EagleThreadBot
{
    partial class Program
    {
		public static HttpClient httpClient = new HttpClient();
		private static Timer timer = new Timer();
		//public static InteractivityExtension Interactivity { get; set; }
		private static TagIndex TagList { get; set; }
		public static Config Configuration { get; private set; }
		public static DiscordClient Client { get; private set; }
		public static SlashCommandsExtension Slashies { get; private set; }
	}
}
