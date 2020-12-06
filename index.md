---
title: Bienvenue
---
# Bienvenue

## Liste des actions

<table>
	<tr><th>Nom</th><th>Nom anglais</th></tr>
	{% for item in site.actions %}
	  <tr>
	  	<td><a href="/srd/pf2{{ item.url }}">{{item.title}}</a></td>
	  	<td>{{item.titleEn}}</td>
	  </tr>
	{%- endfor -%}
</table>