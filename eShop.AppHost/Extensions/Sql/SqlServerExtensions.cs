namespace eShop.AppHost.Extensions.Sql;

public static class SqlServerExtensions
{
    public static IResourceBuilder<SqlServerServerResource> WithAuthentication(
        this IResourceBuilder<SqlServerServerResource> builder, string password)
    {
        builder.WithEnvironment("MSSQL_SA_PASSWORD", password);
        
        return builder;
    }
}