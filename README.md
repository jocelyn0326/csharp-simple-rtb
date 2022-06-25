# Intowow Assignment- 郭宜萱(jocelyn0326@gmail.com)


## Summary
Implement two http servers: an exchange server and a bidder according to Intowow’s simplified version RTB protocol.

## Environment
[.NET 5](https://docs.microsoft.com/zh-tw/dotnet/core/whats-new/dotnet-5)

[Docker](https://www.docker.com/)

## How to run the project?
1. Open [Docker Desktop](https://www.docker.com/products/docker-desktop/)
2. cd to current project  which contains **docker-compose.yml**
3. `docker-compose up`

![](https://i.imgur.com/SPzJHht.png)

4. Check the status of containers: `docker compose ps`

 ![](https://i.imgur.com/cVEnqlh.png)

5. How to delete and shutdown the container: `docker compose down`

![](https://i.imgur.com/XaecxUy.png)


## How to test the project?

### Test flow:

![](https://i.imgur.com/44DgIZ9.gif)



## API Introduction

### `POST /init_session`
url: http://localhost:8000/init_session


```
{
  "session_id": "1",
  "estimated_traffic": 10,
  "bidders": [
    {
      "name": "A",
      "endpoint": "http://bidder-server/"
    },
    {
      "name": "C",
      "endpoint": "http://bidder-server/"
    },
    {
      "name": "B",
      "endpoint": "http://bidder-server/"
    }
  ],
  "bidder_setting": {
    "budget": 10,
    "impression_goal": 2
  }
}

```



Response code 200:
![](https://i.imgur.com/HV1rIeV.png)



**Remove session_id param from request data:**
Response code 400, returned by Model Validation:
![](https://i.imgur.com/ewbTMtt.png)

**Post same session_id**
![](https://i.imgur.com/KndrLzo.png)

**Post same bidder_name**
![](https://i.imgur.com/NxJuyzt.png)



---
### `POST /bid_request`
url: http://localhost:8000/bid_request

```
{
  "floor_price": 2,
  "timeout_ms": 500,
  "session_id": "1",
  "user_id": "jocelyn",
  "request_id": "1"
}

```
Response code 200:

![](https://i.imgur.com/nvB8TJj.png)
 
          
          
**Send the same request again with request_id:**
Response code 400
![](https://i.imgur.com/2ZOIeH5.png)

**Send the session_id which is not exist:**
Response code 400
```
{
  "floor_price": 2,
  "timeout_ms": 500,
  "session_id": "100",
  "user_id": "jocelyn",
  "request_id": "1"
}
```
![](https://i.imgur.com/QXNa0uR.png)

**Occur request timeout:**
![](https://i.imgur.com/o6xZUVd.png)

---





### `POST /end_session`
url: http://localhost:8000/end_session/
**Request formate:**

| Property | Type | Description |
| -------- | -------- | -------- |
| session_id     | string     | Get by **`Get /session_id`**    |


```
{
    "session_id": "1"
}
```

**Response format:**
| Property | Type | Description |
| -------- | -------- | -------- |
| result     | HttpStatusCode     |      |
|error |string | Custom error message|

Response code: 200
![](https://i.imgur.com/ydrSeSm.png)


#### Try to end the same session:
Response code: 400
![](https://i.imgur.com/5ZXzBIy.png)

---

### `Get /session_id`
url: http://localhost:8000/session_id/
#### Used for Test only, to get current session id and status.
**Response format:**

 | Type | Description |
 | -------- | -------- |
| Dictionary<string, SessionData>    | created by **POST/session_init**     |

![](https://i.imgur.com/FN4eksd.png)




