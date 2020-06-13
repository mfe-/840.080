# Drug-drug interaction Tool (DDI)

## 1. Drug-drug interaction Tool

Personen, die mehrere Medikamente einnehmen, müssen auf mögliche Wechselwirkungen mit anderem Arzneimittel achten.
Welche Medikamente zusammen eingenommen werden dürfen, kann qualifiziertes Apotheker- oder Ärztepersonal entscheiden.
 
Für AnwenderInnen, die zu mehreren Tageszeiten Medikamente einnehmen müssen, wie z.B. ältere Personengruppen, kann eine App Überblick und Sicherheit verschaffen, wann, welche und wie viele Medikamente eingenommen wurden. Sollten die Personen ein neues Medikament einnehmen müssen, kann eine App weiters auf mögliche Wechselwirkungen hinweisen und Alternativen anbieten. Eine solche App soll in diesem Projekt als Prototyp entworfen werden.

Letztendlich soll die App so konzipiert sein, dass sie lediglich zusätzliche Informationen anbietet und nicht entsprechendes Fachpersonal ersetzt. Die Zielgruppe sind somit Patientinnen. 

## 1.2 Drug-drug interaction Tool

Abgesehen von der Protokollierung der Medikamenteneinnahme soll die App beim Hinzufügen eines Wirkstoffs, auf potenzielle Wechselwirkungen mit den bereits bestehenden Medikamenten hinweisen. Eine wichtige Rolle spielt hier die verwendete Datenquelle.

### 1.3 Datenquellen für DDI 

issue #2 Übersicht zu den in Frage kommenden Datenquellen und Vergleich, Erklärung für letztendliche Auswahl der Datenquelle für die Applikation: 2 P

## 1.4 DDI Kriterien

Was macht das ideale DDI Tool aus?

### 1.4.1 Alert Fatigue 
`Wie kann Alert Fatigue vermieden werden, ohne wirklich wichtige Alerts unter den Tisch fallen zu lassen? `

### 1.4.2 Zielgruppe und Benutzerbindung

Wie bereits erwähnt richtet sich die App an einen Personenkreis die mehrere verschiedenen Medikamente einnehmen müssen. Dabei handelt sich öfter um ältere Personengruppen [1] [2] [3] und Personen, die in ihrer Mobilität eingeschränkt sind (z.B. Rehapatient_innen).

 [1]: https://www.medicarerights.org/medicare-watch/2016/04/28/blog-aarp-survey-highlights-prescription-drug-use-among-older-a 
 [2]: https://www.msdmanuals.com/home/older-people%E2%80%99s-health-issues/aging-and-drugs/aging-and-drugs#:~:text=Older%20people%20tend%20to%20take,disorders%20are%20taken%20for%20years 
 [3]: https://www2.health.vic.gov.au/hospitals-and-health-services/patient-care/older-people/medication/medication-and-ageing#:~:text=As%20we%20age%2C%20physiological%20changes,being%20implicated%20in%20hospital%20admissions.
 
Aufgrund dessen sollte die App einfach zu bedienen sein. So könnte das hinzufügen bzw. aufnehmen von Medikamentennamen in die App alternativ mittels Mobilgerätkamera und OCR erfolgen, um eine Protokollierung zu ermöglichen.

Eine höhere Benutzerbindung kann mit einer einfachen Bedingung der App und mittels Gamification erreicht werden. 

`Wie können Benutzer möglichst motiviert werden, solche Tools sinnvoll zu verwenden? Für welche Personengruppen sollen solche Tools verfügbar gemacht werden, und wie? -> `

### 1.4.3 Recommendations 

recommendations: alternativen Wirkstoff bei DDI vorschlagen, bei alert Arzt Fragen oder Apothekennummer anzeigen,

`Welche Recommendations sollte ein solches Tool machen? `

### 1.4.4 Zugang

`Wie würde das Tools am besten verfügbar gemacht (App, Internet, installierte Applikation, Integration in EHR / ePrescribing?)`

## 2. Anforderungen an Prototyp

Da Hauptaugenmerkmal ein DDI Prototyp ist, wurden die Anforderungen entsprechend priorisiert.

| Nr.  | Bezeichnung | Prio Martin  | Prio Eylül    |   |
|---|---|---|---|---|
| 1.1.1 | Medikamente auflisten | Middle  |  High |   |
| 1.1.2  | Medikamenteninformationen aufrufen  | Middle  |  Middle |   |
| 1.1.3  | Medikament hinzufügen  | Middle  | Middle  | Middle  |
| 1.1.3.1 | Medikament hinzufügen mittels OCR  | Super Low  |  Low |   |
| 1.1.3.2  | Medikament entfernen  | | Middle  | Middle  |
| 1.1.4  | Alarm  | Super Low  |   Low|   |
| 1.1.5  | DDI Check  | Super High  |  High |   |

