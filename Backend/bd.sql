-- ============================================================
-- Plataforma Ciudadana de Incidencias Urbanas
-- Script de creación de base de datos (PostgreSQL)
-

-- Tabla de usuarios (ciudadanos + admins, sin tabla separada)
CREATE TABLE usuarios (
    id_usuario              SERIAL PRIMARY KEY,
    nombre                  VARCHAR(60)  NOT NULL,
    apellido                VARCHAR(60)  NOT NULL,
    email                   VARCHAR(100) NOT NULL UNIQUE,
    contrasena              VARCHAR(200) NOT NULL,           -- se guarda el HASH, nunca la contraseña real
    es_admin                BOOLEAN      NOT NULL DEFAULT FALSE,
    localidad_residencia    VARCHAR(60),
    fecha_registro          DATE         NOT NULL DEFAULT CURRENT_DATE
);

-- Categorias de incidencias (Alumbrado, Bacheo, Residuos, etc.)
CREATE TABLE Categoria (
    id_categoria    SERIAL PRIMARY KEY,
    nombre          VARCHAR(60)  NOT NULL,
    descripcion     VARCHAR(200)
);

-- Incidencia: el reporte en si
CREATE TABLE incidencia (
    id_incidencia         SERIAL PRIMARY KEY,
    titulo                VARCHAR(100) NOT NULL,
    detalles              TEXT         NOT NULL,
    direccion             VARCHAR(150),
    latitud               DOUBLE PRECISION,
    longitud              DOUBLE PRECISION,
    estado                VARCHAR(20)  NOT NULL DEFAULT 'Pendiente'
                              CHECK (estado IN ('Pendiente', 'EnProceso', 'Resuelto', 'Rechazado')),
    fecha_creacion        DATE         NOT NULL DEFAULT CURRENT_DATE,
    fecha_actualizacion   DATE,
    id_usuario            INTEGER      NOT NULL REFERENCES usuarios(id_usuario)   ON DELETE CASCADE,
    id_categoria          INTEGER      NOT NULL REFERENCES categoria(id_categoria) ON DELETE RESTRICT
);

-- Comentario: seguimiento de una incidencia
CREATE TABLE comentario (
    id_comentario     SERIAL PRIMARY KEY,
    texto             TEXT    NOT NULL,
    fecha_creacion    DATE    NOT NULL DEFAULT CURRENT_DATE,
    id_incidencia     INTEGER NOT NULL REFERENCES incidencia(id_incidencia) ON DELETE CASCADE,
    id_usuario        INTEGER NOT NULL REFERENCES usuarios(id_usuario)      ON DELETE RESTRICT
);

-- indices para acelerar las consultas más frecuentes
-- (el ranking agrupa incidencias por usuario, esto lo acelera)
CREATE INDEX idx_incidencia_usuario    ON incidencia(id_usuario);
CREATE INDEX idx_incidencia_categoria  ON incidencia(id_categoria);
CREATE INDEX idx_comentario_incidencia ON comentario(id_incidencia);

-- ============================================================
-- Datos iniciales (seed) de categorias
-- ============================================================
INSERT INTO categoria (nombre, descripcion) VALUES
    ('Alumbrado público',         'Luminarias apagadas o dañadas'),
    ('Bacheo y calles',           'Pozos y roturas de pavimento'),
    ('Residuos',                  'Basurales o falta de recolección'),
    ('Arbolado urbano',           'Poda o caída de ramas'),
    ('Semáforos y señalización',  'Semáforos fuera de servicio'),
    ('Otros',                     'Otras incidencias urbanas');
