---
title: Liste des états préjudiciables
---

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