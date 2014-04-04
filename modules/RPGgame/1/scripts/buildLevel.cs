//-----------------------------------------------------------------------------
function buildLevelOne()
{
	createObject($this, "20 20", 20, "RPGAssets:tree1" , "circle" );
	createObject($this, "-20 20", 40, "RPGAssets:houseRed1" , "square" );
	createTile($this, "RPGAssets:ground", 10, 15, 15);
	createTile($this, "RPGAssets:grass", 5, 5, 5);
}


function buildAquarium(%scene)
{
    // A pre-built aquarium of size 100x75, with blue water, some haze, and some nice rocks.
    // Triggers will be provide around the edges to let the developer know when objects in the
    // aquarium have reached the edges.

    // Background
    %background = new Sprite();
    %background.setBodyType( "static" );
    %background.setImage( "TropicalAssets:background" );
    %background.setSize( 100, 75 );
    %background.setCollisionSuppress();
    %background.setAwake( false );
    %background.setActive( false );
    %background.setSceneLayer(30);
    %scene.add( %background );
    
    
    
    // Far rocks
    %farRocks = new Sprite();
    %farRocks.setBodyType( "static" );
    %farRocks.setPosition( 0, -7.5 );
    %farRocks.setImage( "TropicalAssets:rocksfar" );
    %farRocks.setSize( 100, 75 );
    %farRocks.setCollisionSuppress();
    %farRocks.setAwake( false );
    %farRocks.setActive( false );
    %farRocks.setSceneLayer(20);
    %scene.add( %farRocks );
    
    // Near rocks
    %nearRocks = new Sprite();
    %nearRocks.setBodyType( "static" );
    %nearRocks.setPosition( 0, -8.5 );
    %nearRocks.setImage( "TropicalAssets:rocksnear" );
    %nearRocks.setSize( 100, 75 );
    %nearRocks.setCollisionSuppress();
    %nearRocks.setAwake( false );
    %nearRocks.setActive( false );
    %nearRocks.setSceneLayer(10);
    %scene.add( %nearRocks );
    
    addAquariumBoundaries( %scene, 100, 75 );
}

//-----------------------------------------------------------------------------

function addAquariumBoundaries(%scene, %width, %height)
{
    // add boundaries on all sides of the aquarium a bit outside of the border of the tank.
    // The triggers allow for onCollision to be sent to any fish or other object that touches the edges.
    // The triggers are far enough outside the tank so that objects will most likely be just out of view
    // before they are sent the onCollision callback.  This way will they can adjust "off stage".

    // Calculate a width and height to use for the bounds.
    // They should be bigger than the aquarium itself.
    %wrapWidth = %width * 1;
    %wrapHeight = %height * 1;

    %scene.add( createOneAquariumBoundary( "left",   -%wrapWidth/2 SPC 0,  5 SPC %wrapHeight) );
    %scene.add( createOneAquariumBoundary( "right",  %wrapWidth/2 SPC 0,   5 SPC %wrapHeight) );
    %scene.add( createOneAquariumBoundary( "top",    0 SPC -%wrapHeight/2, %wrapWidth SPC 5 ) );
    %scene.add( createOneAquariumBoundary( "bottom", 0 SPC %wrapHeight/2,  %wrapWidth SPC 5 ) );
}

//-----------------------------------------------------------------------------

function createOneAquariumBoundary(%side, %position, %size)
{
    %boundary = new SceneObject() { class = "AquariumBoundary"; };
    
    %boundary.setSize( %size );
    %boundary.side = %side;
    %boundary.setPosition( %position );
    %boundary.setSceneLayer( 1 );
    %boundary.createPolygonBoxCollisionShape( %size );
    // the objects that collide with us should handle any callbacks.
    // remember to set those scene objects to collide with scene group 15 (which is our group)!
    %boundary.setSceneGroup( 15 );
    %boundary.setCollisionCallback(false);
    %boundary.setBodyType( "static" );
    %boundary.setCollisionShapeIsSensor(0, false);
    return %boundary;
}

//-----------------------------------------------------------------------------


//-----------------------------------------------------------------------------
