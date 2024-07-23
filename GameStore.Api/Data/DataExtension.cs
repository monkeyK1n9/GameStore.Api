using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtension
{

    /// <summary>
    /// We create this method to run migrations immediately as we start the application to be up-to-date with the schema changes
    /// </summary>
    /// <param name="app"></param>
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        await dbContext.Database.MigrateAsync();
    }
}
