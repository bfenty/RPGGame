//RPG Controls Behavior

if (!isObject(RPGControlsBehavior))
{
    %template = new BehaviorTemplate(RPGControlsBehavior);

    %template.friendlyName = "RPG Controls";
    %template.behaviorType = "Input";
    %template.description  = "Top-Down movement control";

    %template.addBehaviorField(upKey, "Key to bind to upward movement", keybind, "keyboard up");
    %template.addBehaviorField(downKey, "Key to bind to downward movement", keybind, "keyboard down");
    %template.addBehaviorField(leftKey, "Key to bind to left movement", keybind, "keyboard left");
    %template.addBehaviorField(rightKey, "Key to bind to right movement", keybind, "keyboard right");
    
    //Set movement speed
    %speed = 20;
    %template.addBehaviorField(verticalSpeed, "Speed when moving vertically", float, %speed);
    %template.addBehaviorField(horizontalSpeed, "Speed when moving horizontally", float, %speed);
    
    //%this.owner.setAngularDamping(100);
}

function RPGControlsBehavior::onBehaviorAdd(%this)
{
    if (!isObject(GlobalActionMap))
       return;

	%this.owner.enableUpdateCallback();

    GlobalActionMap.bindObj(getWord(%this.upKey, 0), getWord(%this.upKey, 1), "moveUp", %this);
    GlobalActionMap.bindObj(getWord(%this.downKey, 0), getWord(%this.downKey, 1), "moveDown", %this);
    GlobalActionMap.bindObj(getWord(%this.leftKey, 0), getWord(%this.leftKey, 1), "moveLeft", %this);
    GlobalActionMap.bindObj(getWord(%this.rightKey, 0), getWord(%this.rightKey, 1), "moveRight", %this);

    %this.up = 0;
    %this.down = 0;
    %this.left = 0;
    %this.right = 0;
}

function RPGControlsBehavior::onBehaviorRemove(%this)
{
    if (!isObject(GlobalActionMap))
       return;

    %this.owner.disableUpdateCallback();

    GlobalActionMap.unbindObj(getWord(%this.upKey, 0), getWord(%this.upKey, 1), %this);
    GlobalActionMap.unbindObj(getWord(%this.downKey, 0), getWord(%this.downKey, 1), %this);
    GlobalActionMap.unbindObj(getWord(%this.leftKey, 0), getWord(%this.leftKey, 1), %this);
    GlobalActionMap.unbindObj(getWord(%this.rightKey, 0), getWord(%this.rightKey, 1), %this);

    %this.up = 0;
    %this.down = 0;
    %this.left = 0;
    %this.right = 0;
}

function RPGControlsBehavior::updateMovement(%this)
{
	//Move
	%this.owner.setLinearVelocityX((%this.right - %this.left) * %this.horizontalSpeed);
	%this.owner.setLinearVelocityY((%this.up - %this.down) * %this.verticalSpeed);
	%this.updateAnimation(%this, %this.owner.heading);
	
	if(%this.right - %this.left == 0 && %this.up - %this.down == 0)
	{   	
		%this.owner.anim = "RPGAssets:heroFrontStandAnim";
		%this.owner.PlayAnimation(%this.owner.anim); 
	}
}

function RPGControlsBehavior::updateAnimation(%this, %direction)
{
	echo("@@@Update ANIMATION" SPC %direction);
	//If not moving, stop animation
		//determine direction
		switch$ ( %direction )
		{
			case "north":
				%this.owner.anim = "RPGAssets:heroBackAnim";
				%this.owner.PlayAnimation(%this.owner.anim);
			case "south":
				%this.owner.anim = "RPGAssets:heroFrontAnim";
				%this.owner.PlayAnimation(%this.owner.anim);
			case "east":
				%this.owner.anim = "RPGAssets:heroSideAnim";
				%this.owner.PlayAnimation(%this.owner.anim);
			case "west":
				%this.owner.anim = "RPGAssets:heroSideAnim";
				%this.owner.PlayAnimation(%this.owner.anim);
				%flip = %this.right - %this.left < 0; //turn the sprite around
				%this.owner.setFlipX(%flip);
		}
}

function RPGControlsBehavior::moveUp(%this, %val)
{
	%this.up = %val;
	%this.owner.heading = "north";
	%this.updateMovement();
}

function RPGControlsBehavior::moveDown(%this, %val)
{
	%this.down = %val;
	%this.owner.heading = "south";
	%this.updateMovement();
}

function RPGControlsBehavior::moveLeft(%this, %val)
{
	%this.left = %val;
	%this.owner.heading = "west";
	%this.updateMovement();
}

function RPGControlsBehavior::moveRight(%this, %val)
{
	%this.right = %val;
	%this.owner.heading = "east";
	%this.updateMovement();
}

function RPGControlsBehavior::onCollision(%this, %object, %collisionDetails)
{
	//I'll put something here
}
