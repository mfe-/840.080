# 1 Drug-drug interaction Tool (DDI) 

Personen die mehrere Medikamente einnehmen, müssen auf mögliche Wechselwirkungen mit anderen Arzneimittel achten.
Welche Medikamente zusammen eingenommen werden dürfen, kann entsprechendes qualifiziertes Apotheker- oder Ärztepersonal entscheiden.
 
Für Anwender_innen die zu mehreren Tageszeiten Medikamente einnehmen müssen z.B. bei älteren Personengruppen, kann eine App Überblick und Sicherheit verschaffen, wann welche und wieviel Medikamente eingenommen wurden. Sollten die Personen ein neues Medikament einnehmen müssen, kann eine App weiters auf mögliche Wechselwirkungen hinweisen und alternativen anbieten. 
Eine solche App soll in diesem Projekt als Prototyp entworfen werden.
Letzendlich soll jedoch die App so konzipiert sein, dass sie zusätzlich Informationen anbietet und nicht dafür vorgesehenes Fachpersonal ersetzt. Die Zielgruppe sind somit Patient_innen. 

## 1.1 Anforderungen an Prototyp

| Nr.  | Bezeichnung  | Prio Martin  | Prio Eylül    |   |
|---|---|---|---|---|
| 1.1.1 | Medikamenten auflisten | Middle  |   |   |
| 1.1.2  | Medikamenteninformationen aufrufen  | Middle  |   |   |
| 1.1.3  | Medikament hinzufügen  | Middle  |   |   |
| 1.1.4  | Alarm  | Super Low  |   |   |
| 1.1.5  | DDI Check  | Super High  |   |   |

### 1.1.1 Medikamenten auflisten

Auflistung von Medikamenten.
Ein Medikament beinhaltet Arzneimittelname (Produktname) und Wirkstoff und Menge (in mg).
Zusätzlich kann zum Medikament angegeben werden, wann es eingenommen werden muss. (Jeden Tag, Jeden Mo, Jeden Di,...)
Tageszeiten z.B. 8.00 11.00 17.00 Uhr

### 1.1.2 Medikamenteninformationen aufrufen

Anfrage an http://bio2rdf.org/drugbank:DB00683 Service und Informationen zum Arzneimittel anzeigen.

### 1.1.3 Medikament hinzufügen

Beim Hinzufügen eines Medikaments wird dieses in die "Medikamenten auflistung" (1.1.1) hinzugefügt. Dabei werden die Stammdaten eingegeben, (Name, Wirkstoff) und wann das Medikament eingenommen werden soll.
Vor dem hinzufügen wird überprüft ob es Wechselwirkungen mit anderen eingetragenen Arzneimittel gibt.
Dies passiert über die CombinedDatasetConservativeTWOSIDES.csv liste da die Daten aus einer wissenschaftlich evidenzbasierten Arbeit stammen. Wird eine Wechselwirkung gefunden, kann nach alternative Arzneimittel gesucht werden. Diese können z.B. über Wikidata mit SPARQL gesucht werden. Können keine alternativen gefunden werden, muss eine Warnung an den Benutzer ausgegeben werden. 
Mittels override kann dennoch das Medikament zur Auflistung hinzugefügt werden.

### 1.1.4 Alarm 

Alarm möglichkeit, dass Medikament eingenommen wird. Der Alarm kann bei den Medikamentendetails hinzugefügt werden.

### 1.1.5 DDI 

Soll die Möglichkeit bieten manuell Medikamente auf mögliche Wechselwirkung zu testen, ohne diese in die Medikamentenliste aufzunehmen

### 1.1.6 Medikamenteneinahme Protokollieren

## 1.2 Screens

### 1.2.1 Medikamentenauflistung (Startscreen)

Medikamentenauflistung, listenartig (Name, Wirkstoff). Vlt noch anzeigen, wieviel Pillen man heute einnehmen muss.
Ganz unten am Ende der Medikamentenauflistung Button mit "Take Pill(s)"
Unter "Take Pill(s)" einen weiteren Button mit "Medikament hinzufügen"
Unter ""Medikament hinzufügen" ein weiterer Button mit "Medikamenten Wechselwirkungen prüfen" (Anforderung 1.1.4 DDI )

### 1.2.2 Medikamentenauflistung (Startscreen)/Medikamentendetails

Tappt man auf ein Medikament aus Screen 1.2.1 kommt man auf diesen Screen. Hier kann man alle weiteren Details zum Medikament eingeben.
Wann man wieviel einnehmen muss; Alarm setzen (neuer Screen); Zusatzinformationen aus dem Internet (siehe Anforderung 1.1.2) Medikament löschen;

### 1.2.3 Medikamentenauflistung (Startscreen)/Medikamentendetails/Alarm

Alarm(e) festlegen für Medikament

### 1.2.4 Medikamentenauflistung (Startscreen)/Take Pill(s)

Zeigt Medikamente an die heute eingenommen werden müssen (listenform). Zusätzlich gibt es zwei Buttons mit + und - um festzulegen ob man
Medikament eingenommen hat. Siehe Anforderung 1.1.6 

### 1.2.5 Medikamentenauflistung (Startscreen)/Medikament hinzufügen

Besteht hauptsächlich aus Eingabeelemente. Hier sollte auch gesondert der Hinweis für "Wechselwirkung" dargestellt sein; Mögliche alternativen;

### 1.2.6 Medikamentenauflistung (Startscreen)/DDI

Wenn man auf den Button "Medikamenten Wechselwirkungen prüfen" klickt gelangt man hier. Liste bei der Medikamente eingegeben werden können. Dann ein Button mit Prüfen oder so, der dann den DDI check durchführt

## Requirements

1. https://visualstudio.microsoft.com/de/xamarin/