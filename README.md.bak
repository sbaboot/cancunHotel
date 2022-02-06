## Alten Challenge 

### Context
Post-Covid scenario:
People are now free to travel everywhere but because of the pandemic, a lot of hotels went
bankrupt. Some former famous travel places are left with only one hotel.
You’ve been given the responsibility to develop a booking API for the very last hotel in Cancun.
The requirements are:
- API will be maintained by the hotel’s IT department.
- As it’s the very last hotel, the quality of service must be 99.99 to 100% => no downtime
- For the purpose of the test, we assume the hotel has only one room available
- To give a chance to everyone to book the room, the stay can’t be longer than 3 days and
can’t be reserved more than 30 days in advance.
- All reservations start at least the next day of booking,
- To simplify the use case, a “DAY’ in the hotel room starts from 00:00 to 23:59:59.
- Every end-user can check the room availability, place a reservation, cancel it or modify it.
- To simplify the API is insecure.

## Technologies
* [.NET Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1)
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [MariaDb](https://mariadb.org/)
* [Swagger](https://swagger.io/solutions/api-documentation/)
* [AWS - RDS](https://aws.amazon.com/fr/rds/)
* [Postman](https://www.postman.com/downloads/)
* [HeidiSQL](https://www.heidisql.com/download.php)

### Project information
APi developed with multiple projects in order to separate responsabilites of each component.

I used mariadb database hosted on Amazon Web Services (AWS) thanks to RDS Services. Acces seems to be allowed to everyone.If you can't access the API, don't hesitate to contact me {new issue](https://github.com/sbaboot/cancunHotel/issues/new)
I we want to go deeper, you can find the [diagram class](https://github.com/sbaboot/cancunHotel/blob/master/Cancun%20diagram%20class.pdf)

### Prerequisites
To be able to run the project in your machine, you need to consider the following and mandatory requisites:

You need to launch the web app api and use postman to test endpoints. Swagger can be used to.
if you want to access mariadb database you can use HeidiSQL
host: *cancunhotel.czfxewceanxd.eu-west-3.rds.amazonaws.com*
user: *admin*
password: *Toulouse31+*
port: *3306*


### Postman 
`GET` **/api/reservation - Get all reservations

`GET` **/api/reservation/1 - Get a detail reservation

`POST` **/api/reservation - Save a reservation (add or update if id != 0):

        * Request body:
        ```json
		{    
			"reservationId": 0,
			"roomId": 1,
			"clientEmail": "user@mymail.com",
			"clientName": "user name",
			"fromDate": "2022-02-07T12:46:55",
			"toDate": "2022-02-07T19:50:40"
		}
        ```
		
`DELETE` **/api/reservation/1 - Delete detail reservation

`GET` **/api/room/availability - Display room information and available dates to book

### Improvement
if I had more time, I would :
* protect database access
* add unit test
* add new class to manage exceptions
