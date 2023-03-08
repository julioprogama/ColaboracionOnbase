### Consultas

	Descripcion, tabla , campos : corresponde a ....
	

- Consulta de campos asignados a un unity form; la tabla hsi.uffield, filtramos por los campos ufformnum este corresponde a , formrevnum este corresponde a 
		
	```SQL
		select * from hsi.uffield where ufformnum = 103 and formrevnum = 382;
	```
- KW asociados a un unity form, basado en los campos asignados en un unity form
	```SQL
		select * from hsi.keytypetable where keytypenum in 
		(select fieldsourceparam from hsi.uffield where ufformnum = 103 and formrevnum = 382) order by 1;
	```
- Consulta de unity form
	```SQL
		select * from hsi.ufform where ufformnum in (103) order by 1 desc;

- Consulta de revisiones de un unity form
	```SQL
		select * from hsi.ufformrev where ufformnum = 103 order by 2 DESC;
	```
- Consulta para verifiar la instancia o revisón del unity form con la que se almaceno un documento en OnBase
	```SQL
		select * from hsi.ufforminstance where ufformnum in (103) order by 1 desc;

- Consultar maximo de KW autonumericos
	```SQL
		select * from hsi.usermaxnumkeys;
	```
- Consultar KW'S asociados a un document type
	```SQL
		select k.keytype, kk.* 
		from hsi.itemtypexkeyword kk INNER JOIN
		hsi.keytypetable k on k.keytypenum = kk.keytypenum
		where itemtypenum=603 order by 1;
	```
- count of all duplicate Request IDs
	```SQL
		select ki.keyvaluesmall, count(ki.keyvaluesmall) "Count1" 
		from hsi.keyitem206 ki join hsi.itemdata id on ki.itemnum = id.itemnum
		group by ki.keyvaluesmall 
		having count(ki.keyvaluesmall) > 1 
		order by ki.keyvaluesmall desc;
	```
