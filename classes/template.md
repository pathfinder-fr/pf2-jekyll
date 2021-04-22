---
title: template de la page de chaque classe
---
## Page de la classe

### La classe
Dans les fichiers de traduction de Foundry, il faut pouvoir récupérer les champs suivants :
- **ID** utile pour créer des liens ?

### Il faut générer ensuite chaque classe automatiquement

Pour l'affichage sur la page
- **Nom** en français qui formera le titre de la page
- **Name** en anglais en plus petit à placer en dessous
- **Traduction** : en récupérant le champ `État d'origine:` et en cas d'absence de celui-ci le champ `État`. cette indication permet au lecteur de déterminer si c'est notre traduction ou la traduction officielle en vf, voire de l'auto trad 
- **descriptionFr** il faut que le html soit transformé en markdown et il faut générer le contenu des tableaux. Le contenu de descriptionFr constitue l'affichage principal de la page de la classe et contient le contenu de la colonne de droite de la présentation du Ldb

ATTENTION : Le champ descriptionFr ne contient pas certaines capacités des classes. Par exemple, les trafics du roublard sont simplement mentionnés par leur nom avec leur ID qui donne un lien vers la capacité sous Foundry. ce sera la même chose avec les lignages des ensorceleurs, les ordres druidiques

Hypothèse dans l'ordre de ma préférence
- pouvoir utiliser les liens de la description pour les afficher en utilisant l'id pour les appeler et afficher Nom en Fr suivi de descriptionFr. Voir si techniquement c'est possible et rentable sachant qu'il y a d'autres occurences pour les autres classes. Ce sont les seuls liens sortant de la page Foundry apparemment.
- les ajouter manuellement dans la description de la classe dans le compendium Foundry, cela impose cependant de maintenir deux fichiers
- autre solution ?

## Dons de chaque classe
Idéalement, il faudrait  pouvoir générer automatiquement une page à part intitulée **dons-ancestraux-nomdelaclasse** pour avoir les dons de chaque classe correspondante

Sur la page on porte simplement les dons qui sont propres à chaque classe. 

Les dons de classe sont dans la feats.db.

Pour le tri, il s'agit des dons :
- ayant le `"featType": {value": "class",`
- puis dans le champ `traits` le Name de la classe concernée ex : `"traits": {value": fighter})`.
- pour les données, il faut pouvoir extraire les champs suivants :
    - `Nom:` en français
    - `Name`en anglais
    - `PrereqFR:` qui donne les prérequis du don 
    - `DescriptionFr` qui donne la description du don
    - `Avantage` qui permet de résumer le don
    - `"actionType": {"value":` qui donne le type d'action nécessaire pour utiliser le don
    - `"traits": {"value:` qui donnera les traits du don
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
    - les traits du champ `traits`à traduire à partir du json.fr en récupérant la valeur dans le champ `TraitNomdutraitenanglais`du fr.json
- Sur la quatrième ligne
    - **Prérequis** suivi du contenu du champ `PrereqFr`. ATTENTION Quand il y a plusieurs prérequis pour un même don, pour pouvoir les utiliser dans Foundry ils sont séparés par des "|" qui doivent être remplacés par des *, * à l'affichage dans le wiki. S'il n'existe pas, supprimer l'affichage
- en dessous la description du champ `DescriptionFR`.

On pourrait prévoir dans une colonne à droite sur la page, le nom et le niveau de chaque don avec un lien, pour circuler sur la page