### 2.1.1 Medikamente auflisten

Auflistung von Medikamenten.
Ein Medikament beinhaltet Arzneimittelname (Produktname), Wirkstoff und Menge (in mg).
Zusätzlich kann zum Medikament angegeben werden wann es eingenommen werden muss. (Jeden Tag, Jeden Mo, Jeden Di,...)
Tageszeiten z.B. 8.00 11.00 17.00 Uhr

### 2.1.2 Medikamenteninformationen aufrufen

Anfrage an http://bio2rdf.org/drugbank:DB00683 Service und Informationen zum Arzneimittel anzeigen. Z.B. für welche Krankheiten wird das Medikament eingesetzt.

### 2.1.3 Medikament hinzufügen

Beim Hinzufügen eines Medikaments wird dieses in die "Medikamentenauflistung" (1.1.1) hinzugefügt. Dabei werden die Stammdaten eingegeben, (Name, Wirkstoff) und wann das Medikament eingenommen werden soll.
Vor dem Hinzufügen wird überprüft, ob es Wechselwirkungen mit anderen eingetragenen Arzneimitteln gibt.
Dies passiert über die CombinedDatasetConservativeTWOSIDES.csv Liste, da die Daten aus einer wissenschaftlich evidenzbasierten Arbeit stammen. Wird eine Wechselwirkung gefunden, kann nach alternativen Arzneimitteln gesucht werden. Diese können z.B. über Wikidata mit SPARQL gesucht werden. Können keine Alternativen gefunden werden, muss eine Warnung an den Benutzer ausgegeben werden. 
Mittels override kann dennoch das Medikament zur Auflistung hinzugefügt werden.

### 2.1.4 Alarm 

Alarm als Hinweis, dass ein Medikament eingenommen werden soll. Der Alarm kann bei den Medikamentendetails hinzugefügt werden.

### 2.1.5 DDI 

Soll die Möglichkeit bieten manuell Medikamente auf mögliche Wechselwirkung zu testen, ohne diese in die Medikamentenliste aufzunehmen

### 2.1.6 Medikamenteneinahme Protokollieren

## 3 Screens

### 3.2.1 Medikamentenauflistung (Startscreen)

Medikamentenauflistung, listenartig (Name, Wirkstoff). Vlt noch anzeigen, wieviel Pillen man heute einnehmen muss.
Ganz unten am Ende der Medikamentenauflistung Button mit "Take Pill(s)"
Unter "Take Pill(s)" einen weiteren Button mit "Medikament hinzufügen"
Unter ""Medikament hinzufügen" ein weiterer Button mit "Medikamenten Wechselwirkungen prüfen" (Anforderung 1.1.4 DDI )

### 3.2.2 Medikamentenauflistung (Startscreen)/Medikamentendetails

Tappt man auf ein Medikament aus Screen 1.2.1 kommt man auf diesen Screen. Hier kann man alle weiteren Details zum Medikament eingeben.
Wann man wieviel einnehmen muss; Alarm setzen (neuer Screen); Zusatzinformationen aus dem Internet (siehe Anforderung 1.1.2) Medikament löschen;

### 3.2.3 Medikamentenauflistung (Startscreen)/Medikamentendetails/Alarm

Alarm(e) festlegen für Medikament

### 3.2.4 Medikamentenauflistung (Startscreen)/Take Pill(s)

Zeigt Medikamente an, die heute eingenommen werden müssen (Listenform). Zusätzlich gibt es zwei Buttons mit + und - um festzulegen, ob man Medikament eingenommen hat. Siehe Anforderung 1.1.6 

### 3.2.5 Medikamentenauflistung (Startscreen)/Medikament hinzufügen

Besteht hauptsächlich aus Eingabeelementen. Hier sollte auch gesondert der Hinweis für "Wechselwirkung" dargestellt sein; mögliche Alternativen;

### 3.2.6 Medikamentenauflistung (Startscreen)/DDI

Wenn man auf den Button "Medikamenten Wechselwirkungen prüfen" klickt, gelangt man hierher. Liste bei den Medikamenten eingegeben werden können. Dann ein Button mit Prüfen oder so, der dann den DDI check durchführt

## Requirements

1. https://visualstudio.microsoft.com/de/xamarin/
