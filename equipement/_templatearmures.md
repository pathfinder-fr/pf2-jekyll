---
title: éléments pour le tableau des armures
---
| Armures | Type | Prix | Bonus à la CA | Dex max | Malus aux tests | Malus à la Vitesse | Force | Encombrement | Groupe | Traits d’armure |
|:--------|:----:|:----:|:-------------:|:-------:|:---------------:|:------------------:|:-----:|:-------------|:------:|:---------------:|


Pour faire le tri et trouver les armures : elles se trouvent dans equipment.db avec le préfixe armor.
Un moyen simple également de ne pas attraper les armures magiques consiste à viser le champ `level=0` 

À afficher dans le tableau en triant selon le ArmorType puis ordre alphabétique du nom

Pour remplir les champs, voici où les trouver dans l'ordre du tableau :
- Armure : champ `nom` en français
- Type : champ `"armorType":` donne unarmored, light, medium ou heavy à traduire (dans le fr.json la traduction est dans les champs "ArmorTypeUnarmored" ArmorTypeLight ArmorTypeMedium ArmorTypeHeavy)
- Prix : champ `"price": {"value":` donne le prix mais les pièces sont en anglais en dur 2 gp à convertir en 2 po, je ne sais pas gérer mais le fr.json comporte un champ 
```CurrencyAbbreviations": {
      "cp": "pc",
      "gp": "po",
      "pp": "pp",
      "sp": "pa"},
```
- Bonus à la CA : champ `"armor": {"value":` donne le chiffre du bonus. Il faut juste prévoir d'ajouter + devant la valeur à l'affichage qui n'est pas compris
- Dex Max : champ `"dex": {"value":` donne le bonus max de dex à la CA. Il faut prévoir d'ajouter + devant la valeur à l'affichage qui n'est pas compris
- Malus aux test : champ  `"check": {"value` donne le malus au test. Il est inutile de mettre le signe devant qui figure dans les valeurs
- Malus à la vitesse : champ `"speed": {"value": ""}`, donne la valeur en pieds à convertir en m en divisant par 5 
- Force : champ `"strength": {"value":` donne la valeur numérique. Aucun changement à faire
- Encombrement : champ `"equippedBulk": {"value":` ATTENTION ne surtout pas prendre le champ weight
- Groupe : champ `"group": {"value":` donnera le groupe en anglais à traduire (dans le fr.json la traduction est dans les champs "ArmorGroupChain" ArmorGroupComposite ArmorGroupLeather ArmorGroupPlate)
- Traits d'armure : champ `traits value` donnera de l'anglais à traduire (dans le fr.json, la traduction est dans le champ TraitNoisy, TraitFlexible, TraitComfort, TraitBulwark) 

Les autres traits qui seront utiles :
- le niveau de l'objet : champ `level`
- le trait de rareté : champ `traits rarity` (dans le fr.json, la traduction est dans le champ TraitCommon, TraitUncommon, TraitRare)

Voici les champs d'une armure dans les compendiums Foundry (ex Leather armor)
```
{
    "_id": "4tIVTg9wj56RrveA", 
    "data": {"armor": {"value": 1},
        "armorType": {"value": "light"},
        "baseItem": null,
        "brokenThreshold": {"value": null},
        "bulkCapacity": {"value": ""},
        "check": {"value": "-1"},
        "collapsed": {"value": false},
        "containerId": {"value": ""        },
        "description": {
            "chat": "",
            "unidentified": "",
            "value": "<p>A mix of flexible and molded boiled leather, a suit of this type of armor provides some protection with maximum flexibility.</p>"},
        "dex": {"value": "4"},
        "equipped": {"value": true},
        "equippedBulk": {"value": "1"},
        "group": {"value": "leather"},
        "hardness": {"value": null},
        "hp": {"value": null},
        "identification": {"status": "identified"},
        "identified": {"value": true},
        "invested": {"value": false},
        "level": {"value": 0},
        "maxHp": {"value": null},
        "negateBulk": {"value": "0"},
        "potency": {},
        "potencyRune": {"value": ""},
        "preciousMaterial": {"value": ""},
        "preciousMaterialGrade": {"value": ""},
        "price": {"value": "2 gp"},
        "propertyRune1": {"value": ""},
        "propertyRune2": {"value": ""},
        "propertyRune3": {"value": ""},
        "propertyRune4": {"value": ""},
        "quantity": {"value": 1},
        "resiliencyRune": {"value": ""},
        "resilient": {},
        "rules": [],
        "size": {"value": "med"},
        "source": {"value": ""},
        "speed": {"value": ""},
        "stackGroup": {"value": ""},
        "strength": {"value": "10"},
        "traits": {
            "custom": "",
            "rarity": {"value": "common"},
            "value": []},
        "usage": {"value": "wornarmor"},
        "weight": {"value": "2"}},
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/equipment/armor/leather-armor.webp",
    "name": "Leather Armor",
    "permission": {"default": 0},
    "type": "armor"
}
```
