---
Title: Les classes
---
Tout comme l’ascendance de votre personnage joue un rôle essentiel dans la construction de son identité et de sa vision du monde, sa classe permet de déterminer sa formation et sa manière d’évoluer comme aventurier. Le choix de la classe de votre personnage est peut-être la décision la plus importante que vous ferez le concernant. Les groupes de joueurs créent souvent des personnages aux compétences et aux pouvoirs complémentaires. Ils s’assurent par exemple que le groupe possède un guérisseur, un combattant, un personnage furtif et un utilisateur de magie. Il peut donc être intéressant de discuter avec votre groupe avant de faire votre choix.
---

Les règles propres à chaque classe vous permettent de donner vie à d’innombrables concepts de personnages. Vous pouvez ainsi créer un alchimiste brillant mais écervelé, capable de réciter à la demande des formules complexes d’objets alchimiques, mais ayant du mal à se souvenir de la date de l’anniversaire de son meilleur ami. Vous pouvez également incarner une guerrière musclée qui devient aussi inébranlable qu’une montagne une fois qu’elle a un bouclier en main. Votre personnage pourra aussi être un ensorceleur impétueux dont le bout des doigts s’agite en crépitant de la lumière dont il a hérité d’un ancêtre angélique. La façon dont vous orientez votre personnage dans sa classe (comme le choix de la divinité du prêtre, de l’arme du guerrier ou du lignage de l’ensorceleur) lui donne vie dans le cadre des règles et du monde.

Vous trouverez dans les pages suivantes une description des classes de Pathfinder. Chaque rubrique vous donne les informations nécessaires pour jouer un personnage de cette classe et le faire évoluer depuis ses modestes débuts au niveau 1 jusqu’à la puissance vertigineuse qu’il atteindra au niveau 20. En plus des parties consacrées aux classes, vous devrez sûrement consulter les sections suivantes, qui présentent des options de personnage supplémentaires et expliquent comment faire évoluer votre personnage au niveau supérieur.

- La page [Gagner un niveau](generalites/gagner-un-niveau.md), vous explique comment améliorer votre personnage une fois que vous aurez obtenu assez de **points d’expérience** pour passer au niveau supérieur.
- La page [Compagnons animaux et familiers](compagnons-animaux-et-familiers.md), vous présente les règles régissant la création d’un **compagnon animal** ou d’un **familier** qui vous accompagnera dans vos aventures. Vous devez cependant posséder une **capacité de classe** ou un don qui vous permet d’avoir un compagnon ou un familier.
- La page [Archétypes](archetypes.md) vous présentent les options thématiques qui vous permettent de personnaliser encore les pouvoirs de votre personnage. Les archétypes vous permettent de gagner des pouvoirs associés à d’autres classes à partir du niveau 2 ou de tracer une voie thématique. Cependant, l’utilisation de ces règles n’est pas recommandée aux joueurs débutants.

# Liste des classes

<table>
	<tr><th>Nom</th><th>Nom anglais</th></tr>
	{% for item in site.classes %}
	  <tr>
	  	<td><a href="{{ item.url | relative_url }}">{{item.title}}</a></td>
	  	<td>{{item.titleEn}}</td>
	  </tr>
	{%- endfor -%}
</table>