# Unity-Simple-Water-Buoyancy
This is a very simple Water Buoyancy C# Script for Unity3D Game Engine,
## How to setup

   -Add box Collider on Water Body and set it as Trigger.
   -Also Add "WaterBody" component On it (optionally you can change surface level property if needed)
   -Give water object Tag as "Water" (or you can change "waterVolumeTag" property accordingly)
   -Add "Buoyancy" component on a Rigidbody Object that you want to float on WaterBody.



## What this Dose

   -Rigidbody with "Buoyency" Component floats on "Water" Tagged body.
   -Check if Rigidbody is inside X - Z bound of Water body and gives force on Y Upwards.
   -Buoyant force (Upwards force) increases as rigibody dive deep underwater. (can be variable by depthPowerLimit property)



## Limitations

   -This is clearly NOT real world Physics, just simple Up force.
   -No additional forces or drag or waves.
   -As this component is limited to Y axis buoyancy, make sure your Y is up, however feel free to modify.
   -Other collider types will work as long as water surface level stay flat, as this is designed on checking collider bounding box.
   -Rigidbody never fall Asleep if inside water body.
   -Don't stack water Bodies on top or duplicate on same location.

![Unity-Simple-Water-Buoyancy](https://i.imgur.com/6N57ycL.gif)
