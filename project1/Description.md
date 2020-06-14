# Drug-drug interaction Tool (DDI)

## 1. Drug-drug interaction Tool

Personen, die mehrere Medikamente einnehmen, müssen auf mögliche Wechselwirkungen mit anderem Arzneimittel achten.
Welche Medikamente zusammen eingenommen werden dürfen, kann qualifiziertes Apotheker- oder Ärztepersonal entscheiden.
 
Für AnwenderInnen, die zu mehreren Tageszeiten Medikamente einnehmen müssen, wie z.B. ältere Personengruppen, kann eine App Überblick und Sicherheit verschaffen, wann, welche und wie viele Medikamente eingenommen wurden. Sollten die Personen ein neues Medikament einnehmen müssen, kann eine App weiters auf mögliche Wechselwirkungen hinweisen und Alternativen anbieten. Eine solche App soll in diesem Projekt als Prototyp entworfen werden.

Letztendlich soll die App so konzipiert sein, dass sie lediglich zusätzliche Informationen anbietet und nicht entsprechendes Fachpersonal ersetzt. Die Zielgruppe sind somit Patientinnen. 

## 1.2 Drug-drug interaction Tool

Abgesehen von der Protokollierung der Medikamenteneinnahme soll die App beim Hinzufügen eines Wirkstoffs, auf potenzielle Wechselwirkungen mit den bereits bestehenden Medikamenten hinweisen. Eine wichtige Rolle spielt hier die verwendete Datenquelle.

### 1.3 Datenquellen für DDI 

#### CSV File von 

die csv hat 1,010,178 einträge. Wenn man aber auf die einzellenn medikamente gruppiert umfasst der datensatz allerdings nur 1793 drug einträge.

Im datensatz sind auch nicht immer alle Felder gesetzt.
je nach dem aus welcher datenquelle die daten stammen ist das feld befüllt oder nicht. 
So fehlen z.B. größtenteils informationen zu serverity (Es gibt 12.201 Einträge in denen die Serverity gesetzt ist).
Auch fehlt öfters der wert des feldes "label" in dem u.a. eine textuelle beschreibung zu den interaction zu finden ist. 888.265 mal ist kein wert hinterlegt worden oder anders formuliert es gibt für 121.913 einträge einen wert.
Die Information vom feld Label und Serverity sind für die behandlung von alert fatigue wichtig. Da die werte aber so oft fehlen müssen andere mechanismen angewand werden um diesem problem zu begenen.
 
#### Wikidata

Wikidata ist eine der berühmteste Dataquellen. Was wir über Wikidata sagen können ist, dass Wikidata als zentraler Speicher für die strukturierten Daten seiner Wikimedia-Schwesterprojekte einschließlich Wikipedia fungiert. Es hat mehr als 46 Million Datenelemente in seiner Datenbank. Mehr als 50.000 Elemente sind nur über Medikamente. Entsprechend der Mission von Wikimedia kann jedes Daten hinzufügen, bearbeiten und kostenlos verwenden. Es kann als einen Nachteil sehen, aber das hilft die Vergrößerung der Datenbank. Aber dieser Plattform hat mehrere Vorteile:

-	Es ist eine kostenlose und offene Wissensdatenbank, die sowohl von Menschen als auch von Maschinen gelesen und bearbeitet werden kann
-	Es enthält verschiedene Datentypen (z. B. Text, Bilder, Mengen, Koordinaten, geografische Formen, Daten)
-	Es verwendet SPARQL

Besonders der letzte Aspekt erlaubt mit einer Query nach unserer Fragen zu suchen. SPARQL ist eine Abfragesprache für RDF-Datenbanken. Im Gegensatz zu relationalen Datenbanken wie SQL sind Elemente nicht ein Teil einer Tabelle. Stattdessen werden Elemente wie ein Diagramm oder ein Netzwerk miteinander verknüpft, was schnelle Suche eins Elements ermöglicht. Ein Beispiel Query für die Interaktionen eines Drugs (Carbonic anhydrase 1) mit Drug code Q21173164:

