---
title: Les ascendances
---

# Présentation

Chacune des pages consacrées aux différentes ascendances vous donne des informations sur votre ascendance et présente les éléments de règles listés par la suite (elles sont toutes réunies sur la page, à l’exception des **héritages** et des **dons ancestraux**).

## Points de vie

Il s’agit du nombre de **points de vie** conférés à un personnage par son ascendance au niveau 1.
Vous ajouterez ensuite à cette valeur les points de vie conférés par la classe de votre personnage (y compris son modificateur de Constitution).

## Taille

Il s’agit de la taille et de la corpulence des membres de votre ascendance.
La taille Moyenne correspond approximativement à la taille et au poids d’un adulte humain, la Petite taille correspond approximativement à la moitié de cela.

## Vitesse

Il s’agit de la distance que peut parcourir un membre de votre ascendance à chaque fois qu’il utilise une action (comme Marcher rapidement) pour se déplacer.

## Primes de caractéristiques

Il s’agit de la liste des caractéristiques auxquelles vous appliquez une **prime de caractéristique** quand vous choisissez cette ascendance.
La plupart des ascendances vous confèrent des primes dans deux caractéristiques spécifiques, ainsi qu’une prime libre que vous pouvez appliquer à une autre caractéristique de votre choix. 

## Pénalité de caractéristique

Cette liste vous indique la caractéristique à laquelle appliquer une **pénalité de caractéristique** quand vous choisissez cette ascendance.
Toutes les ascendances à l’exception des humains confèrent une pénalité de caractéristique. 

## Langues

Vous trouverez ici les langues que les membres de votre ascendance parlent au niveau 1.
Si votre modificateur d’Intelligence est supérieur ou égal à +1, vous pouvez choisir d’autres langues dans la liste qui vous est proposée dans cette section. 

## Traits

Ces catégories ne sont pas liées à des avantages particuliers, mais elles permettent de déterminer l’influence de certains sorts, effets et autres aspects du jeu sur votre personnage.

## Pouvoirs spéciaux

Les paragraphes affichés dans la marge représentent vos pouvoirs, vos sens et les autres qualités partagées par tous les membres de votre ascendance. Certaines ascendances ne confèrent pas de pouvoirs spéciaux.

## Héritages

Au niveau 1, vous choisissez un **héritage** qui reflète les pouvoirs qui vous ont été transmis par vos ancêtres, ou qui sont couramment possédés par les membres de votre ascendance dans l’environnement dans lequel vous êtes né ou avez grandi.
Vous ne possédez qu’un seul héritage et, une fois choisi, vous ne pouvez plus en changer. Un héritage n’est pas la même chose qu’une culture ou une ethnie.
Cela dit, certaines cultures ou ethnies sont essentiellement composées de membres qui ont un héritage commun.

## Dons ancestraux

Les dons ancestraux vous aident à personnaliser votre personnage.
Vous gagnez votre premier don ancestral au niveau 1, puis un nouveau don au niveau 5, au niveau 9, au niveau 13 et au niveau 17, comme indiqué dans le tableau de progression présenté dans la description de chaque classe.  
Les dons ancestraux sont organisés par niveau. Au moment de la création de votre personnage, vous pouvez seulement choisir un don ancestral de niveau 1.
Vous aurez ensuite la possibilité de choisir nouveau un don en utilisant la liste des dons de votre niveau et d’un niveau inférieur.
Ces dons sont parfois associés à des **prérequis**, des conditions que votre personnage doit remplir avant de pouvoir les sélectionner. 

# Liste des ascendances

<table>
    <thead>
        <tr>
            <th>Nom</th>
            <th>Nom Anglais</th>
        </tr>
    </thead>
    <tbody>
	{% for pair in site.data.ascendances %}
      {%- assign key =   pair[0] -%}
      {%- assign value = pair[1] -%}
      {% if key != "ascendance-vierge" %}
	    <tr>
            <td><a href="{{ key }}.html">{{ value.nom }}</a></td>
	  	    <td>{{ value.nomEn }}</td>
	    </tr>
      {%- endif -%}
	{%- endfor -%}
    </tbody>
</table>