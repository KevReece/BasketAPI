Basket API
===

Description
---
This is a demo API, demonstrating a basic in-memory basket, with: 
- .Net Core WebApi
- continuous integration and deployment (using CircleCI)
- cloud-based container hosting (using Docker and AWS ECS)
- live API documentation (using Swagger)
- auto-generated API client (using NSwag)
- client usage specification (using SpecFlow)

Usage
---
- The latest client NuGet package can be found in the build artifacts here: https://circleci.com/gh/KevReece/BasketAPI
  - select the most recent green 'client-publish' build, then select 'Artifacts'
  - download the displayed .nupkg artifact, and add it as a NuGet source for your project
- You can use the SwaggerUI to get detail on the API interface: http://ec2co-ecsel-1xleu2ezu8ts0-2052958683.eu-central-1.elb.amazonaws.com/swagger/index.html
- The specs show a working example of how to use the basket api client: https://github.com/KevReece/BasketAPI/blob/master/src/BasketApi.Specs/Basket.feature

Assumptions
---
- Client integration is made using NuGet client packages
- API security is out of scope
- Database integration is out of scope
- Scaling can only be added after a database is added
- Handling concurrent requests to in memory basket aren't in scope (this is best handled during database integration)

Development
---
### Local setup
- Install NSwag to path: https://github.com/RSuter/NSwag/wiki/NSwagStudio
- Install SpecFlow as Visual Studio extension (note: special installation as the .Net Core version is currently only in preview: https://specflow.org/2018/specflow-3-public-preview-now-available/)
### First deployment setup
- Create AWS account
- Update aws_account_id and aws_default_region in .circleci/config.yml (for build-api-image and deploy-api)
- Create AWS CircleCI user (with ECR and ECS permissions), and add to CircleCI project "AWS permissions"
- Create AWS ECR named 'basketapi'
- Build/tag/push an example image into the new ECR
- Create AWS ECS using AWS ECS "get started". Setup as: 
  - container 'basketapi', using a 'basketapi' ECR image and port 80 mapping
  - service 'basketapi-service', with load balancer
  - task definition 'basketapi-task-definition'
  - cluster 'basketapi-cluster' 
### Guidelines
- Check https://circleci.com/gh/KevReece/BasketAPI after check ins, to ensure your commits are green
- Run the client project 'generateClientFromLocalhostApi.cmd' after making changes to the API interface, to ensure the client is up to date
- Ensure SpecFlow tests are run locally before committing (the SpecFlow 3 preview currently has an active issue with running in CI: https://github.com/techtalk/SpecFlow/issues/1305)
### TODO
- Infrastructure CD
