-- Script para eliminar la columna Contrasenia de la tabla Usuarios_tbl
-- Ya que ahora usamos Active Directory para autenticación

-- Paso 1: Verificar que la columna existe
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Usuarios_tbl' AND COLUMN_NAME = 'Contrasenia';

-- Paso 2: Eliminar la columna Contrasenia
ALTER TABLE Usuarios_tbl DROP COLUMN Contrasenia;

-- Paso 3: Verificar que se eliminó correctamente
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Usuarios_tbl'
ORDER BY ORDINAL_POSITION;
