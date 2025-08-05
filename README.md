# rabbitmq-dot-net

Esse projeto foi desenvolvido em .net 8.0

As principais tecnologias e conceitos utilizados foram:

 - .net 8.0
 - Rabbitmq
 - MassTransit
 - Consumer

## Rodar Projeto

### `appsettings.json`

- atualizar a `MassTransitConfigs` com `host, User, password` do seu message broker

## Rotas

### `/api/Cliente`

Envia através do body os dados fictícios do cliente e dois parâmetros para simular erros nos serviços proposta e cliente

## `Obs`

- Acompanhe pelos logs os envios das mengens e os consumos