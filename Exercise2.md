# Ejercicio 2 - Manejo Global de Excepciones

Este ejercicio implementa un sistema de manejo global de excepciones en una API ASP.NET Core, que devuelve respuestas JSON estandarizadas con información de trazabilidad.

## Aspectos Implementados

1. **Modelo de Respuesta de Error (`ErrorResponse.cs`)**
   - `TraceId`: Identificador único para rastrear la solicitud
   - `Message`: Mensaje de error general
   - `Detail`: Detalles adicionales del error (opcional)

2. **Middleware Global (`GlobalExceptionMiddleware.cs`)**
   - Captura todas las excepciones no manejadas
   - Genera un identificador de rastreo único (TraceId)
   - Registra los errores en el sistema de logging
   - Devuelve una respuesta JSON estandarizada

3. **Integración con el Pipeline de ASP.NET Core**
   - Registro del middleware en `Program.cs`
   - Configuración antes de otros middlewares para capturar todas las excepciones

## Estructura de la Respuesta JSON

```json
{
  "traceId": "0HMK9VN2J4LKM:00000001",
  "message": "An error occurred while processing your request",
  "detail": "Descripción detallada del error"
}
```

## Características Principales

1. **Trazabilidad**
   - Cada error tiene un identificador único (TraceId)
   - Facilita el seguimiento de problemas en logs

2. **Logging**
   - Registro automático de excepciones
   - Incluye TraceId para correlación

3. **Seguridad**
   - No expone detalles internos en producción
   - Mensajes de error controlados

4. **Consistencia**
   - Formato de respuesta estandarizado
   - Estructura JSON uniforme

## Uso en el Controlador

El middleware maneja automáticamente las excepciones, por lo que no es necesario usar try-catch en cada endpoint. Sin embargo, para errores específicos del negocio, se pueden lanzar excepciones personalizadas que serán manejadas por el middleware.

## Beneficios

1. **Centralización**: Un único punto para el manejo de errores
2. **Mantenibilidad**: Facilita cambios en el formato de respuesta
3. **Debugging**: TraceId facilita la identificación de problemas
4. **Consistencia**: Formato de respuesta uniforme

## Pruebas

Para probar el manejo de excepciones:

1. Realizar una solicitud GET a `/api/v1/Calculate/ejercicio2/{numero1}/{numero2}`
2. Intentar con valores inválidos o que generen excepciones
3. Verificar la respuesta JSON con el TraceId y mensaje de error

## Consideraciones

- El middleware debe registrarse antes de otros middlewares de manejo de solicitudes
- Los logs incluyen el TraceId para facilitar el debugging
- La respuesta siempre mantiene el formato JSON especificado