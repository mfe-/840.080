# 1 Drug-drug interaction Tool (DDI) 

Personen, die mehrere Medikamente einnehmen, müssen auf mögliche Wechselwirkungen mit anderen Arzneimittel achten.
Welche Medikamente zusammen eingenommen werden dürfen, kann qualifiziertes Apotheker- oder Ärztepersonal entscheiden.
 
Für AnwenderInnen die zu mehreren Tageszeiten Medikamente einnehmen müssen (z.B. ältere Personengruppen), können eine App Überblick und Sicherheit verschaffen, wann, welche und wie viele Medikamente eingenommen wurden. Sollten die Personen ein neues Medikament einnehmen müssen, kann eine App weiters auf mögliche Wechselwirkungen hinweisen und Alternativen anbieten. 
Eine solche App soll in diesem Projekt als Prototyp entworfen werden.
Letztendlich soll die App so konzipiert sein, dass sie lediglich zusätzliche Informationen anbietet und nicht entsprechendes Fachpersonal ersetzt. Die Zielgruppe sind somit PatientInnen. 

## 1.1 Anforderungen an Prototyp

| Nr.  | Bezeichnung | Prior Martin | Prior Eylül    |   |
|---|---|---|---|---|
| 1.1.1 | Medikamente auflisten | Middle |  Middle |   |
| 1.1.2  | Medikamenteninformationen aufrufen  | Middle  | Middle  |   |
| 1.1.3  | Medikament hinzufügen  | Middle  | Middle  |   |
| 1.1.4  | Medikament entfernen  |   |  Low |   |
| 1.1.5  | Alarm  | Super Low  |   | Low  |
| 1.1.6  | DDI Check  | Super High  |   | High  |

### 1.1.1 Medikamente auflisten

Auflistung von Medikamenten.
Ein Medikament beinhaltet Arzneimittelname (Produktname), Wirkstoff und Menge (in mg).
Zusätzlich kann zum Medikament angegeben werden wann es eingenommen werden muss. (Jeden Tag, Jeden Mo, Jeden Di, usw.)
Tageszeiten z.B. 8.00 11.00 17.00 Uhr

### 1.1.2 Medikamenteninformationen aufrufen

Anfrage an http://bio2rdf.org/drugbank:DB00683 Service und Informationen zum Arzneimittel anzeigen.

### 1.1.3 Medikament hinzufügen

Beim Hinzufügen eines Medikaments wird dieses in die "Medikamentenauflistung" (1.1.1) hinzugefügt. Dabei werden die Stammdaten eingegeben, (Name, Wirkstoff) und wann das Medikament eingenommen werden soll.
Vor dem Hinzufügen wird überprüft, ob es Wechselwirkungen mit anderen eingetragenen Arzneimitteln gibt.
Dies passiert über die CombinedDatasetConservativeTWOSIDES.csv Liste, da die Daten aus einer wissenschaftlich evidenzbasierten Arbeit stammen. Wird eine Wechselwirkung gefunden, kann nach alternativen Arzneimitteln gesucht werden. Diese können z.B. über Wikidata mit SPARQL gesucht werden. Können keine Alternativen gefunden werden, muss eine Warnung an den Benutzer ausgegeben werden. 
Mittels Override kann dennoch das Medikament zur Auflistung hinzugefügt werden.

### 1.1.4 Medikament entfernen

### 1.1.5 Alarm 

Alarm als Hinweis, dass ein Medikament eingenommen werden soll. Der Alarm kann bei den Medikamentendetails hinzugefügt werden. 

### 1.1.6 DDI 

Soll die Möglichkeit bieten manuell Medikamente auf mögliche Wechselwirkung zu testen, ohne diese in die Medikamentenliste aufzunehmen

### 1.1.7 Medikamenteneinnahme Protokollieren

## 1.2 Screens

### 1.2.1 Medikamentenauflistung (Startscreen)

Medikamentenauflistung, listenartig (Name, Wirkstoff). Vlt noch anzeigen, wieviel Pillen man heute einnehmen muss und wann man sie einnehmen soll.
Ganz unten am Ende der Medikamentenauflistung Button mit "Take Pill(s)"
Unter "Take Pill(s)" einen weiteren Button mit "Medikament hinzufügen"
Unter ""Medikament hinzufügen" ein weiterer Button mit "Medikamenten Wechselwirkungen prüfen" (Anforderung 1.1.4 DDI)

### 1.2.2 Medikamentenauflistung (Startscreen)/Medikamentendetails

Tappt man auf ein Medikament aus Screen 1.2.1 kommt man auf diesen Screen. Hier kann man alle weiteren Details zum Medikament eingeben.
Wann man wieviel einnehmen muss; Alarm setzen (neuer Screen); Zusatzinformationen aus dem Internet (siehe Anforderung 1.1.2) Medikament löschen;
Wenn der Patient muss ein Medikament nicht mehr einnehmen muss, kann er das Medikament von der Liste entfernen. Dafür gibt es einen Button in der „Medikamentendetails“.

### 1.2.3 Medikamentenauflistung (Startscreen)/Medikamentendetails/Alarm

Alarm(e) festlegen für Medikament

### 1.2.4 Medikamentenauflistung (Startscreen)/Take Pill(s)

Zeigt Medikamente an, die heute eingenommen werden müssen (Listenform). Zusätzlich gibt es zwei Buttons mit + und - um festzulegen, ob man Medikament eingenommen hat. Siehe Anforderung 1.1.6 

### 1.2.5 Medikamentenauflistung (Startscreen)/Medikament hinzufügen

Besteht hauptsächlich aus Eingabeelementen. Hier sollte auch gesondert der Hinweis für "Wechselwirkung" dargestellt sein; mögliche Alternativen;

### 1.2.6 Medikamentenauflistung (Startscreen)/DDI

Wenn man auf den Button "Medikamenten Wechselwirkungen prüfen" klickt, gelangt man hierher. Liste bei den Medikamenten eingegeben werden können. Dann ein Button mit Prüfen oder so, der dann den DDI check durchführt

## Requirements

1. https://visualstudio.microsoft.com/de/xamarin/
