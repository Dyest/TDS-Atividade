USE restaurante;

CREATE TABLE Mesa (
	MesaId INT NOT NULL AUTO_INCREMENT,
	Numero INT NOT NULL,
	Status INT NOT NULL,
	HoraAbertura TEXT NULL,
	PRIMARY KEY (MesaId)
);

INSERT INTO restaurante.Mesa (`MesaId`,`Numero`,`Status`,`HoraAbertura`) VALUES (1,1,0,'2023-05-16 23:55:00.000000');
INSERT INTO restaurante.Mesa (`MesaId`,`Numero`,`Status`,`HoraAbertura`) VALUES (2,2,0,'2023-05-06 23:55:00.000000');
INSERT INTO restaurante.Mesa (`MesaId`,`Numero`,`Status`,`HoraAbertura`) VALUES (3,3,1,'2023-05-04 23:55:00.000000');
INSERT INTO restaurante.Mesa (`MesaId`,`Numero`,`Status`,`HoraAbertura`) VALUES (4,4,0,'2023-05-01 23:55:00.000000');
INSERT INTO restaurante.Mesa (`MesaId`,`Numero`,`Status`,`HoraAbertura`) VALUES (5,5,1,'2023-05-31 23:55:00.000000');

CREATE TABLE Garcon (
	GarconId INT NOT NULL AUTO_INCREMENT,
	Nome TEXT NOT NULL,
	Sobrenome TEXT NOT NULL,
	Cpf TEXT NOT NULL,
	Telefone TEXT NOT NULL,
	PRIMARY KEY (GarconId)
);

INSERT INTO restaurante.Garcon (`GarconId`,`Nome`,`Sobrenome`,`Cpf`,`Telefone`) VALUES (1,'Jorge','Aikes','197.845.968-42','(97) 98564-8675');
INSERT INTO restaurante.Garcon (`GarconId`,`Nome`,`Sobrenome`,`Cpf`,`Telefone`) VALUES (2,'Carlos','Alexandre','149.875.467-64','(32) 16548-9432');
INSERT INTO restaurante.Garcon (`GarconId`,`Nome`,`Sobrenome`,`Cpf`,`Telefone`) VALUES (3,'Pedro','Silva','679.875.621-654','(32) 16574-6878');
INSERT INTO restaurante.Garcon (`GarconId`,`Nome`,`Sobrenome`,`Cpf`,`Telefone`) VALUES (4,'Lucas','Sampaio','497.984.854-65','(31) 65486-7854');
INSERT INTO restaurante.Garcon (`GarconId`,`Nome`,`Sobrenome`,`Cpf`,`Telefone`) VALUES (5,'Lurdes','Campeira','658.789.798-6456','(32) 16874-9874');

CREATE TABLE Categoria (
CategoriaId INT NOT NULL AUTO_INCREMENT,
Nome TEXT NOT NULL,
Descricao TEXT NOT NULL,
PRIMARY KEY (CategoriaId)
);

INSERT INTO restaurante.Categoria (`CategoriaId`,`Nome`,`Descricao`) VALUES (1,'Pizzas','Redondas e saborosas.');
INSERT INTO restaurante.Categoria (`CategoriaId`,`Nome`,`Descricao`) VALUES (2,'Bebidas','As mais geladas da cidade.');
INSERT INTO restaurante.Categoria (`CategoriaId`,`Nome`,`Descricao`) VALUES (3,'Sobremesas','Doce como um mel.');

CREATE TABLE Produto (
	ProdutoId INT NOT NULL AUTO_INCREMENT,
	Nome TEXT NOT NULL,
	Descricao TEXT NOT NULL,
	Preco FLOAT NOT NULL,
	CategoriaId INT NOT NULL,
	PRIMARY KEY (ProdutoId),
	FOREIGN KEY (CategoriaId) REFERENCES Categoria (CategoriaId) ON DELETE CASCADE
);

