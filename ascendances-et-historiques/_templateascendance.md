----
Title: template du fichier des ascendances
---
## Page de l'ascendance

### L'ascendance
Dans les fichiers de traduction de Foundry, il faut pouvoir récupérer les champs suivants :
- **ID** utile pour créer des liens ?

Pour l'affichage sur la page
- **Nom** en français qui formera le titre de la page
- **Name** en anglais en plus petit à placer en dessous
- **Traduction** : en récupérant le champ `État d'origine:` et en cas d'absence de celui-ci le champ `État`. cette indication permet au lecteur de déterminer si c'est notre traduction ou la traduction officielle en vf, voire de l'auto trad 
- **description (fr)** le html est transformé en markdown et constitue l'affichage principal de la page de l'ascendance

A la fin de cette page, il faudrait pouvoir afficher :

### **Héritages de l'ascendance** :
les héritages de l'ascendance sont à aller chercher dans la base de données **ancestryfeatures.db**.
    - Pour effectuer le tri dans les fichiers de cette DB pour prendre les seuls héritages correspondant à la race, il faut d'abord trier tous les fichiers qui sont des héritages indiqués par le champ `featType` : `"featType": {"value": "heritage"}`. 
    - Une fois que ces fichiers sont récupérés, il faut faire le tri pour s'assurer que la value du champ trait correspond à la page de l'ascendance sur laquelle on souhaite place l'héritage ex :`"traits": {value": elf})` si `Name` a la valeur elf
    - On peut aussi envisager de récupérer les fichiers ayant le `"traits": {value": versatile heritage})` qui désigne les héritages qu'on peut utiliser avec toutes les ascendances.
    - une fois le tri de ces données effectué, il faut alors faire un cleanup du html de la `descriptionFr` pour la passer en markdown, puis afficher les uns à la suite des autres tous les héritages sélectionnés sur la page de l'ascendance les uns à la suite des autres par ordre alphabétique en affichant  : 
        - le `nom` en gras 
        - le `Name` en petit, 
        - l'`État d'origine:` et en cas d'absence l'`État` qui permet de montrer si c'est une traduction libre ou officielle 
        - la `descriptionFr` à afficher en markdown

### Informations complémentaires
Dans une colonne séparée située à droite de la page de l'ascendance, il faut aller chercher d'autres du json de l'ascendance pour afficher  
- **Points de vie**
situé dans le champ `hp` du json de l'ancestry. C'est une valeur numérique qui ne nécessite pas de retraitement
 
- **Taille**  
Afficher la valeur du champ qu'il faut aller chercher dans le champ `size`du json de l'ascendance. La taille est en anglais. Il y aura plusieurs valeurs possibles "med" "sm" "tiny". Dans le fr.json, pour récupérer la valeur d'affichage on a pas mieux que :
```
    "ActorSizeGargantuan": "Gigantesque",
    "ActorSizeHuge": "Très Grande",
    "ActorSizeLarge": "Grande",
    "Actor**SizeMed**ium": "Moyenne",
    "Actor**SizeSm**all": "Petite",
    "Actor**SizeTiny**": "Très petite",
