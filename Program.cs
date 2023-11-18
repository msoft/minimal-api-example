using MinApiExample;

var builder = WebApplication.CreateBuilder(args);

// Razor
builder.Services.AddRazorPages();

builder.Services.AddTransient<IServiceToInject, ServiceToInject>();
//builder.Services.AddTransient(typeof(ClassToInject));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            //policy.WithOrigins("http://otherorigin.com",
            //                    "http://www.contoso.com");
            policy.WithOrigins("*");
        });
});

var app = builder.Build();

//app.Use(async (context, next) =>
//{
//    // ...
//    await next(context);
//});

//app.MapGet("/", () => "Hello World!");

// Razor
app.UseHttpsRedirection();
app.UseStaticFiles();

// Razor
app.UseRouting();

app.UseCors();

// Razor
app.UseAuthorization();




//app.MapMethods("/", new List<string> { "GET", "PATCH" }, () => "This is a GET response");
//app.MapGet("/", () => "This is a GET");
//app.MapGet("/", delegate () { return "This is a GET"; });
//app.MapGet("/", async () => { await Task.Run<string>(() => "This is a GET"); });
//app.MapGet("/", MinApiExample.Example.Get);

//app.MapGet("/", () => {
//    return Results..Ok("This is a GET response");
//});

//app.MapGet("/order/{id}", (string id, IServiceToInject instance) => $"This ID is: {id} and Inner member value: {instance.InnerMember}");
app.MapGet("/order/{id}", (string id) => $"Order ID is: {id}");


//app.MapGet("/client", () => Results.Stream( Json(new { FirstName = "Douglas", LastName = "Crockford" }));

app.Use(async (context, next) =>
{
    Console.WriteLine($"1. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
    await next(context);
});

// Razor
app.MapRazorPages();

app.UseEndpoints(_ => { });

app.Use(async (context, next) =>
{
    Console.WriteLine($"2. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
    await next(context);
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
    Console.WriteLine($"3. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
    await next(context);
});

app.Run();

