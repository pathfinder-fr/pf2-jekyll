---
title: Les états
---
Les résultats de divers types de tests peuvent vous soumettre à un état, vous-même ou, plus rarement, un objet. Les états affectent votre façon de vous comporter d’une manière ou d’une autre. Vous pouvez ainsi être paralysé par la terreur ou vous déplacer plus vite grâce à un sort ou un objet magique. Un état représentera ce qu’il vous arrive quand une créature réussit à absorber votre sang ou votre essence vitale, tandis que d’autres représenteront les attitudes des autres créatures envers vous et leur manière d’interagir avec vous. Les états sont persistants. 

Quand vous êtes affecté par un état, son effet dure jusqu’à ce que la durée indiquée arrive à son terme, que l’état soit dissipé, ou que des conditions présentées dans son profil le fassent se terminer. 

Voilà une brève description de chacun d’entre eux :
- **À terre** Vous êtes étendu au sol et il est plus facile de vous attaquer.
- **Accéléré** Vous avez une action supplémentaire à chaque tour.
- **Affaibli** Vous vous sentez sans force.
- **Amical** Un PNJ qui se trouve dans cet état a une attitude bienveillante envers vous.
- **Aveugle** Vous ne pouvez pas voir.
- **Blessé**. Vous étiez à deux doigts de mourir et vous ne vous en êtes pas encore remis entièrement.
- **Brisé** Cet objet ne peut pas être utilisé normalement jusqu’à ce qu’il ait été réparé.
- **Caché** Une créature dont vous vous cachez sait où vous vous trouvez, mais elle ne peut pas vous voir.
- **Condamné** Votre âme est en péril et la mort s’approche à grands 
pas.
- **Confus** Vous attaquez vos alliés et ennemis sans distinction.
- **Contrôlé** Une autre créature décide de vos actions.
- **Dégâts persistants** Vous subissez des dégâts à chaque round.
- **Drainé** Une hémorragie ou un effet comparable a drainé votre vitalité.
- **Ébloui** Tout ce qui vous entoure est dans l’état masqué.
- **Effrayé** La peur que vous ressentez fait perdre de l’efficacité à vos attaques et à votre défense.
- **Empoigné/Agrippé** Une créature, un objet ou de la magie vous maintient en place.
- **En fuite** Vous devez fuir.
- **Entravé** Vous êtes attaché et ne pouvez pas bouger, ou une créature vous a saisi et immobilisé.
- **Étourdi** Vous ne pouvez pas faire d’action.
- **Fasciné** Vous n’arrivez pas détacher votre attention de quelque chose.
- **Fatigué** Vos défenses sont abaissées et vous n’arrivez pas à vous concentrer pendant la phase d’exploration.
- **Hostile** Un PNJ qui se trouve dans cet état vous veut du mal.
- **Immobilisé** Vous ne pouvez pas vous déplacer.
- **Inamical** Un PNJ qui se trouve dans cet état ne vous apprécie pas.
- **Inaperçu** Une créature ignore complètement votre présence.
- **Inconscient** Vous êtes endormi ou assommé.
- **Indifférent** Un PNJ dans cet état ne vous juge pas, en bien comme en mal.
- **Invisible** Les créatures ne peuvent pas vous voir.
- **Malade** Vous vous sentez nauséeux.
- **Maladroit** Vous ne pouvez pas vous déplacer aussi facilement ou gracieusement que d’habitude.
- **Masqué** Du brouillard ou une forme d’occultation a pour effet de vous rendre difficile à voir et à prendre pour cible.
- **Mourant** La mort approche.
- **Non détecté** Si vous êtes non détecté par une créature, elle ne sait pas où vous vous trouvez.
- **Observé** Vous êtes clairement visible.
- **Paralysé** Votre corps est figé sur place.
- **Pétrifié** Vous avez été changé en pierre.
- **Pris au dépourvu** Vous n’êtes pas capable de vous défendre au mieux de vos capacités.
- **Ralenti** Vous perdez des actions à chaque tour.
- **Serviable** Un PNJ qui se trouve dans cet état veut vous apporter son aide.
- **Sourd** Vous ne pouvez pas entendre.
- **Stupéfié** Vous n’avez pas accès à toutes vos facultés mentales et avez du mal à lancer des sorts.
- **Surchargé** Vous portez une charge trop importante pour vous.

# Liste des états préjudiciables

<table>
	<tr><th>Nom</th><th>Nom anglais</th></tr>
	{% for item in site.etats %}
	  <tr>
	  	<td><a href="{{ item.url | relative_url }}">{{item.title}}</a></td>
	  	<td>{{item.titleEn}}</td>
	  </tr>
	{%- endfor -%}
</table>