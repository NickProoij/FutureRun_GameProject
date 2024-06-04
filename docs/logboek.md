# Dagelijks Logboek

## Dag 1 - 2024-05-27

**Doelen voor de dag:**
- Het project opzetten in Unity
- Basis model voor de omgeving maken in Asset Forge (waar de speler op zal lopen)
- Het model importeren in het Unity-project en zorgen dat het werkt met alle materialen en textures

**Voltooide Taken:**
- Een nieuw Unity-project gemaakt en de scène ingesteld
- Basis model voor de omgeving gemaakt in Asset Forge
- Model geïmporteerd in Unity en materialen en textures toegepast

**Uitdagingen:**
- Het maken van mijn eigen modellen was best moeilijk
- Ik kwam ook een probleem tegen waarbij de posities tussen de 3 modellen iets afweken, wat problemen veroorzaakte bij het verbinden in Unity

**Volgende Stappen:**
- Een script maken dat automatisch nieuwe tegels toevoegt en verwijdert
- Een script maken dat de camera automatisch beweegt
- Een eenvoudig script maken dat de speler beweegt en de gebruiker in staat stelt de speler naar links of rechts te laten gaan

## Dag 2 - 2024-05-28

**Doelen voor de dag:**
- De 3 tile object modellen afmaken in Asset Forge en de posities ervan corrigeren
- Een script schrijven dat willekeurig kiest tussen 3 tile objecten en deze automatisch toevoegt en verwijdert
- Een script schrijven dat de camera automatisch met een vaste snelheid beweegt
- Een script schrijven dat het spelerobject automatisch beweegt en de gebruiker in staat stelt om de speler naar links en rechts te laten bewegen in de banen
- Het gladder maken van de baanwisselbeweging
- De sprongmogelijkheid aan het speler script toevoegen
- Beginnen met het maken of zoeken naar verzamelbare item-assets

**Voltooide Taken:**
- De 3 tile object modellen afgemaakt in Asset Forge en de posities ervan gecorrigeerd
- Een script geschreven dat willekeurig kiest tussen 3 tile objecten en deze automatisch toevoegt en verwijdert
- Een script geschreven dat de camera automatisch met een vaste snelheid beweegt
- Een script geschreven dat het spelerobject automatisch beweegt en de gebruiker in staat stelt om de speler naar links en rechts te laten bewegen in de banen
- De baanwisselbeweging gladder gemaakt

**Uitdagingen:**
- Het schrijven van de *levelgenerator* (het auto tile generatie script) was moeilijk en kostte veel tijd
- Ik heb te veel tijd verspild aan het proberen de tile modellen te corrigeren

**Volgende Stappen:**
- De mogelijkheid toevoegen om de speler te laten springen
- De *lerp* een beetje minder maken voor meer competitieve spelers
- Een score- en tijdteller implementeren
- Verzamelbare items verzamelen of maken

## Dag 3 - 2024-05-29

**Doelen voor de dag:**
- De lerp minder traag maken
- Een scoreteller toevoegen die automatisch toeneemt terwijl de speler speelt
- De camera laten volgen wanneer de speler van baan wisselt
- De mogelijkheid toevoegen om de speler te laten springen
- De mogelijkheid toevoegen om van baan te wisselen terwijl de speler springt

**Voltooide Taken:**
- Alle doelen voor vandaag zijn voltooid

**Uitdagingen:**
- De colliders waren behoorlijk vervelend omdat de speler steeds door de grond viel
- Het verbeteren van de grondcontrole kostte wat tijd; ik heb uiteindelijk een raycast gebruikt

**Volgende Stappen:**
- Een scoreteller implementeren
- Verzamelbare items verzamelen of maken
- Obstakels met botsing toevoegen
- Een systeem maken dat bijhoudt wanneer de speler een obstakel raakt
- Ervoor zorgen dat het spel eindigt als de hit counter boven de 2 komt
- Ervoor zorgen dat het spel direct eindigt wanneer de speler een specifiek groot/zwaar obstakel raakt

## Dag 4 - 2024-05-30

