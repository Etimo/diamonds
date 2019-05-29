# Game engine

Read more about the structure of the game engine [here](./src/game-engine/)

Run game engine example locally with live reload:
```
yarn run start:game-engine:dev
```

# Api

Example api [here](./src/api) using [NestJs](https://nestjs.com/) (basically "Angular for backends"). Also implements basic swagger.

Run api locally with live reload:
```
yarn run start:api:dev
```

When started, api is available at `localhost:3000`. Swagger is available at `localhost:3000/docs`.

# General

How to do stuff:
* `yarn run test` - Run tests
* `yarn run test:cov` - Run tests with coverage report
* `yarn run build` - Compile and build to `/dist` folder

[Jest](https://jestjs.io/) is used for testing.

Prettier is used for formatting and runs on every commit for the staged parts using https://prettier.io/docs/en/precommit.html#option-4-precise-commits-https-githubcom-jameshenry-precise-commits

