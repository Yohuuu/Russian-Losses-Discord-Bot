using Discord;
using Discord.Commands;
using Newtonsoft.Json;


namespace RussiaLossesBot.Modules
{
    public class Commands : ModuleBase
    {
        private HttpClient client = new HttpClient();

        [Command("help")]
        public async Task Help()
        {
            var embed = new EmbedBuilder()
              .WithDescription("!todaylosses - shows russian losses for today; !totallosses - shows total losses of russian army during the war")
              .WithTitle("List of commands")
              .WithAuthor(Context.Client.CurrentUser)
              .WithCurrentTimestamp()
              .WithColor(Discord.Color.Orange);
            await ReplyAsync(embed: embed.Build());
        }

        [Command("todaylosses")]
        public async Task TodayLosses()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://russianwarship.rip/api/v1/statistics/latest");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Increase losses = JsonConvert.DeserializeObject<Increase>(responseBody);
                Data data = JsonConvert.DeserializeObject<Data>(responseBody);
                Root root = JsonConvert.DeserializeObject<Root>(responseBody);


                var embed = new EmbedBuilder()

                // i have no idea how to make it show data without typping every stat in
                .WithTitle($"Russian army losses for {root.data.date} (day {root.data.day})")
                .WithDescription($"Personnel units: {root.data.increase.personnel_units}\n" +
                                     $"Tanks: {root.data.increase.tanks}\n" +
                                     $"Armoured fighting vehicles: {root.data.increase.armoured_fighting_vehicles}\n" +
                                     $"Anti-aircraft warfare systems: {root.data.increase.aa_warfare_systems}\n" +
                                     $"Planes: {root.data.increase.planes}\n" +
                                     $"Helicopters: {root.data.increase.helicopters}\n" +
                                     $"UAV(unmanned aerial vehicle) systems: {root.data.increase.uav_systems}\n" +
                                     $"Cruise missiles: {root.data.increase.cruise_missiles}\n" +
                                     $"Warships/cutters: {root.data.increase.warships_cutters}\n" +
                                     $"Vehicles/fuel tanks: {root.data.increase.vehicles_fuel_tanks}\n" + 
                                     $"Special military equip: {root.data.increase.special_military_equip}\n")
                .WithCurrentTimestamp()
                .WithColor(Discord.Color.Orange);
                embed.AddField("Source", root.data.resource);
                await ReplyAsync(embed: embed.Build());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                await ReplyAsync("Exception!" + e.Message);

            }         
        }

        [Command("totallosses")]
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
                Stats stats = JsonConvert.DeserializeObject<Stats>(responseBody);

                var embed = new EmbedBuilder()

                .WithTitle($"Total russian army losses")
                .WithDescription($"Personnel units: {root.data.stats.personnel_units}\n" +
                                     $"Tanks: {root.data.stats.tanks}\n" +
                                     $"Armoured fighting vehicles: {root.data.stats.armoured_fighting_vehicles}\n" +
                                     $"Anti-aircraft warfare systems: {root.data.stats.aa_warfare_systems}\n" +
                                     $"Planes: {root.data.stats.planes}\n" +
                                     $"Helicopters: {root.data.stats.helicopters}\n" +
                                     $"UAV(unmanned aerial vehicle) systems: {root.data.stats.uav_systems}\n" +
                                     $"Cruise missiles: {root.data.stats.cruise_missiles}\n" +
                                     $"Warships/cutters: {root.data.stats.warships_cutters}\n" +
                                     $"Vehicles/fuel tanks: {root.data.stats.vehicles_fuel_tanks}\n" +
                                     $"Special military equip: {root.data.stats.special_military_equip}\n")
                .WithCurrentTimestamp()
                .WithColor(Discord.Color.Orange);
                await ReplyAsync(embed: embed.Build());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                await ReplyAsync("Exception!" + e.Message);

            }
        }
        public class Increase
        {
            public int personnel_units { get; set; }
            public int tanks { get; set; }
            public int armoured_fighting_vehicles { get; set; }
            public int artillery_systems { get; set; }
            public int aa_warfare_systems { get; set; }
            public int planes { get; set; }
            public int helicopters { get; set; }
            public int vehicles_fuel_tanks { get; set; }
            public int warships_cutters { get; set; }
            public int cruise_missiles { get; set; }
            public int uav_systems { get; set; }
            public int special_military_equip { get; set; }
        }
        public class Data
        {
            public string date { get; set; }
            public int day { get; set; }
            public string resource { get; set; }
            public Increase increase { get; set; }
            public Stats stats { get; set; }
        }
        public class Root
        {
            public string message { get; set; }
            public Data data { get; set; }
        }
        public class Stats
        {
            public int personnel_units { get; set; }
            public int tanks { get; set; }
            public int armoured_fighting_vehicles { get; set; }
            public int artillery_systems { get; set; }
            public int aa_warfare_systems { get; set; }
            public int planes { get; set; }
            public int helicopters { get; set; }
            public int vehicles_fuel_tanks { get; set; }
            public int warships_cutters { get; set; }
            public int cruise_missiles { get; set; }
            public int uav_systems { get; set; }
            public int special_military_equip { get; set; }
        }
    }
}