SELECT ?medication ?MedicationLabel 
WHERE
{
  ?medication wdt:P129 wd:Q21173164.   Suche Interaktionen der Medikament
 SERVICE wikibase:label { bd:serviceParam wikibase:language "[AUTO_LANGUAGE],en". }  print Drugs code mit ihrem Label
}  

### Drug Interaction API
Das ist auch ein freies Sofware, wo wir für unser drug-drug interaction Applikation verwenden konnten.  Es ist von Lister Hill National Center for Biomedical Communications, U.S. National Library of Medicine, 8600 Rockville Pike, Bethesda, MD 20894 und National Institutes of Health/Department of Health & Human Services bereitgestellt und diese Organisationen haben es unter Informationsfreiheit für alle Menschen zur Verfügung gestellt. Es gibt 5 Data Source Alternative, inkludiert Medikament-Interaktion. Auf die API können Clients über die Interaction RESTful-Web Service zugreifen. Ersten Montag jedes Monats wir es aktualisiert. 

REST-Architekturen bestehen aus Clients und Servern. Clients initiieren Anforderungen an Server. Server verarbeiten Anforderungen und geben entsprechende Antworten zurück. Anfragen und Antworten basieren auf der Übertragung von "Darstellungen" von "Ressourcen". Eine Ressource kann im Wesentlichen jedes kohärente und aussagekräftige Konzept sein, das angesprochen werden kann. Eine Darstellung einer Ressource ist normalerweise ein Dokument, das den aktuellen oder beabsichtigten Status einer Ressource erfasst. Web Service returniert Daten in XML- oder JSON-Formaten. Mit einem Get Method kann man sehr einfach die Informationen von Server lesen.

### Andere Datenquellen
Es gibt mehrere Datenbanken dafür zur Verfügung gestellt. Um Klinikern bei der Identifizierung von Risiken zu helfen, die mit dem kombinierten Gebrauch von zwei Medikamenten verbunden sind, stehen Arzneimittelinteraktionsbücher und durchsuchbare Datenbanken für Arzneimittelinteraktionen zur Verfügung.[1] 

Einige sind folgendes:
-	FDA [2]
-	Drug Interaction Solution [3]
-	UK international[4]	
-	Alert, Drug Interaction Databases[5]

Quellen:
[1] https://www.sciencedirect.com/topics/immunology-and-microbiology/drug-drug-interaction
[2] https://www.fda.gov/media/92328/download
[3] https://www.druginteractionsolutions.org/solutions/drug-interaction-database/
[4] https://www.drugs.com/uk/ 
[5] https://www.uspharmacist.com/article/computerized-clinical-decision-support-and-drug-interaction-databases

## 1.4 DDI Kriterien

Heutzutage nehmen die Menschen verschiedene Medikamente gegen ihre unterschiedlichen Krankheiten. Manche Patienten wie alte Personen müssen täglich mehrere Medikamente einnehmen. Hier muss man beachten, dass sie je mehr Medikamente einnehmen, desto größer wird die Wahrscheinlichkeit, dass ihr Medikament mit einem anderen Medikament interagiert. Ein DDI Tool ist hier sehr hilfreich. Diese Interaktionen können die Wirksamkeit der Medikamente verringern, geringfügige oder schwerwiegende unerwartete Nebenwirkungen verstärken oder sogar den Blutspiegel und die mögliche Toxizität eines bestimmten Arzneimittels erhöhen. Deswegen ist ein DDI Tool hier sehr hilfreich, um diese Wirkungen zu reduzieren.

### 1.4.1 Alert Fatigue 
Alert Fatigue tritt auf, wenn man einer großen Anzahl häufiger Alarme ausgesetzt ist und folglich für diese desensibilisiert wird. Desensibilisierung kann zu längeren Reaktionszeiten oder zum Fehlen wichtiger Alarme führen. Alert Fatigue tritt in vielen Branchen auf, einschließlich im Gesundheitswesen, wo elektronische Monitore klinische Informationen wie Vitalfunktionen und Blutzuckeralarme so häufig und häufig aus so geringen Gründen verfolgen, dass sie die Dringlichkeit und Aufmerksamkeit verlieren, die sie haben sollen. 

