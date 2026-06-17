# Property Service

## Descrizione

Il **Property Service** è un microservizio responsabile della gestione degli immobili.

Fornisce operazioni CRUD (Create, Read, Update, Delete) sugli immobili e aggiorna il loro stato in base agli eventi ricevuti da altri servizi.

## Architettura

Il servizio fa parte di un'architettura a microservizi e comunica con:

* **Offer Service** (tramite eventi asincroni)

## Avvio del servizio

Per avviare il servizio in locale con Docker:

```bash
docker-compose up
```

Il servizio sarà disponibile su:

```
http://localhost:7801
```

## API

Documentazione Swagger disponibile qui:

```
http://http://localhost:7801/swagger/index.html
```

## Integrazioni

### Eventi Kafka consumati

| Topic          | Evento       | Provenienza   | Descrizione                                                       |
| -------------- | ------------ | ------------- | ----------------------------------------------------------------- |
| StatusProperty | PropertySelled | Offer Service | Aggiorna lo stato dell'immobile quando un'offerta viene accettata |

