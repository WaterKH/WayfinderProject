using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WayfinderProject;
using WayfinderProject.Domain.Factories;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.JiminyJournal;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;
using WayfinderProject.Domain.Models.MoogleShop;
using WayfinderProject.Domain.Models.MoogleShop.SubData;
using WayfinderProject.Domain.Strategies.JiminyJournal;
using WayfinderProject.Domain.Strategies.MemoryArchive;
using WayfinderProject.Domain.Strategies.MoogleShop;
using WayfinderProject.Services;
using WayfinderProject.Services.JiminyJournal;
using WayfinderProject.Services.MemoryArchive;
using WayfinderProject.Services.MoogleShop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DataServiceFactory>();

// Memory Archive Services and Strategies
builder.Services.AddScoped<InteractionService>();
builder.Services.AddScoped<InterviewService>();
builder.Services.AddScoped<SceneService>(); 
builder.Services.AddScoped<TrailerService>();

builder.Services.AddScoped<IDataFilterStrategy<Interaction<ScriptLine>, InteractionScriptWrapper>, InteractionFilterStrategy>();
builder.Services.AddScoped<IDataFilterStrategy<Interview<ScriptLine>, InterviewDialogueWrapper>, InterviewFilterStrategy>();
builder.Services.AddScoped<IDataFilterStrategy<Scene<ScriptLine>, SceneScriptWrapper>, SceneFilterStrategy>();
builder.Services.AddScoped<IDataFilterStrategy<Trailer<ScriptLine>, TrailerScriptWrapper>, TrailerFilterStrategy>();

// Jiminy Journal Services and Strategies
builder.Services.AddScoped<CharacterEntryService>();
builder.Services.AddScoped<EnemyEntryService>();
builder.Services.AddScoped<ReportEntryService>();
builder.Services.AddScoped<StoryEntryService>();

builder.Services.AddScoped<IDataFilterStrategy<CharacterEntry>, CharacterEntryFilterStrategy>();
builder.Services.AddScoped<IDataFilterStrategy<EnemyEntry>, EnemyEntryFilterStrategy>();
builder.Services.AddScoped<IDataFilterStrategy<ReportEntry>, ReportEntryFilterStrategy>();
builder.Services.AddScoped<IDataFilterStrategy<StoryEntry>, StoryEntryFilterStrategy>();

// Moogle Shop Services and Strategies
builder.Services.AddScoped<InventoryService>();
builder.Services.AddScoped<RecipeService>();

builder.Services.AddScoped<IDataFilterStrategy<Inventory, EnemyDropWrapper>, InventoryFilterStrategy>();
builder.Services.AddScoped<IDataFilterStrategy<Recipe, MaterialWrapper>, RecipeFilterStrategy>();

builder.Services.AddBlazoredModal(); 
builder.Services.AddBlazoredToast();


await builder.Build().RunAsync();
