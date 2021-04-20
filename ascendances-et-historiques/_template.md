----
Title: template des classes
---
- Créer une page de regroupement pour faire apparaître toutes les ascendances accessibles en les affichant par ordre alphabétique avec une couleur ou une indication en fonction de la rareté. Pour gérer la Rareté : il y a un trait `rarity` qui donne la rareté de l'ascendance

- générer automatiquement les pages des ascendances *à partir de la structure des pages du compendium des ascendances de Foundry* issues de la ancestries.db :

Sur chaque page d'ascendance afficher :
- **Nom** en français avec un titrage h1
- **Name** en anglais en plus petit
- Traduction : **État d'origine:** et en cas d'absence **État** qui permettra de dire si c'est libre ou officielle 

- **description (fr)** le html doit être cleanup et constituera le centre de la page (fait avec juste un souci de cleanup sur les noms propres de l'ascendance)

- **Héritages de l'ascendance** :
les héritages de l'ascendance sont à aller chercher dans **ancestryfeatures.db**.
    - Pour effectuer le tri dans les données :
       - récupérer d'abord tous les fichiers dont le featType est un héritage :  
       `"featType": {"value": "heritage"}. Cela va d'abord permettre de récupérer tous les héritages 
       - puis dans ces fichiers, il faut faire le tri pour s'assurer que la value du trait correspondant à l'ascendance correspond à l'ascendance ex :`"traits": {value": elf})`,
    - une fois le tri effectué, il faut alors faire un cleanup, puis afficher l'héritage sur la page en affichant par ordre alphabétique  

- **nom** **Name**, 
- description (Fr) 

Dans une colonne à droite de la page, il faut aller chercher des valeurs du json de l'ascendance pour afficher  
- **Points de vie**  
afficher la valeur du champ de `hp` du json de l'ancestry 
 
- **Taille**  
Traduire la valeur du champ `size`du json de l'ancestry: la taille est en anglais

**Vitesse**
le champ `speed`donne la vitesse mais en pieds, à convertir en multipliant par 1,5 la valeur et en ajoutant m

**Primes de caractéristiques**
les `champs boosts 0 et 1` du json de l'ancestry contiennent les primes de caractéristiques. Elles sont en anglais et sous forme d'abbréviation. Pour les traduire, on peut partir du fr.json et du champ "AbilityXxx" "AbilityStr": pour Str pour récupérer la valeur Force.  
Prime de caractéristique libre

NB (voir pour l'humain qui dispose de deux primes de caractéristique libre)

**Pénalité de caractéristique**
champ `flaws` du json de l'ancestry. Elles sont en anglais et sous forme d'abbréviation. Idem que ci-dessus pour récupérer la caractéristique. 

**langues** languages donne les langues parlées d'office par l'ascendance

*Langues supplémentaires* champ `additionallanguages` du json. Elles sont en anglais et peuvent être traduites à partir de Languagenomdelaraceanglaise pour récupérer la valeur dans le fr.json  

**traits**
trait value donne les traits de l'ascendance en anglais à traduire avant affichage 


// viennent ensuite les capacités de l'ascendance. et renvoie à l'id d'une particularité qui se trouve dans la ancestryfeatures.db et qui permet d'aller la chercher pour l'afficher
champ `items name = 0898m` donne une id qui correspond à la vision en anglais (vision dans le noir, vision nocturne)
champ `fvpio` donne une id qui correspond à une capacité raciale qui se trouve dans la ancestryfeatures.db
champ `ouwoo` donne une particularité raciale (dans la ancestryfeatures.db avec son id qui permet d'aller la chercher pour l'affichage avec le nom et la description) 

**nom**  
description


    
- Idéalement, générer soit à part, soit à la suite une page des dons de chaque ascendance en extrayant les dons ayant une valeur ancestry dans la feats.db, le nom de l'ascendance `"traits": {value": elf})` puis afficher en triant par niveau puis ordre alphabétique en affichant : nom du don en fr, type d'action, niveau puis en dessous le nom anglais puis les prérequis, puis la descriptionFr. 
    - Pour certaines ascendances (demi-orc, demi-elfe), les pages n'existent pas dans le compendium Foundry en tant que telles et elles permettent d'obtenir les dons des deux ascendances mêlées ex elfe et humain. Voir ce qui peut être réalisé ou créé mais en option
