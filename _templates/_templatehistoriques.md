---
Title: template pour les historiques
---
## Champs utiles
Dans Foundry, nous avons les champs
- `ÌD` donne l'identifiant unique de chaque donnée
- `Nom` donne le nom
- `Name`donne le nom en vo
- `DescriptionEn` contient les prérequis et la description en vo
- `DescriptionFr` contient les prérequis et la description en vf 
- `État` indique l'origine de la traduction officielle, libre, aucune, auto trad ou que le texte vo est changé 
- `État d'origine` indique l'état antérieur de la traduction en cas de changement

Autres champs utiles à extraire de la background.db
- `"boosts": {"0": {"value": [` ATTENTION uniquement le boost 0 (pas le 1). Cela donne les primes de caractéristique dans lesquelles il faut choisir une prime quand on prend l'historique. Il faudra ensuite les traduire à partir du fr.json et les afficher en les séparant par 'ou'
- `"items": "id":` donne l'ID du don de compétence obtenu à afficher idéalement avec un lien cliquable pour aller sur le don
- `Source` donne la source en vo
- `"trainedLore":,` donne la compétence de Connaissance. Par contre, c'est en anglais et il n'y a pas nécessairement de correspondance en vf. La Connaissance est donnée dans le corps de la description
- `"trainedSkills": {"value": ["nat"`donne les trois premières lettres de la compétence dans laquelle l'historique donne le rang qualifié à aller chercher (ex : nat pour Nature)
- `"traits": "rarity": "value":` pour obtenir la rareté à aller chercher dans le fr.json avec traitXxx où Xxx est la rareté pour récupérer la valeur en vf

## Tableau des historiques
Générer une page qui contient la liste des historiques dans un tableau

Le **tableau** sur 5 colonnes affiche les champs
| Nom | Primes de carac | qualifié en | Connaissance | Don de compétence | Source |
|:----|:---------------:|:-----------:|:-------------|:------------------|:-------| 
| `Nom` | `boosts` | `"trainedSkills"`| `"trainedLore":,` | `items`ID | `Source`

## Une page de regroupement
Générer automatiquement une page regroupant tous les historiques en affichant par ordre alpha tous les ID de la background.db en affichant
    - **`Nom`**contenant le nom en vf, suivie du `trait rarity`
    - En dessous le `Name` en vo dans une police plus petite
    - suivi de `Descriptionfr` 

### Liste des champs des historiques
```
{
    "_id": "eYY3bX7xSH7aicqT",
    "data": {
        "boosts": {
            "0": {
                "value": [
                    "dex",
                    "str"]},
            "1": {"value": ["cha","con","dex","int","str","wis"]}},
        "description": {
            "chat": "",
            "unidentified": "",
            "value": "<p><strong>Prerequisite</strong>: Region - Old Cheliax</p>\n<hr />\n<p>You grew up breeding and training the famous horses of the Atteran Ranches in northern Nidal. You may even be sympathetic to the Desnan dissidents who hide there from the Umbral Court.</p>\n<p>Choose two ability boosts. One must be to <strong>Strength</strong> or <strong>Dexterity</strong>, and one is a free ability boost.</p>\n<p>You're trained in the Nature skill and the Animal Lore skill. You gain the @Compendium[pf2e.feats-srd.Train Animal]{Train Animal} skill feat.</p>"},
        "items": {"g8ggs": {
                "id": "nowEaHgIyij7im8F",
                "img": "systems/pf2e/icons/features/feats/feats.webp",
                "level": "1",
                "name": "Train Animal",
                "pack": "pf2e.feats-srd"}},
        "rules": [],
        "source": {"value": ""},
        "trainedLore": "Animal Lore",
        "trainedSkills": {"value": ["nat"]},
        "traits": {
            "custom": "",
            "rarity": {"value": "common"},"value": []}},
    "effects": [],
    "flags": {},
    "img": "systems/pf2e/icons/default-icons/mystery-man.svg",
    "name": "Atteran Rancher",
    "permission": {"default": 0},
    "type": "background"}
```
