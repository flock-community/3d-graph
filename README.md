# 3D dependency graph

This project imports file and shows them as a 3d graph.

## Installing

Run `npm install`.

## Running the app

Run `npm start`. Navigate in your browser to 'http://localhost:4200/dashboard'.

## File formats

The json in 'example/test.json' shows the format of the graph to import.

To automatically color the graph, a color scheme file can be provided, such as in 'example/test-color-scheme.json'. This (partially) matches the keys to the node ids to obtain the specified color. Add a 'default' key to color all the nodes not matching any of the other keys.
