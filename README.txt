# Servicio REST - Movimientos de Inventario

**Desarrollado por:** Batko Makhno Condoy Molina  
**Curso:** Tercero "A" de Desarrollo de Software  
**Práctica:** Servicios REST con ASP.NET Core Web API + Entity Framework Core

---

## Descripción

Este proyecto implementa un servicio REST para la gestión de **Movimientos de Inventario**.  
Se utiliza la misma base de datos de la práctica anterior de SOAP (`PracticaSOAP`) y se agrega la tabla `Movimiento_Inventario`.

Se aplica **soft delete** (eliminación lógica): al eliminar un registro, este no se borra físicamente, sino que se marca como inactivo (`Estado = false`).

---

## Tecnologías utilizadas

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger (OpenAPI)
- Postman (para pruebas)

### Paquetes NuGet principales
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Swashbuckle.AspNetCore

---

## Estructura de la Base de Datos

**Base de datos:** `PracticaSOAP`

### Tabla: Movimiento_Inventario

| Campo            | Tipo             | Descripción                                      |
|------------------|------------------|--------------------------------------------------|
| IdMovimiento     | INT IDENTITY     | Clave primaria                                   |
| IdProducto       | INT              | Clave foránea hacia la tabla Producto            |
| TipoMovimiento   | NVARCHAR(20)     | Solo acepta: `Entrada` o `Salida`                |
| Cantidad         | INT              | Cantidad del movimiento                          |
| FechaMovimiento  | DATETIME         | Fecha y hora del movimiento                      |
| Observacion      | NVARCHAR(250)    | Comentario opcional                              |
| Estado           | BIT              | Soft delete (1 = Activo, 0 = Inactivo)           |

---

## Endpoints disponibles

| Método   | Ruta                          | Descripción                              |
|----------|-------------------------------|------------------------------------------|
| GET      | `/api/Movimientos`            | Obtiene la lista de movimientos activos  |
| GET      | `/api/Movimientos/{id}`       | Obtiene un movimiento por su Id          |
| POST     | `/api/Movimientos`            | Crea un nuevo movimiento                 |
| PUT      | `/api/Movimientos/{id}`       | Actualiza un movimiento existente        |
| DELETE   | `/api/Movimientos/{id}`       | Desactiva un movimiento (soft delete)    |

---

## Cadena de conexión

La cadena de conexión se encuentra en el archivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=DESKTOP-CA3OBOH;Initial Catalog=PracticaSOAP;Integrated Security=True;Trust Server Certificate=True"
}