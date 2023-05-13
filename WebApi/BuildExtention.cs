public static class BuildExtention
{
    public static WebApplication BuildWithSpa(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        app.UseHttpLogging();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("https://localhost:7080/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "api";
            });
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseHttpsRedirection();

        app.Use((ctx, next) =>
        {
            if (ctx.Request.Path.StartsWithSegments("/api"))
            {
                ctx.Response.StatusCode = 404;
                return Task.CompletedTask;
            }
            return next();
        });

        app.UseSpa(spa =>
        {
            spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
        });
        return app;
    }
}