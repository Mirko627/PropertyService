# Order Service

## Descrizione

Microservizio responsabile della gestione degli ordini.
Permette la creazione, l'aggiornamento e la consultazione degli ordini dei clienti.

## Responsabilità

- Creazione ordini
- Aggiornamento stato ordine
- Consultazione storico ordini
- Pubblicazione eventi relativi agli ordini

## Tecnologie

- Java 21
- Spring Boot 3
- PostgreSQL
- Kafka
- Docker

## Architettura

Il servizio comunica con:
- User Service
- Payment Service
- Kafka Broker

## API

La documentazione completa delle API è disponibile tramite Swagger:

http://localhost:8080/swagger-ui/index.html

## Configurazione

Variabili d'ambiente principali:

| Variabile | Descrizione |
|------------|------------|
| DB_HOST | Host del database |
| DB_PORT | Porta del database |
| DB_NAME | Nome database |
| KAFKA_BROKER | Broker Kafka |

## Avvio locale

### Tramite Docker

```bash
docker compose up -d
