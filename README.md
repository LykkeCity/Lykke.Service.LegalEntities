# Lykke.Service.LegalEntities

Lykke entities and swift credentials service

Client: [Nuget](https://www.nuget.org/packages/Lykke.Service.LegalEntities.Client/)

# Client usage

Register client services in container:

```cs
ContainerBuilder builder;
...
var settings = new LegalEntitiesServiceClientSettings("http://<service>:[port]/");
builder.RegisterInstance(new LegalEntitiesClient(settings))
    .As<ILegalEntitiesClient>()
    .SingleInstance();
```

Now you can use:

* ILegalEntitiesClient - HTTP client for service API