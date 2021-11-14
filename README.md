# Getting Started

-   Install docker
-   `cd ./WebApp`
-   `docker build -t game-listing-app .`
-   `docker-compose -f stack.yml up`

This will run the Api and the database to store the data
The Api will be exposed on port 5000

-   You can navigate to http://localhost:5000/listings/reset-data to reset the listing with the test data
-   You can also navigate to http://localhost:5000/listings in order to see all the lisitings

Challenges encountered

-   Debugging why the images weren't being displayed
    The main challenge was to understand the project, the components and the tools that were used to display the panels
    I ended up fixing the image directly in the UIPanel in order to only load the listing when we render the UIPanel,
    that would work well with a carousel with infinite scrolling (load the next page when scroll position is near the end)
