---
Title: Description des sorts
---
## Format des sorts
Les sorts suivent le format suivant :  
NOM &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SORT 1  
TRAIT DE RARETE TRAITS  
**Traditions** arcanique, divine, occulte, primordiale  
**Incantation** 10 min (matériel, somatique, verbal) ; **Conditions** une cloche en argent de 3 po comme focaliseur  **Déclencheur** Une créature accomplit un acte
**Portée** contact ; **Zone** explosion 6 m  **Cibles** une créature volontaire
**Jet de sauvegarde** Réflexes basique **Durée** 8 h

---

Description  
**Succès critique** texte  
**Succès** texte 

---

**Intensifié** texte

## Les champs à extraire des bases de données
Pour pouvoir afficher chaque sort, les sorts focalisés, il faudra pouvoir disposer de tous les champs suivants en français. Certains devront être extraits et traduits avant de pouvoir être proprement réinjectés dans le wiki. D'autres devront être traduits à partir du fr.json du système Pf2.

### Les nouveaux champs à créer pour les extraire et les traduire dans Foundry :
Il faudrait pouvoir créer les champs suivants dans les fichiers des sorts de la spells.db dans les fichiers du compendium foundry pour pouvoir y mettre, lorsque les valeurs existent :
- un **champ pour le résumé court** qui sera affiché dans les tableaux des listes de sorts pour résumer les effets du sort
    - récupérer les résumés courts en vo des sorts sur le site Archive of nethys pour les placer dans le résumé 
    - créer un champ `resume vo` pour y placer le contenu récupéré
    - créer un champ `resume (vf)` pour nous permettre d'y porter la traduction
- un **champ pour la zone** du sort : 
    - extraire la valeur du champ `areasize:` dans la spells.db en vo des sorts
    - créer un champ `areasize (fr):` pour nous permettre d'y porter la traduction
- un **champ pour les composants matériels** particuliers de certains sorts
    - extraire la valeur du champ `materials:` 
    - créer un champ `materials (fr):` pour nous permettre d'y porter la traduction
- un **champ pour la Portée** puisque les distances sont inscrites en pieds en dur 100 feet' dans les fichiers spells
    - extraire la valeur du champ `range:` 
    - créer un champ `range (fr):` pour qu'on puisse traduire les portées que je pourrais automatiser en grande partie sous VSC avec un trouver remplacer les occurences les plus fréquentes
- un **champ pour la cible**
    - il faudrait extraire le champ `target:`
    - créer un champ `target (fr):`  pour qu'on puisse traduire les cibles
- un champ pour les **Incantateurs secondaires** 
    - il faudrait extraire le champ `secondarycasters:` 
    - créer un champ `secondarycasters (fr):`
- un champ pour le **Test Principal** 
    - il faudrait extraire le champ `primarycheck:`
    - créer un champ `primarycheck: (fr)`
- un champ poue le **Test secondaire** 
    - il faudrait extraire le champ `secondarycheck:`
    -  créer un champ `secondarycheck (fr):`

### Les valeurs des champs que l'on peut récupérer dans le pack anglophone quitte à les traduire automatiquement
- l'**ID** pour obtenir chaque sort et s'assurer la concordance
- le **type** de sort que l'on trouve dans le champ `spellcategorie:` avec categor**ie** à la fin du champ anglophone qui donne une des valeurs suivantes : *spell, ritual, focus* ATTENTION il y a un autre champ spellcategor**y** avec un y qui décrit autre chose et qu'il ne faut pas utiliser
- **Niveau** dans le champ `level:` du compendium anglophone qui est réutilisable sans changement
- **traits** qui sont dans le champ `traits:` 
- **Tradition(s)** qui se trouve dans le champ `traditions:` 
- **Incantation** qui nécessite les champs `time` et `components:`
- **Jet de sauvegarde** dans le champ `save:`
   - avec un champ `basic:` qui indique si le jet est basique ou non
   - un champ `value:` qui indique Reflexe, Will, Fortitude 
- **Durée** dans le champ `duration:`

### Dans les compendiums français
- **Nom du sort** dans le champ `Nom (fr):` du compendium Foundry qui se place à gauche en majuscules
- **Description** comprenant les **Prérequis** et l'**Intensification**
- **Déclencheur** Les déclencheurs sont actuellement dans le champ description et je propose qu'on les y laisse.
- **Succès** Idem
- **Intensification** Idem

Une fois extraits et traduits, on y prendra aussi, si la valeur n'est pas nulle :
- **Condition** dans le champ `materials (fr):` 
- **Zone** dans le champ `areasize (fr):` 
- **Portée** dans le champ `range (fr):` 
- **Cibles** dans le champ `target (fr):` 

