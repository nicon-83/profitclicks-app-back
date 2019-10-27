# profitclicks-app REST API
опубликовано на Heroku
https://profit-clicks.herokuapp.com/

# Frontend приложения
https://github.com/nicon-83/profitclicks-app-front.git

# База данных Postgresql на хостинге ElephantSQL
https://customer.elephantsql.com

### GET Получить все контакты 
```
https://profit-clicks.herokuapp.com/api/user
```

### GET Получить контакт по id 
```
https://profit-clicks.herokuapp.com/api/user/id
```

### POST Добавить новый контакт 
```
https://profit-clicks.herokuapp.com/api/user
request body application/json
{
	"FirstName":"Иван",
	"LastName":"Иванов",
	"MiddleName":"Иванович"
}
```

### PUT Обновить контакт по id
```
https://profit-clicks.herokuapp.com/api/user/id
request body application/json
{
	"FirstName":"Иван",
	"LastName":"Иванов",
	"MiddleName":"Иванович"
}
```

### DELETE Удалить контакт по id 
```
https://profit-clicks.herokuapp.com/api/user/id
```

### POST Добавить новый номер контакту
```
https://profit-clicks.herokuapp.com/api/phone
request body application/json
{
	"UserId":"6",
	"Number":"654-455-977"
}
```

### DELETE Удалить номер по его id
```
https://profit-clicks.herokuapp.com/api/phone/id
```


