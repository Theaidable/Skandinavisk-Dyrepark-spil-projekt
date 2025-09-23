# Skandinavisk Dyrepark spil projekt

## Kort overblik over UI.
Hele menusystemet i spillet består af: **StartMenu**, **PauseMenu**, **SettingsMenu**, **TryAgainMenu**.
Alt er bygget med PNG ikoner (ingen tekst på knapperne).
MMD kan ændre på udseendet direkte i Unity, f.eks udskiftning af billeder, flytte rundt på knapper eller ændre på farve og størrelse på tekster.
Lydknappen kan også få nye ikoner (sound on/off), og tekster som Score/Timer/Slutresultat kan ændres på under Inspector (farve, font, størrelse, placering).
Selve funktionerne er kodet, så det visuelle er det eneste de behøver at ændre.
Vores start scenen er **MapScene** som er kortet over parken og spille scenen er **SkandiPark**.

**UI og menusystemet**
Delen her er ift. brugerfladen i appen - **StartMenu**, **PauseMenu**, **SettingsMenu**, **TryAgainMenu**.

**Hvordan UI/Menu er bygget op**
- **UI Paneler**
  - **StartMenu**: Det bliver vist når scenen bliver kaldt, ved at man trykker på det lille isbjørne ikon på kortet, hvor der så kommer ikoner frem til at vælge hvad man så vil gøre, play, settings eller gå tilbage til kortet.
  - **PauseMenu**: Det bliver vist når man midt under spillet trykker på pause knappen oppe i højre hjørne, hvor så en pause menu kommer frem. Du kan herefter så vælger mellem Resume, Sound on/off eller gå tilbage til kortet.
  - **TryAgainMenu**: Når tiden er løbet ud, kommer denne menu frem, hvor du så kan vælge at prøve igen eller gå tilbage til kortet.
  - **SettingsMenu**: Tilgås fra **StartMenu** og indeholder lydknappen og tilbage til kortet.

- **Knapper**
  - Alle knapperne som er lavet bruger bare en simpel PNG med et flat design, der er kun brugt billeder, ingen tekst.

- **Scripts**
  - **StartMenuController.cs**: Til styringen af **StartMenu** og **SettingsMenu**.
  - **UIManagerGameOne.cs**: Til styringen af **Score**, **Timer**, **PauseMenu** og **TryAgainMenu**.
  - **SoundToggle.cs**: Til styring af om man vælger lyd til eller fra. Ikonet skifter også for at vise om sound er on eller off.
 
**Hvis der vil ændres i noget af det**
- **Grafisk**
  - Knapper bruger PNG billeder. For at ændre et ikon:
    1. Find knappen du vil ændre i **Hierarchy** (venstre side) og tryk på knappen.
    2. I **Inspector** (højre side) skal man finde **Image (Script)**.
    3. Udskift billedet med et nyt.
    - Knappernes farve og størrelse kan ændres i **Inspector**. Placering kan ændres med **Move Tool** i Unity ved at trække dem rundt efter tool er valgt.

  - **Lydknappen** findes i både **PauseMenu** og **SettingsMenu**.
    1. For at finde dem skal man kigge i **Hierarchy**.
    2. Når menuen er fundet, skal man folde den ud så man kan se hvad der ligger under den.
    3. Derefter vælger man så **SoundButton**.
    4. Over i **Inspector** skal man rulle ned og finde **SoundToggle (Script)**.
    5. Der vil være 3 felter:
       1. **Sound On Icon** som er til ikonet for Sound On.
       2. **Sound Off Icon** som er til ikonet for Sound Off.
       3. **Button Image** er selve billed objektet (skal ikke ændres).
    - Træk et nyt billede ind i enten Sound On eller Off Icon for at ændre billedet.

- **Tekst**
  - UI tekster som **Score**, **Timer**, og **SlutResultat** bliver vist med TextMeshPro.
  - Man kan ændre **farve, font, størrelse og placering**.
    1. Find over i **Hierarchy** (venstre side), **scoreText**, **timerText** eller **finalScoreText**.
    2. Klik på den tekst man vil ændre.
    3. I **Inspector** (højre side) kan man så finde tools til at ændre på **farve, font, størrelse og placering**.
  - Tekst som f.eks **"Score:"** er kodet ind i scriptet og kan ikke ændres uden at redigere koden.
