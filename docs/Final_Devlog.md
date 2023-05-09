# Dev Log

## Crops
Crops each have an individual class because each plant has a seperate sprite for DayOne - DaySix, 
having a garden manager handel that responsibility fully would be a mess of a switch case.

Free plot was made in responce to an bug in which I was unable to plant a plant that was previously in the garden

Since initally I was using the same instance of a crop when planting a new one, even reseting the values would cause issues

I ended up adjusting how plants are planted, allowing a new plant to be created to avoid the issue
of being unable to have multiple of the same plant in the field.

## Interfaces
The interfaces were made by me asking what each plant could have done to it.

Each plant could be fertilized, harvested, watered, and caused to grow.

It could be questioned how important they are since there is not another object that utilizes 
the interfaces, but I still feel they are utilized and implimented properly.
 
## Windows
Overall I am happy with how these are designed. 

Though I am certin there is room to move more repeate code from the windows into the base window class
I feel they are build up pretty well. 

In adition having the Window Managers handel the opening and closing of the windows feels correct.

## Player
I feel the player is a bit overkill, but since I wanted to use the IPlayerController from MonogameLibrary
I based my player off of the one in the Library.

I feel it could have been done with less classes, but I don't think how it is currently is destinctly bad

## Animation Manager
I intially intended and drew out more animations, but only ended up implimenting the watering.

Though I feel the manager for one animation feels overboard, I feel it is well designed for when I add
more in the future. And I feel that it is better that a seprate manager handle animations over them 
being in Game Manager

## Garden Manager
There is a lot of code that danced between this class and other like Plant. 

I feel content with the responsibility it has now, and do feel a Garden Manager was necessary

For awhile I had a Plant Manager that was going to handel all of the plants, but as I thought about it further
it was better to seperate the responsibility between the plant classes as expressed above.

## Grid Manager
Getting the grid working how I wanted took time, but overall I am happy with where it is now

The only element I don't like is the set ratio, and how on some screens the grid dosn't fit

Initally I build the grid to fill dynamically depending on screen size, but once I realized to
utilize a multidimensional array as a terrian guide to change the texture to the terrain I had in mind
I had to limit myself to a set grid.

in the future I may look into scalling the elements depending on screen size, but that seems like a mess.

Probally just hard setting the window size would be easier.

Terrian is not utilized exactly how I had in mind, but The grid was built with grid squares, and the
interaction and character reach was essnetial, so switching over to Terrain fully was not an option.

## Shop Manager && Stats Manager && WinManager

These simply handel the opening and closing of their respective windows, or better expressed the creation,
and removal of the windows.

I feel seperating the responsability is important and that it shouldn't be the games responsibility

## Game Manager
I am sure there are arguments for was to better and further seperate Game Manager

I needed a central hub for the game to run and for the differnt classes to be able to get info about
the others without creating a new value.

There could be arguments for a hotbar manager but it did not feel nesacary, and I think it is alright
for the game to manage to hotbar, but it could be seperated,

ItemTexture is not a value I love it is just what needed to be done to allow plants to be added 
to the hot bar without their grapics changing. Though since I later fixed the issue that was causing
that I'm sure it could be removed

I ended up not allow plants to stack because it was causing issue with Quality since I was not
calculating in the plant that was about to increase the current plant in the inventory had a higher qualtity.
I'm sure it could be fixed but I think it is fine that crops don't stack.

There is an issue where if there is one see in the inventory and a fertilizer in the next once
the seed is planted the fertilizer is the used since it moves to the seeds spot and is now selected. 
This is a slight bug but I do not feel it is a big deal.
