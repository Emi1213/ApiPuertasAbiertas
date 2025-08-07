-- Script para modificar tablas existentes y agregar CASCADE DELETE
-- Ejecutar solo si las tablas ya existen sin CASCADE DELETE

-- Verificar si existe la restricción actual para Personal
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Personal_Empresa')
BEGIN
    -- Eliminar la restricción existente
    ALTER TABLE Personal_tbl DROP CONSTRAINT FK_Personal_Empresa;
    PRINT 'Restricción FK_Personal_Empresa eliminada';
END

-- Crear la nueva restricción con CASCADE DELETE
ALTER TABLE Personal_tbl 
ADD CONSTRAINT FK_Personal_Empresa 
    FOREIGN KEY (Id_Empresa) 
    REFERENCES Empresas_tbl(Id) 
    ON DELETE CASCADE;
PRINT 'Nueva restricción FK_Personal_Empresa creada con CASCADE DELETE';

-- Verificar las restricciones creadas
SELECT 
    fk.name AS ForeignKeyName,
    tp.name AS ParentTable,
    cp.name AS ParentColumn,
    tr.name AS ReferencedTable,
    cr.name AS ReferencedColumn,
    fk.delete_referential_action_desc AS DeleteAction
FROM sys.foreign_keys fk
INNER JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
INNER JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.columns cp ON fkc.parent_object_id = cp.object_id AND fkc.parent_column_id = cp.column_id
INNER JOIN sys.columns cr ON fkc.referenced_object_id = cr.object_id AND fkc.referenced_column_id = cr.column_id
WHERE tp.name = 'Personal_tbl'
ORDER BY fk.name;
