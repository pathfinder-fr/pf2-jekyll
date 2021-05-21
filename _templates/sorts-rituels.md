---
Title: Template de description des rituels
---
Template à faire après avoir créé le template de description des sorts pour avoir tous les champs nécessaires extraits

## Format des rituels
Les sorts suivent le format suivant :  
NOM &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RITUEL 1  
TRAITS  
**Incantation** 10 min (matériel, somatique, verbal) ; **Coût** ingrédients pour créer un diagramme de protection **Incantateurs secondaires**"** 4, ils doivent être des fidèles de votre religion
**Test Principal** Religion (expert)  **Test secondaire** Artisanat, Représentation, Connaissance agricole
**Portée** contact ; **Zone** explosion 6 m  **Cibles** une créature volontaire
**Jet de sauvegarde** Réflexes basique **Durée** 8 h

---

Description  
**Succès critique** texte  
**Succès** texte
**Échec** texte
**Éche critique** texte 

---

**Intensifié** texte

### Les champs à traduire en vf au préalable
Pour pouvoir afficher chaque sort, il faudra avoir extrait d'autres champs pour les exporter dans les compendiums pour foundry à l'effet de les traduire

- **Incantateurs secondaires** se trouve dans la valeur du champ `secondarycasters` 
- **Test Principal** qui nécessite la valeur du champ `primarycheck`   
- **Test secondaire** qui nécessite la valeur du champ `secondarycheck` 
- **Coût** qui nécessite le champ `costs`
- **Zone** dans le champ `areasize` 
- **Cibles** dans le champ `target` 
- **Durée** dans le champ `duration`

### Dans le pack anglophone
- l'**ID** pour obtenir chaque sort et s'assurer la concordance
- le **type** de sort que l'on trouve dans le champ `spellcategorie` avec categor**ie** à la fin du compendium anglophone qui donne *spell, ritual, focus* ATTENTION il y a un autre champ spellcategor**y** avec un y qui décrit autre chose et qu'il ne faut pas utiliser
- le **Niveau** dans le champ `Level` du compendium anglophone sans changement
- les **traits** qui sont dans le champ `traits` à traduire à partir du fr.json
- le champ **Incantation** qui sont dans les champs :
    -`time` à traduire à partir du fr.json
    - et `components` à traduire à partir du fr.json
- **Portée**qui est dans le champ `range` qui donne touch qui devra être traduit en contact ou bien des distances en pieds qu'il faudra convertir en divisant par 5 et en multipliant la valeur par 1,5

### Dans les compendiums français
- **Nom du sort** dans le champ `Nom` du compendium Foundry qui se place à gauche en majuscules
- **Description** comprenant les **Prérequis** et l'**Intensification**
- **Déclencheur** Les déclencheurs sont actuellement dans le champ description et je propose qu'on les y laisse.
- **Succès** Idem
- **Intensification** Idem

Les champs qui auront été traduits
- **Incantateurs secondaires** se trouve dans la valeur du champ `secondarycasters (fr):` 
- **Test Principal** qui nécessite la valeur du champ `primarycheck (fr):`   
- **Test secondaire** qui nécessite la valeur du champ `secondarycheck (fr):` 
- **Coût** qui nécessite le champ `costs (fr):`
- **Zone** dans le champ `areasize (fr):` 
- **Cibles** dans le champ `target (fr):` 
- **Durée** dans le champ `duration (fr):`

## Afficher les champs des rituels
Pour les trier, il faut prendre les sorts issus de la spells.db dont la valeur du champ `spellcategorie:` est égale à ritual  
Puis :

- **Nom** en majuscule 
- Sur la même ligne le mot **RITUEL** qui se place à droite en majuscules
* suivi juste après du niveau qui résulte de la valeur du champ `level:` sans changement
- En dessous, en blanc et en majuscules sur un fond coloré en fonction de la rareté du sort, les traits qui auront été  traduits à partir du fr.json avec dans l'ordre le trait de rareté suivi des autres traits  
- En dessous **Incantation** suivi des valeurs :
    - du champ `time` qui donne 
        - soit le nombre d'actions par exemple *3*, qu'il faudrait alors traduire en affichant l'image correspondant  
        - soit une durée en minutes ou en heures 
    - des composantes de l'incantation dans le champ `components` qui donne verbal, material, somatic qu'il faut traduire en allant les chercher dans le fr.json dans le champ SpellComponent comme ci dessous et mettre entre parenthèses ex : **(**verbal, matériel **)**
```
    "SpellComponentM": "Matériel",
    "SpellComponentS": "Somatique",
    "SpellComponentV": "Verbal",
```

Sur la même ligne, après un point virgule *;* si la valeur n'est pas nulle **Coût** puis la valeur du champ `costs (fr):`  
Sur la même ligne, après un point virgule *;* si la valeur n'est pas nulle **Incantateurs secondaires** puis la valeur du champ `secondarycasters: (fr)` 

Sur la ligne suivante, si la valeur des champs suivants n'est pas nulle :
- **Test Principal** puis la valeur du champ `primarycheck (fr):` 
- **Test secondaire** puis la valeur du champ `secondarycheck (fr):` 

- Sur la ligne suivante, après un point virgule ; si la valeur des champs n'est pas nulle, **Portée** suivi du contenu du champ `range (fr):`
- Sur la même ligne, après un point virgule ; si la valeur n'est pas nulle **Zone** suivi du contenu du champ `areasize (fr):`
- Sur la même ligne, après un point virgule ; si la valeur n'est pas nulle **Cibles** suivi du champ `target (fr):`

- Sur la ligne suivante **Durée** qu'on récupère dans le champ `duration (fr)`

- On pose une ligne de séparation pour distinguer les statistiques du contenu

- On affiche le contenu du champ `description (fr)` des fichiers français qu'il faut nettoyer en conservant le gras et les sauts de ligne. La description donne le déclencheur, la description, les résultats des différentes marges de réussite et les effets de l'intensification si les données existent

### Exemple de champs d'un rituel (ex : le sort commune) 
```
{
    "_id": "7DN13ILADW2N9Z1t",
    "data": {
        "ability": {"value": ""},
        "area": {"value": ""},
        "areasize": {"value": ""},
        "components": {"value": ""},
        "cost": {"value": "rare incense worth a total value of 150 gp"},
        "damage": {
            "applyMod": false,
            "value": ""},
        "damageType": {"value": ""},
        "description": {
            "chat": "",
            "unidentified": "",
            "value": "<p>You call upon an unknown planar entity </p>"},
        "duration": {"value": "up to 10 minutes"},
        "hasCounteractCheck": {"value": false},
        "level": {"value": 6},
        "location": {"value": ""},
        "materials": {"value": ""},
        "prepared": {"value": ""},
        "primarycheck": {"value": "Occultism (master) or Religion (master)"},
        "range": {"value": ""},
        "rules": [],
        "save": {
            "basic": "",
            "value": ""},
        "scaling": {
            "formula": "",
            "mode": "none"},
        "school": {"value": "divination"},
        "secondarycasters": {"value": "1"},
        "secondarycheck": {"value": "Occultism or Religion (whichever is used for the primary check)" },
        "source": {"value": ""},
        "spellCategorie": {"value": "ritual"},
        "spellCategory": {"value": ""},
        "spellType": {"value": "utility"},
        "sustained": {"value": false},
        "target": {"value": ""},
        "time": {"value": "1 day"},
        "traditions": {"value": []},
        "traits": {
            "custom": "",
            "rarity": {"value": "uncommon"},
            "value": ["divination"]}},
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/spells/commune.webp",
    "name": "Commune",
    "permission": {"default": 0},
    "type": "spell"}
