# Scripts de Base de Datos - ApiPuertasAbiertas

## Despliegue en Producción / Nueva Máquina

### Para instalación nueva (base de datos vacía):

1. **Ejecutar el script principal:**
   ```sql
   -- Ejecutar: Scripts/01_CrearTablas.sql
   ```
   Este script crea todas las tablas con las restricciones `CASCADE DELETE` configuradas correctamente.

### Para actualizar base de datos existente:

1. **Verificar restricciones existentes:**

   ```sql
   -- Verificar restricciones actuales
   SELECT
       fk.name AS ForeignKeyName,
       fk.delete_referential_action_desc AS DeleteAction
   FROM sys.foreign_keys fk
   INNER JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
   WHERE tp.name IN ('Personal_tbl', 'Ingreso_tbl');
   ```

2. **Actualizar restricciones si es necesario:**
   ```sql
   -- Ejecutar: Scripts/02_ModificarConstraints.sql
   ```

### Verificar funcionamiento:

1. **Ejecutar prueba (solo en desarrollo):**
   ```sql
   -- Ejecutar: Scripts/03_TestCascadeDelete.sql
   ```

## Comportamiento Esperado

Con `CASCADE DELETE` configurado:

- **Al eliminar una Empresa:**

  - Se eliminan automáticamente todos los `Personal` asociados
  - Se eliminan automáticamente todos los `Ingreso` de ese personal

- **Al eliminar un Personal:**
  - Se eliminan automáticamente todos sus `Ingreso`

## Connection Strings Recomendados

### Desarrollo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ApiPuertasAbiertasDev;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

### Producción:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=servidor-prod;Database=ApiPuertasAbiertas;User Id=usuario;Password=contraseña;TrustServerCertificate=true;"
}
```

## Validaciones en Código

Aunque la base de datos maneja las eliminaciones en cascada, el código C# incluye validaciones adicionales:

1. **Verificación de existencia** antes de eliminar
2. **Manejo de errores** apropiado
3. **Logs de auditoría** si es necesario

## Consideraciones de Rendimiento

- Los índices están creados en las columnas de clave foránea
- Las eliminaciones en cascada pueden ser intensivas en bases de datos grandes
- Considera usar `soft delete` (estado = false) para registros históricos importantes
