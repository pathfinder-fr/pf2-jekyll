---
title: template pour les tableaux des armes au corps à corps et à distance
---
Pour faire le tri et trouver les armes : elles se trouvent dans equipment.db avec la valeur du champ `type` égale à weapon.

## Tableau des attaques à mains nues
### Format du tableau
| Attaque à mains nues | Prix | Dégâts | Encombrement | Mains | Catégorie | Traits d’arme |
|:---------------------|:----:|:------:|:------------:|:-----:|:---------:|:--------------|

### Tri et extraction des données
Dans equipment.db du projet anglais
- on trie les fichiers dont la valeur du champ `type` est **weapon**
- en triant les fichiers dont la valeur du champ `"weaponType":` est **unarmed** pour éviter les armes manufacturées
- en triant les armes dont la valeur du champ `level` est **0** pour éviter de récupérer les armes magiques

Il faut extraire alors pour chaque fichier
- la valeur `rarity`
- la valeur `Name`
- l`ID`
- la valeur du `price` qui devrait nous donner les prix en vo avec les gp au lieu des po, les sp au lieu des pa...
- dans le champ `damage`
    - la valeur du champ `dice` qui donne le nombre de dé(s) de dégâts réutilisable comme telle 
    - la valeur du champ `die` qui donne le(s) dé(s) de dégâts réutilisable comme telle 
    - la valeur du champ `damageType` qui donnera Bludgeoning,Piercing,Slashing qu'il faudra traduire à partir du fr.json qui contient le champ `DamageTypeBludgeoning` avec une valeur dont **il ne faudra reprendre que la première lettre**
- la valeur du champ `weight` réutilisable comme telle
- on a un problème avec la colonne du tableau mains puisque s'il existe bien un champ `hands`, il n'est apparemment jamais rempli dans le fichier anglophone selon mon sondage rapide (testé sur greatsword, club et falchion). On gardera le champ vierge mais il faudra aller chercher l'information ailleurs. Au pire, il faudrait rajouter un champ mains dans les données du compendium Foundry pour que je puisse le rajouter à la main  
- la valeur du champ `group` qu'il faudra traduire en allant chercher dans le fr.json dans la valeur du champ `WeaponGroupXxxx` où Xxx est le nom anglais du group pour récupérer la valeur d'affichage en français
- toutes les valeurs du champ `traits` qu'il faut traduire à partir du fr.json dans le champ traitXXX où Xxx est le nom du trait **en supprimant les tirets** ex TraitVersatileB

Dans la traduction française des compendiums Foundry, on ira ensuite récupérer simplement la valeur du champ `nom` correspondant au `name` et à `l'ID` du fichier

### Affichage
On complète le tableau avec dans l'**ordre alphabétique** de la valeur de `nom` avec les valeurs des champs suivants : 
| nom | price | dice+die (damageType traduit) | weight | mains | valeur en français de group | traits d'armes en vf

---

## Tableaux des attaques au corps à corps
### Format du tableau des armes simples courantes
| **Armes simples courantes** | Prix | Dégâts | Encombrement | Mains | Catégorie | Traits d’arme |
|:----------------------------|:----:|:------:|:------------:|:-----:|:---------:|:--------------|

### Tri et extraction des données
Dans equipment.db du projet anglais
on trie les fichiers dont la valeur du champ `type` est **weapon**
On trie les fichiers dont la valeur du champ `"weaponType":` est **"simple"** pour exclure les attaques à mains nues et les armes de guerre ou avancées
On trie les fichiers dont la valeur du champ `rarity` est **common** pour éviter les armes peu courantes
On ne prend que les armes dont la valeur du champ `level` est **0** pour éviter les armes magiques
On prend les armes dont la valeur du champ `"range"` est **melee** et celle qui ont une valeur dans `traits` égale à **thrown** 

ATTENTION : Cette dernière règle de double tri melee **et** thrown est impérative pour que les armes de jet soient portées avec les armes au corps à corps

Il faut extraire pour chaque fichier
- la valeur `rarity`
- la valeur `Name`
- l`ID`
- la valeur du `price` qui devrait nous donner les prix en vo avec les gp au lieu des po, les sp au lieu des pa...
- dans le champ `damage`
    - la valeur du champ `dice` qui donne le nombre de dé(s) de dégâts réutilisable comme telle 
    - la valeur du champ `die` qui donne le(s) dé(s) de dégâts réutilisable comme telle 
    - la valeur du champ `damageType` qui donnera Bludgeoning,Piercing,Slashing qu'il faudra traduire à partir du fr.json qui contient le champ `DamageTypeBludgeoning` avec une valeur dont **il ne faudra reprendre que la première lettre**
- la valeur du champ `weight` réutilisable comme telle
- on a un problème avec la colonne du tableau mains puisque s'il existe bien un champ `hands`, il n'est apparemment jamais rempli dans le fichier anglophone selon mon sondage rapide (testé sur greatsword, club et falchion). On gardera le champ vierge mais il faudra aller chercher l'information ailleurs. Au pire, il faudrait rajouter un champ mains dans les données du compendium Foundry pour que je puisse le rajouter à la main  
- la valeur du champ `group` qu'il faudra traduire en allant chercher dans le fr.json dans la valeur du champ `WeaponGroupXxxx` où Xxx est le nom anglais du group pour récupérer la valeur d'affichage en français
- toutes les valeurs du champ `traits` qu'il faut traduire à partir du fr.json dans le champ traitXXX où Xxx est le nom du trait **en supprimant les tirets** ex TraitVersatileB

Dans la traduction française des compendiums Foundry, on ira ensuite récupérer simplement la valeur du champ `nom` correspondant au `name` et à `l'ID` du fichier

