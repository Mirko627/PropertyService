# Property Service

## Descrizione

Il **Property Service** è un microservizio responsabile della gestione degli immobili.

Fornisce operazioni CRUD (Create, Read, Update, Delete) e applica controlli di autorizzazione per garantire che solo il proprietario possa modificare o eliminare una proprietà.

## Architettura

Il servizio fa parte di un'architettura a microservizi:

* Espone API REST
* Le operazioni di scrittura sono protette tramite autenticazione JWT
* Comunica in modo asincrono con **Offer Service** tramite Apache Kafka

## Avvio del servizio

Per avviare il servizio in locale:

```bash id="gk2n8p"
# esempio
dotnet run
```

Oppure con Docker:

```bash id="z9x1dw"
docker-compose up
```

Il servizio sarà disponibile su:

```id="q3v7lm"
http://localhost:7801
```

## API

Documentazione Swagger disponibile qui:

```id="r8m2yc"
http://localhost:7801/swagger/index.html
```

## Autenticazione e autorizzazione

Il servizio utilizza **JWT (JSON Web Token)** per proteggere le operazioni sensibili.

### Accesso pubblico

Le seguenti operazioni sono accessibili senza autenticazione:

* Visualizzazione delle proprietà (`GET`)

### Accesso autenticato

Le seguenti operazioni richiedono un token JWT valido:

* Creazione di una proprietà
* Modifica di una proprietà
* Eliminazione di una proprietà

Il client deve includere il token nell'header HTTP:

```id="n4t6bp"
Authorization: Bearer <token>
```

### Regole di autorizzazione

* Un utente autenticato può:

  * creare proprietà
  * modificare **solo le proprie proprietà**
  * eliminare **solo le proprie proprietà**

## Endpoints principali

| Metodo | Endpoint           | Autenticazione | Descrizione                   |
| ------ | ------------------ | -------------- | ----------------------------- |
| GET    | /api/Property      | ❌ No           | Recupera tutte le proprietà   |
| GET    | /api/Property/{id} | ❌ No           | Recupera una proprietà per ID |
| POST   | /api/Property      | ✅ Sì           | Crea una nuova proprietà      |
| PUT    | /api/Property/{id} | ✅ Sì           | Aggiorna una proprietà        |
| DELETE | /api/Property/{id} | ✅ Sì           | Elimina una proprietà         |

## Integrazioni

### Eventi Kafka consumati

| Topic        | Evento        | Provenienza   | Descrizione                                                         |
| ------------ | ------------- | ------------- | ------------------------------------------------------------------- |
| offer-events | OfferAccepted | Offer Service | Aggiorna lo stato della proprietà quando un'offerta viene accettata |

* Gli aggiornamenti di stato delle proprietà avvengono in risposta agli eventi Kafka
* I controlli di autorizzazione si basano sull'utente contenuto nel JWT
