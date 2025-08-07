-- Script de prueba para verificar CASCADE DELETE
-- ADVERTENCIA: Este script es solo para pruebas en ambiente de desarrollo

-- 1. Insertar datos de prueba
INSERT INTO Empresa_tbl (Nombre, Estado) VALUES ('Empresa Prueba', 1);
DECLARE @EmpresaId INT = SCOPE_IDENTITY();

INSERT INTO Personal_tbl (Nombres, Apellidos, Estado, Id_Empresa) 
VALUES ('Juan', 'Pérez', 1, @EmpresaId);
DECLARE @PersonalId INT = SCOPE_IDENTITY();

INSERT INTO Ingreso_tbl (Id_Personal, FechaHoraIngreso, TipoMotivo) 
VALUES (@PersonalId, GETDATE(), 1);

-- 2. Verificar que los datos se insertaron
SELECT 'Datos insertados:' AS Mensaje;
SELECT * FROM Empresa_tbl WHERE Id = @EmpresaId;
SELECT * FROM Personal_tbl WHERE Id_Empresa = @EmpresaId;
SELECT * FROM Ingreso_tbl WHERE Id_Personal = @PersonalId;

-- 3. Eliminar la empresa (debería eliminar personal e ingresos en cascada)
DELETE FROM Empresa_tbl WHERE Id = @EmpresaId;

-- 4. Verificar que se eliminaron en cascada
SELECT 'Después de eliminar empresa:' AS Mensaje;
SELECT COUNT(*) AS EmpresasRestantes FROM Empresa_tbl WHERE Id = @EmpresaId;
SELECT COUNT(*) AS PersonalRestante FROM Personal_tbl WHERE Id_Empresa = @EmpresaId;
SELECT COUNT(*) AS IngresosRestantes FROM Ingreso_tbl WHERE Id_Personal = @PersonalId;

PRINT 'Prueba de CASCADE DELETE completada. Si los conteos son 0, funciona correctamente.';
