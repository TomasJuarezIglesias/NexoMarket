# NexoMarket - Proyecto .NET Framework

Aplicación ASP.NET Web Forms utilizando Entity Framework Database First.

## Requisitos

- Visual Studio 2019 o 2022
- .NET Framework 4.7.2
- SQL Server
- Conexión configurada en `web.config` y `app.config` 

---

## Levantar el proyecto

#### 1. Clonar el repositorio
#### 2. Correr Script DB
#### 3. Abrir la solución
#### 4. Restaurar paquetes NuGet
#### 5. Configurar la conexión a base de datos

Abrí el archivo `Web.config` y `app.config` para modificar la cadena de conexión:

```xml
<connectionStrings>
	<add name="NexoMarketEntities" 
      connectionString="metadata=res://*/NexoMarketModel.csdl|res://*/NexoMarketModel.ssdl|res://*/NexoMarketModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=SERVIDOR;initial catalog=NexoMarket;integrated security=True;multipleactiveresultsets=True;trustservercertificate=True;application name=EntityFramework&quot;"
      providerName="System.Data.EntityClient"/>
</connectionStrings>
```

#### 6. Actualizar el modelo desde base de datos (si corresponde)

1. Abrí `NexoModel.edmx`
2. Clic derecho en el diseñador → `Actualizar modelo desde la base de datos`
3. Seleccioná las tablas nuevas o modificadas

#### 7. Compilar y ejecutar

- Establecé el proyecto Web como **proyecto de inicio**