```

- **Vitesse**
Le champ `speed` de l'ancestry donne une valauer numérique qui correspond à la vitesse mais en pieds. 
    - On peut convertir en divisant par 5 , puis en multipliant par 1,5 la valeur donnée en pieds et en ajoutant la lettre m. 
    - On peut réfléchir à afficher en m et en pieds
    - On peut afficher en cases en divisant par 5

**Primes de caractéristiques**
Elles sont dans les champs `boosts 0, 1, 2` du json de chaque ascendance. 
    - Les champs `boost 0 et 1` contiennent des valeurs en anglais et sous forme d'abbréviation (str, dex, con, int, wis, cha). Pour les traduire, on peut aller récupérer dans le fr.json du système de Foundry le champ "AbilityXxx" où Xxx est égal aux trois premières lettre de la caractéristique ("AbilityStr": pour str) pour récupérer la valeur d'affichage de la caractéritique. Cela devrait afficher deux primes de caractéristique
    - le champ `boost 2` pose un problème puisqu'il comprend toutes les caractéristiques car cela signifie dans Foundry qu'on peut allouer la prime dans n'importe quelle caractéristique. Quand il y a plusieurs valeurs, il faudrait prévoir que l'affichage indique *Prime de caractéristique libre*

Si la règle peut être codée cela permettrait de coder aussi l'humain qui dispose de deux primes de caractéristique libre de sorte que les champs `boosts 0, 1,` indiquent toutes les deux les 6 caractéristiques. Cela permettrait d'afficher deux fois *Prime de caractéristique libre*

- **Pénalité de caractéristique**
Ce qui doit être affiché doit être récupéré dans le champ `flaws` du json de l'ancestry. Comme les primes de caractéristique, la pénalité est en anglais et sous forme abrégée (ex : str). Idem que ci-dessus pour afficher la valeur de la caractéristique en français. S'il n'y en a pas, il faudrait ne pas afficher du tout la pénalité

- **langues** 
Le champ `languages` de l'ancestry donne les langues parlées d'office par l'ascendance. Elles peuvent être traduites à partir du fr.json en récupérant la valeur d'affichage du champ `Languagenomdelalangueenanglais` pour obtenir la valeur d'affichage en français 

- *Langues supplémentaires* 
le champ `additionallanguages` de l'ancestry donne les langues qu'un membre de l'ascendance peut parler s'il dispose d'un modif d'Intelligence positif. Comme au-dessus, elles sont en anglais et peuvent être traduites à partir du fr.json et du champ `Languagenomdelaraceanglaise`   

- **traits**
`trait value` donne les traits de l'ascendance en anglais. La valeur est ensuite à récupérer dans le fr.json pour l'afficher en français dans le champ `TraitNomdutraitanglais` 

- viennent ensuite les capacités de l'ascendance. 
Le champ `items name` donne les ID qui correspondent à des capacités de l'ascendance qui se trouvent toutes dans la **ancestryfeatures.db**. L'ID permet d'aller chercher chacune des capacités de la race pour pouvoir les afficher sur la page. Cela concerne soit des capacités, soit la vision dans le noir ou nocturne de l'ascendance.

Dans la **ancestryfeatures.db** en allant chercher le fichier dont l'ID a été récupérée au-dessus, on récupère pour l'affichage :
    - **nom**  
    - **descriptionFr**
    - **Traduction** État d'origine ou État antérieur

## Dons de l'ascendance
Idéalement, il faudrait ensuite pouvoir générer automatiquement une page à part intitulée **dons-ancestraux-nomdelascendance**.

Sur la page on porte simplement les dons qui sont propres à chaque ascendance. 
Les dons ancestraux sont dans la feats.db.
Pour le tri, il s'agit des dons :
- ayant le `"featType": {value": "ancestry",`
- puis dans le champ `trait` le nom de l'ascendance concernée ex : `"traits": {value": elf})`.
- pour les données, il faut pouvoir extraire les champs suivants :
    - `Nom:` en français
    - `Name`en anglais
    - `PrereqFR:` qui donne les prérequis du don 
    - `DescriptionFr` qui donne la description du don
    - `Avantage` qui permet de résumer le don
    - `"actionType": {"value":` qui donne le type d'action nécessaire pour utiliser le don  
    - `"level": {value":` qui donne le niveau sous une forme numérique ne nécessitant pas de traitement,
    - `"rarity": {"value":` qui donne les valeurs common, uncommon ou rare qui peuvent être traduites à partir du fr.json

Une fois qu'on a récupéré ces champs, sur la page, l'affichage des dons se fait en les triant par niveau dans l'ordre croissant, puis au sein du même niveau, par ordre alphabétique en affichant : 
- Sur la première ligne
    - *Nom* du don
    - suivi  du *type d'action* du champ `actionType` ATTENTION la valeur doit être convertie en image à récupérer dans le fichiers images du pf2-jekyll) 
    - suivi le plus à droite possible (un float right ?) la mention **niveau** suivi du contenu du champ `niveau` 
- Sur la deuxième ligne
    - le nom anglais du champ `Name` entre parenthèses, en plus petit
- Sur la troisième ligne
    - **Prérequis** suivi du contenu du champ `PrereqFr`. ATTENTION Quand il y a plusieurs prérequis pour un même don, ils sont séparés par des "|" qui doivent être remplacés par des *, * à l'affichage dans le wiki. S'il n'existe pas, supprimer l'affichage
- en dessous la description du champ `DescriptionFR`.

On pourrait prévoir dans une colonne à droite sur la page, le nom et le niveau de chaque don avec un lien, pour circuler sur la page

## Notes sur les héritages versatiles
Certains héritages d'ascendance sont versatiles.
Les pages descriptives des héritages versatiles n'existent pas dans le compendium Foundry en tant que telles.
Les héritages versatiles permettent d'obtenir les dons d'une ascendances mais aussi des dons propres à l'héritage versatile.
Ces héritages permettent d'accéder à des dons particuliers

Il va falloir repérer les dons ayant le trait d'un héritage versatile et traduire les pages des héritages versatiles qui ne sont pas traduites dans les compendium Foundry :
- pages des dons des demi-elfes et leurs dons 
- page des demi-orcs et leurs dons,
- Aasimar
- Aphorite
- Changeling
- Dhampir
- Duskwalker
- Ganzi
- Ifrit
- Oread
- Suli
- Sylph
- Tiefling
- Undine
- Beastkin

## Champs des fichiers utilisés
Voici les champs d'une ascendance dans les compendiums Foundry (Android)
```
{
    "_id": "GfLwE884NoRC7cRi",
    "data": {
        "additionalLanguages": {"count": 0,
            "custom": "",
            "value": ["abyssal", "celestial","draconic","dwarven","undercommon","utopian"]},
        "boosts": {"0": {"value": ["dex"]},
            "1": {"value": ["int"]},
            "2": {"value": ["str", "dex", "con", "int", "wis", "cha"]}},
        "description": {"value": "<p><em>Technological wonders from another world, androids have synthetic bodies and living souls. Their dual nature makes them quick-thinking and calm under pressure, but </em><em>comfortable in stillness and solitude.</em></p>"},
        "flaws": {"0": {"value": ["cha"]}},
        "hp": 8,
        "items": {"0898m": {"id": "DRtaqOHXTRtGRIUT",
                    "img": "systems/pf2e/icons/features/ancestry/low-light-vision.webp",
                    "level": 1,
                    "name": "Low-Light Vision",
                    "pack": "pf2e.ancestryfeatures"},
                "fvpio": {"id": "uSAYmU7PO2QoOWhB",
                    "img": "systems/pf2e/icons/default-icons/feat.svg",
                    "level": 1,
                    "name": "Emotionally Unaware",
                    "pack": "pf2e.ancestryfeatures"},
                "ouwoo": {"id": "mnhmhOKWLiOD0lev",
                    "img": "systems/pf2e/icons/default-icons/feat.svg",
                    "level": 1,
                    "name": "Constructed",
                    "pack": "pf2e.ancestryfeatures"}},
        "languages": {"custom": "",
            "value": ["androffan","common"]},
        "reach": 5,
        "rules": [],
        "size": "med",
        "source": {"value": ""
        },
        "speed": 25,
        "traits": {
            "custom": "",
            "rarity": {"value": "rare"},
            "value": ["android","humanoid"]
        },
        "vision": "normal"},
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/default-icons/alternatives/ancestries/android.svg",
    "name": "Android",
    "permission": {"default": 0},
    "type": "ancestry"
}

```
Voici les champs d'un héritage dans le compendium ancestryfeatures.db 
```
{
    "_id": "Nd9hdX8rdYyRozw8",
    "data": {"actionCategory": {"value": ""},
        "actionType": {"value": "passive"},
        "actions": {"value": ""},
        "description": {
            "chat": "",
            "unidentified": "",
            "value": "<p>In your long life, you've dabbled in many paths and many styles. Choose a class other than your own. You gain the multiclass dedication feat for that class, even though you don't meet its level prerequisite. You must still meet its other prerequisites to gain the feat.</p>"},
        "featType": {"value": "heritage"},
        "level": {"value": 1},
        "prerequisites": {"value": []},
        "rules": [],
        "source": {"value": ""},
        "traits": {
            "custom": "",
            "rarity": {"value": "common"},
            "selected": {"elf": "Elf"},
            "value": ["elf"]}},
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/features/ancestry/ancient-elf.webp",
    "name": "Ancient Elf",
    "permission": {"default": 0},
    "type": "feat"
}
```
Voici les champs d'un don ancestral (orc ferocity)
```
{
    "_id": "PlhPpdwIV0rIAJ8K",
    "data": {
        "actionCategory": {"value": ""},
        "actionType": {"value": "reaction"},
        "actions": {"value": ""},
        "description": {
            "chat": "",
            "unidentified": "",
            "value": "<p><strong>Frequency</strong> once per day</p>\n<p><strong>Trigger</strong> You would be reduced to 0 Hit Points but not immediately killed</p>\n<hr />\n<p>Fierceness in battle runs through your blood, and you refuse to fall from your injuries. You avoid being knocked out and remain at 1 Hit Point, and your wounded condition increases by 1.</p>"},
        "featType": {            "value": "ancestry"},
        "level": {"value": 1},
        "prerequisites": {
            "value": []},
        "rules": [],
        "source": {"value": ""},
        "traits": {
            "custom": "",
            "rarity": {"value": "common"},
            "value": ["orc"]}},
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/features/feats/feats.webp",
    "name": "Orc Ferocity",
    "permission": {"default": 0},
    "type": "feat"
}
```
