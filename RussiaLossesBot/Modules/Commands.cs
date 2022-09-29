using Discord;
using Discord.Commands;
using Newtonsoft.Json;


namespace RussiaLossesBot.Modules
{
    public class Commands : ModuleBase
    {
        private HttpClient client = new HttpClient();

        [Command("losses")]
        public async Task TotalLosses()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://russianwarship.rip/api/v1/statistics/latest");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Increase losses = JsonConvert.DeserializeObject<Increase>(responseBody);
                Data data = JsonConvert.DeserializeObject<Data>(responseBody);
                Root root = JsonConvert.DeserializeObject<Root>(responseBody);

                foreach(var item in Convert.ToString())
                {
                    int i = 0;
                    i++;
                    await ReplyAsync(Convert.ToString(i));
                }

                var embed = new EmbedBuilder()
                    .WithDescription("")
                    .WithTitle($"Total russian army losses for {root.data.date}")
                    .WithAuthor(Context.Client.CurrentUser)
                    .WithCurrentTimestamp()
                    .WithColor(Discord.Color.Orange);
                await ReplyAsync(embed: embed.Build());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);

            }         
        }
        public class Increase
        {
            public int personnel_units { get; set; }
            public int tanks { get; set; }
            public int armoured_fighting_vehicles { get; set; }
            public int artillery_systems { get; set; }
            public int mlrs { get; set; }
            public int aa_warfare_systems { get; set; }
            public int planes { get; set; }
            public int helicopters { get; set; }
            public int vehicles_fuel_tanks { get; set; }
            public int warships_cutters { get; set; }
            public int cruise_missiles { get; set; }
            public int uav_systems { get; set; }
            public int special_military_equip { get; set; }
            public int atgm_srbm_systems { get; set; }
        }
        public class Data
        {
            public string date { get; set; }
            public int day { get; set; }
            public string resource { get; set; }
            public Increase increase { get; set; }
        }
        public class Root
        {
            public string message { get; set; }
            public Data data { get; set; }
        }

    }
}