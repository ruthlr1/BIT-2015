<?php 
	header("content-type: application/json");
	$f = fopen("http://api.bandsintown.com/events/on_sale_soon.json?app_id=1","r");
	while(!feof($f))
	{
		echo (fread($f,1024));
	}
	fclose($f);

?>