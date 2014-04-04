function createObject(%this, %pos, %size, %image, %shape)
{
	//Create the scene's object
	%sprite = new Sprite()
	{
		Image = %image;
		//class = "tree";
		position = %pos;
		size = %size;
		SceneLayer = "15";
		SceneGroup = "15";
		CollisionCallback = true;
	};
	
	//Set the mask shape
	switch$ ( %shape )
	{
		case "circle":
			%sprite.createCircleCollisionShape(%size/2);
			
		case "square":
			%sprite.createPolygonBoxCollisionShape(%size, %size);
	}
	
	%sprite.SetBodyType(static);
	%sprite.setCircleCollisionLocalPosition("0 10");
	%sprite.setCollisionShapeIsSensor(0, false);
	%sprite.setCollisionGroups( "15");
	
	//Add to the scene
	mainScene.add( %sprite );
}

function createTile(%this, %image, %size, %xarea, %yarea)
{
	//Tiles
	%halfXArea = %xArea/2;
	%halfYArea = %yarea/2;
	for(%x = -%halfXArea; %x < %halfXArea; %x++)
	{
		for(%y = -%halfYArea; %y < %halfYArea; %y++)
		{
			%posX = %x * %size;
			%posY = %y * %size;
			
			%tile = new Sprite()
			{
				position = %posX SPC %posY;
				image = %image;
				size = %size;
			};
			%tile.setBodyType( "static" );
			%tile.setCollisionSuppress();
			%tile.setAwake( false );
			%tile.setActive( false );
			%tile.setSceneLayer(30);
			mainScene.add( %tile );
			
		}
	}
}
