# 📘 Documentación del API Personas (ASP.NET Core)

## 🔹 Descripción General

Este API permite gestionar registros de personas en una base de datos SQL Server mediante operaciones CRUD. Está desarrollado en ASP.NET Core y expone endpoints RESTful para interactuar con los datos.

---

## 🔹 Endpoints Disponibles

| Método | Endpoint | Descripción | Ejemplo de uso |
| --- | --- | --- | --- |
| **GET** | ``/api/persona`` | Obtiene la lista completa de personas | ``GET ``/api/persona`` |
| **GET** | ``/api/persona/{id}`` | Obtiene una persona específica por su ID | ``GET ``/api/persona/5`` |
| **POST** | ``/api/persona`` | Registra una nueva persona | ``POST ``/api/persona`` |
| **PUT** | ``/api/persona/{id}`` | Actualiza los datos de una persona existente | ``PUT ``/api/persona/5`` |
| **DELETE** | ``/api/persona/{id}`` | Elimina una persona por su ID | ``DELETE ``/api/persona/5`` |

---

## 🔹 Modelo de Datos (DTO)

```csharp
public class PersonaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public int Edad { get; set; }
    public string Email { get; set; }
}
```

---

## 🔹 Ejemplos de Peticiones

### ➡️ Crear Persona (POST)

```http
POST /api/persona
Content-Type: application/json

{
  "nombre": "Juan",
  "apellido": "Pérez",
  "edad": 30,
  "email": "juan.perez@email.com"
}
```

## ➡️ Obtener Persona (GET)

```http
GET /api/persona/1
```

---

## ➡️ Actualizar Persona (PUT)

```http
PUT /api/persona/1
Content-Type: application/json

{
  "id": 1,
  "nombre": "Juan",
  "apellido": "Pérez",
  "edad": 31,
  "email": "juan.perez@email.com"
}
```

---

## ➡️ Eliminar Persona (DELETE)

```http
DELETE /api/persona/1
```

---

## 🔹 Respuestas del API
  * 200 OK → Operación exitosa.

  * 201 Created → Persona creada correctamente.

  * 400 Bad Request → Datos inválidos.

  * 404 Not Found → Persona no encontrada.

  * 500 Internal Server Error → Error en el servidor.

---

## 🔹 Notas Técnicas
  * El controlador PersonaController hereda de ControllerBase y utiliza Entity Framework Core para interactuar con la base de datos.

  * Se recomienda configurar Swagger/OpenAPI para documentar y probar los endpoints de manera interactiva.

  * La conexión a la base de datos se define en appsettings.json.

---

## 🔹 Configuración

  1. Configurar la cadena de conexión en appsettings.json:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=PersonasDB;Trusted_Connection=True;"
}
```

  2. Ejecutar migraciones con Entity Framework:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

  3. Levantar el proyecto:

```bash
dotnet run
```

## 🔹 Documentación Interactiva (Swagger)
Este proyecto incluye Swagger/OpenAPI para probar los endpoints desde el navegador:

URL: ``https://localhost:5001/swagger``


---
