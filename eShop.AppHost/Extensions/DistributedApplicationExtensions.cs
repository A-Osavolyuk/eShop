namespace eShop.AppHost.Extensions;

public static class DistributedApplicationExtensions
{
    public static IResourceBuilder<ProjectResource> WaitForReference(this IResourceBuilder<ProjectResource> builder, IResourceBuilder<IResourceWithConnectionString> resource)
    {
        builder.WithReference(resource);
        builder.WaitFor(resource);
        
        return builder;
    }
    
    public static IResourceBuilder<ProjectResource> WaitForReference(this IResourceBuilder<ProjectResource> builder, IResourceBuilder<ProjectResource> resource)
    {
        builder.WithReference(resource);
        builder.WaitFor(resource);
        
        return builder;
    }
    
    public static IResourceBuilder<ExecutableResource> WaitForReference(this IResourceBuilder<ExecutableResource> builder, IResourceBuilder<ProjectResource> resource)
    {
        builder.WithReference(resource);
        builder.WaitFor(resource);
        
        return builder;
    }
}