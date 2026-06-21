-- Personalización de pantallas por usuario (paridad XAF — ListView).
-- Guarda el layout del grid + sumarios (blob JSON opaco) por par (Login, Vista).
-- Idempotente: se puede correr varias veces sin error.
IF OBJECT_ID('dbo.PreferenciaVista', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PreferenciaVista
    (
        Login    nvarchar(50)  NOT NULL,
        Vista    nvarchar(200) NOT NULL,   -- clave de pantalla (ej. "Trabaja", "Afiliado:list", "SP_Cuota:IDPrestamo")
        Json     nvarchar(max) NOT NULL,   -- VistaState serializado (GridPersistentLayout + sumarios)
        FechaMod datetime2     NOT NULL CONSTRAINT DF_PreferenciaVista_FechaMod DEFAULT SYSDATETIME(),
        CONSTRAINT PK_PreferenciaVista PRIMARY KEY (Login, Vista)
    );
END
GO