## Champs des fichiers utilisés
Voici les champs d'une class dans les compendiums Foundry (rogue)
```
{
    "_id": "LO9STvskJemPkiAI",
    "data": {"abilityBoostLevels": {"value": [5,10,15,20]},
        "ancestryFeatLevels": {"value": [1,5,9,13,17]},
        "attacks": {"advanced": 0,
            "martial": 0,
        "   other": {"name": "Rapier, Sap, Shortbow, and Shortsword",
        "rank": 1},
            "simple": 1,
            "unarmed": 1},
        "classDC": 1,
        "classFeatLevels": {"value": [1,2,4,6,8,10,12,14,16, 18, 20]},
        "defenses": {
            "heavy": 0,
            "light": 1,
            "medium": 0,
            "unarmored": 1},
        "description": {
            "value": "<p><em>You are skilled and opportunistic . Using your sharp wits and quick reactions, you take advantage of your opponents' missteps and strike where it hurts most. You play a dangerous game, seeking thrills and testing your skills, and likely don't care much for any laws that happen to get in your way. While the path of every rogue is unique and riddled with danger, the one thing you all share in common is the breadth and depth of your skills.</em></p>"},
        "generalFeatLevels": {
            "value": [3,7,11,15,19]},
        "hp": 8,
        "items": {
            "0kdn2": {"id": "WiM7X4xmpMx4s6LD",
                "img": "systems/pf2e/icons/features/classes/weapon-specialization.webp",
                "level": 7,
                "name": "Weapon Specialization (Level 7)",
                "pack": "pf2e.classfeatures"},
            "1nwhg": {
                "id": "v8UNEJR5IDKi8yqa",
                "img": "systems/pf2e/icons/features/classes/weapon-tricks.webp",
                "level": "5",
                "name": "Weapon Tricks",
                "pack": "pf2e.classfeatures"},
            "1zn3l": {
                "id": "SHpjmM4A3Sw4GgDz",
                "img": "systems/pf2e/icons/features/classes/light-armor-master.webp",
                "level": 19,
                "name": "Light Armor Mastery (Level 19)",
                "pack": "pf2e.classfeatures"},
            "2xl4q": {
                "id": "myvcir1LEkaVxOlE",
                "img": "systems/pf2e/icons/features/classes/master-tricks.webp",
                "level": "13",
                "name": "Master Tricks",
                "pack": "pf2e.classfeatures"},
            "4al2i": {
                "id": "Z7HX6TeFsaup7Dx9",
                "img": "systems/pf2e/icons/features/classes/greater-weapon-specialization.webp",
                "level": 15,
                "name": "Greater Weapon Specialization (Level 15)",
                "pack": "pf2e.classfeatures"},
            "517c6": {
                "id": "25GSAotUcDwInYgG",
                "img": "systems/pf2e/icons/features/classes/great-fortitude.webp",
                "level": 9,
                "name": "Great Fortitude (Level 9)",
                "pack": "pf2e.classfeatures"},
            "6ddtr": {
                "id": "pZYkb12t5DSwtts7",
                "img": "systems/pf2e/icons/features/classes/light-armor-expertise.webp",
                "level": 13,
                "name": "Light Armor Expertise (Level 13)",
                "pack": "pf2e.classfeatures"},
            "7w4br": {
                "id": "uGuCGQvUmioFV2Bd",
                "img": "systems/pf2e/icons/features/classes/rogues-racket.webp",
                "level": 1,
                "name": "Rogue's Racket",
                "pack": "pf2e.classfeatures"},
            "8tar0": {
                "id": "nLwPMPLRne1HnL00",
                "img": "systems/pf2e/icons/features/classes/incredible-sense.webp",
                "level": 13,
                "name": "Incredible Senses (Level 13)",
                "pack": "pf2e.classfeatures"},
            "cifjd": {
                "id": "f3Dh32EU4VsHu01b",
                "img": "systems/pf2e/icons/features/classes/rogue-expertise.webp",
                "level": "11",
                "name": "Rogue Expertise",
                "pack": "pf2e.classfeatures"},
            "e9ikq": {
                "id": "j1JE61quDxdge4mg",
                "img": "systems/pf2e/icons/features/classes/sneak-attack.webp",
                "level": "1",
                "name": "Sneak Attack",
                "pack": "pf2e.classfeatures"},
            "g1dmk": {
                "id": "L5D0NwFXdLiVSnk5",
                "img": "systems/pf2e/icons/features/classes/improved-evasion.webp",
                "level": 13,
                "name": "Improved Evasion (Level 13)",
                "pack": "pf2e.classfeatures"},
            "hg9s4": {
                "id": "xmZ7oeTDcQVXegUP",
                "img": "systems/pf2e/icons/features/classes/slippery-mind.webp",
                "level": 17,
                "name": "Slippery Mind",
                "pack": "pf2e.classfeatures"},
            "i70zj": {
                "id": "0npO4rPscGm0dX13",
                "img": "systems/pf2e/icons/features/classes/vigilant-senses.webp",
                "level": 7,
                "name": "Vigilant Senses (Level 7)",
                "pack": "pf2e.classfeatures"},
            "nilrb": {
                "id": "9SruVg2lZpNaYLOB",
                "img": "systems/pf2e/icons/features/classes/debilitating-strikes.webp",
                "level": "9",
                "name": "Debilitating Strikes",
                "pack": "pf2e.classfeatures"},
            "pfjze": {
                "id": "W1FkMHYVDg3yTU5r",
                "img": "systems/pf2e/icons/features/classes/double-debilitation.webp",
                "level": "15",
                "name": "Double Debilitation",
                "pack": "pf2e.classfeatures"},
            "qx6de": {
                "id": "w6rMqmGzhUahdnA7",
                "img": "systems/pf2e/icons/features/classes/surprice-attack.webp",
                "level": "1",
                "name": "Surprise Attack",
                "pack": "pf2e.classfeatures"},
            "sjjee": {
                "id": "PNpmVmD21zViDtGC",
                "img": "systems/pf2e/icons/features/classes/deny-advantage.webp",
                "level": 3,
                "name": "Deny Advantage (Level 3)",
                "pack": "pf2e.classfeatures"},
            "thypm": {
                "id": "SUUdWG0t33VKa5q4",
                "img": "systems/pf2e/icons/features/classes/master-strike.webp",
                "level": 19,
                "name": "Master Strike",
                "pack": "pf2e.classfeatures"},
            "xn3fx": {
                "id": "MV6XIuAgN9uSA0Da",
                "img": "systems/pf2e/icons/features/classes/evasion.webp",
                "level": 7,
                "name": "Evasion (Level 7)",
                "pack": "pf2e.classfeatures"}
        },
        "keyAbility": {
            "value": [
                "cha",
                "dex",
                "int",
                "str"]},
        "perception": 2,
        "rules": [],
        "savingThrows": {
            "fortitude": 1,
            "reflex": 2,
            "will": 2},
        "skillFeatLevels": {"value": ["1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20"]},
        "skillIncreaseLevels": {"value": ["2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20"]},
        "source": {"value": ""},
        "trainedSkills": {"additional": 7,"value": ["ste"]},
        "traits": {
            "custom": "",
            "rarity": {"value": "common"},
            "value": []}},
    "effects": [
        {"_id": "eU0UcjCxRq73jfMb",
            "changes": [
                {"key": "data.attributes.perception.rank",
                    "mode": 4,
                    "value": 2},
                {"key": "data.saves.fortitude.rank",
                    "mode": 4,
                    "value": 1},
                {"key": "data.saves.reflex.rank",
                    "mode": 4,
                    "value": 2},
                {"key": "data.saves.will.rank",
                    "mode": 4,
                    "value": 2},
                {"key": "data.skills.ste.rank",
                    "mode": 4,
                    "value": 1},
                {"key": "data.martial.simple.rank",
                    "mode": 4,
                    "value": 1},
                {"key": "data.martial.weapon-base-rapier.rank",
                    "mode": 4,
                    "value": 1},
                {"key": "data.martial.weapon-base-sap.rank",
                    "mode": 4,
                    "value": 1},
                {"key": "data.martial.weapon-base-shortbow.rank",
                    "mode": 4,
                    "value": 1},
                {"key": "data.martial.weapon-base-shortsword.rank",
                    "mode": 4,
                    "value": 1},
                {"key": "data.martial.unarmed.rank",
                    "mode": 4,
                    "value": 1},
                {"key": "data.martial.unarmored.rank",
                    "mode": 4,
                    "value": 1},
                {"key": "data.martial.light.rank",
                    "mode": 4,
                    "value": 1},
                {"key": "data.attributes.classDC.rank",
                    "mode": 4,
                    "value": 1}],
            "disabled": false,
            "duration": {
                "rounds": null,
                "seconds": null,
                "startRound": null,
                "startTime": null,
                "startTurn": null,
                "turns": null},
            "flags": {},
            "icon": "systems/pf2e/icons/classes/rogue.webp",
            "label": "Initial Proficiencies",
            "origin": "",
            "tint": "",
            "transfer": true}],
    "flags": {},
    "img": "systems/pf2e/icons/classes/rogue.webp",
    "name": "Rogue",
    "permission": {"default": 0},
    "type": "class"}

```
Voici les champs d'un don de classe (adrenaline-rush)
```
{
    "_id": "BBj6jrdyff7QOgjH",
    "data": {
        "actionCategory": {
            "value": ""
        },
        "actionType": {
            "value": "passive"
        },
        "actions": {
            "value": ""
        },
        "description": {
            "chat": "",
            "unidentified": "",
            "value": "<p>In the heat of battle, you are capable of amazing feats of strength.</p>\n<p>While you are @Compendium[pf2e.actionspf2e.Rage]{Raging}, increase your encumbered and maximum Bulk limits by 2; you also gain a +1 status bonus to Athletics checks to lift heavy objects, @Compendium[pf2e.actionspf2e.Escape]{Escape}, and @Compendium[pf2e.actionspf2e.Force Open]{Force Open}.</p>"
        },
        "featType": {
            "value": "class"
        },
        "level": {
            "value": 1
        },
        "location": "",
        "prerequisites": {
            "value": []
        },
        "rules": [
            {
                "key": "PF2E.RuleElement.FlatModifier",
                "label": "Adrenaline Rush (Lift heavy objects, escape, force open)",
                "predicate": {
                    "all": [
                        "rage"
                    ],
                    "any": [
                        "action:escape",
                        "action:force-open",
                        "lift-heavy-object"
                    ]
                },
                "roll-options": [
                    "athletics",
                    "all"
                ],
                "selector": "athletics",
                "type": "status",
                "value": 1
            }
        ],
        "source": {
            "value": ""
        },
        "traits": {
            "custom": "",
            "rarity": {
                "value": "common"
            },
            "value": [
                "barbarian",
                "rage"]}},
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/features/feats/feats.webp",
    "name": "Adrenaline Rush",
    "permission": {"default": 0},
    "type": "feat"}
```
