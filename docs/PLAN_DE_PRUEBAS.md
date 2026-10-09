# Plan de pruebas funcionales — Módulo Gestión de Clientes

Completar la columna "Resultado obtenido" y adjuntar capturas de pantalla.

| ID | Caso de prueba | Método | Pasos | Resultado esperado | Resultado obtenido |
|----|----------------|--------|-------|--------------------|--------------------|
| CP01 | Listar clientes | GET | Abrir `/Clientes` | Tabla con los clientes registrados | |
| CP02 | Buscar cliente | GET | Escribir "ana" en el buscador y enviar | Solo se muestran coincidencias | |
| CP03 | Registrar cliente válido | POST | Nuevo cliente → datos válidos → Guardar | Mensaje "Cliente registrado correctamente." | |
| CP04 | Nombre muy corto | POST | Nombre "Al" → Guardar | Error de validación en nombre | |
| CP05 | Correo inválido | POST | Correo "abc" → Guardar | Error "Ingrese un correo válido." | |
| CP06 | Correo duplicado | POST | Usar un correo ya existente | Error "Ese correo ya está registrado." | |
| CP07 | Teléfono inválido | POST | Teléfono "123" → Guardar | Error de teléfono | |
| CP08 | Editar cliente | GET + POST | Editar → cambiar nombre → Guardar | Mensaje "Cliente actualizado correctamente." | |
| CP09 | Eliminar cliente | POST | Eliminar → confirmar | Mensaje "Cliente eliminado correctamente." | |
| CP10 | Id inexistente | GET | `/Clientes/Editar/999` | Mensaje "El cliente no existe." | |
| CP11 | Recargar tras guardar | — | Presionar F5 después de guardar | No se duplica el registro (PRG) | |
