IF NOT EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'TuningVariant'
    AND COLUMN_NAME = 'isDeleted'
)
BEGIN
    -- Add the column if it doesn't exist
    ALTER TABLE [TuningVariant]
    ADD [isDeleted] BIT DEFAULT 'FALSE';
    
    -- Update the column values to 'false'
    UPDATE [TuningVariant]
    SET [isDeleted] = 'false';
END
ELSE
BEGIN
    PRINT 'Column isDeleted already exists in TuningVariant table.';
END