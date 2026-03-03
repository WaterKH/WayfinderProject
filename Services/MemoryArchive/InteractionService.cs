using WayfinderProject.Domain;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Services.MemoryArchive
{
    public class InteractionService : 
        BaseDataService<
            Interaction<ScriptLine>, 
            InteractionCriteria,
            ScriptLine,
            InteractionScriptWrapper,
            InteractionWrapper<Interaction<ScriptLine>>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Interactions", "Areas", "Characters", "Games", "Music", "Worlds"
        };

        public InteractionService(HttpClient httpClient, IDataFilterStrategy<Interaction<ScriptLine>, InteractionScriptWrapper> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<Interaction<ScriptLine>> MapWrapperToData(
            InteractionWrapper<Interaction<ScriptLine>> wrapper)
        {
            return wrapper.WrappedMemoryArchiveData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }

        public IEnumerable<Interaction<ScriptLine>> GetFiltered(InteractionCriteria criteria)
        {
            return Data.Where(interaction =>
                !Utilities.FilterFailed(criteria.Areas, interaction.Areas) &&
                !Utilities.FilterFailed(criteria.Characters, interaction.Characters) &&
                !Utilities.FilterFailed(criteria.Games, [interaction.Game]) &&
                !Utilities.FilterFailed(criteria.Music, interaction.Music) &&
                !Utilities.FilterFailed(criteria.Worlds, interaction.Worlds)
            );
        }

        public IEnumerable<string> GetAreas() => Utilities.GetUniqueData(Data.SelectMany(s => s.Areas));
        public IEnumerable<string> GetCharacters() => Utilities.GetUniqueData(Data.SelectMany(s => s.Characters));
        public IEnumerable<string> GetGames() => Utilities.GetUniqueData(Data.Select(s => s.Game));
        public IEnumerable<string> GetMusic() => Utilities.GetUniqueData(Data.SelectMany(s => s.Music));
        public IEnumerable<string> GetWorlds() => Utilities.GetUniqueData(Data.SelectMany(s => s.Worlds));
    }
}