Puis on complète le tableau avec dans l'**ordre alphabétique** de la valeur du champ `Nom` et on complète  
| nom | price | dice+die (damageType traduit) | weight | mains | valeur en français de group | traits d'armes en vf

### Format du tableau des armes simples peu courantes
| **Armes simples peu courantes** | Prix | Dégâts | Encombrement | Mains | Catégorie | Traits d’arme |
|:--------------------------------|:----:|:------:|:------------:|:-----:|:---------:|:--------------|

On répète le même script que pour les armes simples **courantes** sauf qu'il faut trier les fichiers en retenant la valeur du champ `rarity` **uncommon**

### Format du tableau des Armes de guerre courantes
| **Armes de guerre courantes** | Prix | Dégâts | Encombrement | Mains | Catégorie | Traits d’arme |
|:------------------------------|:----:|:------:|:------------:|:-----:|:---------:|:--------------|

On répète les scripts du tableaux des armes **courantes** avec comme seul changement la valeur du champ `WeaponType` qui est égale à **martial**

### Format du tableau des Armes de guerre peu courantes
| **Armes de guerre courantes** | Prix | Dégâts | Encombrement | Mains | Catégorie | Traits d’arme |
|:------------------------------|:----:|:------:|:------------:|:-----:|:---------:|:--------------|

On répète les scripts des tableaux des armes **martiales** courantes sauf qu'il faut trier les fichiers en retenant la valeur du champ `rarity` **uncommon**

### Format du tableau des armes avancées
| **Armes avancées** | Prix | Dégâts | Encombrement | Mains | Catégorie | Traits d’arme |
|:-------------------|:----:|:------:|:------------:|:-----:|:---------:|:--------------|

On répète les scripts des tableaux l'opération avec la valeur du champ `WeaponType` qui est égale à **advanced** mais comme il n'y a que des armes avancées peu courantes, on les affiche dans un seul et même tableau l'ordre alphabétique

---

## Tableau des attaques à distance
### Armes à distance simples
| **Armes à distance simples** | Prix | Dégâts | Portée | Recharge | Encombrement | Mains | Catégorie | Traits d’arme |
|:-----------------------------|:----:|:------:|:------:|:--------:|:------------:|:-----:|:---------:|:--------------|

Dans equipment.db du projet anglais
- on trie les fichiers dont la valeur du champ `type` est **weapon**
- en triant les fichiers dont la valeur du champ `"weaponType":` est **simple** pour éviter les armes manufacturées
- en traint les armes dont la valeur du champ `level` est **0** pour éviter de récupérer les armes magiques
- on exclut les fichiers dont la valeur du champ `"range"` est **melee** ou ceux qui comportent une valeur dans le champ `traits` égale à thrown

Il faut extraire alors pour chaque fichier  
- la valeur `rarity`
- la valeur `Name`
- l`ID`
- la valeur du `price` qui devrait nous donner les prix en vo avec les gp au lieu des po, les sp au lieu des pa...
- dans le champ `damage`
    - la valeur du champ `dice` qui donne le nombre de dé(s) de dégâts réutilisable comme telle 
    - la valeur du champ `die` qui donne le(s) dé(s) de dégâts réutilisable comme telle 
    - la valeur du champ `damageType` qui donnera Bludgeoning,piercing,slashing qu'il faudra traduire à partir du fr.json
- la valeur du champ `range` qui est en pieds qu'il faudra diviser par 5 et multiplier par 1,5 pour l'afficher en m
- la valeur du champ `weight` réutilisable comme telle
- on a un problème avec la colonne mains puisque s'il existe bien un champ `hands` avec les mêmes problématiques que pour les autres armes  
- la valeur du champ `group` qu'il faudra traduire en allant chercher dans le fr.json dans la valeur du champ `WeaponGroupXxxx` où Xxx est le nom anglais du group pour récupérer la valeur d'affichage en français
- toutes les valeurs du champ `traits` qu'il faut traduire à partir du fr.json dans le champ traitXXX où Xxx est le nom du trait en supprimant les tirets ex TraitVersatileB

On récupère dans la traduction française des compendiums Foundry simplement la valeur du champ `nom` correspondant au `name` et à `l'ID`

Puis on complète le tableau avec dans l'**ordre alphabétique** de la valeur du champ `Nom` et on complète  
| nom | price | dice+die (damageType en vf) | rangeconverti+m | Reload | weight | mains | valeur en français de group | traits en vf

### Format du tableau des Armes à distance de guerre
| **Armes à distance de guerre** | Prix | Dégâts | Portée | Recharge | Encombrement | Mains | Catégorie | Traits d’arme |
|:-------------------------------|:----:|:------:|:------:|:--------:|:------------:|:-----:|:---------:|:--------------|

Mêmes opérations que les armes simples sauf qu'on trie les fichiers en recherchant cette fois dans le champ `"weaponType":` la valeur : **"martial"** 

On affiche dans le tableau dans l'**ordre alphabétique** de la valeur du champ `Nom` et on complète  
| nom | price | dice+die (damageType traduit) | rangeconverti+m | Reload | weight | mains | valeur en français de group | traits en vf

Format du tableau des Armes évoluées de guerre
| **Armes à distance évoluées** | Prix | Dégâts | Portée | Recharge | Encombrement | Mains | Catégorie | Traits d’arme |
|:------------------------------|:----:|:------:|:------:|:--------:|:------------:|:-----:|:---------:|:--------------|

On affiche dans le tableau dans l'**ordre alphabétique** de la valeur du champ `Nom` et on complète  
| nom | price | dice+die (damageType traduit) | rangeconverti+m | Reload | weight | mains | valeur en français de group | traits en vf

## Champs disponibles d'un fichier arme
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
