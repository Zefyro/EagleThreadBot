using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace EagleThreadBot.Common
{
	internal class TagIndex
	{
		[JsonProperty("identifier")]
		public String Identifier { get; set; }

		[JsonProperty("aliases")]
		public List<String> Aliases { get; set; }

		[JsonProperty("url")]
		public String Url { get; set; }
	}
}
