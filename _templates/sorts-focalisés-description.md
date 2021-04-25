---
Title: Description des sorts focalisés
---
Ce template est à réaliser après avoir réalisé le template de la decription des sorts

## Tri des données
La fonction de tri dans la spells.db peut se faire avec la valeur focus dans le champ `spellcategorie`  
Elle peut aussi se faire avec la valeur focus dans le champ tradition

Pour récupérer les sorts focalisés de chaque classe, il n'existe pas de champ dédié.  

Le seul moyen est de vérifier si dans le champ `traits`, il existe une valeur qui corresponde au nom anglais d'une classe.
(par exemple si dans le champ `trait`il y a **bard** qui figure parmi les classes de la class.db, alors il s'agit d'un sort focaisé de cette classe.

Cela se corse ensuite avec d'autres sorts focalisés qui sont propres à certains archétypes.

## Champs utiles

### Format des sorts
Les sorts suivent le format suivant :  
NOM &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; FOCALISÉ 1  
TRAIT RARETE TRAITS  
**Incantation** 10 min (matériel, somatique, verbal) ; **Conditions** une cloche en argent de 3 po comme focaliseur  **Déclencheur** Une créature accomplit un acte
**Portée** contact ; **Zone** explosion 6 m  **Cibles** une créature volontaire
**Jet de sauvegarde** Réflexes basique **Durée** 8 h

---

Description  
**Succès critique** texte  
**Succès** texte 

---

**Intensifié** texte

## Pages à créer
Il y a deux solutions. La seconde a ma préférence pour permettre de faire plus facilement des recherches.

### sur une page unique intitulée liste-focalises.md ?
Le Ldb propose une page unique qui affiche les sorts focalisés pour chaque classe qui en dispose en plaçant les sorts les uns à la suite des autres par ordre alphabétique pour chaque classe (barde, champion, druide, ensorceleur, magicien, moine, prêtre). On peut reprendre cette première option mais la page risque de devenir très longue.

### sur des pages distinctes sorts-focalises-nomdelaclasse.md ?
Ces sorts sont en réalité des capacités propres à chaque classe. (des litanies, des sorts de domaine, des sorts de lignage, des sorts d'école.)

Il me semble préférable de générer une page pour chaque classe qui possède des sorts de focalisation. Pour cela, il faut être capable de trouver tous les sorts focalisés de la classe dans le champ `traits`

La règle à créer serait alors la suivante :  
- vérifier si une valeur du champs `traits` est égale à une valeur du champ `name` d'un fichier de la class.db
- Si c'est le cas, alors, il faut créer une page sorts-focalises-nomdelaclasse.md
- Y afficher seulement les description des sorts focalisés ayant le trait classés par niveau puis par ordre alphabétique.

## Afficher les sorts sur la page
Même si les sorts focalisés ont tous les paramètres des sorts, il y en a souvent beaucoup moins à afficher que les sorts classiques.

- **Nom** en majuscule 
- Sur la même ligne le mot **FOCALISÉ** qui se place à droite en majuscules
* suivi juste après du Niveau qui résulte de la valeur du champ `level` sans changement
- En dessous, en blanc et en majuscules sur un fond coloré en fonction de la rareté, les traits qui peuvent être traduits à partir du fr.json 

- En dessous **Incantation** suivi des valeurs :
    - du champ `time` qui donne 
        - soit le nombre d'actions par exemple *3*, qu'il faudrait alors traduire en affichant l'image correspondant  
        - soit une durée en minutes ou en heures qu'il faut afficher  
    - des composantes de l'incantation dans le champ `components` qui donne verbal, material, somatic qu'il faut traduire en allant les chercher dans le fr.json dans le champ SpellComponent comme ci dessous et mettre entre parenthèses ex : **(**verbal, matériel **)**
```
    "SpellComponentM": "Matériel",
    "SpellComponentS": "Somatique",
    "SpellComponentV": "Verbal",
```
- Sur la même ligne, **si la valeur n'est pas nulle**, **Condition** suivi de la valeur qu'il faudra aller chercher dans le champs `matérials (fr)` dans le compendium français

- Sur la ligne suivante, **si la valeur n'est pas nulle**, **Portée** suivi du contenu du champ `range (fr)` qu'il faudra aller chercher dans le compendium français
- Sur la même ligne, **si la valeur n'est pas nulle** **Zone** suivi du contenu du champ `areasize (fr)` qu'il faudra aller chercher dans le compendium français 
- Sur la même ligne **si la valeur n'est pas nulle** **Cibles** suivi du champ `target (fr)` qu'il faudra aller chercher dans le compendium français
- Sur la ligne suivante **si la valeur n'est pas nulle** **Jet de sauvegarde** 
    - **Jet de sauvegarde** dans le champ `save`
   - la valeur du champ `basic` qui indique si le jet est basique ou non qu'il faudra traduire
   - un champ `value` qui indique Reflexe, Will, Fortitude qu'il faut traduire à partir du fr.json
- Sur la ligne suivante **Durée** qu'on récupère dans le champ `duration`

- On pose une ligne de séparation pour distinguer les statistiques du contenu

- On affiche le contenu du champ `description (fr)` des fichiers français qu'il faut nettoyer en conservant le gras et les sauts de ligne. Ils donneront le déclencheur, la description, les résultats des différentes marges de réussite et les effets de l'intensification si les données existent.


## Exemple de fichier d'un sort focalisé : Allegro.json

```
{
    "_id": "IQchIYUwbsVTa9Mc",
    "data": {"ability": {"value": ""},
        "area": {
            "areaType": "",
            "value": null},
        "areasize": {"value": ""},
        "components": {"value": "verbal"},
        "cost": {"value": ""},
        "damage": {
            "applyMod": false,
            "value": ""},
        "damageType": {"value": ""},
        "description": {
            "chat": "",
            "unidentified": "",
            "value": "<p>You perform rapidly, speeding up your ally. The ally becomes @Compendium[pf2e.conditionitems.Quickened]{Quickened} and can use the additional action to Strike, Stride, or Step.</p>"},
        "duration": {"value": "1 round"},
        "hasCounteractCheck": {
            "value": false},
        "level": {"value": 0},
        "location": {"value": ""},
        "materials": {"value": ""},
        "prepared": {"value": ""},
        "primarycheck": {"value": ""},
        "range": {"value": "30 feet"},
        "rules": [],
        "save": {
            "basic": "",
            "value": ""},
        "scaling": {
            "formula": "",
            "mode": "none"},
        "school": {"value": "enchantment"},
        "secondarycasters": {"value": ""},
        "secondarycheck": {"value": ""},
        "source": {"value": ""},
        "spellCategorie": {"value": "focus"},
        "spellCategory": {"value": ""},
        "spellType": {"value": "utility"},
        "sustained": {"value": false},
        "target": {"value": "1 ally"},
        "time": {"value": "1"},
        "traditions": {
            "custom": "",
            "value": ["focus"]},
        "traits": {
            "custom": "",
            "rarity": {"value": "uncommon"},
            "value": [
                "bard",
                "cantrip",
                "composition",
                "emotion",
                "enchantment",
                "mental"
            ]
        }
    },
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/spells/allegro.webp",
    "name": "Allegro",
    "permission": {
        "default": 0
    },
    "type": "spell"
}

```