using CvAI.API.Services.Abstract;
using CvAI.API.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(x => true)));

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ILangflowService,LangflowService>();
builder.Services.AddScoped<IPdfToTextService, PdfToTextService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
