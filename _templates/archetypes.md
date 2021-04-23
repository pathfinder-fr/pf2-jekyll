---
title: template pour générer la page des types d'archétypes
---
## Les archétypes
Idéalement, il faudrait générer deux pages : 

### une page des dons multiclasse
ATTENTION : Je ne sais pas s'il est possible d'utiliser une fonction de tri dans archetypes.db en allant chercher les archétypes dont le nom correspond à celui d'une classe de base  
- Archétype alchimiste pour la classe d'alchimiste
- Archétype barde pour barde
- Archétype druide pour druide (...)

Si c'est le cas, cela permettrait d'avoir une page de regroupement pour les seuls archétypes multiclasse qui ont des règles spécifiques. 

Sous Foundry, nous disposons des champs suivants
- `ID`
- `Name`
- `Nom`
- `État`
- `État d'origine`
- `Description (en)`
- `Description (fr)`

La page description contient toute la description complète de chaque archétype, c'est à dire le fluff et tous les dons de l'archétype classé dans l'ordre que l'on retrouve également dans la feats.db


Sur chaque page, il faut afficher 
- le `nom`
- le `name`
- l'`état d'origine` et à défaut l'`état`

Suivi de la `description Fr` : Il y a besoin de faire un nettoyage pour supprimer les liens et de récupérer les balises de titre pour disposer d'une page propre pour chacun.

```
Dans les compendium foundry
Name: Investigator
Nom: Enquêteur
État: changé
État d'origine: libre