Wir versuchen eine einfache und user-friendly App zu schaffen. Bei unserer App geben wir die Kontrolle zu dem User, die Alarms zu setzen Eine zusätzliche Warnung geben wir nur, wenn unser Algorithmus eine Wechselwirkung entgegenkommt und es keine Alternative findet. 

### 1.4.2 Zielgruppe und Benutzerbindung

Wie bereits erwähnt richtet sich die App an einen Personenkreis die mehrere verschiedenen Medikamente einnehmen müssen. Dabei handelt sich öfter um ältere Personengruppen [1] [2] [3] und Personen, die in ihrer Mobilität eingeschränkt sind (z.B. Rehapatient_innen).

 [1]: https://www.medicarerights.org/medicare-watch/2016/04/28/blog-aarp-survey-highlights-prescription-drug-use-among-older-a 
 [2]: https://www.msdmanuals.com/home/older-people%E2%80%99s-health-issues/aging-and-drugs/aging-and-drugs#:~:text=Older%20people%20tend%20to%20take,disorders%20are%20taken%20for%20years 
 [3]: https://www2.health.vic.gov.au/hospitals-and-health-services/patient-care/older-people/medication/medication-and-ageing#:~:text=As%20we%20age%2C%20physiological%20changes,being%20implicated%20in%20hospital%20admissions.
 
Aufgrund dessen sollte die App einfach zu bedienen sein. So könnte das hinzufügen bzw. aufnehmen von Medikamentennamen in die App alternativ mittels Mobilgerätkamera und OCR erfolgen, um eine Protokollierung zu ermöglichen.

Eine höhere Benutzerbindung kann mit einer einfachen Bedingung der App und mittels Gamification erreicht werden. 

### 1.4.2.1 Motivation für die Zielgruppe

Menschen, die mehrere Medikamente einnehmen sollen, sind unter dem größten Risiko für Wechselwirkungen ausgesetzt. Arzneimittelwechselwirkungen tragen aufgrund der Kosten für die medizinische Versorgung, die zur Behandlung von Problemen erforderlich sind, die durch Änderungen der Wirksamkeit oder Nebenwirkungen verursacht werden, zu den Kosten der Gesundheitsversorgung bei. Interaktionen können auch zu psychischem Leiden führen, das vermieden werden kann. Menschen sollen sich für ihre physische und auch psychische Gesundheit schützen und damit können Sie manche Gesundheitsausgabenvermeiden.

### 1.4.2.2 Für welche Personengruppen sollen solche Tools verfügbar gemacht werden?

Wir haben als Team unsere Zielgruppen als alle PatientInnen entschieden. Wir wissen, dass viele Menschen jeder Alter verschiedene Medikamente einnehmen sollen. Sie können sehr schnell mit einer Applikation die Interaktionen schauen, neue Medikamente addieren und ein Alarm dafür einstellen. Unser App kann jeder verwenden z.B. Angestellte im Gesundheitssektor, Studenten, Akademiker aber unsere vorrangige Zielgruppe ist die Patienten, wer die App am meisten benutzen müssen.

### 1.4.3 Recommendations 

Ein DDI Tool soll den User die wichtigen Informationen über die Medikamente und die Wechselwirkungen zeigen. Wenn es eine Wechselwirkung eintritt, soll es diese zeigen und zusätzlich den alternativen Wirkstoffe empfehlen. Der User soll Medikamenten in DDI prüfen können, seine eigenen Medikamente in der App speichern und addieren/entfernen. Bei einerm Alert kann der Patient seinen Arzt fragen oder die Apothekennummer anzeigen.

### 1.4.4 Zugang
Am besten wäre das DDI-Tool in allen Plattformen verfügbar, wie zum Beispiel auf mobil App Stores, als Desktop Applikation, über Internet (Webseite), usw. Electronic prescribing ist eine sehr schöne Alternative, denn es beschreibt die Möglichkeit, fehlerfreie, genaue und verständliche Rezepte elektronisch vom Gesundheitsdienstleister an die Apotheke zu senden. Mit diesem Weg kann man am Beginn die Wechselwirkungen vermeiden. Also, es reduziert solche Risiken. Aber wir wollen die Patienten und andere Menschen diese Tools auch verwenden, deswegen finden wir eine mobile Applikation besser.

