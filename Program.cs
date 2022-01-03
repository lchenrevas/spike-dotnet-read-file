var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/upload",
async Task<IResult>(HttpRequest request) =>
{
    if (!request.HasFormContentType)
        return Results.BadRequest();

    var form = await request.ReadFormAsync();
    var formFile = form.Files["Data"];

    if (formFile is null || formFile.Length == 0)
        return Results.BadRequest();

    await using var stream = formFile.OpenReadStream();

    var reader = new StreamReader(stream);
    var text = await reader.ReadToEndAsync();

    return Results.Ok(text);
});
app.Run();