**Doelen voor de dag:**
- Obstakels maken/exporteren in Asset Forge
- Willekeurig verschillende obstakels spawnen op elk van de 3 banen
- Colliders/collision toevoegen aan de obstakels
- Een hit counter systeem toevoegen voor het raken van obstakels
- Een scoreteller toevoegen die toeneemt naarmate het spel vordert
- Verzamelbare items maken/exporteren in Asset Forge
- Verzamelbare items implementeren in Unity
- Willekeurig verzamelbare items spawnen op elke baan
- Verzamelbare items kunnen oppakken en score toevoegen aan de scoreteller
- Player spring hoogte aanpassen
- Een counter voor de opgepakte coins toevoegen

**Voltooide Taken:**
- Alle doelen zijn behaald behalve de obstacle hit counter

**Uitdagingen:**
- Het maken van de random obstacle generator was complexer dan ik had verwacht, ik moest het uiteindelijk synchroniseren met de tegelcreatie

**Volgende Stappen:**
- Zeldzame verzamelbare items toevoegen, zodra 5 zijn verzameld, verhoogt de score multiplier met 1
- Een hit counter toevoegen voor wanneer de speler een obstakel raakt
- Het spel stoppen wanneer een speler een groot obstakel raakt
- Een manier vinden om te voorkomen dat de speler vast komt te zitten op kleine obstakels
- Een speelermodel toevoegen
- Animaties toevoegen aan het speelermodel
- Animaties toevoegen aan verzamelbare items
- Een startscherm maken
- Een eindscherm maken
- Een leaderboard maken dat de top 10 laatste runs/spelen opslaat


## Dag 5 - 2024-06-03

**Doelen voor de dag:**
- Zeldzame verzamelbare items toevoegen, zodra 5 zijn verzameld, verhoogt de score multiplier met 1
- Een health system implementeren dat health van de speler verwijdert wanneer obstakels worden geraakt
- Het spel stoppen wanneer de health van de speler op 0 komt
- Knockback toevoegen wanneer de speler een obstakel raakt
- Een speelermodel toevoegen
- Animaties toevoegen aan het speelermodel
- Animaties toevoegen aan verzamelbare items
- Een startscherm maken
- Een eindscherm maken
- Een leaderboard maken dat de top 10 laatste runs/spelen opslaat

**Voltooide Taken:**
- Een health system implementeren dat health van de speler verwijdert wanneer obstakels worden geraakt
- Het spel stoppen wanneer de health van de speler op 0 komt
- Knockback toevoegen wanneer de speler een obstakel raakt

**Uitdagingen:**
- Een goed werkend start scherm, ik moet nog naar iets meer voorbeelden kijken

**Volgende Stappen:**
- Zeldzame verzamelbare items toevoegen, zodra 5 zijn verzameld, verhoogt de score multiplier met 1
- Een speelermodel toevoegen
- Animaties toevoegen aan het speelermodel
- Animaties toevoegen aan verzamelbare items
- Een startscherm maken
- Een eindscherm maken
- Een leaderboard maken dat de top 10 laatste runs/spelen opslaat

## Dag 6 - 2024-06-04

**Doelen voor de dag:**
- Zeldzame verzamelbare items toevoegen, zodra 5 zijn verzameld, verhoogt de score multiplier met 1
- Een speelermodel toevoegen
- Animaties toevoegen aan het speelermodel
- Animaties toevoegen aan verzamelbare items
- Een startscherm maken
- Een eindscherm maken
- Een opties menu toevoegen
- Een leaderboard maken dat de top 10 laatste runs/spelen opslaat

**Voltooide Taken:**
- Een startscherm maken
- Een eindscherm maken
- Een opties menu toevoegen
- restart functionaliteit

**Uitdagingen:**
- Het maken en uitwerken van het main menu en het game over scherm duurde langer dan gedacht

**Volgende Stappen:**
- Zeldzame verzamelbare items toevoegen, zodra 5 zijn verzameld, verhoogt de score multiplier met 1
- Een speelermodel toevoegen
- Animaties toevoegen aan het speelermodel
- Animaties toevoegen aan verzamelbare items
- Een leaderboard maken dat de top 10 laatste runs/spelen opslaat
- Een health bar toevoegen aan de UI
- Een plaatje van een coin toevoegen aan de UI

**Notes for future self:**
- Maybe look into Object Pooling? instead of spawning and deleting objects we use re-use the existing objects from behind the player