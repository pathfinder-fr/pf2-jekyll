---
Title: Template de génération des pages équipement
---
Le matériel contient deux pages qui peuvent être générées automatiquement

## Champs à récupérer
Les fichiers se trouvent dans equipment.db du projet anglais  
Dans le dépôt anglais, on récupère dans le champ `Type` les fichiers dont la valeur est equipment.  
On trie les fichiers dont le champ `level` est égal à 0 pour éviter de récupérer des objets magiques  

On va chercher les valeurs des champs suivants :  
- `Name` pour disposer du nom en anglais  
- `ID` pour disposer de l'ID unique du fichier  
- `rarity` pour faire le tri pour l'affichage  
- `price` qui va nous donner le prix mais avec les abbréviations anglaise gp pour po qu'il faudrait changer  
- `weight`qui va nous donner l'encombrement  

Dans le compendium Foundry, on va avoir besoin  
- du champ `Nom`
- du champ `Description Fr`

ATTENTION : Il n'y a nulle part le nombre de mains. Deux solutions, soit on peut les récupérer sur AON, soit il faudra générer dans le compendium Foundry en français un champ pour qu'on puisse remplir pour aller les récupérer

## Page des Tableaux des équipements
Il y a deux tableaux à générer en fonction de la rareté des éléments d'équipement sur la page matériel-tableaux.md  

### Affichage dans les tableaux

#### Format du Tableau du matériel courant
| **Objet courant** | Prix | Enc. | Mains |
|:------------------|:----:|:----:|:-----:|

#### remplir le tableau
On trie les fichiers équipment qui ont une valeur de `rarity` égale à **common** 
On place par **ordre alphabétique** les valeurs des champs suivants :  
| Nom | Price | weight | On place espace insécable pour le moment |

#### Format du Tableau du matériel peu courant
| **Objet peu courant** | Prix | Enc. | Mains |
|:----------------------|:----:|:----:|:-----:|

On trie les fichiers équipment qui ont une valeur de `rarity` égale à **uncommon** 
On place par **ordre alphabétique** les valeurs des champs suivants :  
| Nom | Price | weight | On place espace insécable pour le moment |

## Description des différents équipements d'aventuriers
On affiche les pièces d'équipement sur la page matériel les uns à la suite des autres, par ordre alphabétique de la valeur du champ `nom` avec  
- la valeur de **Nom** 
- suivi de la valeur de (Name) entre parenthèses
- suivi de la valeur de DescriptionFr

## Champs d'un fichier equipment
```
Exemple avec les outils d'artisan
{
    "_id": "y34yjumCFakrbtdw",
    "data": {
        "baseItem": null,
        "brokenThreshold": {
            "value": 0},
        "bulkCapacity": {"value": ""},
        "collapsed": {"value": false},
        "containerId": {"value": ""},
        "description": {
            "chat": "",
            "unidentified": "",
            "value": "<p>You need these tools to create items from raw materials with the Craft skill. Different sets are needed for different work, as determined by the GM; for example, blacksmith's tools differ from woodworker's tools.</p>"},
        "equipped": {"value": false},
        "equippedBulk": {"value": ""},
        "hardness": {"value": 0},
        "hp": {"value": 0},
        "identification": {"status": "identified"},
        "identified": {"value": true},
        "invested": {"value": false},
        "level": {"value": 0},
        "maxHp": {"value": 0},
        "negateBulk": {"value": "0"},
        "preciousMaterial": {"value": ""},
        "preciousMaterialGrade": {"value": ""},
        "price": {"value": "4 gp"},
        "quantity": {"value": 1},
        "rules": [],
        "size": {"value": "med"},
        "source": {"value": ""},
        "stackGroup": {"value": ""},
        "traits": {
            "custom": "",
            "rarity": {"value": "common"},
            "usage": {"value": "held-in-one-hand"},
            "value": []},
        "usage": {"value": "held-in-one-hand"},
        "weight": {"value": "2"}},
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/equipment/adventuring-gear/artisan-tools.webp",
    "name": "Artisan's Tools",
    "permission": {"default": 0},
    "type": "equipment"}
```