---
title: template pour le tableau des armes au corps à corps
---
Pour faire le tri et trouver les armes : elles se trouvent dans equipment.db avec le préfixe weapon.

## Tableau des attaques à mains nues
| Attaque à mains nues | Prix | Dégâts | Encombrement | Mains | Catégorie | Traits d’arme |
|:---------------------|:----:|:------:|:------------:|:-----:|:---------:|:--------------|

Dans equipment.db
On trie les fichiers en recherchant dans le champ `"weaponType":` la valeur : "unarmed" 
On ne prend que les armes dont le champ `level` = 0

Il faut extraire alors pour chaque fichier
- la valeur `rarity`
- la valeur `Name`
- l`ID`
- la valeur du `price` qui devrait nous donner les prix en vo avec les gp au lieu des po, les sp au lieu des pa...
- dans le champ `damage`
    - la valeur du champ `dice` qui donne le nombre de dé(s) de dégâts réutilisable comme telle 
    - la valeur du champ `die` qui donne le(s) dé(s) de dégâts réutilisable comme telle 
    - la valeur du champ `damageType` qui donnera Bludgeoning,piercing,slashing qu'il faudra traduire à partir du fr.json
- la valeur du champ `weight` réutilisable comme telle
- on a un problème avec la colonne mains puisque s'il existe bien un champ `hands`, il n'est jamais rempli dans le fichier anglophone selon mon sondage rapide (testé sur greatsword, club et falchion). Il faudra donc aller chercher l'information ailleurs et au pire, la rajouter dans le compendium Foundry pour que je puisse le rajouter  
- la valeur du champ `group` qu'il faudra traduire en allant chercher dans le fr.json dans la valeur du champ `WeaponGroupXxxx` où Xxx est le nom anglais du group pour récupérer la valeur d'affichage en français
- toutes les valeurs du champ `traits` qu'il faut traduire à partir du fr.json dans le champ traitXXX où Xxx est le nom du trait en supprimant les tirets ex TraitVersatileB

On récupère dans la traduction française des compendiums Foundry simplement la valeur du champ `nom` correspondant au `name` et à `l'ID`

Puis on complète le tableau avec dans l'**ordre alphabétique** de la valeur du `nom`  
| nom | price | dice+die (damageType traduit) | weight | mains | valeur en français de group | traits d'armes en vf

## Tableau des attaques au corps à corps

### Armes simples

| Armes simples | Prix | Dégâts | Encombrement | Mains | Catégorie | Traits d’arme |
|:--------------|:----:|:------:|:------------:|:-----:|:---------:|:--------------|

Dans equipment.db
On trie les fichiers en recherchant cette fois dans le champ `"weaponType":` la valeur : **"simple"** 
On ne prend que les armes dont la valeur du champ `level` est **0** et dont la valeur du champ `"range"` est **melee**,

Il faut extraire alors pour chaque fichier  
- la valeur `rarity`
- la valeur `Name`
- l`ID`
- la valeur du `price` qui devrait nous donner les prix en vo avec les gp au lieu des po, les sp au lieu des pa...
- dans le champ `damage`
    - la valeur du champ `dice` qui donne le nombre de dé(s) de dégâts réutilisable comme telle 
    - la valeur du champ `die` qui donne le(s) dé(s) de dégâts réutilisable comme telle 
    - la valeur du champ `damageType` qui donnera Bludgeoning,piercing,slashing qu'il faudra traduire à partir du fr.json
- la valeur du champ `weight` réutilisable comme telle
- on a un problème avec la colonne mains puisque s'il existe bien un champ `hands`, il n'est jamais rempli dans le fichier anglophone selon mon sondage rapide (testé sur greatsword, club et falchion). Il faudra donc aller chercher l'information ailleurs et au pire, la rajouter dans le compendium Foundry pour que je puisse le rajouter  
- la valeur du champ `group` qu'il faudra traduire en allant chercher dans le fr.json dans la valeur du champ `WeaponGroupXxxx` où Xxx est le nom anglais du group pour récupérer la valeur d'affichage en français
- toutes les valeurs du champ `traits` qu'il faut traduire à partir du fr.json dans le champ traitXXX où Xxx est le nom du trait en supprimant les tirets ex TraitVersatileB

