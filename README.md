# profitclicks-app REST API
опубликовано на Azure
https://profitclicksapi.azurewebsites.net

# Frontend приложения
https://github.com/nicon-83/profitclicks-app-front.git

# База данных Postgresql на хостинге ElephantSQL
https://customer.elephantsql.com

## GET Получить все контакты 
```
https://profitclicksapi.azurewebsites.net/api/user
```

### GET Получить контакт по id 
```
https://profitclicksapi.azurewebsites.net/api/user/id
```

### POST Добавить новый контакт 
```
https://profitclicksapi.azurewebsites.net/api/user
request body application/json
{
	"FirstName":"Иван",
	"LastName":"Иванов",
	"MiddleName":"Иванович"
}
```

### POST Обновить контакт по id
```
https://profitclicksapi.azurewebsites.net/api/user/id
request body application/json
{
	"FirstName":"Иван",
	"LastName":"Иванов",
	"MiddleName":"Иванович"
}
```

### DELETE Удалить контакт по id 
```
https://profitclicksapi.azurewebsites.net/api/user/id
```

### POST Добавить контакту новый номер 
```
https://profitclicksapi.azurewebsites.net/api/phone
request body application/json
{
	"UserId":"6",
	"Number":"654-455-977"
}
```

### DELETE Удалить номер у контакта
```
https://profitclicksapi.azurewebsites.net/api/phone/id
```


