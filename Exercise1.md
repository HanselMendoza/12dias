1) Limpiar el controlador y hacerlo puro (sin estado)

Objetivo: evitar variables compartidas como numbertwo (los controladores se reusan; el estado es peligroso).

Crea endpoints que solo dependan de los parámetros:

GET /calculate/double/{n} → devuelve n * 2

GET /calculate/diff?a=&b= → devuelve a - b
Criterios de aceptación:

Sin campos/variables a nivel de clase para acumular.

Respuestas 400 si faltan parámetros o no son válidos.
Pistas: usa ActionResult<int> y BadRequest() cuando aplique.

2) Validación y manejo de errores uniforme

Objetivo: respuestas consistentes ante inputs inválidos.

Agrega validaciones (por ejemplo, n entre 1 y 1,000).

Crea un middleware de manejo global de excepciones que devuelva JSON con traceId, message.
Criterios:

Nada de try/catch repetidos en cada acción.

Respuestas 422 para validación, 500 para errores inesperados.

3) Inyección de dependencias (DI) y separación en servicios

Objetivo: sacar la lógica a un servicio.

Interface ICalculatorService con métodos Double, Diff, SumRange(int from, int to).

Implementación CalculatorService.

El controlador solo orquesta.
Criterios:

Servicio registrado en Program.cs con AddScoped.

4) Documentación con Swagger + ejemplos

Objetivo: describir bien tus endpoints.

Anota respuestas posibles (200/400/422).

Agrega example requests/responses en Swagger (Swashbuckle).
Criterios:

UI de Swagger muestra descripciones y ejemplos.

5) DTOs + AutoMapper + FluentValidation

Objetivo: dejar de usar tipos primitivos en el cuerpo.

Endpoint POST /calculate/ops con un body:

{ "op": "multiply", "a": 3, "b": 7 }


Usa un DTO de request y otro de response.

Valida con FluentValidation (op en {add, sub, multiply, divide}, b≠0 si divide).
Criterios:

Validaciones no viven en el controlador.

AutoMapper para mapear modelos → DTOs (aunque sean simples).

6) Persistencia con EF Core (historial)

Objetivo: guardar operaciones realizadas.

Tabla OperationHistory { Id, Op, A, B, Result, CreatedAt }.

POST /calculate/ops guarda y devuelve el resultado.

GET /calculate/history?op=&page=&pageSize= devuelve paginado y filtrado.
Criterios:

Migrations con dotnet ef.

Paginación en servidor (no traerte todo y paginar en memoria).

7) Async/await, logging y métricas

Objetivo: buenas prácticas transversales.

Todos los accesos a BD asíncronos (ToListAsync, SaveChangesAsync).

ILogger en servicio/controlador con logs de nivel Information y Warning en validaciones fallidas.

Exponer /health y /metrics (si te animas con Prometheus).

8) Cache y rate limiting

Objetivo: performance y protección.

Cache en memoria para GET /calculate/double/{n} por 30s.

Rate limiting (ASP.NET Core built-in) para /calculate/ops (p. ej., 5 req/10s por IP).
Criterios:

Respuesta 429 con headers claros cuando se supera el límite.

9) Autenticación y autorización (JWT)

Objetivo: proteger escrituras.

POST /calculate/ops requiere JWT.

GET /calculate/history público o rol “reader”.
Criterios:

Middleware de auth configurado.

Política/role aplicada con [Authorize(Roles="reader")].

10) CQRS con MediatR (opcional pero pro)

Objetivo: escalar ordenadamente.

Comando CreateOperationCommand (handler guarda y retorna).

Query GetHistoryQuery (handler pagina/filtra).
Criterios:

Controlador solo manda comandos/queries via IMediator.

11) SignalR para notificaciones en tiempo real

Objetivo: UX en vivo.

Cuando se cree una operación, notificar por SignalR a clientes suscritos (ej. “nueva operación guardada”).
Criterios:

Hub básico y un cliente web simple que muestra los eventos.

12) Contenedores y despliegue local

Objetivo: portabilidad.

Dockerfile + docker-compose.yml con API + SQL Server (o Postgres).

Variables de entorno para cadenas de conexión.
Criterios:

docker compose up levanta todo y Swagger funciona.