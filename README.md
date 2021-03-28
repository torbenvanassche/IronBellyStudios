# Pooling system
Using a `Queue` I can easily make sure that the object I take from the pool is the oldest, this makers it lesl likely the object is on screen.
The `ObjectPooler` holds all the different item pools (this is a Singleton). The `Spawner`can be placed on any object and will pull from the pool.
Multiple pools are disallowed by the singleton, but multiple spawners can pull from the same pool. 
If the initial amount exceeds the size of the pool it will be increased dynamically (startup & runtime)

# FindNearestNeighbour
using `OnDrawGizmos` to draw the lines in the editor, I have ordered the list of gameobjects in the scene based on distance relative to each object.
As long as the object is part of the pool it will be checked, objects not part of the pool are ignored.

# 3D Randomizer
I took the liberty of providing 3 `Vector2` parameters instead of floats, to give the greatest array of freedom to define the constraining area. The settings for this are available on the prefab instance
(Note that the area is defined by the pivot of the objects, not taking into account the size of the object and constraining it as such.)
