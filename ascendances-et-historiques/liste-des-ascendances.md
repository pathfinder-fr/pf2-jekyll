---
Title: Liste des ascendances
---
# Liste des dons

<table>
	<tr><th>Nom</th><th>Nom anglais</th></tr>
	{% for item in site.data.ascendances %}
	  <tr>
	  	<td><a href="{{ item.url | relative_url }}">{{item.title}}</a></td>
	  	<td>{{item.titleEn}}</td>
	  </tr>
	{%- endfor -%}
</table>