// TODO: UNUSED FOR NOW

//using WayfinderProject.Domain.Interfaces;
//using WayfinderProject.Domain.Models;
//using WayfinderProject.Domain.Models.Filters;

//namespace WayfinderProject.Services.JiminyJournal
//{
//    public class JiminyJournalService<T, TCriteria, TWrapper>(HttpClient httpClient, IDataFilterStrategy<T> filterStrategy) : 
//        BaseDataService<T, TCriteria, TWrapper>(httpClient, filterStrategy)
//            where T : BaseData
//            where TCriteria : FilterCriteria, new()
//            where TWrapper : BaseWrapper<T>, new()
//    {
//        protected override List<string> CategoryPriority => new()
//        {
//            "Entry", "Areas", "Characters", "Games", "Worlds"
//        };

//        protected override IEnumerable<T> MapWrapperToData(TWrapper wrapper)
//        {
//            return wrapper.WrappedData.SelectMany(kvp =>
//                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
//        }
//    }
//}