On récupère dans la traduction française des compendiums Foundry simplement la valeur du champ `nom` correspondant au `name` et à `l'ID`

Puis on complète le tableau avec dans l'ordre ceux dont la valeur du champ `rarity` est **common** puis dans l'**ordre alphabétique** de la valeur du champ `Nom` et on complète  
| nom | price | dice+die (damageType traduit) | weight | mains | valeur en français de group | traits d'armes en vf

On répète l'opération avec dans l'ordre ceux dont la valeur du champ `rarity` est **uncommon** puis dans l'**ordre alphabétique** de la valeur du champ `Nom` et on complète  
| nom | price | dice+die (damageType traduit) | weight | mains | valeur en français de group | traits d'armes en vf

### Armes de guerre
| Armes de guerre | Prix | Dégâts | Encombrement | Mains | Catégorie | Traits d’arme |
|:----------------|:----:|:------:|:------------:|:-----:|:---------:|:--------------|

On répète l'opération avec la valeur du champ `WeaponType` qui est égale à **martial** puis le même processus 

### Armes avancées
On répète l'opération avec la valeur du champ `WeaponType` qui est égale à **advanced** mais comme il n'y a que des armes avancées peu courantes, on les affiche dans l'ordre alphabétique


Voici les champs d'une arme dans les compendiums Foundry anglophones (ex falchion)
```
{
    "_id": "XGtIUZ4ZNKuFx1uL",
    "data": {
        "MAP": {"value": ""},
        "ability": {"value": "str"},
        "baseItem": null,
        "bonus": {"value": 0},
        "bonusDamage": {"value": 0},
        "brokenThreshold": {"value": 0},
        "bulkCapacity": {"value": ""},
        "collapsed": {"value": false},
        "containerId": {
            "value": ""},
        "damage": {
            "damageType": "slashing",
            "dice": 1,
            "die": "d10",
            "value": ""},
        "description": {
            "chat": "",
            "unidentified": "",
            "value": "<p>This weapon is a heavier, two‑handed version of the curved‑bladed scimitar. It is weighted toward the blade's end, making it a powerful slashing weapon.</p>"},
        "equipped": {"value": false},
        "equippedBulk": {"value": ""},
        "group": {"value": ""},
        "hands": {"value": ""},
        "hardness": {"value": 0},
        "hp": {"value": 0},
        "identification": {"status": "identified"},
        "identified": {"value": true},
        "invested": {"value": false},
        "level": {"value": 0},
        "maxHp": {"value": 0},
        "negateBulk": {"value": "0"},
        "potencyRune": {"value": ""},
        "preciousMaterial": {"value": ""},
        "preciousMaterialGrade": {"value": ""},
        "price": {"value": "3 gp"},
        "property1": {
            "critDamage": "",
            "critDamageType": "",
            "critDice": null,
            "critDie": "",
            "criticalConditionType": "",
            "criticalConditionValue": null,
            "damageType": "",
            "dice": null,
            "die": "",
            "strikeConditionType": "",
            "strikeConditionValue": null,
            "value": ""},
        "propertyRune1": {"value": ""},
        "propertyRune2": {"value": ""},
        "propertyRune3": {"value": ""},
        "propertyRune4": {"value": ""},
        "quantity": {"value": 1},
        "range": {"value": "melee"},
        "reload": {"value": ""},
        "rules": [],
        "size": {"value": "med"},
        "source": {"value": ""},
        "splashDamage": {"value": 0},
        "stackGroup": {"value": ""},
        "strikingRune": {"value": ""},
        "traits": {
            "custom": "",
            "rarity": {
                "value": "common"},
            "value": [
                "forceful",
                "sweep"]},
        "usage": {"value": "held-in-one-hand"},
        "weaponType": {
            "value": "martial"},
        "weight": {"value": "2"}},
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/equipment/weapons/falchion.webp",
    "name": "Falchion",
    "permission": {"default": 0},
    "type": "weapon"
}


```
