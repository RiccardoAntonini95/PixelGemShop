# Pixel Gem Shop - Sito di e-commerce a tema gaming


## Descrizione
Pixel Gem Shop è un e-commerce a tema gaming che mira ad offrire agli appasionati un'esperienza di shopping intuitiva e facilita la scoperta di gemme videoludiche.
Il sito è pensato per essere responsive e quindi fruibile anche da dispositivi mobile.

## Tecnologie usate
- **ASP.NET MVC**: Versione 5.2.9
- **Entity Framework**: Versione 6.4.4
- **Bootstrap**: Versione 5.3.3
- **Microsoft SQL Server 2019** : Per il database

## Funzionalità

### Gestione degli Utenti
Sistema di registrazione e accesso per gli utenti, consentendo loro di creare e gestire il proprio profilo, salvare le informazioni personali e tracciare lo storico degli ordini effettuati.

### Suddivisione per ruoli utente
Diverse funzionalità in base a chi è collegato, amministratore o utente.

### Gestione del Catalogo Prodotti
Consente agli amministratori di aggiungere, modificare ed eliminare prodotti dal catalogo, garantendo un aggiornamento costante delle offerte disponibili.

### Gestione inventario prodotti
Sistema di tracciamento stock prodotti, il sito sottolinea la poca disponibilità in magazzino del prodotto o l'assenza momentanea all'utente, inoltre alla conferma dell'ordine da parte dell'utente il prodotto viene automaticamente scalato da quelli in magazzino.

### Carrello degli Acquisti
L'utente può gestire il proprio carrello, consentendo l'aggiunta e la rimozione di prodotti, nonché la visualizzazione di un riepilogo dettagliato prima del pagamento. Inoltre il proprio carrello rimane salvato per sessioni successive, garantendo la continuità e la comodità nell'esperienza di acquisto.

### Recensioni e Valutazioni
Possibilità di lasciare recensioni e valutazioni (con annesse modifiche) sui prodotti acquistati, fornendo informazioni utili ad altri clienti.

## Configurazione del Database

### Prima di tutto creare un nuovo database da SQL Server Management Studio 19
All'interno del progetto è presente una cartella di nome SQL, con al suo interno un file .sql 
```
CreateDatabase
```
per creare il database è necessario aprire il file dall'esplora risorse ed eseguirne la relativa query


Prima di avviare l'applicazione, assicurati di configurare correttamente la connessione al database. Apri il file `web.config` (o `app.config` se stai sviluppando un'applicazione console) e trova la sezione relativa alla connection string. Assicurati di sostituire `DataSource=nome_server` con il nome corretto del server del tuo database.

```xml
<connectionStrings>
    <add name="NomeConnessione" connectionString="Data Source=nome_server;Initial Catalog=nome_database;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```
Inoltre nel database alla tabella Users è necessario creare i record per gli admin che avranno ruolo "Admin", nella sezione di registrazione utente è possibile registrarsi solo come User.

## Installazione
1. Clona il repository dell'applicazione.
2. Assicurati di avere installato ASP.NET MVC e Entity Framework nelle versioni specificate.
3. Sosituisci nel file Web.Config il Data Source con il nome corretto del tuo server.
4. Avvia l'applicazione nel tuo ambiente di sviluppo.

## Utilizzo
1. Accedi all'applicazione utilizzando le credenziali appropriate.
2. Esplora le diverse funzionalità disponibili per amministratori e utenti.
3. Gestisci articoli, sconti e magazzino come amministratore.
4. Esplora il catalogo di prodotti, aggiungi articoli al carrello, conferma gli ordini e lascia recensioni come utente.


## Coming soon
- **Light Mode**: Possibilità di cambiare da tema scuro a chiaro
- **Ricerca avanzata con filtri**
- **Sistema di pagamento**

