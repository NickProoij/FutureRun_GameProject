# Overdrachtsdocument voor Game Project

## Project Informatie

- **Unity Versie**: 2022.3.30f1
- **Scènes**:
  - Main Menu
  - Game

## Belangrijke Scripts en Functionaliteiten

1. **ScoreManager.cs**
   - Beheert de score, munten en vermenigvuldiger van de speler.
   - Verhoogt de score op basis van de afgelegde afstand.
   - Behandelt het toevoegen van punten voor het verzamelen van munten en het verhogen van de vermenigvuldiger door het verzamelen van objecten.
   - Slaat de uiteindelijke score op in het leaderboard.

2. **CameraMovement.cs**
   - Zorgt ervoor dat de camera de speler volgt met een vloeiende overgang.

3. **MainMenu.cs**
   - Behandelt de functionaliteit van het hoofdmenu, inclusief het starten van het spel, afsluiten van de applicatie en het weergeven en resetten van het leaderboard.

4. **PlayerController.cs**
   - Beheert de bewegingen van de speler, inclusief baanwisselingen, springen en botsingen.
   - Behandelt de gezondheid van de speler (begint met 100 health) en toont het eindscherm wanneer de gezondheid op is.
   - Voegt punten toe aan de score en slaat de score op in het leaderboard bij het einde van het spel.

5. **LevelGenerator.cs**
   - Genereert het spelniveau dynamisch door tegels, obstakels en verzamelbare objecten te spawnen.
   - Verwijdert oude tegels en objecten om de prestaties te optimaliseren.

6. **EndScreen.cs**
   - Toont het eindscherm met de uiteindelijke score en het aantal verzamelde munten.
   - Behandelt het herstarten van het spel of terugkeren naar het hoofdmenu.

7. **Leaderboard.cs**
   - Beheert het leaderboard, inclusief het opslaan en laden van scores, en het resetten van het leaderboard.

8. **Obstacle.cs**
   - Behandelt obstakels in het spel en de schade die ze aan de speler toebrengen.

## Specifieke Instructies

- **Leaderboard Opschonen**:
  Om het leaderboard te resetten, is er een functie `ResetLeaderboard()` in het `MainMenu`-script die alle opgeslagen scores verwijdert.

- **Aanpassingen aan de Camera**:
  De camera volgt de speler met een vloeiende overgang. Deze functionaliteit is geïmplementeerd in `CameraMovement.cs`. De offset is zo ingesteld dat de camera de speler op een vaste afstand volgt, rekening houdend met de X- en Z-posities.

- **Health Systeem**:
  Het gezondheidssysteem is geïmplementeerd in `PlayerController.cs`, waarbij de speler begint met 100 health. Obstakels brengen schade toe aan de speler, en wanneer de gezondheid op is, eindigt het spel.

- **Dynamische Level Generatie**:
  Het niveau wordt dynamisch gegenereerd door het `LevelGenerator`-script. Dit script zorgt voor het spawnen van tegels, obstakels en verzamelbare objecten, en verwijdert oude objecten om de prestaties te optimaliseren.

## Vragen of Ondersteuning

Bij vragen of voor verdere ondersteuning kunt u contact opnemen met de oorspronkelijke ontwikkelaar of de projectdocumentatie raadplegen.

---

Met dit document kan een andere engineer gemakkelijk verder werken aan het game project, alle belangrijke scripts en functionaliteiten begrijpen, en de nodige aanpassingen doorvoeren.