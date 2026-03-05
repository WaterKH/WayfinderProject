using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;
using WayfinderProject.Domain.Models.MoogleShop;

namespace WayfinderProject.Domain
{
    public static class Utilities
    {
        public static string GetCodeForGame(string game)
        {
            return game switch
            {
                "Kingdom Hearts" => "_kh1",
                "Kingdom Hearts Re:Chain of Memories" => "_khrecom",// TODO what do we do about the addendum?
                "Kingdom Hearts II" => "_kh2",
                "Kingdom Hearts 358/2 Days" => "_khdays",
                "Kingdom Hearts Birth By Sleep" => "_khbbs",
                "Kingdom Hearts Re:Coded" => "_khrecoded",
                "Kingdom Hearts Dream Drop Distance" => "_kh3d",
                "Kingdom Hearts 0.2" => "_kh0_2",
                "Kingdom Hearts χ" => "_khx",
                "Kingdom Hearts χ Back Cover" => "_khxbc",
                "Kingdom Hearts Unchained χ" => "_kh_unchainedx",// TODO what do we do about the addendum?
                "Kingdom Hearts Union χ" => "_kh_unionx",// TODO what do we do about the addendum?
                "Kingdom Hearts III" => "_kh3",
                "Kingdom Hearts Dark Road" => "_khdr",
                "Kingdom Hearts Melody of Memory" => "_khmom",
                _ => "",
            };
        }

        public static string GetImage(string option, bool displayIcons = true)
        {
            string path = "";

            if (!displayIcons)
                return path;

            switch (option)
            {
                case "Entries":
                case "Interviews":
                case "Interactions":
                case "Records":
                case "Scenes":
                case "Trailers":
                case "Inventory Items":
                case "Recipe Items":
                    path = "images/icons/scenes_gray.png";
                    break;
                case "Games":
                    path = "images/icons/games_gray.png";
                    break;
                case "Worlds":
                case "Providers":
                case "Currencies":
                    path = "images/icons/worlds_gray.png";
                    break;
                case "Areas":
                case "Categories":
                case "Synthesis Materials":
                case "Costs":
                    path = "images/icons/areas_gray.png";
                    break;
                case "Characters":
                case "Enemies":
                case "Participants":
                case "Translators":
                    path = "images/icons/characters_gray.png";
                    break;
                case "Music":
                    path = "images/icons/music_gray.png";
                    break;
                default:
                    break;
            }

            return path;
        }

        public static string GetScriptPath(object item, string game)
        {
            string path = "";
            string code = GetCodeForGame(game);

            if (item is Interaction<ScriptLine>)
                path = $"data/seed/scripts/interactions/{code}_interactions.json";
            else if (item is Interview<ScriptLine>)
                path = $"data/seed/scripts/interviews/{code}_interviews.json";
            else if (item is Scene<ScriptLine>)
                path = $"data/seed/scripts/scenes/{code}_lines.json";
            else if (item is Trailer<ScriptLine>)
                path = $"data/seed/scripts/trailers/{code}_trailers.json";

            else if (item is Inventory)
                path = $"data/seed/scripts/inventory/{code}_inventory.json";
            else if (item is Recipe)
                path = $"data/seed/scripts/recipes/{code}_recipes.json";

            return path;
        }

        public static async Task<T> LoadJsonAsync<T>(HttpClient httpClient, string path) where T : new()
        {
            try
            {
                string json = await httpClient.GetStringAsync(path);
                return JsonSerializer.Deserialize<T>(json) ?? new T();
            }
            catch (Exception)
            {
                // TODO: Add logging
                return new T();
            }
        }

        public static int Hash(string seed)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] encoded = Encoding.UTF8.GetBytes(seed);
                byte[] hashBytes = sha256.ComputeHash(encoded);

