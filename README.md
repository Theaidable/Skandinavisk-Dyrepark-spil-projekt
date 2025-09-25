# Skandinavisk Dyrepark spil projekt
I denne README fil vil du finde en kort guide til at arbejde videre med "Whack A Seal". README'en forklarer, hvordan du:
- Ændrer grafik og baggrund via GraphicsBank
- Opdaterer lyde via SoundBank
- Redigerer prefabs, og ændrer variabler på de forskellige huller
- Styrer sværhedsgrad af spillets progression
- Laver et WebGL build
## 1. Scener
Spillet består af 2 scener:
- MapMenu -> Dette er spillets hovedmenu, hvorfra man starter spillet. I vores tilfælde starter man fra Skandinavisk Dyreparks kort over parken. Hvis man spiller spillet, så skal man klikke på logoet med isbjørnen for at skifte scene til selve spillet
- SkandiPark -> Dette er scenen med selve "gameplayet"
### Sådan åbner/skifter du scene
1. Åbn Project-vinduet og gå til **Assets/Scenes/**
2. **Dobbeltklik** på **MapMenu** eller **SkandiPark** for at åbne scenen.
3. Tryk **Play** for at køre den aktuelle scene

## 2. Ændre grafik via GraphicsBank
Til at ændre grafik har vi lavet en "bank" for at alt grafik. Man kan finde GraphicsBank ved at åbne **Assets/Content/Scriptable Asssets/** og klikke 1 gang på asseten "GraphicsBank". Når man har klikken på banken, så kan man i højre side af Unity, i inspector vinduet, ændrer grafikken inde i spillet eller tilføje sprites. 

De mapper man kan tilføje sprites til er følgende:
- Map
- Background -> Materials
- Objects -> Hole
- Objects -> PolarBear
- Objects -> Seal
- UI -> Buttons

Man importere sprites ved at gå til bunden af inspectoren, under overskriften "Importere sprites". Man skal nu vælge en mappe som man vil tilføje sit sprite til. Dette gøres med brug af Scrolldown menuen, eller man kan trykke på "Vælg Mappe" for at åbne stifinderen og finde de mulige mapper. Man kan derudover trykke "Åbn Mappe" for at åbne mappen og se hvilke filer der er inde i mappen, hvor man også kan slette filerne herfra. Hvis man gerne vil finde mappen inde i projektet, så kan man klikke på "Ping i project", og mappen vil nu lyse inde i projektet. Hvis man laver nogle ændringer i mapperne, tilføjelser eller redigering af mapperne, og unity ikke har opfanget det, så kan man klikke på "Genindlæs mapper", som opdaterer det hele.

For at skifte sprite på de forskellige felter, så kan man enten trække sit PNG hen på feltet, eller man kan klikke på den lille cirkel til højre for feltet til at lede efter det sprite man har tilføjet.

Man kan ændre udseende på følgende:
**1. Sæl og isbjørn**
- For begge er der et felt for deres normale udseende, og udseendet efter de er blevet slået.
- For sælen er det Standard Seal, og Standard Seal Hit
- For isbjørnen er det Polar Bear, og Polar Bear Hit

**2. UI -> InGameOverLay**
- Dette er det UI overlay som bliver vist mens spillet kører
- ScoreFrame er den boks som ens "score" bliver vist i
- TimerFrame er den boks som tiden bliver vist i
- PauseButton er den knap som man kan bruge til at pause spillet

**3. UI -> PauseMenu**
- Dette er det UI overlau som bliver vist mens spillet er pauset
- Resume Button er den knap man bruger til at starte spillet igen
- Main Menu Button er den knap som fører tilbage til kortet (main menu)
- Pause Sound Button er den knap som tænder/slukker lyden

## 3. Ændre lyd via SoundBank

## 4. Prefabs: Redigere huller

## 5. Ændre sværhedsgrad af spillet

## 6. WebGL build

-----------------------

## Kort overblik over UI.
Hele menusystemet i spillet består af: **StartMenu**, **PauseMenu**, **SettingsMenu**, **TryAgainMenu**.
Alt er bygget med PNG ikoner (ingen tekst på knapperne).
MMD kan ændre på udseendet direkte i Unity, f.eks udskiftning af billeder, flytte rundt på knapper eller ændre på farve og størrelse på tekster.
Lydknappen kan også få nye ikoner (sound on/off), og tekster som Score/Timer/Slutresultat kan ændres på under Inspector (farve, font, størrelse, placering).
Selve funktionerne er kodet, så det visuelle er det eneste de behøver at ændre.
Vores start scene er **MapScene** som er kortet over parken, og spillets scene er **SkandiPark**.

**UI og menusystemet**
Delen her er ift. brugerfladen i appen - **StartMenu**, **PauseMenu**, **SettingsMenu**, **TryAgainMenu**.

**Hvordan UI/Menu er bygget op**
- **UI Paneler**
  - **StartMenu**: Det bliver vist når scenen bliver kaldt, ved at man trykker på det lille isbjørne ikon på kortet, hvor der så kommer ikoner frem til at vælge hvad man så vil gøre, play, settings eller gå tilbage til kortet.
  - **PauseMenu**: Det bliver vist når man midt under spillet trykker på pause knappen oppe i højre hjørne, hvor så en pause menu kommer frem. Du kan herefter så vælger mellem Resume, Sound on/off eller gå tilbage til kortet.
  - **TryAgainMenu**: Når tiden er løbet ud, kommer denne menu frem, hvor du så kan vælge at prøve igen eller gå tilbage til kortet.
  - **SettingsMenu**: Tilgås fra **StartMenu** og indeholder lydknappen og tilbage til kortet.

- **Knapper**
  - Alle knapper som er lavet bruger en simpel PNG med et flat design, hvor der er kun brugt billeder, ingen tekst.

- **Scripts** -> Det her tænker jeg skal slettes
  - **StartMenuController.cs**: Til styringen af **StartMenu** og **SettingsMenu**.
  - **UIManagerGameOne.cs**: Til styringen af **Score**, **Timer**, **PauseMenu** og **TryAgainMenu**.
  - **SoundToggle.cs**: Til styring af om man vælger lyd til eller fra. Ikonet skifter også for at vise om sound er on eller off.
 
**Hvis der vil ændres i noget af det**
- **Grafisk** -> Der skal indsættes billeder i hvert step for at man kan nemt finde rundt
  - Knapper bruger PNG billeder. For at ændre et ikon:
    1. Find knappen du vil ændre i **Hierarchy** (venstre side) og tryk på knappen.
    2. I **Inspector** (højre side) skal man finde **Image**.
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