------ Description (en) ------
<h1>Investigator</h1>
<p><span>You've developed a keen eye for investigating mysteries.</span></p>
<h2>@Compendium[pf2e.feats-srd.CZXhJS55rG5H6PpB]{Investigator Dedication} Feat 2</h2>
<p><strong>Prerequisites</strong> Intelligence 14</p>
<hr />
<p>You gain the on the case class feature, which grants you both the @Compendium[pf2e.actionspf2e.xTK2zsWFyxSJvYbX]{Pursue a Lead} activity and @Compendium[pf2e.actionspf2e.25WDi1cVUrW92sUj]{Clue In} reaction. You become trained in Society and another skill of your choice. If you were already trained in Society, you instead become trained in an additional skill of your choice. You also become trained in investigator class DC.</p>
<p><strong>Special</strong> You can't select another dedication feat until you have gained two other feats from the investigator archetype.</p>
<h2>@Compendium[pf2e.feats-srd.X0NFLIn1bqj6bnd0]{Basic Deduction} Feat 4</h2>
<p><strong>Prerequisites</strong> Investigator Dedication</p>
<hr />
<p>You gain a 1st- or 2nd-level investigator feat of your choice.</p>
<h2>@Compendium[pf2e.feats-srd.7XcQ8Ygz5cubGxdC]{Investigator's Stratagem} Feat 4</h2>
<p><strong>Prerequisites</strong> Investigator Dedication</p>
<hr />
<p>You play out battles in your head, allowing you to strike when the time is right. You gain the Devise a Stratagem action; however, when you substitute its result for your attack roll, you can't use your Intelligence modifier in place of your Strength or Dexterity modifier. You also can't use your Intelligence modifier on other rolls from abilities that expand Devise a Stratagem, such as the Athletic Strategist feat.</p>
<h2>@@Compendium[pf2e.feats-srd.L1rCuwsCKWd9zlS3]{Advanced Deduction}} Feat 6</h2>
<p><strong>Prerequisites</strong> Basic Deduction</p>
<hr />
<p>You gain one investigator feat. For the purpose of meeting its prerequisites, your investigator level is equal to half your character level.</p>
<p><strong>Special</strong> You can select this feat more than once. Each time you select it, you gain another investigator feat.</p>
<h2>@Compendium[pf2e.feats-srd.Ig431EeRy3FKMmMq]{Keen Recollection} Feat 6</h2>
<p><strong>Prerequisites</strong> Investigator Dedication</p>
<hr />
<p>You can recall all sorts of information. You gain the keen recollection class feature.</p>
<h2>@Compendium[pf2e.feats-srd.v4O6eDiSOkzQZHmT]{Skill Mastery} Feat 10</h2>
<p><strong>Prerequisites</strong> Investigator Dedication, trained in at least one skill and expert in at least one skill</p>
<hr />
<p>Increase your proficiency rank in one of your skills from expert to master and in another of your skills from trained to expert. You gain a skill feat associated with one of the skills you chose.</p>
<p><strong>Special</strong> You can select this feat up to five times.</p>
<h2>@Compendium[pf2e.feats-srd.t8CAK8ylu23PUxbn]{Master Spotter} Feat 12</h2>
<p><strong>Prerequisites</strong> Investigator Dedication, expert in Perception</p>
<hr />
<p>Your proficiency rank in Perception increases to master.</p>
------ Description (fr) ------
<h1>Enquêteur</h1>
<p>Vous avez développé un œil affûté pour enquêter sur les mystères.</p>
<h2 style="text-align: left;">@Compendium[pf2e.feats-srd.CZXhJS55rG5H6PpB]{Dévouement d'enquêteur} Don 2</h2>
<p><strong>Prérequis</strong> Intelligence 14</p>
<hr />
<p>vous obtenez la capacité de classe @Compendium[pf2e.classfeatures.6FasgIXUJ1X8ekRn]{Sur l'affaire}, qui vous accorde à la fois l'activité @Compendium[pf2e.actionspf2e.xTK2zsWFyxSJvYbX]{Suivre une piste} et la réaction @Compendium[pf2e.actionspf2e.25WDi1cVUrW92sUj]{Partager les indices}. Vous devenez qualifié en Société et une autre compétence de votre choix. Si vous étiez déjà qualifié en Société, vous devenez qualifié en une compétence supplémentaire de votre choix. Vous devenez qualifié en DD de classe d'Enquêteur.</p>
<p><strong>Spécial</strong> Vous ne pouvez pas choisir un autre don de dévouement tant que vous n'avez pas choisi deux autres dons de l'archétype d'enquêteur.</p>
<h2>@Compendium[pf2e.feats-srd.X0NFLIn1bqj6bnd0]{Déduction basique} Don 4</h2>
<p><strong>Prérequis</strong> Dévouement d'enquêteur</p>
<hr />
<p>Vous obtenez un don d'enquêteur de niveau 1 ou 2 de votre choix.</p>
<h2>@Compendium[pf2e.feats-srd.7XcQ8Ygz5cubGxdC]{Stratagème d'Enquêteur} Don 4</h2>
<p><strong>Prérequis</strong> Dévouement d'enquêteur</p>
<hr />
<p>Vous simulez les combats dans votre tête, vous permettant de @Compendium[pf2e.actionspf2e.VjxZFuUXrCU94MWR]{Frapper} au bon moment. Vous obtenez l'action @Compendium[pf2e.actionspf2e.m0f2B7G9eaaTmhFL]{Concevoir un stratagème} ; quoi qu'il en soit, lorsque vous substituez son résultat à votre jet d'attaque, vous ne pouvez utiliser votre modificateur d'Intelligence au lieu de votre modificateur de Force ou de Dextérité.</p>
<p>Vous ne pouvez pas davantage utiliser votre modificateur d'Intelligence sur d'autres jets provenant de capacités qui sont une extension de Concevoir un Stratagème, tel que le don @Compendium[pf2e.feats-srd.Tu1hOEr6Ko9Df54L]{Stratège athlétique}.</p>
<h2>@@Compendium[pf2e.feats-srd.L1rCuwsCKWd9zlS3]{Déduction avancé}} Don 6</h2>
<p><strong>Prérequis</strong> Déduction basique</p>
<hr />
<p>Vous obtenez un don d'enquêteur. Pour les besoin de remplir ses prérequis, votre niveau d'enquêteur est de la moitié de vontre niveau de personnage.</p>
<p><strong>Spécial</strong> Vous pouvez choisir ce don plus d'une fois. Chaque fois que vous le choisissez, vous obtenez un autre don d'enquêteur.</p>
<h2>@Compendium[pf2e.feats-srd.Ig431EeRy3FKMmMq]{Souvenirs affûtés} Don 6</h2>
<p><strong>Prérequis</strong> Dévouement d'enquêteur</p>
<hr />
<p>Vous pouvez vous souvenir de toutes sortes d'information. Vous obtenez la capacité de classe @Compendium[pf2e.classfeatures.DZWQspPi4IkfXV2E]{Souvenirs affûtés}.</p>
<h2>@Compendium[pf2e.feats-srd.v4O6eDiSOkzQZHmT]{Maîtrise des compétences (Enquêteur)} Don 10</h2>
<p><strong>Prérequis</strong> Dévouement de l'enquêteur, qualifié dans au moins une compétence et expert dans au moins une autre</p>
<hr>
<p>Augmentez le rang de maîtrise de l’une de vos compétences d’expert à maître et le rang d'une autre de vos compétences de qualifié à expert. Vous gagnez un don de compétence associé à l’une des compétences que vous avez choisies.</p>
<p><strong>Spécial</strong> Vous pouvez sélectionner ce don jusqu’à cinq fois.</p>
<h2>@Compendium[pf2e.feats-srd.t8CAK8ylu23PUxbn]{Dénicheur maître (Enquêteur)} Don 12</h2>
<p><strong>Prérequis</strong> Dévouement de l'enquêteur, expert en Perception</p>
<hr />
<p>votre rang de maîtrise en Perception passe à maître.</p>```
```

```
Dans le projet anglais, l'archetype donne :
{
    "_id": "Buptz08MArCtY41w",
    "content": "<h1>Investigator</h1>\n<p><span>You've developed a keen eye for investigating mysteries.</span></p>\n<h2>@Compendium[pf2e.feats-srd.Investigator Dedication]{Investigator Dedication} Feat 2</h2>\n<p><strong>Prerequisites</strong> Intelligence 14</p>\n<hr />\n<p>You gain the on the case class feature, which grants you both the @Compendium[pf2e.actionspf2e.Pursue a Lead]{Pursue a Lead} activity and @Compendium[pf2e.actionspf2e.Clue In]{Clue In} reaction. You become trained in Society and another skill of your choice. If you were already trained in Society, you instead become trained in an additional skill of your choice. You also become trained in investigator class DC.</p>\n<p><strong>Special</strong> You can't select another dedication feat until you have gained two other feats from the investigator archetype.</p>\n<h2>@Compendium[pf2e.feats-srd.Basic Deduction]{Basic Deduction} Feat 4</h2>\n<p><strong>Prerequisites</strong> Investigator Dedication</p>\n<hr />\n<p>You gain a 1st- or 2nd-level investigator feat of your choice.</p>\n<h2>@Compendium[pf2e.feats-srd.Investigator's Stratagem]{Investigator's Stratagem} Feat 4</h2>\n<p><strong>Prerequisites</strong> Investigator Dedication</p>\n<hr />\n<p>You play out battles in your head, allowing you to strike when the time is right. You gain the Devise a Stratagem action; however, when you substitute its result for your attack roll, you can't use your Intelligence modifier in place of your Strength or Dexterity modifier. You also can't use your Intelligence modifier on other rolls from abilities that expand Devise a Stratagem, such as the Athletic Strategist feat.</p>\n<h2>@@Compendium[pf2e.feats-srd.Advanced Deduction]{Advanced Deduction}} Feat 6</h2>\n<p><strong>Prerequisites</strong> Basic Deduction</p>\n<hr />\n<p>You gain one investigator feat. For the purpose of meeting its prerequisites, your investigator level is equal to half your character level.</p>\n<p><strong>Special</strong> You can select this feat more than once. Each time you select it, you gain another investigator feat.</p>\n<h2>@Compendium[pf2e.feats-srd.Keen Recollection]{Keen Recollection} Feat 6</h2>\n<p><strong>Prerequisites</strong> Investigator Dedication</p>\n<hr />\n<p>You can recall all sorts of information. You gain the keen recollection class feature.</p>\n<h2>@Compendium[pf2e.feats-srd.Skill Mastery (Investigator)]{Skill Mastery} Feat 10</h2>\n<p><strong>Prerequisites</strong> Investigator Dedication, trained in at least one skill and expert in at least one skill</p>\n<hr />\n<p>Increase your proficiency rank in one of your skills from expert to master and in another of your skills from trained to expert. You gain a skill feat associated with one of the skills you chose.</p>\n<p><strong>Special</strong> You can select this feat up to five times.</p>\n<h2>@Compendium[pf2e.feats-srd.Master Spotter (Investigator)]{Master Spotter} Feat 12</h2>\n<p><strong>Prerequisites</strong> Investigator Dedication, expert in Perception</p>\n<hr />\n<p>Your proficiency rank in Perception increases to master.</p>\n",
    "flags": {},
    "name": "Investigator",
    "permission": {
        "default": 0
    }
}
```

### Les archétypes distincts
Les archétypes distincts ouvrent des carrières différentes.

Idéalement, il faudrait une page de regroupement pour les lister dans un tableau.

Il faudrait ensuite une page par archétype 

Sur chaque page ainsi créée, il faut afficher 
- le `nom`
- le `name`
- l'`état d'origine` et à défaut l'`état`

Suivi de la `description Fr` : Il y a besoin de faire un nettoyage pour supprimer les liens et de récupérer les balises de titre pour disposer d'une page propre pour chacun.