## Afficher les champs sur la page de description de chaque sort
- **Nom** en majuscule 
- Sur la même ligne mais en float right le mot **SORT** qui se place à droite en majuscules suivi juste après du niveau qui résulte de la valeur du champ `level` sans changement

- En dessous, les traits du champ `traits:` qui peuvent être traduits à partir du fr.json à partir de la valeur du chmaptTraitNomdutraitenanglais. Ils sont placés en majuscules en blanc sur un fond coloré qui est fonction de la rareté du sort. On commence par y afficher le trait de rareté dans le champ `rarity` suivi des autres traits 

- Sur la ligne en dessous **Tradition(s)** suivi des valeurs du champ `traditions` qu'il faut traduire à l'aide du fr.json et des valeurs qui sont obtenues à partir de :
```
    "SpellTraditionArcane": "Arcanique",
    "SpellTraditionDivine": "Divine",
    "SpellTraditionOccult": "Occulte",
    "SpellTraditionPrimal": "Primordiale",
```
- Sur la ligne en dessous **Incantation** suivi des valeurs :
    - du champ `time` qui donne :
        - soit un nombre d'actions par exemple *3*, qu'il faut alors traduire en affichant l'image correspondant à 3actions.png dans le dossier images
        - soit une durée en minutes ou en heures qu'il faut afficher
    - les composantes de l'incantation dans le champ `components:` qui donne verbal, material, somatic qu'il faut traduire en allant les chercher dans le fr.json dans le champ `SpellComponent:` comme ci-dessous et mettre entre parenthèses ex : **(**verbal, matériel **)**
```
    "SpellComponentM": "Matériel",
    "SpellComponentS": "Somatique",
    "SpellComponentV": "Verbal",
```
- Sur la même ligne, précédé d'un *point virgule*, et si la valeur n'est pas nulle, **Condition** suivi de la valeur qu'il faudra aller chercher dans le champs `materials (fr):` dans le compendium français

- Sur la ligne suivante:
    - **si la valeur n'est pas nulle**, **Portée** suivi du contenu du champ `range (fr):` qu'il faudra aller chercher dans le compendium français
    - Sur la même ligne, **si la valeur n'est pas nulle** **Zone** suivi du contenu du champ `areasize (fr):` qu'il faudra aller chercher dans le compendium français 
    - Sur la même ligne si la valeur n'est pas nulle **Cibles** suivi du champ `target (fr):` qu'il faudra aller chercher dans le compendium français

- Sur la ligne suivante **Jet de sauvegarde** qu'il faut aller chercher dans le champ `save`
   - en commençant par la valeur du champ `basic` qui indique si le jet est basique ou non qu'il faudra traduire à partir du fr.json
   - puis le champ `value` qui indique Reflexe, Will, Fortitude qu'il faut traduire à partir du fr.json

- Sur la ligne suivante **Durée** qu'on récupère dans le champ `duration`

- On pose une ligne de séparation pour distinguer les statistiques de la description du sort

- On affiche le contenu du champ `description (fr):` des fichiers français qu'il faut *nettoyer* en conservant le gras et les sauts de ligne. Ils donnent le déclencheur, la description, les résultats des différentes marges de réussite et les effets de l'intensification si les données existent




## Liste des champs existant pour un sort (le sort Alarm)
```
{
    "_id": "4WAib3GichxLjp5p",
    "data": {"ability": {"value": ""},
        "area": {"areaType": "","value": null},
        "areasize": {"value": "20-foot burst"},
        "components": {"value": "material, somatic, verbal"},
        "cost": {"value": ""},
        "damage": {"applyMod": false,"value": ""},
        "damageType": {"value": ""},
        "description": {
            "chat": "",
            "unidentified": "",
            "value": "<p>You ward... </p>"},
        "duration": {"value": "8 hours"},
        "hasCounteractCheck": {"value": false},
        "level": {"value": 1},
        "location": {"value": 147},
        "materials": {"value": "3 gp silver bell focus"},
        "prepared": {"value": ""},
        "primarycheck": {"value": ""},
        "range": {"value": "touch"},
        "rules": [],
        "save": {
            "basic": "",
            "value": ""},
        "scaling": 
            {"formula": "",
            "mode": "none"},
        "school": {"value": "abjuration"},
        "secondarycasters": {"value": ""},
        "secondarycheck": {"value": ""},
        "source": {"value": ""},
        "spellCategorie": {"value": "spell"},
        "spellCategory": {"value": ""},
        "spellType": {"value": "utility"},
        "sustained": {"value": false},
        "target": {"value": ""},
        "time": {"value": "10 minutes"},
        "traditions": {"value": ["arcane","divine","occult","primal"]},
        "traits": {
            "custom": "",
            "rarity": {"value": "common"},
            "value": ["abjuration"]}},
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/spells/alarm.webp",
    "name": "Alarm",
    "permission": {"default": 0},
    "type": "spell"
}
```
