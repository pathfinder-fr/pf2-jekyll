----
Title: template des ascendances
---
- Créer une page de regroupement pour faire apparaître toutes les ascendances accessibles en les affichant par ordre alphabétique avec une couleur ou une indication en fonction de la rareté. Pour gérer la Rareté : il y a un trait `rarity` dans chaque ascendance qui détermine la rareté de l'ascendance donnée.

- générer automatiquement les pages des ascendances *à partir de la structure des pages du compendium des ascendances de Foundry* issues de la ancestries.db :

Sur chaque page d'ascendance afficher :
- **Nom** en français avec un titrage h1
- **Name** en anglais en plus petit
- **Traduction** : en récupérant l'`État d'origine:` et en cas d'absence l'`État` qui permet de montrer si c'est une traduction libre ou officielle 

- **description (fr)** le html doit être transformé en markdown et constitue l'affichage au centre de la page

A la fin de cette page, il faudrait pouvoir afficher :
- **Héritages de l'ascendance** :
les héritages de l'ascendance sont à aller chercher dans **ancestryfeatures.db**.
    - Pour effectuer le tri dans les données qui figurent dans la DB :
       - il faut d'abord récupérer tous les fichiers qui sont des héritages indiqués par le champ `featType` : `"featType": {"value": "heritage"}. 
       - Une fois que ces fichiers héritage sont récupérés, il faut encore faire le tri pour s'assurer que la value du champ trait correspond à la page de l'ascendance sur laquelle on souhaite place l'héritage ex :`"traits": {value": elf})` si  `Name` a la valeur elf,
    - une fois le tri des données effectué, il faut alors faire un cleanup du html de la description pour la passer en markdown, puis afficher de tous les héritages de l'ascendance sur la page de l'ascendance les uns à la suite des autres en affichant par ordre alphabétique : 
        - **nom** en gros 
        - **Name** en petit, 
        - **Traduction** en récupérant l'`État d'origine:` et en cas d'absence l'`État` qui permet de montrer si c'est une traduction libre ou officielle 
        - description (Fr) 

Dans une colonne séparée située à droite de la page, il faut aller chercher des valeurs du json de l'ascendance pour afficher  
- **Points de vie**
puis en dessous afficher la valeur numérique contenue dans le champ `hp` du json de l'ancestry 
 
- **Taille**  
puis en dessous Afficher la valeur du champ qu'il faut aller chercher dans le champ `size`du json de l'ancestry. La taille est en anglais. Il faut la traduire à partir du fr.json

- **Vitesse**
puis le champ `speed`donne la vitesse mais en pieds, à convertir en multipliant par 1,5 la valeur et en ajoutant la lettre m. On peut réfléchir à afficher en pieds, en m et en cases

**Primes de caractéristiques**
Il faut aller les chercher dans les champs `boosts 0, 1, 2` du json de chaque ancestry. Ces champs contiennent desvaleurs en anglais et sous forme d'abbréviation (Str, Dex,Con, Int, Wis, Cha). Pour les traduire, on peut aller récupérrer dans le fr.json du système de Foundry le champ "AbilityXxx" où Xxx est égal aux trois premières lettre de la caractéristique ("AbilityStr": pour Str) pour récupérer la valeur d'affichage de la caractéritisuqe. Cela devrait afficher deux primes de caractéristique
puis mention Prime de caractéristique libre. NB (voir pour traiter la page de l'humain qui dispose de deux primes de caractéristique libre)

- **Pénalité de caractéristique**
Ce qui doit être affiché doit être récupérer dans le champ `flaws` du json de l'ancestry. comme les primes, la pénalité est en anglais et sous forme abrégée (ex : Str). Idem que ci-dessus pour afficher la valeur de la caractéristique en français. 

- **langues** 
Le champ `languages` de l'ancestry donne les langues parlées d'office par l'ascendance. Elles peuvent être traduites à partir du fr.json en récupérant la valeur d'affichage du champ `Languagenomdelalangueenanglais` pour afficher la valeur en français 

- *Langues supplémentaires* 
le champ `additionallanguages` de l'ancestry donne les langues qu'un membre de l'ascendance peut parler s'il dispose d'un modif d'Intelligence positif. Comme au-dessus, elles sont en anglais et peuvent être traduites à partir de `Languagenomdelaraceanglaise` pour ensuite récupérer la valeur d'affichage dans le fr.json  

- **traits**
`trait value` donne les traits de l'ascendance en anglais qui sont à récupérer dans le fr.json pour les afficher 

// viennent ensuite les capacités de l'ascendance. Cela renvoie à l'id des capacités qui se trouvent dans la **ancestryfeatures.db**. L'ID permet d'aller chercher chacune des capacités de la race pour ensuite l'afficher.
champ `items name = 0898m` donne une id qui correspond à la vision en anglais (vision dans le noir, vision nocturne)
champ `fvpio` donne une id qui correspond à une capacité raciale qui se trouve dans la ancestryfeatures.db
champ `ouwoo` donne une particularité raciale 

Dans la ancestryfeatures.db l'id permet d'aller chercher pour l'affichage  
    - **nom**  
    - descriptionFr
    - État d'origine ou État antérieur

- Idéalement, il faudrait générer sur une page à part dons-ancestraux-nomdelascendance, les dons propres à chaque ascendance en extrayant dans la feats.db les dons ayant la valeur ancestry, puis le nom de l'ascendance concernée dans les traits ex : `"traits": {value": elf})`.

Il faudrait les afficher au centre en les triant par niveau puis par ordre alphabétique en affichant : Nom du don en fr, type d'action, niveau puis en dessous le nom anglais puis les prérequis, puis la descriptionFr. 
On pourrait prévoir dans une colonne à droite, en petit juste le nom et le niveau pour circuler sur la page

    - Pour certaines ascendances (demi-orc, demi-elfe), les pages n'existent pas dans le compendium Foundry en tant que telles et elles permettent d'obtenir les dons des deux ascendances mêlées ex elfe et humain. On aura aussi à gérer les héritages versatiles qui peuvent être ajoutés à toutes les ascendances
