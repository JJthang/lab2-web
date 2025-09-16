using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllersWithViews();   // controllers + views
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// (Tuỳ chọn) CORS example
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Error / Swagger
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // app.UseSwagger();
    // app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // cho wwwroot
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "images")),
    RequestPath = "/images"
});

app.UseRouting();

app.UseCors("AllowAll"); // nếu cần CORS
// app.UseAuthentication(); // nếu dùng auth
app.UseAuthorization();

// Map attribute-routed controllers (API) AND conventional MVC routes
app.MapControllers(); // attribute routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // conventional routing for views

app.Run();
