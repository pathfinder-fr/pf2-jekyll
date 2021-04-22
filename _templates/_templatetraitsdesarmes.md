---
Title : template de la page traits des armes
---
Il existe des traits qui apparaissent au fil des suppléments de règles

Les traits des armes sont traduits dans le fr.json du système Foundry.
- Il y a un champ `TraitXxxxx`où Xxxxx est le nom du trait qui donne le nom du trait
- un champ TraitDescriptionXxxx qui contient la description du trait.


Le seul souci est qu'en l'état, on ne peut pas différencier les traits des armes avec les autres traits dans la base de données des équipements. Il faut aller les chercher, les trier pour les utiliser.

Il faudrait donc aller dans le equipment.db du projet anglais, 
- trouver tous les équipements dont le champ `type` a une value de weapon
- puis pour ces **seuls** fichiers collecter toutes les valeurs du champ `traits`
- générer la liste en supprimant les doublons

- puis aller chercher dans le fr.json du system Foundry pour chacun 
    - la valeur du champ TraitXxxx et TraitDescriptionXXX 
    - les afficher sur la page traits-des-armes.md en les rangeant dans l'ordre alphabétique de la valeur du TraitXXX en français suivi de TraitDescriptionXXX
