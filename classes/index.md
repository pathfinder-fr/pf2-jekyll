---
title: Liste des classes
---

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