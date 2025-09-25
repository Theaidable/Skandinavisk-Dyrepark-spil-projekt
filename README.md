# Skandinavisk Dyrepark spil projekt
I denne README fil vil du finde en kort guide til at arbejde videre med "Whack A Seal". README'en forklarer, hvordan du:
- Ændrer grafik og baggrund via GraphicsBank
- Opdaterer lyde via SoundBank
- Redigerer prefabs, og ændrer variabler på de forskellige huller
- Styrer sværhedsgrad af spillets progression

## 1. Scener
Spillet består af 2 scener:
- MapMenu -> Dette er spillets hovedmenu, hvorfra man starter spillet. I vores tilfælde starter man fra Skandinavisk Dyreparks kort over parken. Hvis man spiller spillet, så skal man klikke på logoet med isbjørnen for at skifte scene til selve spillet.
- SkandiPark -> Dette er scenen med selve "gameplayet".
### Sådan åbner/skifter du scene
1. Åbn Project-vinduet og gå til **Assets/Scenes/**.
2. **Dobbeltklik** på **MapMenu** eller **SkandiPark** for at åbne scenen.
3. Tryk **Play** for at køre den aktuelle scene.

## 2. Kort overblik over UI.
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

## 3. Ændre grafik via GraphicsBank
GraphicsBank samler al grafik ét sted, så du kan udskifte udseende uden kode.

### Hvor ligger den?
- Åbn **Assets/Content/Scritable Assets/** og klik på GraphicsBank.
- Når asset'et er markeret, kan du ændre felter i **Inspector-vinduet** til højre.

### Tilføj/organisér sprites (importværktøjet i bunden)
- **Importér til mappe:** Vælg en undermappe i dropdown -> Map, Background, Objects -> Hole, PolarBear, Seal eller UI -> Buttons.
- Man kan også importér til mappe ved at klikke på **"Vælg Mappe"** for at bruge stifinder.
- **"Åbn Mappe" / "Ping i Project"** åbnet mappen i stifinder, eller fremhæver den i Unity's Project-vindue.
- **"Genindlæs Mapper"** bruges hvis du har oprettet/omdøbet mapper, og dropdown'en ikke er opdateret endnu.

Importeren kopierer filen ind i porjektets mappe og giver automatisk et unikt filnavn. Der er ikke noget som bliver overskrevet.

### Skift sprites på felterne
- Træk et **PNG, JPG eller JPEG** direkte ind på feltet, eller klik den lille cirkel til højre i feltet for at vælge et eksisterende asset.
- Ændringer ses øjeblikkeligt i editoren for UI og baggrund.

### Hvad kan du ændre?
**1. Sæl og Isbjørn**
- Hver figur har to felter -> Alimndeligt udseende og hit-udseende (når de er blevet slået/ramt).
  - Sælen har felteren **Standard Seal** og **Standard Seal Hit**.
  - Isbjørnen har **Polar Bear** og **Polar Bear Hit**.

**2. UI -> InGameOverLay (vises mens spillet kører)**
- **Score Framer** -> Rammen/box omkring scoren.
- **Timer Framer** -> Rammen/box omkring timeren.
- **Pause Button** -> Knappen øverst til højre.

**3. UI -> PauseMenu (vises når spillet er pauset)**
- **Resume Button** -> Fortsætter spillet.
- **Main Menu Button** -> Tilbage til kortet.
- **Pause Sound Button** -> Ikon for lyd **tændt**.
- **Pause Sound Button Off** -> Ikon for lyd **slukket**.

**4. UI -> TryAgainMenu (vises når tiden er gået)**
- **Try Again Menu** -> Start runden forfra.
- **Back To Main Menu** -> Tilbage til kortet.

**5. UI -> StartMenu (vises før gameplay)**
- **Play Button** -> Start spillet.
- **Settings Button** -> Åbner settings.
- **Back Button** -> Tilbage til main menu/kort.

**6. UI -> SettingsMenu**
- **Settings Sound Button** -> Lyd **tændt**.
- **Settings Sound Button Off** -> Lyd **slukket**.
- **Settings Back Button** -> Tilbage til StartMenu.

Alle knapper som er lavet bruger en simpel PNG med et flat design, hvor der er kun brugt billeder, ingen tekst.

**7. Background**
- **Background Texture** -> Udskfiter selve billedet på baggrundens materiale.

Der er nogle knapper som har samme funktion, hvor samme sprite kan bruges, men det er opsat sådan at man har muligheden for at have forskelligt udseende til forskellige menuer hvis det er noget man har lyst til.

**Tekst**
  - UI tekster som **Score**, **Timer**, og **SlutResultat** bliver vist med TextMeshPro.
  - Man kan ændre **farve, font, størrelse og placering**.
    1. Find over i **Hierarchy** (venstre side), **scoreText**, **timerText** eller **finalScoreText**.
    2. Klik på den tekst man vil ændre.
    3. I **Inspector** (højre side) kan man så finde tools til at ændre på **farve, font, størrelse og placering**.
  - Tekst som f.eks **"Score:"** er kodet ind i scriptet og kan ikke ændres uden at redigere koden.

## 4. Ændre lyd via SoundBank
**SoundBank** samler al lyd ét sted -> Baggrundsmusik (BGM) og effekter (SFX). Herfra kan du skifte klip og justere lydniveauet uden at røre kode.

### Hvor ligger den?
- Åbn **Assets/Content/Scriptable Assets/** og klik på **SoundBank**.
- Klik asset'et og brug **Inspector-vinduet** i højre side.

###Importér/organisér lyd (importværktøjet i bunden)
I bunden af Inspector-vinduet finder du **"Importér Lyd"**
- **Imporér til mappe** -> Vælg hvilken mappen der skal tilføjes lyd til, enten BGM eller SFX.
- Klik på **"Tilføj lyd til valgte mappe"**, og vælg enten en .wav, .mp3 eller .ogg fil og tilføj til en mappe.
- På samme måde som med grafik kan man nu skifte lydklippene ved at trække en lydfil ind i feltet, eller klik den lille cirkel for at vælge et eksisterende klip.
- **Music Volume** og **SFX Volume** kan justere lyd niveauet på lydklippene.

### Hvad kan du ændre?
**1. Baggrundsmusik**
- **Background Music** -> Hovedmusik i selve spilscenen.

**2. Lydeffekter**
- **Seal Hit Sfx** -> Når en sæl rammes.
- **Polarbear Hit Sfx** -> Hvis man rammer isbjørnen.
- **Pop Up Sfx** -> Når en sæl/isbjørn kommer op ad et hul.
- **Button Click** -> UI-knappe klikke lyd.

## 4. Prefabs: Redigere huller
Hullerne er bygget som **prefabs**, så én ændring kan ændre alle huller på samme tid.

### Hvor finder jeg dem?
- Gå til **Assets/Prefabs/**, hvor man nu finder to prefabs.
  - SealHole -> Varianten der bruges i spilscenen, og dermed er det et hul med en isring.
  - SealHoleNoIce -> Varianten her har en ingen isring (kunne bruges hvis vi fik implementeret global-opvarmning event).
 
Dobbeltklik på en prefab for at åbne den i **Prefab Mode** og redigere den.

### Hvad kan du ændre i prefab'en?
**1. Spawn-rates (SealController)**
- Åbn child-objektet Seal i prefab'en og vælg SealController.
- Felterne for **spawn-rates** bestemmer sandsynligheden for sæl vs isbjørn spawn.
  - Sørg for, at summer bliver 1.0 (100%).
  - I øjeblikket er der Seal = 0.75, Polar Bear = 0.25 -> 75% for seal spawn og 25% chance for polar bear spawn.
- Disse værdier påvirker kun hvilken figur der popper op af hullet -> Hastighed/timings styrer af DifficultyManageren.

**2. Klik-område (collider)**
- På **Seal** kan du ændre størrelsen af collideren inde i **BoxCollider2D**.
  - Større collider giver et større område, hvor spilleren kan "slå".

**3. Hullets udseende (ring-sprite)**
- Find child-objektet **IceHole**.
- Skift **sprite** direkte her, da det ikke er blevet tilføjet til GraphicsBank endnu...

### Huller i scenen
- Man kan finde hullerne under ----- Game ---- / SealHoleManager /
- Man kan ændre placeringen af de nuværende huller ved at klikke på et SealHole, og justér **Transform -> Postion (X/Y)** for at flytte det.
- Hvis du vil skfite variant, så kan du højreklikke på hullet **-> Prefab -> Select Asset -> SealHole / SealHoleNoIce**.
- Man kan tilføje flere huller ved at trække et prefab ind i scenen og ændre dens placering.

## 5. Ændre sværhedsgrad af spillet
Sværhedsgraden styrer centralt af DifficultyManager (sidder på SealHoleManager i scenen). Manageren bestemmer følgende:
- Tempoet -> Hvor hurtigt hullere popper op/ned. Denne variabel er blevet kaldt Base Show Duration.
- Hvor længe en sæl/isbjørn er oppe. Denne variabel er blevet kaldt Base Stay Duration.
- Hvor mange huller der er akjtive undervejs i runden. Dette er variablen Base Delay Range, hvilket gør at der går et tilfædigt tal mellem x og y inden den næste spawner, så spawning er mere tilfældigt.
- Der er nogle levels i spillet som øger sværhedsgraden, hvor man kan styre tiden mellem øgning af level via variablen Difficult Increase Interval.