INSERT INTO restaurante.Produto (`ProdutoId`,`Nome`,`Descricao`,`Preco`,`CategoriaId`) VALUES (1,'Heineken','Não há igual, a melhor.',1000,2);
INSERT INTO restaurante.Produto (`ProdutoId`,`Nome`,`Descricao`,`Preco`,`CategoriaId`) VALUES (2,'Cupcake','Doce e com muito glacê.',800,3);
INSERT INTO restaurante.Produto (`ProdutoId`,`Nome`,`Descricao`,`Preco`,`CategoriaId`) VALUES (3,'Pizza de Calabresa','Vem com muita calabresa e orégano.',15000,1);

CREATE TABLE Atendimento (
	AtendimentoId INT NOT NULL AUTO_INCREMENT,
	MesaId INT NOT NULL,
	AtendimentoFechado INT NOT NULL,
	DataCriacao TEXT NULL,
	DataSaida TEXT NULL,
	PRIMARY KEY (AtendimentoId),
	FOREIGN KEY (MesaId) REFERENCES Mesa (MesaId) ON DELETE CASCADE
);

INSERT INTO restaurante.Atendimento (`AtendimentoId`,`MesaId`,`AtendimentoFechado`,`DataCriacao`,`DataSaida`) VALUES (1,1,1,'2023-05-27 03:05:36.308530','2023-05-27 03:06:50.868681');
INSERT INTO restaurante.Atendimento (`AtendimentoId`,`MesaId`,`AtendimentoFechado`,`DataCriacao`,`DataSaida`) VALUES (2,3,1,'2023-05-27 03:05:50.839249','2023-05-27 03:07:04.800170');
INSERT INTO restaurante.Atendimento (`AtendimentoId`,`MesaId`,`AtendimentoFechado`,`DataCriacao`,`DataSaida`) VALUES (3,2,1,'2023-05-27 03:06:07.730965','2023-05-27 03:06:43.114172');

CREATE TABLE Pedido (
PedidoId INT NOT NULL AUTO_INCREMENT,
AtendimentoId INT NOT NULL,
GarconId INT NOT NULL,
HorarioPedido TEXT NOT NULL,
PRIMARY KEY (PedidoId),
FOREIGN KEY (AtendimentoId) REFERENCES Atendimento (AtendimentoId) ON DELETE CASCADE,
FOREIGN KEY (GarconId) REFERENCES Garcon (GarconId) ON DELETE CASCADE
);

INSERT INTO restaurante.Pedido (`PedidoId`,`AtendimentoId`,`GarconId`,`HorarioPedido`) VALUES (1,1,2,'2023-05-27 03:06:20.000000');
INSERT INTO restaurante.Pedido (`PedidoId`,`AtendimentoId`,`GarconId`,`HorarioPedido`) VALUES (2,3,5,'2023-05-27 03:06:36.000000');
INSERT INTO restaurante.Pedido (`PedidoId`,`AtendimentoId`,`GarconId`,`HorarioPedido`) VALUES (3,2,3,'2023-05-27 03:06:57.000000');


CREATE TABLE Pedido_Produto (
PedidoProdutoId INT NOT NULL AUTO_INCREMENT,
PedidoId INT NOT NULL,
ProdutoId INT NOT NULL,
Quantidade FLOAT NOT NULL,
PRIMARY KEY (PedidoProdutoId),
FOREIGN KEY (PedidoId) REFERENCES Pedido (PedidoId) ON DELETE CASCADE,
FOREIGN KEY (ProdutoId) REFERENCES Produto (ProdutoId) ON DELETE CASCADE
);

INSERT INTO restaurante.Pedido_Produto (`PedidoProdutoId`,`PedidoId`,`ProdutoId`,`Quantidade`) VALUES (1,1,1,1);
INSERT INTO restaurante.Pedido_Produto (`PedidoProdutoId`,`PedidoId`,`ProdutoId`,`Quantidade`) VALUES (2,2,3,1);
INSERT INTO restaurante.Pedido_Produto (`PedidoProdutoId`,`PedidoId`,`ProdutoId`,`Quantidade`) VALUES (3,3,2,1);