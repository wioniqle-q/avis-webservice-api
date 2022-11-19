# 🚧 Avis Webservice API

[![CircleCI](https://dl.circleci.com/status-badge/img/gh/Noirrs/getStructure/tree/circleci-project-setup.svg?style=shield)](https://dl.circleci.com/status-badge/redirect/gh/Noirrs/getStructure/tree/circleci-project-setup)

### 🌀 Avis Webservice is a powerfull API where you can find everything that you search made with ASP.NET

 Still on the proccess, if you want announce a problem, you can create an issue on Github

# ⚡Quick API Docs

**Example Request:**  

```js 
POST 
avis.com/api/v1/account/{parameters}

{
	body: {
		"Name": "testuser",
		"Password": "testuserpassword"
	}
}

* API endpoint: api.avis.com/

* Name: Your account's username
* Password: Your account's password
```

**Example Response:** 


```js
* Response Type: Object | Error<typless>

{
  success: true,
  error: null
}
```

# ✍️ Source Code

## Tech Stacks
 
 - [x] DotNet 7.0
 - [x] ASP.NET
 - [x] MongoDB
 - [x] CQRS
 
## Features
  
 ### Account
 - [x] Create an account
 - [x] Edit your account
 - [x] Deactive/Active your account
 
 ### User
 - [x] Login Account

## Resources

C#: https://learn.microsoft.com/en-us/dotnet/csharp/

DotNet: https://dotnet.microsoft.com/en-us/

AspNet: https://dotnet.microsoft.com/en-us/apps/aspnet

MongoDB: https://www.mongodb.com/




## Running the app

```bash
# development
$ dotnet run 
```


# 📱 Contact

<div align="center">
<a href="https://github.com/wioniqle-q" target="_blank"><img src="https://img.shields.io/badge/wioniqle-backend developer%20-191717.svg?&style=for-the-badge&logo=github&logoColor=white"></a>
<a href="https://github.com/Noirrs" target="_blank"><img src="https://img.shields.io/badge/Noirrs-backend developer%20-191717.svg?&style=for-the-badge&logo=github&logoColor=white"></a>
</div>
<div align="center">
<a href="https://discord.com/users/790018895847096380" target="_blank"><img src="https://shields.io/badge/Wioniqle-111111.svg?&style=for-the-badge&logo=discord"></a>
<a href="https://discord.com/users/922078187788308510" target="_blank"><img src="https://shields.io/badge/Noir-111111.svg?&style=for-the-badge&logo=discord"></a>
  </div>




