---
# ATTENTION : Ne modifiez pas ce fichier
# Ce fichier est généré automatiquement par un script d'après les données du module Foundry VTT officiel et de sa traduction
title: Médecin
titleEn: Medic
id: CA22ZhzFPjahrO4W
urlFr: https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/blob/master/data/archetypes/CA22ZhzFPjahrO4W.htm
urlEn: https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/blob/master/packs/data/archetypes.db/medic.json
layout: archetypes
---
# Médecin

Vous avez étudié des techniques innombrables pour founir une assistance médicale, faisant de vous un médecin et un soigneur sans égal.

<h2 style="text-align: left;">[Dévouement de Médecin](../dons/dévouement-de-médecin.md) Don 2

**Prérequis** qualifié en Médecine, Médecine militaire

Vous devenez un expert en Médecine. Lorsque vous obtenez un Succès avec [Médecine militaire](../dons/médecine-militaire.md) ou [Soigner les blessures](../actions/soigner-les-blessures.md), la cible récupère 5 PV supplémentaire avec un DD 20, 10 PV avec un DD 30 ou 15 PV avec un DD 40. Une fois par jour, vous pouvez utiliser Médecine militaire sur une créature qui est temporairement immunisée. Si vous êtes maître en Médecine, Vous pouvez le faire une fois par heure.

**Spécial** Vous ne pouvez choisir un autre don de dévouement avant d'avoir choisi deux autres dons de l'archétype de Médecin.

## [Tournée du médecin](../dons/tournée-du-médecin.md) Don 4

**Prérequis** 

Vous vous déplacez pour fournir des soins immédiats à ceux qui en ont besoin. <a class="entity-link" data-pack="pf2e.actionspf2e" data-id="Bcxarzksqt9ezrs6" draggable="true">Marchez rapidement</a> et puis utilisez une des actions suivantes : <a class="entity-link" data-pack="pf2e.feats-srd" data-id="wYerMk6F1RZb0Fwt" draggable="true">Médecine militaire</a> ou <a class="entity-link" data-pack="pf2e.actionspf2e" data-id="KjoCEEmPGTeFE4hh" draggable="true">Soigner un empoisonnement</a>. Vous pouvez dépenser une seconde action pour, à la place, Marcher rapidement et puis <a class="entity-link" data-pack="pf2e.actionspf2e" data-id="MHLuKy4nQO2Z4Am1" draggable="true">Prodiguer les premiers soins</a> ou [Soigner un état](../dons/soigner-un-état.md) (si vous possédez ce dernier ; voir ci-dessous).

## [Soigner un état](../dons/soigner-un-état.md) Don 4

**Prérequis** Dévouement de Médecin

**Conditions** Vous transportez des outils de guérisseur ou vous les portez et disposez d'une main libre.

Vous soignez une créature adjacente pour essayer de réduire l'état [Maladroit](../conditions/maladroit.md), [Affaibli](../conditions/affaibli.md) ou [Malade](../conditions/malade.md). Si une créature souffre de plusieurs états sur cette liste, choissez en un.

tentez un test de contre contre cet état, en utilisant votre modificateur de Médecine comme votre modificateur et la source de l'état pour déterminer le DD.

Vous ne pouvez soigner un état qui provient d'un artefact ou d'un effet au delà du niveau 20 à moins que vous ne soyez [Infirmier légendaire](../dons/infirmier-légendaire.md) ; même dans ce cas, le DD de contre augmente de 10.

Soigner un état qui est continuellement appliqué sous certaines circonstances (par exemple, l'état affaibli qu'un personnage bon obtient lorsqu'il transporte une arme *impie*) n'a pas d'effet tant que les circonstances se produisent.

**Succès critique** Réduisez la valeur de l'état de 2.

**Succès** Réduisez la valeur de l'état de 1.

**Échec critique** Augmentez la valeur de l'état de 1.

## [Soins holistiques](../dons/soins-holistiques.md) Don 6

**Prérequis** qualifié en Diplomatie, Soigner un état

Vous fournissez des soins émotionnels et spirituels. Ajoutez <a class="entity-link" data-pack="pf2e.conditionitems" data-id="TBSHQspnbcqxsmjL" draggable="true"><i class="fas fa-book-open"></i>Effrayé</a>, <a class="entity-link" data-pack="pf2e.conditionitems" data-id="e1XGnhKNSQIm5IXg" draggable="true"><i class="fas fa-book-open"></i>Stupéfié</a> et <a class="entity-link" data-pack="pf2e.conditionitems" data-id="dfCMdR4wnpbYNTix" draggable="true"><i class="fas fa-book-open"></i>Étourdi</a> à la liste des états que vous pouvez réduire avec [Soigner un état](../dons/soigner-un-état.md). Si la condition Étourdi possède une durée au lieu d'une valeur, vous ne pouvez pas utiliser Soigner un état pour la réduire.

## [Ressusciter](../dons/ressusciter.md) Don 16

**Prérequis** Dévouement de Médecin, légendaire en Médecine

**Conditions** Vous transportez des outils de guérisseur ou les portez et possédez une main libre. De même, le corps de la cible est pour l'essentiel intact

Vous pouvez utiliser Médecine pour ressusciter le mort. Faites un test de Médecine contre un DD40 pour réanimer une créature morte qui n'a pas été morte depuis plus de 3 rounds. Si vous obtenez un succès, la cible revient à la vie avec les effets de <a class="entity-link" data-pack="pf2e.spells-srd" data-id="IkGYwHRLhkuoGReG" draggable="true">Rappel à la vie</a>, sauf qu'elle possède toujours la condition <a class="entity-link" data-pack="pf2e.conditionitems" data-id="Yl48xTdMh3aeQYL2" draggable="true"><i class="fas fa-book-open"></i>Blessée</a> qu'elle possédait avant d'être <a class="entity-link" data-pack="pf2e.conditionitems" data-id="yZRUzMqrMmfLu0V1" draggable="true"><i class="fas fa-book-open"></i>Mourante</a>, augmentée de 1 (ou Blessée 1 si elle n'était pas Blessée avant de mourir). Que vous obteniez un succès ou pas, la cible est temporairement immunisée à Ressusciter pendant 1 journée.
