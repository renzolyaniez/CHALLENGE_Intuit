# Documentación de endpoints — ClientesController

Base route: `/Clientes`  
Controlador: API_Clientes.Controllers.ClientesController

Resumen: este controlador expone operaciones CRUD básicas sobre la entidad `Cliente`. Las operaciones delegan en la interfaz `ICliente` y devuelven códigos HTTP estándar según el resultado.

---

## Modelo: Cliente (resumen de propiedades relevantes)
- Id: int
- Nombres: string (Required)
- Apellidos: string (Required)
- Fecha_Nacimiento: DateTime (DataType.Date)
- Cuit: string (Required, Regex: ^(20|23|24|25|26|27|30|33|34)-?\d{8}-?\d$)
- Domicilio: string
- Telefono: string (Required)
- Email: string (Required, EmailAddress)

La entidad implementa IValidatableObject; la API realiza validación de modelo estándar antes de invocar la lógica de dominio.

---

## GET /Clientes
Descripción: Obtiene todos los clientes o filtra por texto de búsqueda.

Query parameters:
- `search` (opcional) — si se suministra y no es whitespace, la llamada usa `_cliente.Search(search)`; si no, usa `_cliente.GetAll()`.

Responses:
- 200 OK — Devuelve `List<Cliente>` (array JSON).
  Ejemplo respuesta:
````````

# Response
````````markdown
[
    {
        "id": 1,
        "nombres": "Juan",
        "apellidos": "Pérez",
        "fecha_nacimiento": "1980-01-01",
        "cuit": "20-12345678-9",
        "domicilio": "Av. Siempre Viva 123",
        "telefono": "01122334455",
        "email": "juan.perez@email.com"
    },
    {
        "id": 2,
        "nombres": "Ana",
        "apellidos": "Gómez",
        "fecha_nacimiento": "1990-05-15",
        "cuit": "27-87654321-0",
        "domicilio": "Calle Falsa 456",
        "telefono": "02233445566",
        "email": "ana.gomez@email.com"
    }
]
````````

## GET /Clientes/{id}
Descripción: Obtiene un cliente por su ID.

Responses:
- 200 OK — Devuelve el objeto `Cliente` correspondiente al ID.
  Ejemplo respuesta:
````````response
{
    "id": 1,
    "nombres": "Juan",
    "apellidos": "Pérez",
    "fecha_nacimiento": "1980-01-01",
    "cuit": "20-12345678-9",
    "domicilio": "Av. Siempre Viva 123",
    "telefono": "01122334455",
    "email": "juan.perez@email.com"
}
````````

## POST /Clientes
Descripción: Crea un nuevo cliente.

Request body: debe contener un objeto `Cliente` sin el campo `Id`.

Responses:
- 201 Created — Devuelve el objeto `Cliente` creado, incluyendo su ID generado.
  Ejemplo respuesta:
````````response
{
    "id": 3,
    "nombres": "Pedro",
    "apellidos": "Díaz",
    "fecha_nacimiento": "1985-08-25",
    "cuit": "27-13579246-0",
    "domicilio": "Ruta 40 Km 5",
    "telefono": "01199887766",
    "email": "pedro.diaz@email.com"
}
````````

## PUT /Clientes/{id}
Descripción: Actualiza un cliente existente.

Request body: debe contener un objeto `Cliente` completo.

Responses:
- 200 OK — Devuelve el objeto `Cliente` actualizado.
  Ejemplo respuesta:
````````response
{
    "id": 3,
    "nombres": "Pedro",
    "apellidos": "Díaz",
    "fecha_nacimiento": "1985-08-25",
    "cuit": "27-13579246-0",
    "domicilio": "Ruta 40 Km 10",
    "telefono": "01155667788",
    "email": "pedro.d@email.com"
}
````````

## DELETE /Clientes/{id}
Descripción: Elimina un cliente por su ID.

Responses:
- 204 No Content — Confirma que el cliente fue eliminado.

---

## Consideraciones y recomendaciones
- Validación de modelo: la API depende de la validación declarativa en el modelo (`[Required]`, `[EmailAddress]`, etc.). Asegurarse de que `ModelState` sea validado (ASP.NET Core lo hace automáticamente con [ApiController]).
- Código RESTful: actualmente POST y PUT devuelven 200 OK sin Location ni cuerpo con el recurso actualizado. Considerar devolver 201 Created en POST con Location y/o el recurso creado.
- Manejo de errores: los mensajes vienen de la capa `_cliente` (tu implementación de `IGeneric<Cliente>`). Estandarizar formato de error (objeto con `error` o `message`) puede facilitar consumidores.
- Seguridad: añadir autenticación/autorización si el API estará expuesto públicamente.

---

Fin de la documentación.