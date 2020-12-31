# The Sheppard's Chocolate Box Story

Just before Christmas, Bill decided to set up a record:

Request:
* Endpoint: `api/createfamily`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "chocolateBoxSize": 1
}
```

Response:

`Family created.`

Bill then made sure to get his requests in first before letting the rest of the family know about the idea.

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Bill",
    "chocolateName": "Milk cube",
    "quantity": 2
}
```

Response:

`Your reservation has been made.`

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Bill",
    "chocolateName": "Kathmandu",
    "quantity": 2
}
```

Response:

`Your reservation has been made.`

Bill then let the rest of the family know that they could also reserve their favourite chocolates.

Along came Ann, and she to liked the Kathmandu chocolates.

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Ann",
    "chocolateName": "Kathmandu",
    "quantity": 2
}
```

Response:

`Your reservation has been made.`

So did Julia as well. Unknown to her though, there was only one remaining.

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Julia",
    "chocolateName": "Kathmandu",
    "quantity": 2
}
```

Response:

`All of the Kathmandu chocolates where almost gone. You've reserved 1.`

So she reserved another type as well.

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Julia",
    "chocolateName": "Delhi",
    "quantity": 2
}
```

Response:

`Your reservation has been made.`

Then along came Johnny, he loved chocolates......

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Johnny",
    "chocolateName": "Kathmandu",
    "quantity": 2
}
```

Response:

`Sorry, all of the Kathmandu chocolates are reserved.`

Johnny wasn't impressed by this. So he tried to make sure he would not go without.

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Johnny",
    "chocolateName": "Milk cube",
    "quantity": 2
}
```

Response:

`Your reservation has been made.`

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Johnny",
    "chocolateName": "Discreet",
    "quantity": 1
}
```

Response:

`Your reservation has been made.`

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Johnny",
    "chocolateName": "Ambanje",
    "quantity": 2
}
```

Response:

`Your reservation has been made.`

Johnny loves his chocolate, so he kept, going..... Or did he!

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Johnny",
    "chocolateName": "Discreet",
    "quantity": 2
}
```

Response:

`You've been caught with your hand on the chocolate box multiple times! You've reached your three reservations.`

Bill also decided that he wanted another Milk cube. He to was caught by the Azure Functions trying to keep things fair.

Request:
* Endpoint: `api/reserve`
* Verb: `POST`
* Body:
```json
{
    "familyName": "Sheppard",
    "name": "Bill",
    "chocolateName": "Milk cube",
    "quantity": 2
}
```

Response:

`You really like the Milk cube chocolates. You can't reserve them more than once.`

So, Bill decided to look at how things were going.

Request:
* Endpoint: `api/getfamily`
* Verb: `POST`
* Body:
```json
{
  "familyName": "Sheppard"
}
```

Response:
```json
{
    "familyName": "Sheppard",
    "reservations": [
        {
            "name": "Bill",
            "chocolateName": "Milk cube",
            "quantity": 2,
            "reservedDateTime": "2020-12-31T18:00:18.26645Z"
        },
        {
            "name": "Bill",
            "chocolateName": "Kathmandu",
            "quantity": 2,
            "reservedDateTime": "2020-12-31T18:00:28.761739Z"
        },
        {
            "name": "Ann",
            "chocolateName": "Kathmandu",
            "quantity": 2,
            "reservedDateTime": "2020-12-31T18:00:36.914078Z"
        },
        {
            "name": "Julia",
            "chocolateName": "Kathmandu",
            "quantity": 1,
            "reservedDateTime": "2020-12-31T18:00:46.687895Z"
        },
        {
            "name": "Julia",
            "chocolateName": "Delhi",
            "quantity": 2,
            "reservedDateTime": "2020-12-31T18:00:59.277259Z"
        },
        {
            "name": "Johnny",
            "chocolateName": "Milk cube",
            "quantity": 2,
            "reservedDateTime": "2020-12-31T18:01:23.504503Z"
        },
        {
            "name": "Johnny",
            "chocolateName": "Discreet",
            "quantity": 1,
            "reservedDateTime": "2020-12-31T18:01:33.506703Z"
        },
        {
            "name": "Johnny",
            "chocolateName": "Ambanje",
            "quantity": 2,
            "reservedDateTime": "2020-12-31T18:01:45.735408Z"
        }
    ],
    "chocolateBox": {
        "chocolateBoxSize": 1,
        "chocolates": [
            {
                "name": "Ambanje",
                "quantity": 3
            },
            {
                "name": "Delhi",
                "quantity": 3
            },
            {
                "name": "Discreet",
                "quantity": 4
            },
            {
                "name": "Kathmandu",
                "quantity": 0
            },
            {
                "name": "Milk cube",
                "quantity": 1
            }
        ],
        "remainingChocolates": 11
    }
}
```
