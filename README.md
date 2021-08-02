# Levo-CodeChallenge

**Video Demo**
https://user-images.githubusercontent.com/6778416/127822831-43b1aaee-b188-429c-b7af-216f9a302445.mp4

**Requirements **
User:
1.Register
2.Login/Logout
3.CRUD Operations
4.Web Api implementation
5.Entity framework implementation
6.View all users items unsigned/ anonymous user
7.Only registered user can perform CRUD on their own items
8.No one user can update other user's items

The above video briefly showcases the ability of the web app

The solution have 2 projects

1. Web App 
2. Web Api

Web App: Todo List
- Asp.Net Core MVC framework
- Authentication -Microsoft Identity Invidiual Accounts Authentication and Authorization
- Default DI 
- Http calls to web api in order to perform any CRUD operations
- Todo menu item enabled only for a registered and confirmed user
- CRUD operations only for logged in users 
- Loggin users can create and perform UD operations only to their created todo items
- On the home page an user anonymous or signed in can view all other users todos including theirs

Web Api:
- Asp.Net Core Web API
- Entity core code first contexts
- Default DI
- Enabled form based post call with json as content type

Below is the screen shot of the enabled enpoints via webapi
![swagger](https://user-images.githubusercontent.com/6778416/127824521-eaef8a6c-a715-4f47-99af-1685976e7f0a.png)






