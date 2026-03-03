using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WayfinderProject;
using WayfinderProject.Domain.Factories;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;
using WayfinderProject.Domain.Strategies;
using WayfinderProject.Services;
using WayfinderProject.Services.MemoryArchive;

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

builder.Services.AddScoped<InventoryService>();
builder.Services.AddScoped<JournalService>();
builder.Services.AddScoped<RecipeService>();

builder.Services.AddBlazoredModal(); 
builder.Services.AddBlazoredToast();


await builder.Build().RunAsync();