                return BitConverter.ToInt32(hashBytes, 0);
            }
        }

        public static IEnumerable<string> GetUniqueData(IEnumerable<string> items)
            => items.Distinct().OrderBy(x => x);

        public static bool FilterFailed(List<string> filter, List<string> data)
            => filter.Count > 0 && !filter.All(selected => data.Contains(selected));

        public static FilterState Refresh<T, TSubData, TSubDataWrapper>(
            IEnumerable<T> allData, 
            IDictionary<string, TSubDataWrapper> subData,
            FilterState state, 
            List<FilterRule<T>> rules
        )
            where T : BaseData<TSubData>, IFilterable
            where TSubDataWrapper : IBaseSubWrapper<TSubData>
        {
            var filteredPool = allData;

            if (!string.IsNullOrWhiteSpace(state.SearchTerm))
            {
                filteredPool = filteredPool.Where(item =>
                {
                    var lines = subData.Values
                        .Where(wrapper => wrapper.WrappedSubData.ContainsKey(item.Name))
                        .SelectMany(wrapper => wrapper.WrappedSubData[item.Name])
                        .Cast<object>();
                    return item.ContainsText(state.SearchTerm, lines);
                });
            }

            foreach (var rule in rules)
            {
                if (state.Selected.TryGetValue(rule.Id, out var selected) && selected.Any())
                {
                    filteredPool = filteredPool.Where(item =>
                        selected.All(s => rule.Selector(item).Contains(s)));
                }
            }

            foreach (var rule in rules)
            {
                var resultsInPool = filteredPool
                    .SelectMany(rule.Selector)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();

                state.Available[rule.Id] = resultsInPool;

                if (state.Selected.ContainsKey(rule.Id))
                {
                    state.Selected[rule.Id] = state.Selected[rule.Id]
                        .Intersect(resultsInPool)
                        .ToList();
                }
            }

            foreach (var rule in rules)
            {
                var resultsInPool = filteredPool
                    .SelectMany(rule.Selector)
                    .Distinct()
                    .ToList();

                var currentlySelected = state.Selected.ContainsKey(rule.Id)
                    ? state.Selected[rule.Id]
                    : new List<string>();

                state.Available[rule.Id] = resultsInPool
                    .Except(currentlySelected)
                    .OrderBy(s => s)
                    .ToList();
            }

            return state;
        }

        public static FilterState RefreshJiminyJournal<T>(
                IEnumerable<T> allData, 
                FilterState state, 
                List<FilterRule<T>> rules)
            where T : BaseJiminyJournalData
        {
            var filteredPool = allData;

            if (!string.IsNullOrWhiteSpace(state.SearchTerm))
            {
                filteredPool = filteredPool.Where(item =>
                {
                    return item.Description.Contains(state.SearchTerm) || 
                        item.AdditionalInformation.Contains(state.SearchTerm);
                });
            }

            foreach (var rule in rules)
            {
                if (state.Selected.TryGetValue(rule.Id, out var selected) && selected.Any())
                {
                    filteredPool = filteredPool.Where(item =>
                        selected.All(s => rule.Selector(item).Contains(s)));
                }
            }

            foreach (var rule in rules)
            {
                var resultsInPool = filteredPool
                    .SelectMany(rule.Selector)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();

                state.Available[rule.Id] = resultsInPool;

                if (state.Selected.ContainsKey(rule.Id))
                {
                    state.Selected[rule.Id] = state.Selected[rule.Id]
                        .Intersect(resultsInPool)
                        .ToList();
                }
            }

            foreach (var rule in rules)
            {
                var resultsInPool = filteredPool
                    .SelectMany(rule.Selector)
                    .Distinct()
                    .ToList();

                var currentlySelected = state.Selected.ContainsKey(rule.Id)
                    ? state.Selected[rule.Id]
                    : new List<string>();

                state.Available[rule.Id] = resultsInPool
                    .Except(currentlySelected)
                    .OrderBy(s => s)
                    .ToList();
            }

            return state;
        }
    }
}
