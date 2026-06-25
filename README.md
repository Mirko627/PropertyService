# Property Service

## Descrizione

Il **Property Service** è un microservizio responsabile della gestione degli immobili.

Fornisce operazioni CRUD sugli immobili e consente ai proprietari di gestirne lo stato durante il ciclo di vita della compravendita.

## Architettura

Il servizio fa parte di un'architettura a microservizi:

* Espone API REST
* Le operazioni di scrittura sono protette tramite autenticazione JWT
* Comunica con **Offer Service** tramite Apache Kafka
* Mantiene uno storico delle variazioni di stato degli immobili

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

Il servizio utilizza **JWT (JSON Web Token)** per proteggere le operazioni di modifica.

### Accesso pubblico

Le seguenti operazioni sono accessibili senza autenticazione:

* Visualizzazione di tutti gli immobili
* Visualizzazione di un immobile specifico

### Accesso autenticato

Le seguenti operazioni richiedono un token JWT valido:

* Creazione di un immobile
* Modifica di un immobile
* Eliminazione di un immobile
* Cambio dello stato di un immobile

Il client deve includere il token nell'header HTTP:

```http id="n4t6bp"
Authorization: Bearer <token>
```

### Regole di autorizzazione

#### Creazione di un immobile

Un utente autenticato può creare nuovi immobili.

L'utente autenticato viene automaticamente associato come proprietario dell'immobile.

#### Modifica di un immobile

Un immobile può essere modificato solo se:

* l'utente autenticato è il proprietario dell'immobile

#### Eliminazione di un immobile

Un immobile può essere eliminato solo se:

* l'utente autenticato è il proprietario dell'immobile

#### Cambio di stato

Lo stato di un immobile può essere modificato solo se:

* l'utente autenticato è il proprietario dell'immobile

Ogni modifica di stato viene registrata nello storico degli stati.

## Stati di un immobile

Un immobile può assumere diversi stati durante il proprio ciclo di vita.

| Stato      | Descrizione                                                            |
| ---------- | ---------------------------------------------------------------------- |
| Available  | Immobile disponibile per visite e offerte                              |
| UnderOffer | Immobile con almeno un'offerta in corso                                |
| Sold       | Immobile venduto                                                       |
| Rejected   | Stato derivante dalla gestione delle offerte (se previsto dal dominio) |

> Gli stati effettivamente disponibili dipendono dall'enumerazione `PropertyStatus` utilizzata dal servizio.

## Endpoints principali

| Metodo | Endpoint           | Autenticazione | Descrizione                 |
| ------ | ------------------ | -------------- | --------------------------- |
| GET    | /api/Property      | ❌ No           | Recupera tutti gli immobili |
| GET    | /api/Property/{id} | ❌ No           | Recupera un immobile per ID |
| POST   | /api/Property      | ✅ Sì           | Crea un nuovo immobile      |
| PUT    | /api/Property/{id} | ✅ Sì           | Aggiorna un immobile        |
| DELETE | /api/Property/{id} | ✅ Sì           | Elimina un immobile         |

## Storico degli stati

Ogni variazione dello stato di un immobile viene registrata tramite una voce di storico che contiene:

* stato precedente
* nuovo stato
* data e ora della modifica

Questo consente di tracciare l'evoluzione dell'immobile nel tempo.

## Integrazioni

### Kafka

#### Eventi Kafka consumati

| Topic        | Evento        | Provenienza   | Descrizione                                                             |
| ------------ | ------------- | ------------- | ----------------------------------------------------------------------- |
| offer-events | OfferCreated  | Offer Service | Aggiorna lo stato dell'immobile quando viene ricevuta una nuova offerta |
| offer-events | OfferAccepted | Offer Service | Aggiorna lo stato dell'immobile quando un'offerta viene accettata       |
| offer-events | OfferRejected | Offer Service | Aggiorna lo stato dell'immobile in seguito al rifiuto di un'offerta     |

#### Gestione degli eventi

Il servizio utilizza gli eventi provenienti dall'Offer Service per mantenere sincronizzato lo stato degli immobili.

Ad esempio:

* una nuova offerta può portare l'immobile nello stato **UnderOffer**
* l'accettazione di un'offerta può portare l'immobile nello stato **Sold**
* il rifiuto di un'offerta può causare il ripristino dello stato precedente o altre transizioni definite dalle regole di business

## Controlli automatici

* Il proprietario viene associato automaticamente all'immobile in fase di creazione.
* Gli immobili vengono creati con stato iniziale **Available**.
* Tutti i controlli di autorizzazione si basano sull'utente autenticato contenuto nel JWT.
* Ogni cambio di stato viene tracciato nello storico delle modifiche.
