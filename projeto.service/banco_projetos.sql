-- import to SQLite by running: sqlite3.exe db.sqlite3 -init sqlite.sql

PRAGMA journal_mode = MEMORY;
PRAGMA synchronous = OFF;
PRAGMA foreign_keys = OFF;
PRAGMA ignore_check_constraints = OFF;
PRAGMA auto_vacuum = NONE;
PRAGMA secure_delete = OFF;
BEGIN TRANSACTION;


CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
`MigrationId` TEXT  NOT NULL,
`ProductVersion` TEXT  NOT NULL,
PRIMARY KEY (`MigrationId`)
);
START TRANSACTION;

CREATE TABLE `ProdutosEmEstoque` (
`Id` INTEGER NOT NULL ,
`Nome` NTEXT NULL,
`Quantidade` INTEGER NOT NULL,
PRIMARY KEY (`Id`)
);

CREATE TABLE `Projetos` (
`Id` INTEGER NOT NULL,
`Nome` NTEXT NOT NULL,
`Status` NTEXT NOT NULL,
`DataInicio` DATETIME NOT NULL,
`DataEntrega` DATETIME NOT NULL,
`ProdutoUtilizadoId` INTEGER NOT NULL,
`QuantidadeUtilizado` INTEGER NOT NULL,
`Descricao` NTEXT NULL,
`Valor` FLOAT NOT NULL,
`RowVersion` BLOB NULL,
PRIMARY KEY (`Id`),
FOREIGN KEY (`ProdutoUtilizadoId`) REFERENCES `ProdutosEmEstoque` (`Id`)
);
CREATE INDEX `IX_Projetos_Nome` ON `Projetos` (`Nome`);
CREATE INDEX `IX_Projetos_ProdutoUtilizadoId` ON `Projetos` (`ProdutoUtilizadoId`);
CREATE INDEX `IX_Projetos_Status` ON `Projetos` (`Status`);
INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230828121048_Realizado ajustes de migration', '7.0.9');
COMMIT;





COMMIT;
PRAGMA ignore_check_constraints = ON;
PRAGMA foreign_keys = ON;
PRAGMA journal_mode = WAL;
PRAGMA synchronous = NORMAL;