## 2. Anforderungen an Prototyp

Da Hauptaugenmerkmal ein DDI Prototyp ist, wurden die Anforderungen entsprechend priorisiert.

| Nr.  | Bezeichnung | Prio Martin  | Prio Eylül    |   
|---|---|---|---|
| 2.1.1 | Medikamente auflisten | Middle  |  High |   
| 2.1.2   | Medikamenteninformationen aufrufen  | Middle  |  Middle |   
| 2.1.3  | Medikament hinzufügen (DDI Check)  | High  | Middle  |   
| 2.1.3.1 | Medikament hinzufügen mittels OCR  | Super Low  |  Low |   
| 2.1.3.2  | Medikament entfernen  | Middle | Middle  |   
| 2.1.4  | Alarm  | Super Low  |   Low|   

### 2.1.1 Medikamente auflisten

Auflistung von Medikamenten.
Ein Medikament beinhaltet Arzneimittelname (Produktname), Wirkstoff und Menge (in mg).
Zusätzlich kann zum Medikament angegeben werden wann es eingenommen werden muss. (Jeden Tag, Jeden Mo, Jeden Di,...)
Tageszeiten z.B. 8.00 11.00 17.00 Uhr

### 2.1.2 Medikamenteninformationen aufrufen

Anfrage an http://bio2rdf.org/drugbank:DB00683 Service und Informationen zum Arzneimittel anzeigen. Z.B. für welche Krankheiten wird das Medikament eingesetzt.

### 2.1.3 Medikament hinzufügen

Beim Hinzufügen eines Medikaments wird dieses in die "Medikamentenauflistung" (2.1.1) hinzugefügt. Dabei werden die Stammdaten eingegeben, (Name, Wirkstoff) und wann das Medikament eingenommen werden soll.
Vor dem Hinzufügen wird überprüft, ob es Wechselwirkungen mit anderen eingetragenen Arzneimitteln gibt.

### 2.1.4 Alarm 

Alarm als Hinweis, dass ein Medikament eingenommen werden soll. Der Alarm kann bei den Medikamentendetails hinzugefügt werden.

### 2.1.6 Medikamenteneinahme Protokollieren

## 3 Screens

### 3.2.1 Medikamentenauflistung (Startscreen)

Medikamentenauflistung, listenartig (Name, Wirkstoff). Vlt noch anzeigen, wieviel Pillen man heute einnehmen muss.
Ganz unten am Ende der Medikamentenauflistung Button mit "Take Pill(s)"
Unter "Take Pill(s)" einen weiteren Button mit "Medikament hinzufügen"
Unter ""Medikament hinzufügen" ein weiterer Button mit "Medikamenten Wechselwirkungen prüfen" (Anforderung 1.1.4 DDI )

### 3.2.2 Medikamentenauflistung (Startscreen)/Medikamentendetails

Tappt man auf ein Medikament aus Screen 2.2.1 kommt man auf diesen Screen. Hier kann man alle weiteren Details zum Medikament eingeben.
Wann man wieviel einnehmen muss; Alarm setzen (neuer Screen); Zusatzinformationen aus dem Internet (siehe Anforderung 1.1.2) Medikament löschen;

### 3.2.3 Medikamentenauflistung (Startscreen)/Medikamentendetails/Alarm

Alarm(e) festlegen für Medikament

### 3.2.4 Medikamentenauflistung (Startscreen)/Take Pill(s)

Zeigt Medikamente an, die heute eingenommen werden müssen (Listenform). Zusätzlich gibt es zwei Buttons mit + und - um festzulegen, ob man Medikament eingenommen hat. Siehe Anforderung 2.1.6 

### 3.2.5 Medikamentenauflistung (Startscreen)/Medikament hinzufügen

Besteht hauptsächlich aus Eingabeelementen. Hier sollte auch gesondert der Hinweis für "Wechselwirkung" dargestellt sein; mögliche Alternativen;


## Requirements

1. https://visualstudio.microsoft.com/de/xamarin/
