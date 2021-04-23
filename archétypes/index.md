---
title: Archétypes
---

# Liste des archétypes

<table>
    <thead>
	    <tr>
            <th>Nom</th>
            <th>Nom anglais</th>
        </tr>
    </thead>
    <tbody>
	{% for item in site.archétypes %}
	  <tr>
	  	<td><a href="{{ item.url | relative_url }}">{{item.title}}</a></td>
	  	<td>{{item.titleEn}}</td>
	  </tr>
	{%- endfor -%}
    </tbody>
</table>