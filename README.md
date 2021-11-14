# Getting Started

-   Install docker
-   `cd ./WebApp`
-   `docker build -t game-listing-app .`
-   `docker-compose -f stack.yml up`

This will run the Api and the database to store the data
The Api will be exposed on port 5000

-   You can access the Spa (Single Page Application) at http://localhost:5000
-   You can navigate to http://localhost:5000/listings/reset-data to reset the listing with the test data
-   You can also navigate to http://localhost:5000/listings in order to see all the listings
-   You can run the UnityApp by opening the available scene and running it, the `TestListings.cs` in `UnityApp/Assets/Scripts/Test/` should already be configured to target the api hosted at http://localhost:5000

## Challenges encountered

-   Debugging why the images weren't being displayed.\
    The main challenge was to understand the project, the components and the tools that were used to display the panels.\
    I ended up fixing the image directly in the UIPanel in order to only load the listing when we render the UIPanel,
    that would work well with a carousel with infinite scrolling (load the next page when scroll position is near the end).

-   Hooking up everything nicely.\
    Configure the project to serve the Spa, then make sure the dockerfile can properly build the image.

## Possible Improvements

-   Add pagination to the listings endpoints in order to make it scalable.\
    In the UnityApp, the carousel needs to be updated so it can fetch the next page when the scrolling nears the end.\
    It also doesn't need to render all the panels at once, only the visible one with a defined margin (i.e. 10 panels after and before).
