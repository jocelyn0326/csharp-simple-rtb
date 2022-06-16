# Intowow Assignment- 郭宜萱(jocelyn0326@gmail.com)


## Summary
Implement two http servers: an exchange server and a bidder according to Intowow’s simplified version RTB protocol.

Currently I have completed two APIs on both servers: init_session and end_session.

## Environment
[.NET 5](https://docs.microsoft.com/zh-tw/dotnet/core/whats-new/dotnet-5)

[Docker](https://www.docker.com/)

## How to run the project?
1. Open [Docker Desktop](https://www.docker.com/products/docker-desktop/)
2. cd to current project  which contains **docker-compose.yml**
3. `docker-compose up`

![](https://i.imgur.com/SPzJHht.png)

4. Check the image status: `docker compose ps`

 ![](https://i.imgur.com/cVEnqlh.png)

5. How to delete and shutdown the container: `docker compose down`

![](https://i.imgur.com/XaecxUy.png)


## How to test the project?

### Test flow:
![](https://i.imgur.com/pPKw8Jz.gif)



## API Introduction

#### Control concurrent sessions by using dependency injection & singleton design pattern.


### POST/init_session

**Request formate:**

| Property | Type | Description |
| -------- | -------- | -------- |
| session_id     | string     | Generate from Constructor     |
|estimated_traffic |int | Get from request|
|Bidder-name| string     | Generate from Constructor     |
|Bidder-endpoint| string     | Generate from Constructor     |
|BidderSetting-budget| int|Get from request|
|BidderSetting-impression_goal| int|Get from request|

```
{
    "estimated_traffic": 5,
    "bidder_setting": {
        "budget": 1000,
        "impression_goal": 100
    }
}
```

**Response format:**
| Property | Type | Description |
| -------- | -------- | -------- |
| result     | HttpStatusCode     |      |
|error |string | Custom error message|

Response code 200:
![](https://i.imgur.com/UR92W0C.png)


**Remove budget parameter from request data:**
Response code 400, returned by Model Validation:
![](https://i.imgur.com/9zr1yVv.png)



---


### Get/session_id
#### Used for Test only, to get current session id and status.
**Response format:**

 | Type | Description |
 | -------- | -------- |
| string    |session_id added by **POST/session_init**     |
|bool | session current status|

![](https://i.imgur.com/7eu5Qtw.png)



---


### POST/init_session

**Request formate:**

| Property | Type | Description |
| -------- | -------- | -------- |
| session_id     | string     | Get by **Get/session_id**    |


```
{
    "session_id": "e35623ca-4c86-497a-b1fc-b5299fb0d87a"
}
```

**Response format:**
| Property | Type | Description |
| -------- | -------- | -------- |
| result     | HttpStatusCode     |      |
|error |string | Custom error message|

![](https://i.imgur.com/H6aDo0T.png)

#### Try to end the same session:
Response code: 400
![](https://i.imgur.com/cjCjn7E.png)

