using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace EagleThreadBot.SlashCommands
{
	[SlashCommandGroup("tag", "Help tags")]
	public class TagCommand : ApplicationCommandModule
	{
		[SlashCommand("ironfarm", "Displays the iron farm help tag.")]
		public async Task IronfarmCommand(InteractionContext ctx)
		{
			await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
			{
				Content =
@"1.16+ 99.99997% Iron Farm Trouble Shooting:

Doesn't spawn anything at all:
 - The villagers need to sleep at least once before it will start working
 - The villagers must be scared of the zombie - do they sleep at night? or do they wake up immediately again? Check the zombie's position and where exactly your soul sand is placed.
 - There should not be more than four air blocks between the beds and the killing chamber: preferably less.
 - What blocks did you use for the spawning chamber floor? there need to be 25 solid blocks in the center surrounded by a row of glass or leaves.
 
Worked for a while, but stopped now:
 - Did the Zombie despawn or die?
 - Are there any iron golems within 16 blocks of any villager in the farm?
 - Is the farm too close to other villagers - at least six blocks distance from any farm villager to a non-farm villager are required

Golems getting stuck in the wall:
 - Make sure the spawning chamber floor is laid out correctly.

- Did you name your zombie ""Sub to EagleEye""?

If nothing of that works:
please explain as detailed as possible and add a bunch of screenshots"
			});
		}

		[SlashCommand("ironfarm_old", "Displays the help tag for the old iron farm.")]
		public async Task IronfarmOldCommand(InteractionContext ctx)
		{
			await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
			{
				Content =
@"1.16 Old Iron Farm Trouble Shooting;

1) the Villagers need to sleep at least once before it will start working
2) the Villagers must be scared of the zombies (they should be running around often and occasionally sleep for a tick or two at night before getting woke up)
3) there should be FOUR air blocks between the beds and the floor of the killing chamber
(Stone -> Bed -> Air -> Air -> Air -> Air -> Floor of killing chamber)

Started working but stopped...
1) make sure an Iron golem hasn't escaped ... they won't spawn a new one until the old one dies
2) and solid blocks (like stone) under the beds... and replace the glass surrounding the villagers with stone and put a bottom half slab on that stone

Still not working...
please be as detailed as possible... and add some screenshots or a short video"
			});
		}

		[SlashCommand("forcedgrowth", "Displays the help tag for Zefyro's forced growth datapack")]
		public async Task ForcedGrowthCommand(InteractionContext ctx)
		{
			await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
			{
				Content =
@"**Forced-Growth datapack**
**Installation**
To install the datapack, you need to make sure you are either playing in singleplayer or have file access to the server you want to install it to. Additionally, the datapack is only supported on Vanilla and Fabric.

1. Head to https://github.com/Zefyro/Forced-Growth/releases and select the version you are using.
2. Download the `.zip` file and put it in your `datapacks` folder.
3. Run `/reload` in-game and enable the datapack.

**Incompatibilities**
This datapack is unsupported on Forge and related projects.
This datapack is unsupported on all Bukkit-derivatives. Additionally, the installation procedure needs to be altered to work with Bukkit-derivatives.
This datapack is entirely incompatible with EssentialsX and similar plugins.
This datapack is entirely incompatible with Purpur, Yatopia and forks based on those jarfiles.

**Known bugs**
v0.1.0 zeroticking kelp is bugged. Fixed in v0.2.0"
			});
		}

		[SlashCommand("shakysand", "Displays the help tag for shaky-sand farms")]
		public async Task ShakySandCommand(InteractionContext ctx)
		{
			await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
			{
				Content =
@"Shaky-Sand 'Not Working' Checklist

Are you playing the correct version? Shaky-sand only works in 1.13, 1.14 and 1.15 (all java)... it won't fast grow in the current 1.16 snapshots. The Boat version only works in 1.14. If you are playing the correct version, what is going wrong;

1) Sand 'Shakes', crops not growing (but also not popping off) - if bamboo is not growing, add light near by. If light didn't help or if it is the sugar cane or cactus that is not growing this is 100% a mod. If you are or know the admin, you should be able to disable that mod. If you don't have any plugins or mods, that means your server has the mods loaded by default :(

2) Sand 'Shakes' but crops are popping off - double check the sand looks the same as it does in the video... odds are your timings are off... double check the clock (and share screenshots of the clock for help if needed). If it is the bamboo only, make sure you grew it one stage (so it looks like a stick and not a pyramid)

3) Everything was working but it broke - these farms are not unload safe, meaning you need to turn them off before you leave the area and unload it (or log off)
3b) But it broke while I was still pretty close, only ~30 blocks away - most likely you are playing with plugins, some of which 'skip ticks' to optimize, but if it skips the wrong tick this machine will break... you need to stand closer and turn if off when you walk away

4) It broke when I turned it off - check the pinned comment in the video you watched... you are most likely missing repeaters coming out of the observer clock"
			});
		}

		[SlashCommand("zerotick", "Displays the help tag for zerotick farms")]
		public async Task ZerotickCommand(InteractionContext ctx)
		{
			await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
			{
				Content =
@"0-tick 'Not Working' Checklist
Note: This is for Java Edition 

Are you playing the correct version? All 0 tick only works in 1.15 or before (20w11a snapshot of 1.16 to be specific)
Kelp - 20w06a to 20w11a
Sugarcane and Cactus - 1.13+ 
If you are playing on the correct version, what is going wrong;

1) Piston freezing or slow (which usually breaks the crop)
- Building it in chunk borders sometimes causes problems, this is one of them, use F3+G to toggle chunk borders.
- Please also check it is built the same as the one in the video

2) Pistons work fine but crops still break
- Most servers has plugins that block 0 tick, sometimes by breaking thems
- Chunk borders could be the reason again
- Also check if it is the same as the video

3) Pistons working but crop does not grow
- If you are on multiplayer then probably due to plugins, paper server does this on default, you can turn it off if you have access to the server files
- lack of sufficient light levels, just put some torches around it and try again
- Wrong version (check the top)

4) Works fine but breaks after a while
- Multiplayer servers delays redstone when theres lag for better performance, unless it is a vanilla server (if you are not sure then it probably isn't)
- Built on chunk borders, this sometimes does not affect the machine but sometimes do, when you unload it (leave the area)"
			});
		}

		[SlashCommand("nopaper", "Displays the no paper help tag.")]
		public async Task NopaperCommand(InteractionContext ctx)
		{
			await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
			{
				Content =
@"**Redstone and Bukkit do not go well together.**

Bukkit and its various forks, including but not limited to Spigot, Paper, Tuinity, Purpur, Yatopia, ..., are intended to work as faster versions of Vanilla.
Not only do they break the Minecraft EULA, Terms of Service and various copyright laws on multiple levels and occasions, but they also have a strange concept of optimization: to make the game run faster, you need to remove features entirely instead of improving the code, see https://gist.github.com/ExaInsanity/99de24abdddccda47bdaa66e01eb7b0a.

 All they are achieving is running at comparable performance levels to vanilla, exposing a terrible plugin API that looks like a joke to fabric and break redstone to irrecoverable levels. Why would anyone use them?

If you are using any bukkit fork, it's time to use vanilla or fabric instead.
If the server you play on uses any bukkit fork, it's time to either convince the owner to use a good server software or switch to a different server."
			});
		}

		[SlashCommand("howmod", "How to become a moderator")]
		public async Task HowModCommand(InteractionContext ctx)
		{
			await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
			{
				Content = "You don't."
			});
		}
	}
}
