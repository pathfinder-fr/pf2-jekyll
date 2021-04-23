---
title: Les archétypes
---

**Il existe une infinité de concepts de personnage possibles, mais vous constaterez peut-être que les dons et les choix de compétences d’une classe donnée ne sont pas suffisants pour concrétiser pleinement votre personnage. Les archétypes vous permettent alors d’élargir ce périmètre couvert par la classe de votre personnage.**

L’application d’un archétype nécessite de sélectionner des dons d’archétype plutôt que des dons de classe. Commencez par trouver l’archétype qui correspond le mieux au concept de votre personnage et sélectionnez le don de dévouement de cet archétype en utilisant l’un des dons de classe que vous avez choisis. Une fois que vous possédez le don de dévouement, vous pouvez sélectionner un don de cet archétype à la place d’un don de classe, à condition que vous remplissiez ses prérequis.

Le don d’archétype que vous sélectionnez reste soumis aux restrictions de sélection du don de classe qu’il remplace. Par exemple, si vous avez acquis une capacité au niveau 6 qui vous a accordé un don de classe de niveau 4 avec le trait nain, vous pouvez échanger ce don de classe uniquement contre un don d’archétype de niveau 4 ou inférieur avec le trait nain. Les dons d’archétype que vous obtenez à la place d’un don de classe sont appelés *dons de classe d’archétype*.

Parfois, un don d’archétype fonctionne comme un **don de compétence** au lieu d’un don de classe. Ces dons d’archétypes ont le trait **compétence** et vous les sélectionnez à la place d’un don de compétence ; sinon, suivez les mêmes règles que ci-dessus. Mais ce ne sont pas des dons de classe d’archétype (par exemple, pour déterminer le nombre de points de vie que vous obtenez grâce au don d’archétype Résilience du guerrier).

Chaque don de dévouement d’archétype correspond à une certaine partie du temps et de la concentration de votre personnage. Ainsi, une fois que vous avez sélectionné un don de dévouement pour un archétype, vous devez satisfaire à ses conditions avant de pouvoir obtenir un autre don de dévouement. En règle générale, vous remplissez les conditions d’un don de dévouement d’archétype en obtenant un certain nombre de dons à partir de la liste de cet archétype. Vous ne pouvez pas réapprendre un don de dévouement tant que vous avez d’autres dons venant de cet archétype.

Parfois, un don d’archétype vous permet d’obtenir un autre don, comme *Concoction basique* de l’alchimiste. Vous devez toujours satisfaire aux prérequis du don que vous obtenez de cette manière.

Deux types spéciaux d’archétypes sont désignés par les traits de classe et de multiclasse. 

### Archétypes multiclasses

Les archétypes ayant le trait multiclasse représentent la diversification de votre qualification dans les spécialités d’une autre classe. Vous ne pouvez pas sélectionner le don de dévouement d’un archétype multiclasse si vous appartenez à la classe du même nom (par exemple, un guerrier ne peut pas sélectionner le don Dévouement du guerrier).

### Archétypes de classe

Les archétypes avec le trait de classe représentent une divergence fondamentale par rapport aux spécialités de votre classe, mais qui existe dans le contexte même de votre classe. Vous ne pouvez sélectionner un archétype de classe que si vous appartenez à la classe du même nom. Les archétypes de classe modifient ou remplacent toujours certaines capacités de classe statiques d’une classe, en plus des nouveaux dons qu’ils proposent. Il est possible de prendre un archétype de classe au niveau 1 s’il altère ou remplace certaines capacités de classe initiales de la classe.

Dans ce cas, vous devez prendre le don de dévouement de cet archétype au niveau 2, puis procéder normalement. Vous ne pouvez jamais avoir plus d’un archétype de classe. 

### Archétypes d’incantation

Certains archétypes vous accordent un niveau d’incantation substantiel, bien que retardé par rapport à un personnage appartenant à une classe spécifique d’incantation. Les archétypes d’incantation sont les archétypes multiclasses des principales classes d’incantation mais aussi des archétypes d’incantation qui ne sont pas des archétypes multiclasses. Un archétype d’incantation vous permet d’utiliser les parchemins, les bâtons et les baguettes de la même manière que si vous apparteniez à une classe d’incantation.

Les archétypes d’incantation accordent toujours dans leur dévouement la capacité à lancer des tours de magie, puis ils proposent un don basique d’incantation, un don d’incantation expert et un don d’incantation maître. Ces dons partagent leur nom avec l’archétype ; par exemple, le don d’incantation maître du magicien s’appelle Incantation du magicien maître. Tous les emplacements de sorts obtenus grâce aux archétypes d’incantation ont des restrictions dépendant de l’archétype ; par exemple, l’archétype barde vous octroie des emplacements de sorts que vous ne pouvez utiliser que pour lancer des sorts occultes de votre répertoire de barde, même si vous êtes un ensorceleur ayant des sorts occultes dans votre répertoire d’ensorceleur.

- **Don d’incantation basique** : disponibles au niveau 4, ces dons vous accordent un *emplacement de sort* de niveau 1. Au niveau 6, ils vous donnent un emplacement de sort de niveau 2 et si vous avez un **répertoire de sorts**, vous pouvez choisir un sort de ce répertoire comme **sort emblématique**. Au niveau 8, ils vous accordent un emplacement de sorts de niveau 3. Les archétypes désignent ces avantages sous le nom d’« *avantages d’incantation basiques* ».
- **Don d’incantation expert** : pris au niveau 12, ces dons font de vous un expert des jets d’attaque de sorts et des DD de la tradition magique appropriée et vous octroient un emplacement de sort de niveau 4. Si vous avez un répertoire de sorts, vous pouvez choisir un second sort de ce répertoire comme sort emblématique. Au niveau 14, ils vous accordent un emplacement de sort de niveau 5, et au niveau 16, un emplacement de sort de niveau 6. Les archétypes désignent ces avantages sous le nom d’ « *avantages d’incantation expert* ».
- **Don d’incantation maître** : lorsque vous atteignez le niveau 18, ces dons font de vous un maître des jets d’attaque de sorts et des DD de la tradition magique appropriée et vous octroient un emplacement de sort de niveau 7. Si vous avez un répertoire de sorts, vous pouvez choisir un troisième sort de ce répertoire comme sort emblématique. Au niveau 20, ils vous accordent un emplacement de sort de niveau 8. Les archétypes désignent ces avantages sous le nom d’ « *avantages d’incantation maître* ».

## Liste des archétypes

<table>
    <thead>
	    <tr>
            <th>Nom</th>
            <th>Nom anglais</th>
        </tr>
    </thead>
    <tbody>
	{% for item in site.archetypes %}
	  <tr>
	  	<td><a href="{{ item.url | relative_url }}">{{item.title}}</a></td>
	  	<td>{{item.titleEn}}</td>
	  </tr>
	{%- endfor -%}
    </tbody>
</table>