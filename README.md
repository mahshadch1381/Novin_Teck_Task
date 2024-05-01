In this project, I tried to save  users' information  in database(mariaDb) and with dapper orm and get their information.
In one of the APIs that returns the user according to the user ID, I also used the cache (InmemeoryCache) and put the users in the cache and checked the cache first.
I also used RabbitMQ in such a way that every user that was added, I put their information in the queue and there is also a service that constantly checks the queue and finds its messages and prints them on the console.
My goal was that, for example, whenever this service sees a message in the queue, it receives that message and sends a welcome email to someone who has just entered.
I also implemented an API for uploading the file in the storage (s3), but after many attempts, it was not fixed and my selected file was not saved correctly.
For this problem i got error with 500 status code, that it didnt know my region and bucket.I t seems my problem is about my informatiom thet i gave when i tried to signup in aws.
