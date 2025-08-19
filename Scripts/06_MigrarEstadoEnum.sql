-- Script para migrar la columna Estado de NVARCHAR a INT
-- cuando ya hay datos existentes

-- Paso 1: Agregar una nueva columna temporal
ALTER TABLE Ingresos_tbl ADD Estado_Temp INT;

-- Paso 2: Mapear los valores string existentes a enteros
UPDATE Ingresos_tbl 
SET Estado_Temp = CASE 
    WHEN Estado = 'EnProceso' OR Estado = 'En proceso' THEN 0
    WHEN Estado = 'RegistroAlarma' THEN 1
    WHEN Estado = 'Cerrado' OR Estado = 'Completado' THEN 2
    WHEN Estado = 'AlarmaDescompuesta' THEN 3
    ELSE 0  -- Por defecto EnProceso
END;

-- Paso 3: Verificar que todos los registros fueron actualizados
SELECT Estado, Estado_Temp, COUNT(*) as Cantidad
FROM Ingresos_tbl 
GROUP BY Estado, Estado_Temp;

-- Paso 4: Eliminar la columna antigua
ALTER TABLE Ingresos_tbl DROP COLUMN Estado;

-- Paso 5: Renombrar la columna temporal
EXEC sp_rename 'Ingresos_tbl.Estado_Temp', 'Estado', 'COLUMN';

-- Paso 6: Verificar el resultado
SELECT TOP 10 Id, Estado FROM Ingresos_tbl;
