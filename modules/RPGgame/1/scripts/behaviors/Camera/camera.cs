if (!isObject(CameraBehavior))
{
	%template = new BehaviorTemplate(CameraBehavior);
	
	%template.friendlyName = "Camera Follow";
	%template.behaviorType = "Camera";
	%template.description = "Camera follows the main character";
	
	%template.addBehaviorField(updateRate, "milliseconds between timer-based point loss", int, 1000);
}

function CameraBehavior::onBehaviorAdd(%this)
{
	//Store our size on start-up as our "normal" size.
	//We will shrink or grow relative to this.
	%this.schedule(%this.updateRate, "moveCam");
}

function CameraBehavior::moveCam(%this)
{	
	mainWindow.setCameraPosition( 0, 0 );
	%this.schedule(%this.burnRate, "lowerLife");
}
