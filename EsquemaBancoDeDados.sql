
CREATE TABLE Endereco(
    id int IDENTITY not null,
    logradouro VARCHAR(256) not NULL,
    numero int,
    cep int,
    bairro VARCHAR(50),
    cidade VARCHAR(30),
    estado VARCHAR(20),
    CONSTRAINT pk_endereco PRIMARY KEY(id)
)


Create table Pessoa(
    id INT IDENTITY(1,1) not null,
    nome VARCHAR(256) not null,
    cpf bigint not null,
    endereco int not null,
    CONSTRAINT pk_pessoa PRIMARY KEY(id),
    CONSTRAINT fk_endereco FOREIGN KEY(endereco) REFERENCES Endereco(id)
)

CREATE TABLE Telefone_tipo(
    id int IDENTITY(1,1) NOT NULL,
    tipo VARCHAR(10) NOT NULL,
    CONSTRAINT pk_telefone_tipo PRIMARY KEY(id) 
)

CREATE TABLE Telefone(
    id int IDENTITY(1,1) not null,
    numero int,
    ddd int,
    tipo int,
    CONSTRAINT pk_telefone PRIMARY KEY (id),
    constraint fk_telefone_tipo FOREIGN KEY (tipo) REFERENCES Telefone_Tipo (id)
)



create table Pessoa_telefone(
    id_pessoa int not null,
    id_telefone int not null,
    CONSTRAINT pk_pessoa_telefone PRIMARY KEY(
        id_pessoa,
        id_telefone
    ),
    CONSTRAINT fk_pessoa FOREIGN KEY (id_pessoa) REFERENCES Pessoa(id),
    CONSTRAINT fk_telefone FOREIGN KEY (id_telefone) REFERENCES Telefone(id)
)


insert into telefone_tipo(tipo) values ('Casa')
insert into telefone_tipo(tipo) values ('Celular')