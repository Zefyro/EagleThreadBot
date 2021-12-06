using System;
using System.IO;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

using Microsoft.Extensions.Logging;

namespace EagleThreadBot.SlashCommands
{
    public class CacheCommand : ApplicationCommandModule
    {
        [SlashRequirePermissions(Permissions.Administrator)]
        [SlashCommand("cache", "Force refresh the cache")]
        public static async Task Cache(InteractionContext ctx)
        {
            if (!Directory.Exists("./cache"))
                Directory.CreateDirectory("./cache");
            if (!File.Exists("./cache/index.json"))
                File.Create("./cache/index.json");

            String index = await Program.httpClient.GetStringAsync($"{Program.Configuration.TagUrl}index.json");

            String cache = File.ReadAllText("./cache/index.json");

            // Force refresh the cache if there are any changes
            if (cache != index)
            {
                File.WriteAllText("./cache/index.json", index);
                Program.Client.Logger.LogInformation(new EventId(10, "Cache"), "Cache updated");
                await ctx.CreateResponseAsync("Cache updated.");
            }
            else
            {
                Program.Client.Logger.LogInformation(new EventId(10, "Cache"), "Cache updated but nothing changed");
                await ctx.CreateResponseAsync("Cache updated but nothing changed.");
            }
        }
    }
}
