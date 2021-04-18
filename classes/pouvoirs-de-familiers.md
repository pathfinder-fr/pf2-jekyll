---
Title: pouvoirs de familiers
---
# Liste des pouvoirs de familiers

<table>
	<tr><th>Nom</th><th>Nom anglais</th></tr>
	{% for item in site.pouvoirs-de-familiers %}
	  <tr>
	  	<td><a href="{{ item.url | relative_url }}">{{item.title}}</a></td>
	  	<td>{{item.titleEn}}</td>
	  </tr>
	{%- endfor -%}
</table>