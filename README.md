# API de processos de projetos.
Está trata-se da api utilizada para processos de projetos dos usuarios no projeto distribuido chamado **Icarus**.



## Tecnologias utilizadas no projeto.
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) ![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white) ![RabbitMQ](https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)



## Endpoint para autenticação

#### Realiza get em todos os projetos.

```http
  GET api/projetos/${pagina}/${resultado}
```

| Header | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | **Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Pagina` | `int` | Parametro para mudança de paginas. |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Resultado` | `int` | Parametro para mudança quantidade de resultados por pagina. |

#### Filtrar projetos por nome.

```http
  GET api/pesquisar/nome/{pagina?}/{resultado?}
```

| Header | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | **Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Pagina` | `int` | Parametro para mudança de paginas. |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Resultado` | `int` | Parametro para mudança quantidade de resultados por pagina. |

#### Filtrar projetos por nome.

```http
  GET api/pesquisar/status//{pagina?}/{resultado?}
```

| Header | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | **Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Pagina` | `int` | Parametro para mudança de paginas. |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Resultado` | `int` | Parametro para mudança quantidade de resultados por pagina. |

| Parametro Query | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `filtro` | `string` | Parametro para pesquisar por status. |


#### Filtrar projeto por id.

```http
  GET api/projeto/{id}
```

| Header | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | **Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Id` | `int` | Parametro para selecionar produto. |



#### Criar novo projeto.

```http
  POST api/Create
```

| Header | Tipo     | Descrição                         |
| :-------- | :------- | :-------------------------------- |
| `Authorization`      | `Authorization` |**Autenticação**. Jwt token |


#### Atualizar projeto.

```http
  POST api/update/{id}
```

| Header | Tipo     | Descrição                         |
| :-------- | :------- | :-------------------------------- |
| `Authorization`      | `Authorization` |**Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Id` | `int` | Parametro para selecionar produto. |


#### Deletar projeto.

```http
  POST api/delete/{id}
```

| Header | Tipo     | Descrição                         |
| :-------- | :------- | :-------------------------------- |
| `Authorization`      | `Authorization` |**Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Id` | `int` | Parametro para selecionar produto. |



## Deployment dotnet

Para rodar este projeto utilizando dotnet realize os seguintes comandos:

```bash
  cd ~/icarus.projeto
```

```bash
  dotnet restore
```

```bash
  cd projeto.service/
```

```bash
  dotnet run
```


## Deployment docker

Para rodar este projeto utilizando docker realize os seguintes comandos:

```bash
  docker run --name=container_autenticacao -p 5112:5112 k4im/projeto:v0.1
```
